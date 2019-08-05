using Native.Csharp.App.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace Native.Csharp.App
{
    public static class GameManager
    {
        public static long QQGroup = 730074397;

        /// <summary>
        /// 获取某人信息
        /// </summary>
        /// <param name="nickname"></param>
        public static void GetUserInfo(string nickname = "棒哥")
        {
            string url = $"http://172.81.250.91:82/userInfo";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["nickname"] = nickname;

            string result = string.Empty;
            try
            {
                result = Common.PostPocketApi(url, JsonConvert.SerializeObject(dic));
            }
            catch (Exception ex)
            {
                File.AppendAllLines("WebClientError.log", new List<string> { result, ex.StackTrace }, Encoding.UTF8);
            } 

            if(result != string.Empty)
            {
                File.AppendAllLines("GetUserInfo.log", new List<string> { result }, Encoding.UTF8);
                UserInfoJson json = JsonConvert.DeserializeObject<UserInfoJson>(result);
                if(json.code == 200)
                {
                    GameData.UserPickedDic[nickname] = json.data;

                    if(nickname == "棒哥")
                    {
                        GameData.SelfInfo = json.data;
                    }
                }
            }            
        }

        public static UserIdol GetUserIdol(string nickname = "棒哥")
        {
            UserIdol ret = null;

            string url = $"http://172.81.250.91:82/userIdol";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["nickname"] = nickname;

            string result = string.Empty;
            try
            {
                result = Common.PostPocketApi(url, JsonConvert.SerializeObject(dic));
            }
            catch (Exception ex)
            {
                File.AppendAllLines("WebClientError.log", new List<string> { ex.StackTrace }, Encoding.UTF8);
            }

            if (result != string.Empty)
            {
                UserIdolJson json = JsonConvert.DeserializeObject<UserIdolJson>(result);
                if (json.code == 200)
                {
                    ret = json.data;

                    if (nickname == "棒哥")
                    {
                        GameData.SelfIdols = ret;
                    }
                }
            }

            return ret;
        }

        public static void CheckFromGroupMessage(string nickname)
        {
            if (nickname == "轮回疯狂暗示" || nickname == "棒哥")
            {
                return;
            }

            GameManager.GetUserInfo(nickname);

            #region 有仇报仇，没仇出击，快点补刀
            bool hasAttack = GameManager.CheckSelfRevenges(nickname);
            if (hasAttack == false)
            {
                GameManager.CheckAttack(nickname);
            }
            #endregion
        }

        #region 复仇计划
        /// <summary>
        /// 检测自己仇人信息
        /// 看某人是否在自己的仇人列表中
        /// </summary>
        public static bool CheckSelfRevenges(string nickname = "棒哥")
        {
            bool hasAttack = false;
            if(GameData.SelfInfo == null)
            {
                return hasAttack;
            }

            foreach (var info in GameData.SelfInfo.revenge)
            {
                // 在仇人列表中
                if (info.nickname.Contains(nickname))
                {
                    // 还有复仇次数
                    if(info.num > info.revenge)
                    {
                        // 看看是否有血量不健康的卡
                        var revengeInfo = GameData.UserPickedDic[nickname];
                        foreach (var idol in revengeInfo.idol)
                        {
                            // 触发复仇
                            if (idol.life <= 30)
                            {
                                TakeRevenge(nickname);
                                hasAttack = true;
                                break;
                            }
                        }
                    }
                    break;
                }
            }

            return hasAttack;
        }

        public static void TakeRevenge(string nickname = "棒哥")
        {
            ReadyToAttack();

            Common.CqApi.SendGroupMessage(QQGroup, "出击 背刺");
            Thread.Sleep(2 * 1000);

            Common.CqApi.SendGroupMessage(QQGroup, $"复仇 {nickname}");
        }

        #endregion


        #region 出击计划
        /// <summary>
        /// 检测自己是否能攻击某人
        /// 看自己是否在某人的仇人列表中
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public static void CheckAttack(string nickname = "棒哥")
        {
            string selfNickname = "棒哥";
            UserInfo attackInfo = GameData.UserPickedDic[nickname];

            bool canAttack = true;
            foreach (var info in attackInfo.revenge)
            {
                // 自己在别人的复仇列表中，表示打过了，不能再打了
                if (info.nickname.Contains(selfNickname))
                {
                    canAttack = false;
                    break;
                }
            }

            if (canAttack)
            {
                TakeAttack(nickname);
            }
        }

        public static void TakeAttack(string nickname = "棒哥")
        {
            ReadyToAttack();

            Common.CqApi.SendGroupMessage(QQGroup, "出击 背刺");
            Thread.Sleep(2 * 1000);

            Common.CqApi.SendGroupMessage(QQGroup, $"全军出击 {nickname}");
        }

        #endregion


        #region 备战计划-有准备有预谋，把要出战的加满血或者复活
        public static void ReadyToAttack()
        {
            var ret = GetUserIdol();
            if(ret != null)
            {
                List<string> needHpIdols = new List<string>();
                foreach (var item in ret.idol)
                {
                    if(item.skill == "背刺" && item.life <= 30)
                    {
                        needHpIdols.Add(item.nickname);
                    }
                }

                Package hpItem = null;
                foreach (var item in GameData.SelfInfo.package)
                {
                    if (item.name == "治疗药水")
                    {
                        hpItem = item;
                        break;
                    }
                }

                bool usedHpItem = false;
                foreach (string needHpName in needHpIdols)
                {
                    if (hpItem != null)
                    {
                        if(hpItem.num > 0)
                        {
                            Common.CqApi.SendGroupMessage(QQGroup, $"治疗 {needHpName}");
                            Thread.Sleep(3 * 1000);
                            usedHpItem = true;
                            hpItem.num -= 1;
                        }                        
                    }
                }

                if (usedHpItem)
                {
                    GetUserIdol();
                    GetUserInfo();
                }                
            }
        }

        #endregion
    }
}
