using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using System.Globalization;
using System.Collections;
using System.Net.Sockets;

namespace Remotocon.Server
{
    class XmlRpcRequest
    {
        private static Stack<XmlRpcElementEnum> elementStack = new Stack<XmlRpcElementEnum>();
        private static Stack<Object> collectionStack = new Stack<Object>();

        public const string datetimeFormat = "yyyyMMdd\\THH\\:mm\\:ss";
        public static readonly DateTimeFormatInfo datetimeInfo = new DateTimeFormatInfo();

        public String MethodName { get; private set; }
        public String ObjectName { get; private set; }
        public List<Object> Params { get; private set; }

        private XmlRpcRequest()
        {
            this.Params = new List<object>();
        }

        public static XmlRpcRequest ParseXmlRpcRequest(byte[] data)
        {
            return XmlRpcRequest.ParseXmlRpcRequest(new XmlTextReader(new MemoryStream(data)));
        }

        public static XmlRpcRequest ParseXmlRpcRequest(MemoryStream ms)
        {
            return XmlRpcRequest.ParseXmlRpcRequest(new XmlTextReader(ms));
        }

        public static XmlRpcRequest ParseXmlRpcRequest(XmlTextReader xtr)
        {
            XmlRpcRequest request = new XmlRpcRequest();

            datetimeInfo.FullDateTimePattern = datetimeFormat;

            string memberName = null;
            object value = null;

            //xtr.Read();
            collectionStack.Push(request.Params);
            while (xtr.Read())
            {
                switch (xtr.NodeType)
                {
                    case XmlNodeType.Element:
                        if (XmlRpcElements.Dict.ContainsKey(xtr.Name))
                            elementStack.Push(XmlRpcElements.Dict[xtr.Name]);
                        else
                            throw new XmlException(String.Format("Unidentified XML node \"{0}\"", xtr.Name));
                        switch (elementStack.Peek())
                        {
                            case XmlRpcElementEnum.array:
                                collectionStack.Push(new List<Object>());
                                break;
                            case XmlRpcElementEnum.struct_:
                                collectionStack.Push(new Dictionary<string, object>());
                                break;
                        }
                        break;
                    case XmlNodeType.Text:
                        if (elementStack.Count < 1)
                            break;
                        switch (elementStack.Peek())
                        {
                            case XmlRpcElementEnum.base64:
                                value = Convert.FromBase64String(xtr.Value);
                                break;
                            case XmlRpcElementEnum.boolean:
                                value = xtr.Value == "1";
                                break;
                            case XmlRpcElementEnum.dateTime:
                                value = DateTime.ParseExact(xtr.Value, "F", datetimeInfo);
                                break;
                            case XmlRpcElementEnum.double_:
                                value = Double.Parse(xtr.Value);
                                break;
                            case XmlRpcElementEnum.i4:
                            case XmlRpcElementEnum.int_:
                                value = Int32.Parse(xtr.Value);
                                break;
                            case XmlRpcElementEnum.methodName:
                                if (request.MethodName == null)
                                    ParseMethodName(xtr.Value, request);
                                else
                                    throw new XmlException("Invalid XML Structure, multiple \"methodName\" nodes detected.");
                                break;
                            case XmlRpcElementEnum.name:
                                memberName = xtr.Value;
                                break;
                            case XmlRpcElementEnum.string_:
                            case XmlRpcElementEnum.value:
                                value = xtr.Value;
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        XmlRpcElementEnum endElement = XmlRpcElements.Dict[xtr.Name];

                        if (elementStack.Peek() == endElement)
                            elementStack.Pop();
                        else
                            throw new XmlException(String.Format("Was expecting end tag for {0}, instead got {1}", elementStack.Peek(), endElement));

                        switch (endElement)
                        {
                            case XmlRpcElementEnum.value:
                                if (collectionStack.Peek() is IList)
                                    ((List<Object>)collectionStack.Peek()).Add(value);
                                break;
                            case XmlRpcElementEnum.member:
                                ((Dictionary<string, Object>)collectionStack.Peek()).Add(memberName, value);
                                break;
                            case XmlRpcElementEnum.array:
                                value = ((List<Object>)collectionStack.Pop());
                                break;
                            case XmlRpcElementEnum.struct_:
                                value = ((Dictionary<string, Object>)collectionStack.Pop());
                                break;
                        }
                        break;                                               
                }
            }

            if (collectionStack.Count != 1)
                throw new XmlException(String.Format("CollectionStack count was {0} at end of parsing. Should be 1.", collectionStack.Count));
            if (elementStack.Count > 0)
                throw new XmlException("Node Stack not empty after parsing.");
            if (request.MethodName == null)
                throw new XmlException("No method name found after parsing.");

            collectionStack.Clear();
            elementStack.Clear();

            return request;
        }

        private static void ParseMethodName(string p, XmlRpcRequest request)
        {
            String[] args = p.Split('.');
            if (args.Length == 1)
                request.MethodName = args[0];
            else if (args.Length == 2)
            {
                request.MethodName = args[1];
                request.ObjectName = args[0];
            }
            else
                throw new XmlException("Invalid methodName \"" + p + "\"");
        }
    }
}
