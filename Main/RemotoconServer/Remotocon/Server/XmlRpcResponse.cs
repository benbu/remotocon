using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;

namespace Remotocon.Server
{
    class XmlRpcResponse
    {
        private object value;
        string faultMessage;
        int faultCode;

        public XmlRpcResponse(object value)
        {
            if (value == null)
                throw new XmlRpcException("Return value is NULL.");

            this.value = value;
        }

        public XmlRpcResponse(string faultMessage, int faultCode)
        {
            this.faultMessage = faultMessage;
            this.faultCode = faultCode;
        }

        public void WriteXml(MemoryStream ms)
        {
            WriteXml(new XmlTextWriter(ms, UTF8Encoding.UTF8));
        }

        public void WriteXml(XmlTextWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement(XmlRpcElements.methodCall);

            if (value == null)
            {
                writer.WriteStartElement(XmlRpcElements.fault);
                writer.WriteStartElement(XmlRpcElements.value);
                writer.WriteStartElement(XmlRpcElements.struct_);
                writer.WriteStartElement(XmlRpcElements.member);

                writer.WriteElementString(XmlRpcElements.name, "faultCode");
                WriteXmlValue(writer, this.faultCode);

                writer.WriteEndElement();
                writer.WriteStartElement(XmlRpcElements.member);

                writer.WriteElementString(XmlRpcElements.name, "faultString");
                WriteXmlValue(writer, this.faultMessage);

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();                
            }
            else
            {
                writer.WriteStartElement(XmlRpcElements.params_);
                writer.WriteStartElement(XmlRpcElements.param);

                WriteXmlValue(writer, this.value);

                writer.WriteEndElement();
                writer.WriteEndElement();
            }
            writer.WriteEndDocument();
            writer.Flush();
        }

        private void WriteXmlValue(XmlTextWriter writer, object value)
        {
            writer.WriteStartElement(XmlRpcElements.value);

            if (value != null)
            {
                if (value is byte[])
                {
                    byte[] ba = (byte[])value;
                    writer.WriteStartElement(XmlRpcElements.base64);
                    writer.WriteBase64(ba, 0, ba.Length);
                    writer.WriteEndElement();
                }
                else if (value is String) writer.WriteElementString(XmlRpcElements.string_, value.ToString());
                else if (value is Int32) writer.WriteElementString(XmlRpcElements.int_, value.ToString());
                else if (value is DateTime) writer.WriteElementString(XmlRpcElements.dateTime, ((DateTime)value).ToString(XmlRpcRequest.datetimeFormat));
                else if (value is Double) writer.WriteElementString(XmlRpcElements.double_, value.ToString());
                else if (value is Boolean) writer.WriteElementString(XmlRpcElements.boolean, (Boolean)value == true ? "1" : "0");
                else if (value is IList)
                {
                    writer.WriteStartElement(XmlRpcElements.array);
                    writer.WriteStartElement(XmlRpcElements.data);
                    if (((IList)value).Count > 0)
                    {
                        foreach (Object member in ((IList)value))
                            WriteXmlValue(writer, member);
                    }
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }
                else if (value is IDictionary)
                {
                    IDictionary h = (IDictionary)value;
                    writer.WriteStartElement(XmlRpcElements.struct_);
                    foreach (String key in h.Keys)
                    {
                        writer.WriteStartElement(XmlRpcElements.member);
                        writer.WriteElementString(XmlRpcElements.name, key);
                        WriteXmlValue(writer, h[key]);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        }
    }
}
