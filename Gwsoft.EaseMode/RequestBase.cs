using Gwsoft.DataSpec;

namespace Gwsoft.EaseMode
{
    /// <summary>
    /// 所有请求基类(包含请求头信息配置)
    /// </summary>
    public abstract class RequestBase : ESPDataBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestBase"/> class.
        /// </summary>
        public RequestBase()
            : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestBase"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public RequestBase(ESPContext context)
            : base(context)
        { }

        #region 属性模型顺序
        /// <summary>
        /// 请求头信息
        /// </summary>
        [ObjectTransferOrder(0, Reverse = false, Offset = 0)]
        public RequestHeader ESP_Header { get; set; }
        #endregion

        /// <summary>
        /// 以索引属性的方式获取或设置上下文中存储的键值
        /// </summary>
        /// <value></value>
        public object this[string key]
        {
            get
            {
                if (Context != null)
                {
                    return Context.GetItem(key);
                }
                else
                {
                    return null;
                }
            }

            set 
            {
                if (Context == null)
                {
                    Context = new ESPContext(); 
                }
                Context.SetItem(key, value);
            }
        }

        /// <summary>
        /// 获取请求用户的编号
        /// </summary>
        /// <returns></returns>
        public int GetRequestUserID()
        {
            if (ESP_Header == null) return 0;
            return ESP_Header.ESP_SoftwareID;
        }

        /// <summary>
        /// 获取当前请求的业务编号
        /// </summary>
        public short GetBusinessID()
        {
            if (ESP_Header == null) return 0;
            return ESP_Header.ESP_BusinessID;
        }

    }
}
