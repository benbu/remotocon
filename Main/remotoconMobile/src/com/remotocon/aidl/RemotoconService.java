package com.remotocon.aidl;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.ArrayList;
import java.util.Hashtable;

import com.remotocon.preferences.Preferences;
import com.remotocon.preferences.ServerPreferences;
import com.remotocon.xmlrpc.XmlRpcClient;

import android.app.Service;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.IBinder;

public class RemotoconService extends Service {

	private XmlRpcClient client;
	
	@Override
	public void onCreate()
	{
		super.onCreate();
        SharedPreferences mainPreferences = getSharedPreferences(Preferences.PREF_MAIN, MODE_PRIVATE);
        int selectedServerIndex = mainPreferences.getInt(Preferences.KEY_SELECTED_SERVER_INDEX, -1);
            
        if(selectedServerIndex > -1)
        {
	        SharedPreferences serverPrefs = getSharedPreferences(ServerPreferences.PREF_SERVER + selectedServerIndex, MODE_PRIVATE);
		    this.client = new XmlRpcClient(serverPrefs);
        }
	}
	
	@Override
	public IBinder onBind(Intent arg0) {
		return mBinder;
	}
	
	private final IRemotoconService.Stub mBinder = new IRemotoconService.Stub(){
	    public byte[] remoteProcedureCall(String objectName, String methodName, byte[] objectArray){
	    	if(client == null)
	    		return null;
	    	
	        ByteArrayInputStream bais = new ByteArrayInputStream(objectArray);
	        byte[] resultBytes = null;
	        try
	        {
		        ObjectInputStream ois = new ObjectInputStream(bais);
		        Object[] params = (Object[])ois.readObject();   	

		        Object result = client.CallRemoteMethod(objectName + "." + methodName, params);
		        
				ByteArrayOutputStream baos = new ByteArrayOutputStream();
				ObjectOutputStream oos = new ObjectOutputStream(baos);
				
				oos.writeObject(result);
				resultBytes = baos.toByteArray();
	        }
	        catch(IOException e) {
	        	return null;
	        } catch (ClassNotFoundException e) {	        	
	        	return null;
			} catch (Exception e) {
				return null;
			}
	        return resultBytes;
	    }
	    
	    @SuppressWarnings("rawtypes")
		public boolean checkServerPluginExists(String packageName, String version)
	    {
	    	ArrayList<Object> serverPlugins = null;
	    	
	    	try
	    	{
	    		serverPlugins = client.getServerPlugins();
	    	}
	    	catch(Exception e)
	    	{
	    		return false;
	    	}
	    	
	        boolean found = false;
			for(Object o : serverPlugins)
			{
				if(o instanceof Hashtable)
				{
					String pkgName = (String)((Hashtable)o).get("AndroidPackageName");
					String ver = (String)((Hashtable)o).get("Version");
					
					if(pkgName.equalsIgnoreCase(packageName))
						if(ver.equalsIgnoreCase(version))
						{
							found = true;
							break;
						}
				}
			}
			
			return found;
	    }
	    
	    public boolean installServerPlugin(String dllURL, String dllName)
	    {
	    	return client.installServerPlugin(dllURL, dllName);
	    }
	};

}
