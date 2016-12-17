using System;
using System.Collections.Generic;
using System.Text;
using Gwsoft.DataSpec;
using System.IO;

namespace Gwsoft.EaseMode
{
    /// <summary>
    /// 分包数据响应
    /// </summary>
    public abstract class PackageResponse : ESPDataBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageResponse"/> class.
        /// </summary>
        public PackageResponse()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageResponse"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PackageResponse(ESPContext context)
            : base(context)
        { }


        #region 属性模型顺序
        /// <summary>
        /// 响应头信息
        /// </summary>
        [ObjectTransferOrder(0, Reverse = false, Offset = 0)]
        public ResponseHeader ESP_Header { get; set; }

        /// <summary>
        /// 服务器端响应码(响应码不为0时弹出提示框)
        /// </summary>
        [ObjectTransferOrder(1, Reverse = true, Offset = -1)]
        public StatusCode ESP_Code { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        [ObjectTransferOrder(2, Reverse = false, Offset = 2)]
        public EaseString ESP_Message { get; set; }

        /// <summary>
        /// 请求的包序号(分包首个包序号为1)
        /// </summary>
        [ObjectTransferOrder(3, Reverse = true, Offset = -1)]
        public short ESP_PackageIndex { get; set; }

        /// <summary>
        /// 后续请求包的个数
        /// </summary>
        [ObjectTransferOrder(4, Reverse = true, Offset = 2)]
        public short ESP_LeavePackageCount { get; set; }

        int _esp_pkgLen = -1;
        /// <summary>
        /// 本次分包返回的数据长度
        /// </summary>
        [ObjectTransferOrder(5, Reverse = true, Offset = 2)]
        public int ESP_PackageLength
        {
            get { return _esp_pkgLen; }
            set { _esp_pkgLen = value; }
        }

        /// <summary>
        /// 当前分包数据
        /// </summary>
        [ObjectTransferOrder(7, Reverse = false, Offset = 4)]
        public byte[] ESP_PackageBytes { get; set; }
        #endregion


        /// <summary>
        /// 创建整个包数据（包含应用数据）
        /// </summary>
        /// <param name="method">当前操作类型</param>
        /// <param name="command">当前操作命令</param>
        /// <param name="appBytes">整个数据业务数据</param>
        /// <param name="insertAppLen">是否在业务数据前插入4字节的数据长度</param>
        /// <returns></returns>
        public byte[] BuildWholePackageBytes(CommandType method, EaseString command, byte[] appBytes, bool insertAppLen)
        {
            byte[] cmdBytes = command.GetNetworkBytes();
            int cmdByteLen = cmdBytes.Length;
            long totalLen = 1L + (long)cmdByteLen
                + ((insertAppLen) ? 4L : 0L)
                + appBytes.LongLength;

            byte[] retBytes = new byte[totalLen];
            retBytes[0] = (byte)method;

            //命令数据
            Buffer.BlockCopy(cmdBytes, 0, retBytes, 1, cmdByteLen);

            //应用数据长度
            if (insertAppLen)
            {
                cmdBytes = BitConverter.GetBytes(appBytes.Length).ReverseBytes();
                Buffer.BlockCopy(cmdBytes, 0, retBytes, 1 + cmdByteLen, cmdBytes.Length);
            }

            //应用数据
            Buffer.BlockCopy(appBytes, 0, retBytes,
                (1 + cmdByteLen + ((insertAppLen) ? 4 : 0)),
                appBytes.Length);

            return retBytes;
        }

        /// <summary>
        /// 获取网络字节序列
        /// </summary>
        /// <returns></returns>
        public override byte[] GetNetworkBytes()
        {
            byte[] toFixBytes = GetInstanceNetworkBytes(p =>
            {
                if (ESP_PackageLength == 0)
                {
                    return p.Name.Equals("ESP_PackageBytes");
                }
                return false;
            });
            ESP_Header.ESP_LeaveLength = toFixBytes.Length - 6;

            byte[] lenBytes = SpecUtil.ReverseBytes(BitConverter.GetBytes(ESP_Header.ESP_LeaveLength));
            for (int i = 0, j = lenBytes.Length; i < j; i++)
            {
                Buffer.SetByte(toFixBytes, 2 + i, lenBytes[i]);
            }
            return toFixBytes;
        }

        ///// <summary>
        ///// 作为自身基类的相关属性绑定
        ///// </summary>
        ///// <typeparam name="TEntity">The type of the entity.</typeparam>
        //protected BindBuilder SubClassPropertyBindAction<TEntity>()
        //    where TEntity : PackageResponse
        //{
        //    return BindBuilder.Instance()
        //            .Add((TEntity resp) => resp.ESP_PackageData,
        //                (s, obj) =>
        //                {
        //                    TEntity cResp = (TEntity)obj;
        //                    cResp.ESP_PackageData = s.ReadNetworkStreamBytes(cResp.ESP_PackageLength);
        //                    return cResp.ESP_PackageData;
        //                });
        //}

        /// <summary>
        /// 填充绑定词典
        /// </summary>
        public override void CustomPropertyBindAction()
        {
            BindBuilder.Instance()
                .Add((PackageResponse h) => h.ESP_PackageBytes,  //分包数据
                 (s, p, obj) =>
                 {
                     PackageResponse instance = (PackageResponse)obj;
                     PropertyBindState state = new PropertyBindState();
                     if (instance.ESP_PackageLength > 0)
                     {
                         state.StreamBind = true;
                         instance.ESP_PackageBytes = s.ReadNetworkStreamBytes(instance.ESP_PackageLength);
                         state.PropertyValue = instance.ESP_PackageBytes;
                     }
                     return state;
                 })
                 .End<PackageResponse>();
        }
    }
}
