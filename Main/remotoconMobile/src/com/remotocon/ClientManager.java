package com.remotocon;

import java.util.ArrayList;

import com.remotocon.globals.Globals;
import com.remotocon.preferences.DebugListServerPreferences;
import com.remotocon.preferences.Preferences;
import com.remotocon.preferences.ServerPreferences;
import com.remotocon.xmlrpc.XmlRpcClient;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;

public class ClientManager extends Activity {
	
	ArrayList<XmlRpcClient> clientArray;
	
	@Override
	public void onCreate(Bundle savedBundleInstance)
	{
		super.onCreate(savedBundleInstance);
		setContentView(R.layout.client_manager);
	}	

	@Override
	public void onResume()
	{
		super.onResume();
		
		clientArray = Preferences.getXmlRpcClients(this);
		
		Button button = (Button) findViewById(R.id.AddClientButton);
		button.setOnClickListener(new OnClickListener(){

			public void onClick(View v) {
				Preferences.setSelectedServerIndex(ClientManager.this, clientArray.size());
    			startActivity(new Intent(ClientManager.this, ServerPreferences.class));				
			}
			
		});
		
		ListView lv = (ListView) findViewById(R.id.ClientManagerListView);
		lv.setAdapter(new XmlRpcClientAdapter(this, R.layout.xmlrpc_client, clientArray));
		lv.setOnItemClickListener(clientOnClickListener);
	}
	
	OnItemClickListener clientOnClickListener = new OnItemClickListener(){

		public void onItemClick(AdapterView<?> parent, View view, int position, long id) {			
			promptEditAction(position);			
		}
	};

	protected void promptEditAction(int clientIndex) {
		final int pos = clientIndex;		
		final CharSequence[] items = {"Edit", "Delete"};

		AlertDialog.Builder builder = new AlertDialog.Builder(this);
		builder.setItems(items, new DialogInterface.OnClickListener() {
		    public void onClick(DialogInterface dialog, int item) {
		    	switch(item)
		    	{
		    		case 0: // edit
		    			Preferences.setSelectedServerIndex(ClientManager.this, pos);
		    			startActivity(new Intent(ClientManager.this, ServerPreferences.class));
		    			break;
		    		case 1: // delete
		    			promptForDelete(pos);
		    			break;
		    	}
		    }
		});
		AlertDialog alert = builder.create();
		alert.show();
	}

	protected void promptForDelete(int clientIndex) {
		
		final int pos = clientIndex;
		
		AlertDialog.Builder builder = new AlertDialog.Builder(this);
		builder.setMessage("Are you sure you want to delete these computer settings?")
		       .setCancelable(false)
		       .setPositiveButton("Delete", new DialogInterface.OnClickListener() {
		           public void onClick(DialogInterface dialog, int id) {
		                delete(pos);
		                ClientManager.this.onResume();
		           }
		       })
		       .setNegativeButton("No", new DialogInterface.OnClickListener() {
		           public void onClick(DialogInterface dialog, int id) {
		                dialog.cancel();
		           }
		       });
		AlertDialog alert = builder.create();
		alert.show();
	}

	protected void delete(int clientIndex) {
		SharedPreferences cur = getSharedPreferences(ServerPreferences.PREF_SERVER + clientIndex, MODE_PRIVATE);
		SharedPreferences next = getSharedPreferences(ServerPreferences.PREF_SERVER + (clientIndex + 1), MODE_PRIVATE);
		
		if(next.getString(ServerPreferences.KEY_SERVER_NAME, "") == "")
		{
			cur.edit().clear().commit();
			Globals.reloadClients = true;
		}
		else
		{
			Editor e = cur.edit();
			e.putString(ServerPreferences.KEY_SERVER_NAME, next.getString(ServerPreferences.KEY_SERVER_NAME, "Computer" + Integer.toString(clientIndex)));
			e.putString(ServerPreferences.KEY_SERVER_ADDRESS, next.getString(ServerPreferences.KEY_SERVER_ADDRESS, ""));
			e.putInt(ServerPreferences.KEY_SERVER_PORT, next.getInt(ServerPreferences.KEY_SERVER_PORT, 4646));
			e.putString(ServerPreferences.KEY_SERVER_USERNAME, next.getString(ServerPreferences.KEY_SERVER_USERNAME, ""));
			e.putString(ServerPreferences.KEY_SERVER_PASSWORD, next.getString(ServerPreferences.KEY_SERVER_PASSWORD, ""));
			e.commit();
			
			delete(clientIndex + 1);			
		}
	}

	private class XmlRpcClientAdapter extends ArrayAdapter<XmlRpcClient>{

	    public ArrayList<XmlRpcClient> items;
	    private LayoutInflater mInflater;

	    public XmlRpcClientAdapter(
				Context context, 
				int textViewResourceId, 
				ArrayList<XmlRpcClient> items) {
	        super(context, textViewResourceId, items);
	        this.items = items;
	        mInflater = LayoutInflater.from(context);
	    }

	    @Override
	    public View getView(int position, View convertView, ViewGroup parent) {
	            View v = convertView;
	            if (v == null) {
	                v = mInflater.inflate(R.layout.xmlrpc_client, null);
	            }
	            XmlRpcClient client = items.get(position);
	            if (client != null) {
	                    TextView tv = (TextView) v.findViewById(R.id.XmlRpcName);
	                    if (tv != null) {
	                    	tv.setText(client.serverName);
	                    }
	            }
	            return v;
	    }
	}
	
	//TODO REMOVE THIS METHOD BEFORE RELEASE, AND THE ASSOCIATED CLASS.
	@Override
	public boolean onCreateOptionsMenu(Menu menu)
	{
		startActivity(new Intent(this, DebugListServerPreferences.class));
		return false;		
	}
}
