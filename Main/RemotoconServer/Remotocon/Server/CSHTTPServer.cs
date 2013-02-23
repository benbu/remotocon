using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Xml;
using RemotoconServerPlugin;

namespace Remotocon.Server
{
    public class CSHTTPServer : IXmlRpcServer
    {
        private int portNum = 5050;
        private TcpListener listener;
        System.Threading.Thread Thread;
        public TextWriter OutStream { get; set; }
        public Encryption encryptor;

        private Dictionary<string, object> handlers;

        public Hashtable respStatus;

        public string Name = "MyHTTPServer/1.0.*";

        public bool IsAlive
        {
            get
            {
                return this.Thread.IsAlive;
            }
        }

        public CSHTTPServer(string userName, string password, int thePort)
        {
            encryptor = new Encryption(userName, password);
            portNum = thePort;

            WriteLog(this.Name);
            respStatusInit();

            handlers = new Dictionary<string, object>();
        }

        private void respStatusInit()
        {
            respStatus = new Hashtable();

            respStatus.Add(200, "200 Ok");
            respStatus.Add(201, "201 Created");
            respStatus.Add(202, "202 Accepted");
            respStatus.Add(204, "204 No Content");

            respStatus.Add(301, "301 Moved Permanently");
            respStatus.Add(302, "302 Redirection");
            respStatus.Add(304, "304 Not Modified");

            respStatus.Add(400, "400 Bad Request");
            respStatus.Add(401, "401 Unauthorized");
            respStatus.Add(403, "403 Forbidden");
            respStatus.Add(404, "404 Not Found");

            respStatus.Add(500, "500 Internal Server Error");
            respStatus.Add(501, "501 Not Implemented");
            respStatus.Add(502, "502 Bad Gateway");
            respStatus.Add(503, "503 Service Unavailable");
        }

        public void Listen()
        {
            bool done = false;

            listener = new TcpListener(new System.Net.IPAddress(new byte[]{127,0,0,1}),portNum);

            listener.Start();

            WriteLog("Listening On: " + portNum.ToString());

            while (!done)
            {
                WriteLog("Waiting for connection...");
                CsHTTPRequest newRequest = new CsHTTPRequest(listener.AcceptTcpClient(), this);
                Thread Thread = new Thread(new ThreadStart(newRequest.Process));
                Thread.Name = "HTTP Request";
                Thread.Start();
            }

        }

        public void WriteLog(string EventMessage)
        {
            lock (this)
            {
                if (OutStream == null)
                    Console.WriteLine(EventMessage);
                else
                    OutStream.WriteLine(EventMessage);
            }
        }

        public void Start()
        {
            // CSHTTPServer HTTPServer = new CSHTTPServer(portNum);

            this.Thread = new Thread(new ThreadStart(this.Listen));
            this.Thread.Start();
        }

        public void Stop()
        {
            listener.Stop();
            this.Thread.Abort();
        }

        public void Suspend()
        {
            this.Thread.Suspend();
        }

        public void Resume()
        {
            this.Thread.Resume();
        }

        public void RegisterHandler(string objectName, object obj)
        {
            if (handlers.ContainsKey(objectName))
                throw new DuplicateObjectMethodNameException("A handler for {0} has already been registered.", objectName);
            handlers.Add(objectName, obj);
        }

        public void OnResponse(ref HTTPRequestStruct hTTPRequestStruct, ref HTTPResponseStruct responseStruct)
        {
            char[] base64chars = Encoding.UTF8.GetChars(hTTPRequestStruct.BodyData);

            WriteLog("Base64Chars: " + new String(base64chars));

            byte[] encBytes = Convert.FromBase64CharArray(base64chars, 0, base64chars.Length);

            XmlRpcRequest xmlRequest = XmlRpcRequest.ParseXmlRpcRequest(encryptor.Decrypt(encBytes));

            WriteLog("\n\n MethodCall: " + xmlRequest.ObjectName + "." + xmlRequest.MethodName);
            WriteLog("\n\n Params ");
            foreach (Object o in xmlRequest.Params)
                WriteLog("\n " + o);

            Object handler = null;

            if(handlers.ContainsKey(xmlRequest.ObjectName))
                handler = handlers[xmlRequest.ObjectName];

            if (handler == null)
                throw new XmlRpcException(String.Format("No handler registered for \"{0}\"", xmlRequest.ObjectName));

            Type handlerType = handler.GetType();
            MethodInfo methodInfo = handlerType.GetMethod(xmlRequest.MethodName);

            if (methodInfo == null)
                throw new MissingMethodException("Method " + xmlRequest.MethodName + " not found.");

            Object returnValue = null;

            lock (this)
            {
                returnValue = methodInfo.Invoke(handler, xmlRequest.Params.ToArray());
            }

            if (returnValue == null)
                throw new XmlRpcException("Method returned NULL.");

            XmlRpcResponse response = new XmlRpcResponse(returnValue);
            MemoryStream ms = new MemoryStream();
            response.WriteXml(new XmlTextWriter(ms, UTF8Encoding.UTF8));

            responseStruct.BodyData = UTF8Encoding.UTF8.GetBytes(Convert.ToBase64String(encryptor.Encrypt(ms.ToArray())));
            responseStruct.BodySize = responseStruct.BodyData.Length;
        }

        public bool InitiateConnection()
        {
            return true;
        }

        public ServerPluginServices PluginServices { get; set; }
    }
}
