using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native.Csharp.App.Model
{
    /// <summary>
    /// Game data cache
    /// </summary>
    public static class GameData
    {
        /// <summary>
        /// 出战阵容
        /// </summary>
        public static Dictionary<string, UserInfo> UserPickedDic = new Dictionary<string, UserInfo>();

        /// <summary>
        /// 自己的信息
        /// </summary>
        public static UserInfo SelfInfo = null;

        public static UserIdol SelfIdols = null;
    }

    #region UserInfoJson

    public class UserInfoJson
    {
        public int code { get; set; }
        public string msg { get; set; }
        public UserInfo data { get; set; }
    }

    public class UserInfo
    {
        public Idol[] idol { get; set; }
        public Package[] package { get; set; }
        public Revenge[] revenge { get; set; }
    }

    public class Idol
    {
        public string nickname { get; set; }
        public int attack { get; set; }
        public int life { get; set; }
        public int defense { get; set; }
        public int battle { get; set; }
        public int star { get; set; }
        public string level { get; set; }
        public string skill { get; set; }
        public int num { get; set; }
        public int alllife { get; set; }
        public bool _lock { get; set; }
    }

    /// <summary>
    /// 道具
    /// </summary>
    public class Package
    {
        public string name { get; set; }
        public int num { get; set; }
    }

    /// <summary>
    /// 仇人
    /// </summary>
    public class Revenge
    {
        public List<string> nickname { get; set; }
        public int num { get; set; }
        public int revenge { get; set; }
    }

    #endregion


    #region UserIdolJson

    public class UserIdolJson
    {
        public int code { get; set; }
        public string msg { get; set; }
        public UserIdol data { get; set; }
    }

    public class UserIdol
    {
        public Idol[] idol { get; set; }
    }

    #endregion

}
