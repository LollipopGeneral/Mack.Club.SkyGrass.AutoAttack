using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native.Csharp.App.Model
{
    public class GameUserModelJson
    {
        public int code { get; set; }
        public string msg { get; set; }
        public GameUserModel data { get; set; }
    }

    public class GameUserModel
    {
        public string[] nickname { get; set; }
        public int gold { get; set; }
        public Idol[] idol { get; set; }
        public bool uninstall { get; set; }
        public int power { get; set; }
        public Package[] package { get; set; }
        public Revenge[] revenge { get; set; }
        public int KO { get; set; }
        public Level level { get; set; }
    }

    public class Level
    {
        public UR[] UR { get; set; }
        public SSR[] SSR { get; set; }
        public SR[] SR { get; set; }
        public R[] R { get; set; }
        public N[] N { get; set; }
    }

    public class UR
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

    public class SSR
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

    public class SR
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

    public class R
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

    public class N
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

    public class Package
    {
        public string name { get; set; }
        public int num { get; set; }
    }

    public class Revenge
    {
        public long user_id { get; set; }
        public string[] nickname { get; set; }
        public int num { get; set; }
        public int revenge { get; set; }
    }

}
