using System;
using System.Collections.Generic;
using System.Text;

namespace Gwsoft.EaseMode
{
    /// <summary>
    /// 操作类型(byte)
    /// </summary>
    public enum CommandType : byte
    {
        /// <summary>
        /// 无任何操作 = 0
        /// </summary>
        None = 0,
        /// <summary>
        /// 发送短信 = 1
        /// </summary>
        SMS = 1,
        /// <summary>
        /// 调用WAP浏览器 = 2
        /// </summary>
        WAP = 2,
        /// <summary>
        /// 拨打电话 = 3
        /// </summary>
        Dial = 3,
        /// <summary>
        /// 主程序存在更新，下载主程序 = 4
        /// </summary>
        Updatable = 4
    }
}
