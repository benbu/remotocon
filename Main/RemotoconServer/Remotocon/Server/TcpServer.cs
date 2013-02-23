using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Xml;
using System.Reflection;
using RemotoconServerPlugin;

namespace Remotocon.Server
{
    public class TcpServer : IXmlRpcServer
    {
        static readonly long MAX_CONNECTION_TICKS = 10000000L * 60L * 30L; // 30 mins (10,000,000 ticks = 1 second

        private TcpListener listener;
        private Thread thread;

        private string userName;
        private string passWord;

        private Dictionary<string, object> handlers = new Dictionary<string, object>();
        private Dictionary<IPAddress, DateTime> connections = new Dictionary<IPAddress, DateTime>();

        public TextWriter OutStream { get; set; }

        public TcpServer(string userName, string passWord, int port)
        {
            this.userName = userName;
            this.passWord = passWord;

            listener = new TcpListener(IPAddress.Any, port);
            thread = new Thread(new ThreadStart(ListenForTcpClients));
            thread.IsBackground = true;
        }

        public void Start()
        {
            WriteLog("Starting Server");
            thread.Start();
        }

        public void Stop()
        {
            WriteLog("Stopping Server");
            thread.Abort();
        }

        private void ListenForTcpClients()
        {
            listener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                WriteLog("Listening...");
                TcpClient client = listener.AcceptTcpClient();

                WriteLog("Connection attempt from " + client.Client.RemoteEndPoint);

                //create a thread to handle communication
                //with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.IsBackground = true;
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();
            IPEndPoint endPoint = (IPEndPoint)tcpClient.Client.RemoteEndPoint;

            MemoryStream inputStream = new MemoryStream();
            MemoryStream outputStream = new MemoryStream();

            byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
            
            int bytesRead = clientStream.Read(buffer, 0, tcpClient.ReceiveBufferSize);

            inputStream.Write(buffer, 0, bytesRead);

            inputStream.Seek(0, SeekOrigin.Begin);

            WriteLog(new String(UTF8Encoding.UTF8.GetChars(inputStream.ToArray())));

            if (IsLoggedIn(endPoint))
                ProcessRequest(inputStream, outputStream);
            else
                ProcessLoginRequest(inputStream, outputStream, endPoint);

            byte[] data = outputStream.ToArray();

            clientStream.Write(data, 0, data.Length);

            clientStream.Flush();

            tcpClient.Close();

            WriteLog(new String(UTF8Encoding.UTF8.GetChars(data)));
        }

        private bool IsLoggedIn(IPEndPoint endPoint)
        {
            DateTime now = DateTime.Now;
            if(connections.ContainsKey(endPoint.Address))
                if (now.Ticks - connections[endPoint.Address].Ticks < MAX_CONNECTION_TICKS)
                {
                    connections[endPoint.Address] = now;
                    return true;
                }
            return false;
        }

        private void ProcessLoginRequest(MemoryStream inputStream, MemoryStream outputStream, IPEndPoint endPoint)
        {
            XmlRpcResponse response = null;

            try
            {
                XmlRpcRequest request = XmlRpcRequest.ParseXmlRpcRequest(inputStream);
                if (request.ObjectName == "Server" && request.MethodName == "Login")
                {
                    if (request.Params.Count == 2 && request.Params[0] is String && request.Params[1] is String)
                    {
                        bool loggedIn;
                        lock (this)
                        {
                            loggedIn = (String)request.Params[0] == this.userName && (String)request.Params[1] == this.passWord;
                        }

                        if (loggedIn)
                        {
                            DateTime now = DateTime.Now;

                            if (connections.ContainsKey(endPoint.Address))
                                connections[endPoint.Address] = now;
                            else
                                connections.Add(endPoint.Address, DateTime.Now);

                            response = new XmlRpcResponse(true);
                        }
                        else
                            throw new LoginException("Invalid Username or Password");
                    }
                    else
                        throw new LoginException("Invalid parameter list for the login method");
                }
                else
                    throw new LoginException("You must be logged in to perform this action.");
            }
            catch (LoginException le)
            {
                response = new XmlRpcResponse(le.Message, le.GetHashCode());
            }
            catch (Exception e)
            {
                response = new XmlRpcResponse("Error", 0);
            }

            try
            {
                response.WriteXml(outputStream);
            }
            catch(Exception e) 
            {
                WriteLog(e.Message);
            }
        }

        private void ProcessRequest(MemoryStream inputStream, MemoryStream outputStream)
        {
            XmlRpcResponse response = null;

            try
            {
                XmlRpcRequest request = XmlRpcRequest.ParseXmlRpcRequest(inputStream);
                response = InvokeMethod(request);
            }
            catch (Exception e)
            {
                response = new XmlRpcResponse(e.Message, e.GetHashCode());
            }

            try
            {
                response.WriteXml(outputStream);
            }
            catch (Exception e)
            {
                WriteLog(e.Message);
            }
        }

        private XmlRpcResponse InvokeMethod(XmlRpcRequest request)
        {
            Object returnValue = null;

            Object handler = null;

            lock (this)
            {
                if (handlers.ContainsKey(request.ObjectName))
                    handler = handlers[request.ObjectName];

                if (handler == null)
                    throw new XmlRpcException(String.Format("No handler registered for \"{0}\"", request.ObjectName));

                Type handlerType = handler.GetType();
                MethodInfo methodInfo = handlerType.GetMethod(request.MethodName);

                if (methodInfo == null)
                    throw new MissingMethodException("Method " + request.MethodName + " not found.");

                returnValue = methodInfo.Invoke(handler, request.Params.ToArray());
            }

            return new XmlRpcResponse(returnValue);
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

        #region IXmlRpcServer Members

        public void RegisterHandler(string objectName, object obj)
        {
            if (handlers.ContainsKey(objectName))
                throw new DuplicateObjectMethodNameException("A handler for {0} has already been registered.", objectName);
            handlers.Add(objectName, obj);
        }

        #endregion
    }
}
