package com.remotocon.preferences;

import java.util.ArrayList;

import com.remotocon.xmlrpc.XmlRpcClient;

import android.content.SharedPreferences;
import android.content.Context;

public class Preferences {
	public static final String PREF_MAIN = "REMOTOCON_MAIN_PREFS";
	public static final String KEY_SELECTED_SERVER_INDEX = "REMOTOCON_SELECTED_SERVER_INDEX";
	
	public static void setSelectedServerIndex(Context context, int index)
	{
		SharedPreferences mainPrefs = context.getSharedPreferences(Preferences.PREF_MAIN, Context.MODE_PRIVATE);
		SharedPreferences.Editor mainEditor =  mainPrefs.edit();
		mainEditor.putInt(Preferences.KEY_SELECTED_SERVER_INDEX, index);
		mainEditor.commit();
	}
	
	public static int getSelectedServerIndex(Context context)
	{
		SharedPreferences mainPrefs = context.getSharedPreferences(Preferences.PREF_MAIN, Context.MODE_PRIVATE);
		return mainPrefs.getInt(Preferences.KEY_SELECTED_SERVER_INDEX, -1);
	}
	
	public static ArrayList<XmlRpcClient> getXmlRpcClients(Context ctxt) {
    	ArrayList<XmlRpcClient> clients = new ArrayList<XmlRpcClient>(); 

    	boolean foundPref = true;
    	for(int i = 0; foundPref; i++)
    	{
    		SharedPreferences serverPrefs = ctxt.getSharedPreferences(ServerPreferences.PREF_SERVER + i, Context.MODE_PRIVATE);    		
    		String sName = serverPrefs.getString(ServerPreferences.KEY_SERVER_NAME, "");    		
    		foundPref = sName == "" ? false : clients.add(new XmlRpcClient(serverPrefs));	
    	}
    	
		return clients;
	}
}
