package com.remotocon.preferences;

import java.util.ArrayList;

import com.remotocon.R;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.ListView;

public class DebugListServerPreferences extends Activity {

	@Override
	public void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.debug_list_servers);
		
		ArrayList<String> clients = new ArrayList<String>();
		
		String temp;
    	for(int i = 0; i < 10; i++)
    	{
    		temp = new String(i + "_");
    		SharedPreferences serverPrefs = getSharedPreferences(ServerPreferences.PREF_SERVER + i, Context.MODE_PRIVATE);    		
    		temp += serverPrefs.getString(ServerPreferences.KEY_SERVER_NAME, "") + "_";
    		temp += serverPrefs.getString(ServerPreferences.KEY_SERVER_ADDRESS, "") + "_";   		
    		temp += Integer.toString(serverPrefs.getInt(ServerPreferences.KEY_SERVER_PORT, 0)) + "_";
    		temp += serverPrefs.getString(ServerPreferences.KEY_SERVER_USERNAME, "") + "_";   		
    		temp += serverPrefs.getString(ServerPreferences.KEY_SERVER_PASSWORD, "") + "_";
    		clients.add(temp);
    	}
    	
    	ListView lv = (ListView) findViewById(R.id.DebugServerListView);
    	lv.setAdapter(new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, clients));
	}
}
