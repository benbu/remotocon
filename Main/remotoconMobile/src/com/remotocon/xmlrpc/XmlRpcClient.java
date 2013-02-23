package com.remotocon.xmlrpc;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.io.Writer;
import java.net.Socket;
import java.net.SocketException;
import java.net.UnknownHostException;
import java.util.ArrayList;

import org.xml.sax.SAXException;

import com.remotocon.preferences.ServerPreferences;

import android.content.SharedPreferences;

public class XmlRpcClient {
	private static final int TIMEOUT = 30 * 1000; //30 Seconds
	
	public String errorMessage = "";
	
	//HttpURLConnection httpCon;
	//Encryptor encryptor;

	public String serverName;
	
	private String host;
	private int port;
	
	private String userName;
	private String password;
	
	public XmlRpcClient(SharedPreferences serverPrefs) {
		serverName = serverPrefs.getString(ServerPreferences.KEY_SERVER_NAME, "");
		host = serverPrefs.getString(ServerPreferences.KEY_SERVER_ADDRESS, "");
		port = serverPrefs.getInt(ServerPreferences.KEY_SERVER_PORT, 0);
		
		userName = serverPrefs.getString(ServerPreferences.KEY_SERVER_USERNAME, "");
		password = serverPrefs.getString(ServerPreferences.KEY_SERVER_PASSWORD, "");
	}
	
	public Object CallRemoteMethod(String objectMethodName, Object... params) throws XmlRpcFaultException, Exception
	{
		boolean tryAgain = true;
		
		XmlRpcResponse response = getMethodResponse(objectMethodName, params);
		
		while(tryAgain)
		{
			tryAgain = false;
			if(response == null)
			{
				errorMessage += "Unable to connect to computer.";
				throw new Exception("Unable to connect to computer.");
			}
			if(response.isFault)
			{
				if(response.FaultMessage.equalsIgnoreCase("You must be logged in to perform this action."))
				{
					if(TryLogin())
					{
						response = getMethodResponse(objectMethodName, params);
						tryAgain = true;
					}
					else
					{
						errorMessage += "Invalid Username or Password";
						throw new XmlRpcFaultException(response.FaultMessage);
					}
				}
				else
				{
					errorMessage += "An Error occurred on the computer.";
					throw new XmlRpcFaultException(response.FaultMessage);
				}
			}
		}
		
		return response.Result;		
	}

	private boolean TryLogin() {
		XmlRpcResponse response = getMethodResponse("Server.Login",this.userName, this.password);
		if(response != null && !response.isFault)
			if(response.Result instanceof Boolean)
				return (Boolean)response.Result;
		return false;
	}

	private XmlRpcResponse getMethodResponse(String objectMethodName, Object... params) {
		
		XmlRpcResponse response = null;
		Socket sock = null;
		InputStream in = null;
		OutputStream out = null;
		
		try
		{	    
		    int bytesRead;
		    byte[] buffer = new byte[4096];
		    
			sock = new Socket(host, port);
			
			sock.setSoTimeout(TIMEOUT);

		    ByteArrayOutputStream baos = new ByteArrayOutputStream();
		    Writer byteWriter = new OutputStreamWriter(baos, "UTF-8");
		    
		    XmlRpcRequest request = new XmlRpcRequest(params);
 		    request.MethodName = objectMethodName;		    
		    request.WriteXml(byteWriter);

			out = sock.getOutputStream();
			out.write(baos.toByteArray());
		    
		    in = sock.getInputStream();		    
		    baos = new ByteArrayOutputStream();			    
            while ((bytesRead = in.read(buffer, 0, 4096)) != -1)
                baos.write(buffer, 0, bytesRead);
		    
		    response = XmlRpcResponse.ParseResponse(new ByteArrayInputStream(baos.toByteArray()));
		} catch (UnknownHostException e) {
			e.printStackTrace();
		} catch(SocketException se) {
			se.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		} catch (SAXException e) {
			e.printStackTrace();
		} catch (Exception e) {
			e.printStackTrace();
		}
		finally
		{
			try
			{
				out.close();
				in.close();
				sock.close();
			}
			catch(Exception ignored){}
		}
		
		return response;
	}
	
	@Override
	public String toString()
	{
		return serverName;
	}

	@SuppressWarnings("unchecked")
	public ArrayList<Object> getServerPlugins() throws Exception {
		return (ArrayList<Object>) this.CallRemoteMethod("Server.GetServerPlugins");
	}

	public boolean installServerPlugin(String dllURL, String dllName) {

    	boolean success = false;
    	try {
    		Object result = CallRemoteMethod("Server.DownloadInstallPlugin", dllURL, dllName);
    		if(result instanceof Boolean)
    			success = (Boolean)result;
		} catch (Exception e) {
			e.printStackTrace();
		}
		
    	return success;
	}
}
