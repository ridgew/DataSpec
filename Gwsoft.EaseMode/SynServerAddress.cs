using System;
using Gwsoft.Configuration;
using Gwsoft.DataSpec;

namespace Gwsoft.EaseMode
{
    /// <summary>
    /// 服务器地址同步(请求封装)
    /// </summary>
    [ImplementState(CompleteState.OK, "1.0(v3.2.1-2.6.9.1)",
        Description = "服务器地址同步(请求封装)", ReleaseDateGTM = "Tue, 05 Jan 2010 05:57:56 GMT")]
    public class SynServerAddressRequest : RequestBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SynServerAddressRequest"/> class.
        /// </summary>
        public SynServerAddressRequest()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynServerAddressRequest"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SynServerAddressRequest(ESPContext context)
            : base(context)
        { }

        /// <summary>
        /// 同步地址配置
        /// </summary>
        [ObjectTransferOrder(20, Reverse = false, Offset = -1)]
        public SynServerAddress ESP_AddressConfig { get; set; }

    }


    /// <summary>
    /// 服务器地址同步(响应封装)
    /// </summary>
    [ImplementState(CompleteState.OK, "1.0(v3.2.1-2.6.9.2)",
        Description = "服务器地址同步(响应封装)", ReleaseDateGTM = "Tue, 05 Jan 2010 05:57:56 GMT")]
    public class SynServerAddressResponse : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SynServerAddressResponse"/> class.
        /// </summary>
        public SynServerAddressResponse()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynServerAddressResponse"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SynServerAddressResponse(ESPContext context)
            : base(context)
        { }

        /// <summary>
        /// 同步地址配置
        /// </summary>
        [ObjectTransferOrder(20, Reverse = false, Offset = -1)]
        public SynServerAddress ESP_AddressConfig { get; set; }

    }

    /// <summary>
    /// 同步地址配置
    /// </summary>
    [Serializable]
    public class SynServerAddress : ESPDataBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SynServerAddress"/> class.
        /// </summary>
        public SynServerAddress()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynServerAddress"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SynServerAddress(ESPContext context)
            : base(context)
        { }

        #region 属性模型顺序

        /// <summary>
        /// 服务IP地址1
        /// </summary>
        [ObjectTransferOrder(20, Reverse = false, Offset = 0)]
        public EaseString ESP_ServerIP1 { get; set; }

        /// <summary>
        /// 服务端口1
        /// </summary>
        [ObjectTransferOrder(22, Reverse = true, Offset = -1)]
        public ushort ESP_ServerPort1 { get; set; }

        /// <summary>
        /// 服务IP地址2
        /// </summary>
        [ObjectTransferOrder(24, Reverse = false, Offset = 2)]
        public EaseString ESP_ServerIP2 { get; set; }

        /// <summary>
        /// 服务端口2
        /// </summary>
        [ObjectTransferOrder(26, Reverse = true, Offset = -1)]
        public ushort ESP_ServerPort2 { get; set; }

        /// <summary>
        /// 服务器域名
        /// <para> 长城业务为:ebook.ccsoft.mobi</para>
        /// <para> 全点业务为:ebook.qdsoft.mobi</para> 
        /// <para> 合作业务为:ebook.hzsoft.mobi</para>
        /// </summary>
        [ObjectTransferOrder(28, Reverse = false, Offset = 2)]
        public EaseString ESP_ServerDomain { get; set; }

        /// <summary>
        /// 服务端口, 服务器端口: 9000
        /// </summary>
        [ObjectTransferOrder(30, Reverse = true, Offset = -1)]
        public ushort ESP_ServerPort { get; set; }

        /// <summary>
        /// WAP网关IP地址,WAP 网关IP,电信网关为:10.0.0.200
        /// </summary>
        [ObjectTransferOrder(32, Reverse = false, Offset = 2)]
        public EaseString ESP_WapGateWayAddress { get; set; }

        /// <summary>
        /// WAP网关端口, WAP 网关端口, 电信网关端口为:80
        /// </summary>
        [ObjectTransferOrder(34, Reverse = true, Offset = -1)]
        public ushort ESP_WapGateWayPort { get; set; }

        /// <summary>
        /// 服务器网关IP地址
        /// </summary>
        [ObjectTransferOrder(36, Reverse = false, Offset = 2)]
        public EaseString ESP_ServerGateWayAddress { get; set; }

        /// <summary>
        ///服务器网关端口，默认7001
        /// </summary>
        [ObjectTransferOrder(38, Reverse = true, Offset = -1)]
        public ushort ESP_ServerGateWayPort { get; set; }

        /// <summary>
        /// 服务器网关请求路径
        /// <para>通过WAP 网关中转时应用的URL</para>
        /// <para>例如:http://www.easegateway.com:7001/ease/servlet/ease?cmd=101&sid=*此处参数的值应该为：/ease/servlet/ease</para>
        /// </summary>
        [ObjectTransferOrder(40, Reverse = false, Offset = 2)]
        public EaseString ESP_ServerGateWayPath { get; set; }
        #endregion

    }

}
