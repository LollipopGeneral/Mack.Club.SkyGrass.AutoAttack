using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Native.Csharp.App.Interface;
using Native.Csharp.App.Model;
using Native.Csharp.Sdk.Cqp;
using Newtonsoft.Json;

namespace Native.Csharp.App.Event
{

	public class Event_AppStatus : IEvent_AppStatus
	{
		#region --公开方法--
		/// <summary>
		/// Type=1001 酷Q启动<para/>
		/// 处理 酷Q 的启动事件回调
		/// </summary>
		/// <param name="sender">事件的触发对象</param>
		/// <param name="e">事件的附加参数</param>
		public void CqStartup (object sender, EventArgs e)
		{
			// 本子程序会在酷Q【主线程】中被调用。
			// 无论本应用是否被启用，本函数都会在酷Q启动后执行一次，请在这里执行插件初始化代码。
			// 请务必尽快返回本子程序，否则会卡住其他插件以及主程序的加载。

			Common.AppDirectory = Common.CqApi.GetAppDirectory ();  // 获取应用数据目录 (无需存储数据时, 请将此行注释)


			// 返回如：D:\CoolQ\app\com.example.demo\
			// 应用的所有数据、配置【必须】存放于此目录，避免给用户带来困扰。
		}

		/// <summary>
		/// Type=1002 酷Q退出<para/>
		/// 处理 酷Q 的退出事件回调
		/// </summary>
		/// <param name="sender">事件的触发对象</param>
		/// <param name="e">事件的附加参数</param>
		public void CqExit (object sender, EventArgs e)
		{
			// 本子程序会在酷Q【主线程】中被调用。
			// 无论本应用是否被启用，本函数都会在酷Q退出前执行一次，请在这里执行插件关闭代码。


		}

        /// <summary>
        /// Type=1003 应用被启用<para/>
        /// 处理 酷Q 的插件启动事件回调
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void AppEnable(object sender, EventArgs e)
        {
            // 当应用被启用后，将收到此事件。
            // 如果酷Q载入时应用已被启用，则在_eventStartup(Type=1001,酷Q启动)被调用后，本函数也将被调用一次。
            // 如非必要，不建议在这里加载窗口。（可以添加菜单，让用户手动打开窗口）
            Common.IsRunning = true;

            new Task(() =>
            {
                long groupId = 730074397;
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;

                while (Common.IsRunning)
                {
                    Thread.Sleep(60 * 1000);

                    int hour = Convert.ToInt32(DateTime.Now.ToString("HH"));
                    if(hour >= 1 && hour < 7)
                    {
                        continue;
                    }

                    int minute = Convert.ToInt32(DateTime.Now.ToString("mm"));

                    if (minute == 55)
                    {
                        Common.CqApi.SendGroupMessage(groupId, "梭哈");
                    }

                    if (minute % 5 == 0)
                    {
                        try
                        {
                            #region 刷新游戏数据
                            Common.CqApi.SendGroupMessage(groupId, "出击 觉醒");
                            string listUrl = "http://172.81.250.91:82/chessUser";
                            string listJsonStr = wc.DownloadString(listUrl);
                            List<GameUserList> userList = JsonConvert.DeserializeObject<List<GameUserList>>(listJsonStr);
                            #endregion

                            #region 复仇
                            string selfUrl = "http://172.81.250.91:82/oneUser";
                            Dictionary<string, object> dic = new Dictionary<string, object>();
                            dic["nickname"] = "棒哥";

                            string selfJsonStr = Common.PostPocketApi(selfUrl, JsonConvert.SerializeObject(dic));
                            GameUserModelJson modelJson = JsonConvert.DeserializeObject<GameUserModelJson>(selfJsonStr);
                            List<string> enemyList = new List<string>();
                            int rank = 0;
                            if (modelJson.code == 200)
                            {
                                rank = modelJson.data.KO - 1;
                                foreach (var enemy in modelJson.data.revenge)
                                {
                                    // 可复仇
                                    if (enemy.revenge < 6)
                                    {
                                        enemyList.Add(enemy.nickname[0]);
                                    }
                                }
                            }
                            if (enemyList.Count != 0)
                            {
                                Common.CqApi.SendGroupMessage(groupId, "出击 觉醒");
                                foreach (var enemyName in enemyList)
                                {
                                    Common.CqApi.SendGroupMessage(groupId, $"复仇 {enemyName}");
                                    Thread.Sleep(1 * 1000);
                                }
                                Common.CqApi.SendGroupMessage(groupId, "出击 尖刺防御");
                            }
                            #endregion

                            if (minute == 55)
                            {
                                #region 全军出击
                                Common.CqApi.SendGroupMessage(groupId, "出击 觉醒");
                                string listUrl = "http://172.81.250.91:82/chessUser";
                                string listJsonStr = wc.DownloadString(listUrl);
                                List<GameUserList> userList = JsonConvert.DeserializeObject<List<GameUserList>>(listJsonStr);
                                int idx = rank == 0 ? 0 : rank - 1;
                                var target = userList[idx];
                                Common.CqApi.SendGroupMessage(groupId, $"全军出击 {target.nickname[0]}");
                                Common.CqApi.SendGroupMessage(groupId, "出击 尖刺防御");
                                #endregion
                            }

                        }
                        catch (Exception ex)
                        {
                            File.AppendAllLines("GameError.log", new List<string> { ex.StackTrace }, Encoding.UTF8);
                        }
                    }
                }
            }).Start();
        }

		/// <summary>
		/// Type=1004 应用被禁用<para/>
		/// 处理 酷Q 的插件关闭事件回调
		/// </summary>
		/// <param name="sender">事件的触发对象</param>
		/// <param name="e">事件的附加参数</param>
		public void AppDisable (object sender, EventArgs e)
		{
			// 当应用被停用前，将收到此事件。
			// 如果酷Q载入时应用已被停用，则本函数【不会】被调用。
			// 无论本应用是否被启用，酷Q关闭前本函数都【不会】被调用。
			Common.IsRunning = false;

		}
		#endregion
	}
}
