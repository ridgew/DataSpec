using System;
using System.Collections.Generic;
using System.Text;
using Gwsoft.DataSpec;
using Gwsoft.Configuration;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

#if UnitTest
using NUnit.Framework;
using System.IO;
#endif

namespace Gwsoft.EaseMode
{
    /// <summary>
    /// Ease字符串(不定长）
    /// </summary>
#if UnitTest
    [TestFixture]
#endif
    [ImplementState(CompleteState.OK, "1.0(v3.2-2.3.1)", Description = "字符串封装", ReleaseDateGTM = "Wed, 30 Dec 2009 10:08:12 GMT")]
    [DebuggerDisplay("{GetRawString(),nq}")]
    public class EaseString : ESPDataBase, IXmlSerializable
    {

        /// <summary>
        /// 初始化 <see cref="EaseString"/> class.
        /// </summary>
        public EaseString()
            : base()
        {
        }

        /// <summary>
        /// 初始化一个 <see cref="EaseString"/> class 实例。
        /// </summary>
        /// <param name="context">The context.</param>
        public EaseString(ESPContext context)
            : base(context)
        {
        }

        #region 传输属性
        /// <summary>
        /// 字符串为空时，此处为0
        /// </summary>
        [ObjectTransferOrder(0, Reverse = true, Offset = 0)]
        [System.Xml.Serialization.XmlAttribute]
        public UInt16 ESP_Length { get; set; }

        /// <summary>
        /// 字符串为空时，此处不存在
        /// </summary>
        [ObjectTransferOrder(1, Reverse = false, Offset = 2)]
        public byte[] ESP_Data { get; set; }

        #endregion
        /// <summary>
        /// 字符串封装的默认编码
        /// </summary>
        [DebuggerDisplay("utf-8")]
        public static Encoding DefaultEncoding
        {
            get { return Encoding.UTF8; }
        }

        /// <summary>
        /// 没有内容的字符串
        /// </summary>
        public static EaseString Empty = new EaseString { ESP_Data = new byte[0], ESP_Length = 0 };

        /// <summary>
        /// 获取原生字符串的封装格式
        /// </summary>
        public static EaseString Get(string rawString)
        {
            byte[] binDat = DefaultEncoding.GetBytes(rawString ?? string.Empty).TrimStart(SpecUtil.UTF8_BOM_BYTES);
            return new EaseString { ESP_Data = binDat, ESP_Length = (ushort)binDat.Length };
        }

        /// <summary>
        /// 获取POCO字符串内容
        /// </summary>
        /// <returns></returns>
        public string GetRawString()
        {
            string strResult = null;
            if (ESP_Data != null && ESP_Data.Length > 0)
            {
                strResult = DefaultEncoding.GetString(ESP_Data);
            }
            return strResult;
        }

        /// <summary>
        /// 获取字节序列总长度
        /// </summary>
        /// <returns></returns>
        public override long GetContentLength()
        {
            return 2L + Convert.ToInt64(ESP_Length);
        }

        /// <summary>
        /// 获取网络字节序列
        /// </summary>
        /// <returns></returns>
        public override byte[] GetNetworkBytes()
        {
            byte[] retBytes = new byte[0];

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            byte[] lenBytes = SpecUtil.ReverseBytes(BitConverter.GetBytes(ESP_Length));
            ms.Write(lenBytes, 0, lenBytes.Length);
            if (ESP_Length > 0)
            {
                ms.Write(ESP_Data, 0, ESP_Data.Length);
            }
            retBytes = ms.ToArray();
            ms.Dispose();

            return retBytes;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == this.GetType())
            {
                EaseString target = (EaseString)obj;
                return (target.ESP_Length == this.ESP_Length &&
                    SpecUtil.AreEqual(target.ESP_Data, this.ESP_Data));
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 返回表示当前 <see cref="T:System.Object"/> 的 <see cref="T:System.String"/>。
        /// </summary>
        /// <returns>
        /// 	<see cref="T:System.String"/>，表示当前的 <see cref="T:System.Object"/>。
        /// </returns>
        public override string ToString()
        {
            return string.Format("[Length:{0},Value:{1}]", ESP_Length, GetRawString());
        }

        #region IXmlSerializable 成员

        /// <summary>
        /// 此方法是保留方法，请不要使用。在实现 IXmlSerializable 接口时，应从此方法返回 null（在 Visual Basic 中为 Nothing），如果需要指定自定义架构，应向该类应用 <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/>。
        /// </summary>
        /// <returns>
        /// 	<see cref="T:System.Xml.Schema.XmlSchema"/>，描述由 <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> 方法产生并由 <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> 方法使用的对象的 XML 表示形式。
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// 从对象的 XML 表示形式生成该对象。
        /// </summary>
        /// <param name="reader">对象从中进行反序列化的 <see cref="T:System.Xml.XmlReader"/> 流。</param>
        public void ReadXml(XmlReader reader)
        {
            int entDeepth = reader.Depth;
            while (reader.Read() && reader.Depth > entDeepth)
            {
                if (reader.NodeType == XmlNodeType.Text || reader.NodeType == XmlNodeType.CDATA)
                {
                    string rawString = reader.Value;
                    byte[] binDat = DefaultEncoding.GetBytes(rawString ?? string.Empty).TrimStart(SpecUtil.UTF8_BOM_BYTES);
                    ESP_Data = binDat;
                    ESP_Length = (ushort)binDat.Length;
                }
            }
            if (reader.NodeType == XmlNodeType.EndElement)
                reader.Read();
        }

        /// <summary>
        /// 将对象转换为其 XML 表示形式。
        /// </summary>
        /// <param name="writer">对象要序列化为的 <see cref="T:System.Xml.XmlWriter"/> 流。</param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteCData(GetRawString());
        }

        #endregion


#if UnitTest
        [Test]
        public void DoTest()
        {
            byte[] dat = Encoding.UTF8.GetBytes("测试");
            EaseString str = new EaseString { ESP_Length = (ushort)dat.Length, ESP_Data = dat };

            //Console.WriteLine("{0}={1}", str.ESP_Length, dat.ESP_Length);

            byte[] networkBytes = str.GetNetworkBytes();
            //Console.WriteLine("Len: {1}, Network Bytes: {0}", BitConverter.ToString(networkBytes), networkBytes.ESP_Length);
            //Console.WriteLine("Total:{0}\r\n{1}", networkBytes.ESP_Length, networkBytes.GetHexViewString());

            EaseString str2 = new EaseString();
            MemoryStream ms = new MemoryStream(networkBytes);
            ms.Position = 0;

            if (!HasImplementDataBind)
            {
                str2.BindFromNetworkStream(ms, 0, false);
            }
            else
            {
                str2.BindMappingWithStream(ms);
            }

            byte[] bytes2cmp = str2.GetNetworkBytes();
            //Console.WriteLine("Cmp Total:{0}\r\n{1}", bytes2cmp.ESP_Length, bytes2cmp.GetHexViewString());

            Assert.That(str.ESP_Length == str2.ESP_Length);
            Assert.That(SpecUtil.AreEqual(str.ESP_Data, str2.ESP_Data));
            Assert.That(str.Equals(str2));
        }
#endif


    }
}
