using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Remotocon.Server
{
    class XmlRpcElements : Dictionary<string, XmlRpcElementEnum>
    {        
        public const string methodCall =         "methodCall"        ;
        public const string methodName =         "methodName"        ;
        public const string params_ =            "params"            ;
        public const string param =              "param"             ;
        public const string value =              "value"             ;
        public const string i4 =                 "i4"                ;
        public const string int_ =               "int"               ;
        public const string boolean =            "boolean"           ;
        public const string string_ =            "string"            ;
        public const string double_ =            "double"            ;
        public const string dateTime =           "dateTime.iso8601"  ; 
        public const string base64 =             "base64"            ;
        public const string struct_ =            "struct"            ;
        public const string array =              "array"             ;
        public const string member =             "member"            ;
        public const string name =               "name"              ;
        public const string data =               "data"              ;
        public const string methodResponse =     "methodResponse"    ;
        public const string fault =              "fault"             ;

        private static XmlRpcElements _dict = null;

        public static XmlRpcElements Dict
        {
            get
            {
                if (_dict == null)
                {
                    _dict = new XmlRpcElements()
                    {
                        {methodCall, XmlRpcElementEnum.methodCall },
                        {methodName, XmlRpcElementEnum.methodName },
                        {params_, XmlRpcElementEnum.params_ },
                        {param, XmlRpcElementEnum.param },
                        {value, XmlRpcElementEnum.value },
                        {i4, XmlRpcElementEnum.i4 },
                        {int_, XmlRpcElementEnum.int_ },
                        {boolean, XmlRpcElementEnum.boolean },
                        {string_, XmlRpcElementEnum.string_ },
                        {double_, XmlRpcElementEnum.double_ },
                        {dateTime, XmlRpcElementEnum.dateTime },
                        {base64, XmlRpcElementEnum.base64 },
                        {struct_, XmlRpcElementEnum.struct_ },
                        {array, XmlRpcElementEnum.array },
                        {member, XmlRpcElementEnum.member },
                        {name, XmlRpcElementEnum.name },
                        {data, XmlRpcElementEnum.data },
                        {methodResponse, XmlRpcElementEnum.methodResponse },
                        {fault, XmlRpcElementEnum.fault }
                    };
                }
                return _dict;
            }
        }                                                                               
    }
}





















