using System;
using System.Collections.Generic;
using System.Text;
using Gwsoft.Configuration;
using Gwsoft.DataSpec;
using System.Collections.Specialized;
#if UnitTest
using NUnit.Framework;
#endif
namespace Gwsoft.EaseMode
{
    /// <summary>
    /// 业务接入请求头(v3.2-2.6.1)
    /// </summary>
    [ImplementState(CompleteState.OK, "1.0(v3.2-2.6.1)", Description = "EASE网络接入请求头信息", ReleaseDateGTM = "Thu, 31 Dec 2009 15:21:28 GMT")]
#if UnitTest
    [TestFixture]
#endif
    public class RequestHeader : ESPDataBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestHeader"/> class.
        /// </summary>
        public RequestHeader()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestHeader"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public RequestHeader(ESPContext context)
            : base(context)
        { }

        #region 传输属性
        /// <summary>
        /// 网络连接成功标志(1010 - 网络连接成功)
        /// </summary>
        [ObjectTransferOrder(0, Reverse = true, Offset = 0)]
        public EaseSuccessFlag ESP_SuccessFlag { get; set; }

        /// <summary>
        /// 包后续长度(不包含此参数长度)
        /// </summary>
        [ObjectTransferOrder(1, Reverse = true, Offset = 2)]
        public int ESP_LeaveLength { get; set; }

        /// <summary>
        /// 头信息参数个数（18）存在屏幕高宽时置为20,存在拨号方式时置为21
        /// </summary>
        [ObjectTransferOrder(2, Reverse = true, Offset = 4)]
        public short ESP_ParamsCount { get; set; }

        /// <summary>
        /// 软件ID
        /// </summary>
        [ObjectTransferOrder(3, Reverse = true, Offset = 2)]
        public int ESP_SoftwareID { get; set; }

