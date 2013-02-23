package com.remotocon.plugins.FileManager;

import java.util.ArrayList;

import com.remotocon.R;
import com.remotocon.preferences.Preferences;
import com.remotocon.preferences.ServerPreferences;
import com.remotocon.xmlrpc.XmlRpcClient;

import android.app.Activity;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ListView;

public class FileManagerMobilePlugin extends Activity {
	
	private XmlRpcClient client;
	private FileSystemEntry currentDir;
	
	public void onCreate(Bundle savedState)
	{
		super.onCreate(savedState);
		
        SharedPreferences mainPreferences = getSharedPreferences(Preferences.PREF_MAIN, MODE_PRIVATE);
        int selectedServerIndex = mainPreferences.getInt(Preferences.KEY_SELECTED_SERVER_INDEX, -1);
            
        if(selectedServerIndex > -1)
        {
	        SharedPreferences serverPrefs = getSharedPreferences(ServerPreferences.PREF_SERVER + selectedServerIndex, MODE_PRIVATE);
		    this.client = new XmlRpcClient(serverPrefs);
		    this.currentDir = new FileSystemEntry("", null, true);
        }        

		setContentView(R.layout.file_manager);		

		ListView lv = (ListView) findViewById(R.id.MainList);
		
		lv.setTextFilterEnabled(true);
		
		lv.setOnItemClickListener(new OnItemClickListener() {
		    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
		    	
		    	FileSystemEntryAdapter adapter = (FileSystemEntryAdapter)parent.getAdapter(); 
		    	FileSystemEntry fse = adapter.items.get(position);
		    	if(fse.isDirectory)
		    	{
		    		currentDir = fse.name == ".." ? currentDir.parentDir : fse;
		    		refreshList();
		    	}
		    }
		});
	}
	
	public void onResume()
	{
		super.onResume();
		refreshList();
	}
	
	protected void refreshList()
	{
		ListView lv = (ListView) findViewById(R.id.MainList);
		
		ArrayList<FileSystemEntry> files;
		
		try
		{
			files = this.getFileList();
			lv.setAdapter(new FileSystemEntryAdapter(this, android.R.layout.simple_list_item_1 ,files));
		}
		catch (Exception e)
		{
			currentDir = currentDir.parentDir;	
		}		
	}
	
	@SuppressWarnings("unchecked")
	private ArrayList<FileSystemEntry> getFileList() throws Exception
	{
		ArrayList<Object> filesAL;
		
		filesAL = (ArrayList<Object>)client.CallRemoteMethod("FileManagerHandler.ListDirectory", currentDir.getFullPath());
		
		ArrayList<FileSystemEntry> files = new ArrayList<FileSystemEntry>();
		
		if(currentDir.parentDir != null) // not root directory
			files.add(new FileSystemEntry("..", null, true));
		
		for(Object o : filesAL)
		{
			ArrayList<Object> fse = (ArrayList<Object>)o;
			files.add(new FileSystemEntry((String)fse.get(1), currentDir, (Boolean)fse.get(0)));
		}
		
		return files;
	}
}
