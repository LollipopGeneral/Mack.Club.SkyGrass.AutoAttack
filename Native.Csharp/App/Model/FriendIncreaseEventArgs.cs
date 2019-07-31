﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Native.Csharp.App.Model
{
	public class FriendIncreaseEventArgs : EventArgs
	{
		/// <summary>
		/// 发送时间
		/// </summary>
		public DateTime SendTime { get; set; }
		/// <summary>
		/// 来源QQ
		/// </summary>
		public long FromQQ { get; set; }

		/// <summary>
		/// 获取或设置一个值，该值指示是否处理过此事件
		/// </summary>
		public bool Handled { get; set; }
	}
}