        /// <summary>
        /// 网络运营商ID(byte)
        /// </summary>
        [ObjectTransferOrder(4, Reverse = false, Offset = 4)]
        public NetworkID ESP_NID { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [ObjectTransferOrder(5, Conditional = true, Reverse = true, Offset = 1)]
        public int ESP_DID { get; set; }

        EaseString _emptyUA = EaseString.Empty;
        /// <summary>
        /// 设备UserAgent(2010-9-2, Ridge)
        /// </summary>
        [ObjectTransferOrder(5, SubOrder = 0.1F, Conditional = true, Reverse = false, Offset = 1)]
        public EaseString ESP_UserAgent
        {
            get { return _emptyUA; }
            set { _emptyUA = value; }
        }

        /// <summary>
        /// 编码格式
        /// </summary>
        [ObjectTransferOrder(6, Reverse = false)]
        public EaseEncode ESP_EncodeType { get; set; }

        /// <summary>
        /// 客户端版本号(默认为0)
        /// </summary>
        [ObjectTransferOrder(7, Reverse = true, Offset = 1)]
        public int ESP_Version { get; set; }

        /// <summary>
        /// 机器识别码/SIM卡识别码
        /// </summary>
        [ObjectTransferOrder(8, Reverse = false, Offset = 4)]
        public EaseString ESP_IMEI { get; set; }

        /// <summary>
        /// 是否隐藏更新(0－隐藏更新 1－正常更新)
        /// </summary>
        [ObjectTransferOrder(9, Reverse = true, Offset = -1)]
        public byte ESP_Update { get; set; }

        #region 客户端信息
        /// <summary>
        /// 客户端协议版本号（默认为：1）
        /// </summary>
        [ObjectTransferOrder(10, Reverse = false, Offset = 1)]
        public byte ESP_SpecVersion { get; set; }

        /// <summary>
        /// 客户端支持压缩算法(0－不压缩 1－lz77压缩算法)
        /// </summary>
        [ObjectTransferOrder(11, Reverse = false, Offset = 1)]
        public EaseCompress ESP_Compress { get; set; }

        /// <summary>
        /// 客户端可用存储空间(单位：字节)
        /// </summary>
        [ObjectTransferOrder(12, Reverse = true, Offset = 1)]
        public int ESP_StoreSize { get; set; }

        /// <summary>
        /// 客户端可用内存大小(单位：字节)
        /// </summary>
        [ObjectTransferOrder(13, Reverse = true, Offset = 4)]
        public int ESP_MemorySize { get; set; }

        /// <summary>
        /// 客户端字体宽度(单位：像素)
        /// </summary>
        [ObjectTransferOrder(14, Reverse = false, Offset = 4)]
        public byte ESP_FontWidth { get; set; }

        /// <summary>
        /// 客户端字体高度(单位：像素)
        /// </summary>
        [ObjectTransferOrder(15, Reverse = false, Offset = 1)]
        public byte ESP_FontHeight { get; set; }

        /// <summary>
        /// 首次连网标志(1-开机 0-使用中 2-清掉缓存)
        /// </summary>
        [ObjectTransferOrder(16, Reverse = false, Offset = 1)]
        public EaseConnectState ESP_ConnectState { get; set; }
        #endregion

        /// <summary>
        /// 业务代码
        /// </summary>
        [ObjectTransferOrder(17, Reverse = true, Offset = 1)]
        public short ESP_BusinessID { get; set; }

        /// <summary>
        /// 会话标识（不大于50）
        /// </summary>
        [ObjectTransferOrder(18, Reverse = false, Offset = 2)]
        public EaseString ESP_SessionID { get; set; }

        /// <summary>
        /// 客户端Cookie存储值（不大于500）
        /// </summary>
        [ObjectTransferOrder(19, Reverse = false, Offset = -1)]
        public EaseString ESP_Cookies { get; set; }

        /// <summary>
        /// 缴费相关信息(其中BREW客户端格式：价格类型,许可类型,过期值,已下载版本序列号)
        /// </summary>
        [ObjectTransferOrder(20, Reverse = false, Offset = -1)]
        public EaseString ESP_FeeState { get; set; }

        #region 客户端信息补充 09-5-7
        /// <summary>
        /// 屏幕宽度
        /// </summary>
        [ObjectTransferOrder(21, Reverse = true, Offset = -1, Conditional = true)]
        public short ESP_ScreenWidth { get; set; }

        /// <summary>
        /// 屏幕高度
        /// </summary>
        [ObjectTransferOrder(22, Reverse = true, Offset = 2, Conditional = true)]
        public short ESP_ScreenHeight { get; set; }

        /// <summary>
        /// 客户端拨号方式(byte)
        /// </summary>
        [ObjectTransferOrder(23, Reverse = false, Offset = 2, Conditional = true)]
        public ClientDialType ESP_DailType { get; set; }
        #endregion

        /// <summary>
        /// 请求功能号0－页面请求,兼容EASE 2.1
        /// <para> 1－页面及资源请求  2－页面请求 </para>
        /// <para> 3－资源请求  4－应用请求</para>
        /// </summary>
        [ObjectTransferOrder(24, Reverse = false, Offset = 1)]
        public RequestType ESP_Protocol { get; set; }
        #endregion

        /// <summary>
        /// 获取或设置移动手机号码(2010-9-3, Ridge)
        /// </summary>
        public string CellPhoneNumber { get; set; }

        /// <summary>
        /// 获取客户端的编码格式
        /// </summary>
        public Encoding GetContentEncoding()
        {
            Encoding ctEnc = Encoding.UTF8;
            switch (ESP_EncodeType)
            {
                case EaseEncode.UTF8:
                    ctEnc = Encoding.UTF8;
                    break;
                case EaseEncode.Unicode:
                    ctEnc = Encoding.Unicode;
                    break;
                case EaseEncode.GB2312:
                    ctEnc = Encoding.GetEncoding("gb2312");
                    break;
                default:
                    break;
            }
            return ctEnc;
        }

        /// <summary>
        /// 填充绑定词典
        /// </summary>
        public override void CustomPropertyBindAction()
        {
            //ParamsCount : 18, 20, 21
            BindBuilder.Instance()
                .Add((RequestHeader h) => h.ESP_ScreenWidth,  //屏幕宽度
                 (s, p, obj) =>
                 {
                     RequestHeader instance = (RequestHeader)obj;
                     PropertyBindState state = new PropertyBindState();
                     if (instance.ESP_ParamsCount > 18)
                     {
                         state.StreamBind = true;
                         instance.ESP_ScreenWidth = s.ReadNetworkStreamAsEntity<short>(2);
                         state.PropertyValue = instance.ESP_ScreenWidth;
                     }
                     return state;
                 })
                 .Add((RequestHeader h) => h.ESP_ScreenHeight,  //屏幕高度
                 (s, p, obj) =>
                 {
                     RequestHeader instance = (RequestHeader)obj;
                     PropertyBindState state = new PropertyBindState();
                     if (instance.ESP_ParamsCount > 18)
                     {
                         state.StreamBind = true;
                         instance.ESP_ScreenHeight = s.ReadNetworkStreamAsEntity<short>(2);
                         state.PropertyValue = instance.ESP_ScreenHeight;
                     }
                     return state;
                 })
                 .Add((RequestHeader h) => h.ESP_DID,  //设备ID
                 (s, p, obj) =>
                 {
                     RequestHeader instance = (RequestHeader)obj;
                     PropertyBindState state = new PropertyBindState();
                     if (instance.ESP_SuccessFlag == EaseSuccessFlag.Success ||
                         instance.ESP_SuccessFlag == EaseSuccessFlag.SuccessUserAgent)
                     {
                         state.StreamBind = true;
                         instance.ESP_DID = s.ReadNetworkStreamAsEntity<int>(4);
                         state.PropertyValue = instance.ESP_DID;
                     }
                     return state;
                 })
                 .Add((RequestHeader h) => h.ESP_UserAgent,  //设备UserAgent(2010-9-2, Ridge)
                 (s, p, obj) =>
                 {
                     RequestHeader instance = (RequestHeader)obj;
                     PropertyBindState state = new PropertyBindState();
                     if (instance.ESP_SuccessFlag == EaseSuccessFlag.SuccessUserAgent)
                     {
                         state.StreamBind = true;
                         instance.ESP_UserAgent = s.ReadNetworkStreamAsEntity<EaseString>();
                         state.PropertyValue = instance.ESP_UserAgent;
                     }
                     return state;
                 }).Add((RequestHeader h) => h.ESP_DailType,  //拨号方式
                 (s, p, obj) =>
                 {
                     RequestHeader instance = (RequestHeader)obj;
                     PropertyBindState state = new PropertyBindState();
                     if (instance.ESP_ParamsCount > 20)
                     {
                         state.StreamBind = true;
                         instance.ESP_DailType = s.ReadNetworkStreamAsEntity<ClientDialType>(1);
                         state.PropertyValue = instance.ESP_DailType;
                     }
                     return state;
                 })
                 .Add((RequestHeader h) => h.ESP_Protocol,  //请求功能号
                 (s, p, obj) =>
                 {
                     RequestHeader instance = (RequestHeader)obj;
                     PropertyBindState state = new PropertyBindState();
                     instance.ESP_Protocol = s.ReadNetworkStreamAsEntity<RequestType>(1);
                     state.PropertyValue = instance.ESP_Protocol;
                     state.StreamBind = true;
                     return state;
                 })
                 .End<RequestHeader>();
        }

        /// <summary>
        /// 获取网络字节序列
        /// </summary>
        /// <returns></returns>
        public override byte[] GetNetworkBytes()
        {
            return GetInstanceNetworkBytes(p =>
            {
                if (ESP_ParamsCount < 20)      //默认18
                {
                    return p.Name.Equals("ESP_ScreenWidth") || p.Name.Equals("ESP_ScreenHeight")
                        || p.Name.Equals("ESP_DailType");
                }

                if (ESP_ParamsCount < 21)
                {
                    return p.Name.Equals("ESP_DailType");
                }

                return false;
            });
        }

        /// <summary>
        /// 同步(设置或更新)Web请求头
        /// </summary>
        /// <param name="currentHeader">当前应用请求实例</param>
        /// <param name="currentWebHeader">当前web请求实例</param>
        [ImplementState(CompleteState.OK, "1.0(v3.2-2.7.1)", Description = "HTTP头信息固定增加参数", ReleaseDateGTM = "Thu, 31 Dec 2009 15:21:28 GMT")]
        public static void WebHeaderSyn(RequestHeader currentHeader, NameValueCollection currentWebHeader)
        {
            #region 测试数据
            /*                                                                                                                                                                             
            User-Agent: EASE Proxy Client/1.0.0.0 (Microsoft Windows NT 5.2.3790 Service Pack 2; zh-CN) WANGQJ (.NET CLR 2.0.50727.3615)      
            Software-ID: 0                                                                                                                    
            Network-ID: 3                                                                                                                     
            Device-ID: 73                                                                                                                     
            Device-UserAgent:                                                                                                                 
            Encoding: 0                                                                                                                       
            IMEI: 460030948805727                                                                                              
            Free-Disk: 7309364                                                                                                                
            Free-Memory: 0                                                                                                                    
            Font-Width: 22                                                                                                                    
            Font-Height: 18                                                                                                                   
            Cookies: ASP.NET_SessionId=wvozur55usuefl55zlzc4v55                                                                                                                         
            Screen-Width: 0                                                                                                                   
            Screen-Height: 0                                                                                                                  
            Dial-up: 0
             */
            #endregion

            string strTemp = null;
            //业务编号(2010-11-10 add by Ridge)
            currentWebHeader.Set("Service-ID", currentHeader.ESP_BusinessID.ToString());

            //Software-ID 服务器分配的用户唯一识别码, 参考2.4.2
            currentWebHeader.Set("Software-ID", currentHeader.ESP_SoftwareID.ToString());

            //Network-ID 网络运营商ID, 参考2.4.3
            currentWebHeader.Set("Network-ID", currentHeader.ESP_NID.GetHashCode().ToString());

            //Device-ID 服务器分配的机型号，参考2.4.1
            currentWebHeader.Set("Device-ID", currentHeader.ESP_DID.ToString());

            strTemp = currentHeader.ESP_UserAgent.GetRawString();
            if (!string.IsNullOrEmpty(strTemp))
            {
                //设备UA(2010-9-3 ridge)
                currentWebHeader.Set("Device-UserAgent", strTemp);
            }

            strTemp = currentHeader.CellPhoneNumber;
            if (!string.IsNullOrEmpty(strTemp))
            {
                currentWebHeader.Set("MSID", strTemp);
            }

            //Encoding 客户端使用的编码格式
            currentWebHeader.Set("Encoding", currentHeader.ESP_EncodeType.GetHashCode().ToString());

            //IMEI 手机机身识别码/手机卡识别码，参考2.4.5/6
            strTemp = currentHeader.ESP_IMEI.GetRawString();
            if (!string.IsNullOrEmpty(strTemp))
            {
                currentWebHeader.Set("IMEI", strTemp);
            }

            //Free-Disk 手机端可用存储空间，单位：字节
            currentWebHeader.Set("Free-Disk", currentHeader.ESP_StoreSize.ToString());

            // Free-Memory 手机端可用内存，单位：字节
            currentWebHeader.Set("Free-Memory", currentHeader.ESP_MemorySize.ToString());

            //Font-Width 手机端字体宽度，单位：像素
            currentWebHeader.Set("Font-Width", currentHeader.ESP_FontWidth.ToString());

            //Font-Height 手机端字体高度，单位：像素
            currentWebHeader.Set("Font-Height", currentHeader.ESP_FontHeight.ToString());

            // Cookies Cookies 参考格式为:参数名1＝参数1 值&……&参数名N=参数N值
            strTemp = currentHeader.ESP_Cookies.GetRawString();
            if (!string.IsNullOrEmpty(strTemp))
            {
                if (string.IsNullOrEmpty(currentWebHeader["Cookie"]))
                {
                    currentWebHeader.Set("Cookie", strTemp);
                }
                else
                {
                    currentWebHeader.Set("Cookie", currentWebHeader["Cookie"] + "; " + strTemp);
                }
            }

            //Screen-Width 手机端屏幕宽度
            currentWebHeader.Set("Screen-Width", currentHeader.ESP_ScreenWidth.ToString());

            //Screen-Height 手机端屏幕高度
            currentWebHeader.Set("Screen-Height", currentHeader.ESP_ScreenHeight.ToString());

            //Dial-up 客户端拨号方式
            currentWebHeader.Set("Dial-up", currentHeader.ESP_DailType.GetHashCode().ToString());
        }

        /// <summary>
        /// 同步远程请求的URL地址，增加请求参数信息
        /// </summary>
        /// <param name="currentHeader">当前应用请求实例</param>
        /// <param name="requestUrl">原始请求URL地址</param>
        /// <returns></returns>
        [ImplementState(CompleteState.OK, "1.0(v3.2-2.7.2)", Description = "2.7.2 页面请求固定增加参数－兼容2.1", ReleaseDateGTM = "Thu, 31 Dec 2009 15:21:28 GMT")]
        public static string HttpRequestURLSyn(RequestHeader currentHeader, string requestUrl)
        {

            System.Collections.Specialized.NameValueCollection nv = new System.Collections.Specialized.NameValueCollection(StringComparer.InvariantCultureIgnoreCase);

            StringBuilder urlBuilder = new StringBuilder();
            int qIdx = requestUrl.IndexOf('?');
            if (qIdx != -1)
            {
                string paramStr = requestUrl.Substring(qIdx + 1);
                urlBuilder.Append(requestUrl.Substring(0, qIdx) + "?");

                string[] subParams = paramStr.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                string[] kvPairs = new string[2];
                foreach (string crtParam in subParams)
                {
                    kvPairs = crtParam.Split('=');
                    if (kvPairs.Length == 2)
                    {
                        nv.Set(kvPairs[0], kvPairs[1]);
                    }
                    else
                    {
                        //TODO?
                        nv.Set(crtParam, crtParam);
                    }
                }
            }

            if (!nv.HasKeys()) { urlBuilder.Append(requestUrl + "?"); }
            #region 参数附加
            /*
               参数名 参数说明
               sid 参考2.4.2
               imei 参考2.4.5
               imsi 参考2.4.6
               nid 参考2.4.3
               did 参考2.4.1
               dtd 拨号方式说明：
                       0－CTNET
                       1－CTWAP
            */
            nv.Set("sid", currentHeader.ESP_SoftwareID.ToString());
            nv.Set("imei", currentHeader.ESP_IMEI.GetRawString());
            nv.Set("imsi", currentHeader.ESP_IMEI.GetRawString()); //??
            nv.Set("nid", currentHeader.ESP_NID.GetHashCode().ToString());
            nv.Set("did", currentHeader.ESP_DID.ToString());
            nv.Set("dtd", currentHeader.ESP_DailType.GetHashCode().ToString());
            #endregion

            foreach (string key in nv.AllKeys)
            {
                urlBuilder.AppendFormat("{0}={1}&", key, nv[key]);
            }

            return urlBuilder.ToString().TrimEnd('&');
        }

#if UnitTest
        private static RequestHeader _RequestHeader4Test = null;
        /// <summary>
        /// 测试辅助
        /// </summary>
        public static RequestHeader RequestHeader4Test
        {
            get
            {
                if (_RequestHeader4Test == null)
                {
                    RequestHeader reqH = new RequestHeader();
                    reqH.ESP_SuccessFlag = EaseSuccessFlag.Success;
                    reqH.ESP_ParamsCount = 21;

                    reqH.ESP_SoftwareID = 10000;
                    reqH.ESP_NID = NetworkID.CTG;
                    reqH.ESP_DID = 6;
                    reqH.ESP_EncodeType = EaseEncode.UTF8;
                    reqH.ESP_Version = 0;
                    byte[] imeiBytes = EaseString.DefaultEncoding.GetBytes("460030920964516");
                    reqH.ESP_IMEI = new EaseString { ESP_Data = imeiBytes, ESP_Length = (ushort)imeiBytes.Length };
                    reqH.ESP_Update = 1;

                    reqH.ESP_SpecVersion = 1;
                    reqH.ESP_Compress = EaseCompress.NoCompress;
                    reqH.ESP_StoreSize = 204800;
                    reqH.ESP_MemorySize = 20480;

                    reqH.ESP_FontWidth = 14;
                    reqH.ESP_FontHeight = 14;
                    reqH.ESP_ConnectState = EaseConnectState.StartUp;

                    reqH.ESP_BusinessID = 10086;

                    byte[] ssBytes = Encoding.UTF8.GetBytes("abcdefghhgfedcba");
                    reqH.ESP_SessionID = new EaseString { ESP_Data = ssBytes, ESP_Length = (ushort)ssBytes.Length };

                    byte[] cookieBytes = Encoding.UTF8.GetBytes("username=ridge&lastlogin=54321");
                    reqH.ESP_Cookies = new EaseString { ESP_Data = cookieBytes, ESP_Length = (ushort)cookieBytes.Length };

                    byte[] feeBytes = Encoding.UTF8.GetBytes("$$$");
                    reqH.ESP_FeeState = new EaseString { ESP_Data = feeBytes, ESP_Length = (ushort)feeBytes.Length };

                    reqH.ESP_ScreenWidth = 240;
                    reqH.ESP_ScreenHeight = 320;
                    reqH.ESP_DailType = ClientDialType.CTNET;
                    reqH.ESP_Protocol = RequestType.UpdateCenter;

                    reqH.ESP_LeaveLength = 1000;

                    _RequestHeader4Test = reqH;
                }
                return _RequestHeader4Test;
            }
        }

        [Test]
        public void DoTest()
        {
            int testCount = 0;
            short[] AvaliableLens = new short[] { 21, 20, 18 };

            //string ttestString = RequestHeader.HttpRequestURLSyn(RequestHeader4Test, "/index.aspx");
        //Console.WriteLine(ttestString);
        //ttestString = RequestHeader.HttpRequestURLSyn(RequestHeader4Test, "/");
        //Console.WriteLine(ttestString);
        //ttestString = RequestHeader.HttpRequestURLSyn(RequestHeader4Test, "/index.aspx?a=b&c=d");
        //Console.WriteLine(ttestString);
        //ttestString = RequestHeader.HttpRequestURLSyn(RequestHeader4Test, "/index.aspx?abcd");
        //Console.WriteLine(ttestString);
        //return;

        start:

            RequestHeader reqH = RequestHeader4Test;
            reqH.ESP_SuccessFlag = EaseSuccessFlag.Success;
            reqH.ESP_ParamsCount = AvaliableLens[testCount];
            //Console.WriteLine("参数个数为:{0}", reqH.ParamsCount);

            byte[] reqHBytes = reqH.GetNetworkBytes();
            //Console.WriteLine("Total:{0}\r\n{1}", reqHBytes.ESP_Length, reqHBytes.GetHexViewString());

            RequestHeader reqH2 = new RequestHeader();
            System.IO.MemoryStream ms = new System.IO.MemoryStream(reqHBytes);
            reqH2.BindFromNetworkStream(ms, 0, false);

            byte[] bytes2cmp = reqH2.GetNetworkBytes();
            //Console.WriteLine(bytes2cmp.GetHexViewString());

            bool currentResult = SpecUtil.AreEqual(reqHBytes, bytes2cmp);
            Assert.That(currentResult, string.Format("**参数为{0}个时，测试失败", reqH.ESP_ParamsCount));

            testCount++;
            if (testCount < 3)
            {
                goto start;
            }
            else
            {
                RequestHeader4Test.ESP_ParamsCount = 21;
            }

        }
#endif

    }
}
