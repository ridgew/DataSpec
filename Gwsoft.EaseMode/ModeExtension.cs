using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Gwsoft.EaseMode
{
    public static class ModeExtension
    {
        /// <summary>
        /// 获取成员信息的组件描述字符
        /// </summary>
        /// <param name="mInfo">类型成员</param>
        public static string GetDescription(this MemberInfo mInfo)
        {
            object[] objAttr = mInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objAttr != null && objAttr.Length > 0)
            {
                return ((DescriptionAttribute)objAttr[0]).Description;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取枚举项的字符描述
        /// </summary>
        public static string GetDescription(this Enum enumItem)
        {
            Type type = enumItem.GetType();
            MemberInfo[] memberInfos = type.GetMember(enumItem.ToString());
            if (memberInfos != null && memberInfos.Length > 0)
            {
                return GetDescription(memberInfos[0]);
            }
            return enumItem.ToString();
        }

    }
}
