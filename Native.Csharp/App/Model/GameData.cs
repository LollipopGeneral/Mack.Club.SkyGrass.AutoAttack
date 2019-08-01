using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native.Csharp.App.Model
{
    public class PickedIdol
    {
        public string nickname { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }
        public int life { get; set; }
    }

    /// <summary>
    /// Game data cache
    /// </summary>
    public static class GameData
    {
        /// <summary>
        /// 出战阵容
        /// </summary>
        public static Dictionary<string, List<PickedIdol>> UserPickedDic = new Dictionary<string, List<PickedIdol>>();

    }
}
