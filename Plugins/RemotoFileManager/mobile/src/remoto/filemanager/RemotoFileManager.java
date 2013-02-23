package remoto.filemanager;

import java.util.ArrayList;

import android.R.color;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.ComponentName;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.Handler;
import android.os.IBinder;
import android.os.Message;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemLongClickListener;
import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;

import com.remotocon.aidl.IRemotoconService;

/**
 * @author Ben
 *
 */
public class RemotoFileManager extends Activity {
	
	private FileSystemEntry currentDir;
	private IRemotoconService remotoconService = null;
	private ListView FileListView;
	
	private boolean connectedService;
	private boolean connectedComputer;
	private boolean serverPluginExists;
	
	private final String dllURL = "http://www.remotocon.com/serverplugins/FileManagerServerPlugin.dll";
	private final String dllName = "FileManagerServerPlugin.dll";
	
	private final String pkgName = "remoto.filemanager";
	private final String version = "1.0";
	
	private ProgressDialog progressDialog;
	
	private static final String ServiceConnErrorMsg = "Unable to communicate with remotocon. Make sure it is installed and configured properly.";

	private final CharSequence[] fileFunctions = {"Cut", "Copy", "Delete", "Rename", "Download", "Properties"};
	private final CharSequence[] folderFunctions = {"Open", "Cut", "Copy", "Delete", "Rename", "Download", "Properties"};
	
	private AlertDialog fileActions;
	private AlertDialog folderActions;
	private AlertDialog getPro;
	
	private int selectedFSEIndex;

    @Override
	public void onCreate(Bundle savedState)
	{
		super.onCreate(savedState);
		
		progressDialog = new ProgressDialog(this);

		setContentView(R.layout.main);
		
		LinearLayout myScreen = (LinearLayout) findViewById(R.id.MainScreen);
		myScreen.setBackgroundColor(color.white);

		FileListView = (ListView) findViewById(R.id.MainList);
		
		FileListView.setTextFilterEnabled(true);
		
		FileListView.setOnItemClickListener(new OnItemClickListener() {
		    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
		    	
		    	FileSystemEntryAdapter adapter = (FileSystemEntryAdapter)parent.getAdapter(); 
		    	FileSystemEntry fse = adapter.items.get(position);
		    	if(fse.isDirectory)
		    		openDirectory(fse);
		    }
		});
		
		AlertDialog.Builder builder = new AlertDialog.Builder(this);
		builder.setMessage("This action is only available on RemotoFileManager Pro (Coming Soon)") //. \n\n Would you like to go to the market to download now?")
		       .setCancelable(false)
//		       .setPositiveButton("Yes", new DialogInterface.OnClickListener() {
//		           public void onClick(DialogInterface dialog, int id) {
//		                RemotoFileManager.this.finish();
//		                startActivity(new Intent(Intent.ACTION_VIEW, Uri.parse("market://search?q=pname:remoto.filemanagerpro")));
//		           }
//		       })
		       .setNegativeButton("Close", new DialogInterface.OnClickListener() {
		           public void onClick(DialogInterface dialog, int id) {
		                dialog.cancel();
		           }
		       });
		getPro = builder.create();
		
		builder = new AlertDialog.Builder(this);
		builder.setTitle("Select Action");
		builder.setItems(fileFunctions, new DialogInterface.OnClickListener() {
		    public void onClick(DialogInterface dialog, int item) {
		    	
		    	switch(item)
		    	{
			    	case 5: // properties
			    		//TODO launch properties window
			    		break;
		    		default:
				        getPro.show();		    			
		    	}
		    }
		});
		fileActions = builder.create();
		
		builder.setItems(folderFunctions, new DialogInterface.OnClickListener() {
		    public void onClick(DialogInterface dialog, int item) {
		    	switch(item)
		    	{
			    	case 0: //open
			    		FileSystemEntry fse = currentDir.contents.get(selectedFSEIndex);
			    		openDirectory(fse);
			    		break;
			    	case 6: // properties
			    		//TODO launch properties window
			    		break;
		    		default:
				        getPro.show();		    			
		    	}
		    }
		});
		folderActions = builder.create();
		
		FileListView.setOnItemLongClickListener(new OnItemLongClickListener() {

			@Override
			public boolean onItemLongClick(AdapterView<?> parent, View view, int position, long id) {
				selectedFSEIndex = position;
				if(currentDir.contents.get(position).isDirectory)
					folderActions.show();
				else
					fileActions.show();
				return true;
			}
		});
		
		progressDialog.setTitle("Initializing");
		progressDialog.setMessage("Connecting to Remotocon...");
		progressDialog.setIndeterminate(true);
		
		final Handler handler = new Handler() {
			   public void handleMessage(Message msg) {
			        if(msg.what == 1)
			        {
						progressDialog.dismiss();
						Toast.makeText(RemotoFileManager.this, ServiceConnErrorMsg, 0).show();
			        }
			   }
		};
		
		final Thread connectRemotoconService = new Thread() {
			public void run()
			{
				int result = 0;
				try {
					connectToRemotoconService();
				} catch (Exception e) {
					result = 1;
				}
				
				handler.sendEmptyMessage(result);
			}
		};
		
		progressDialog.show();
		connectRemotoconService.start();
	}
	
	protected void connectToRemotoconService() throws Exception {
		boolean result = bindService(new Intent("com.remotocon.aidl.IRemotoconService"),
                remotoconServiceConnection, Context.BIND_AUTO_CREATE);
		if(!result)
			throw new Exception();
	}
    
    private ServiceConnection remotoconServiceConnection = new ServiceConnection() {

		@Override
		public void onServiceConnected(ComponentName className, IBinder service) {
			remotoconService = IRemotoconService.Stub.asInterface(service);
			if(remotoconService != null)
			{
				connectedService = true;
				
				progressDialog.setMessage("Connecting to computer...");
				checkForServerPlugin();
				
				if(connectedComputer)
				{				
					if(!serverPluginExists)
					{
						progressDialog.setMessage("Installing plugin on computer...\n\nThis may take a few minutes. This should only occur when using the plugin for the first time, and after updates.");
						installServerPlugin();
					}					
					
					progressDialog.setMessage("Fetching file list...");
					openDirectory(FileSystemEntry.root);
				}
				
				progressDialog.dismiss();
			}
		}

		@Override
		public void onServiceDisconnected(ComponentName name) {
			remotoconService = null;
			connectedService = false;
		}
    	
    };
	
	public void onResume()
	{
		super.onResume();
		if(currentDir != null)
			openCurrentDirectory();
	}
	
	private boolean openCurrentDirectory()
	{
		if(currentDir.contents == null)
			refreshDirContents();
		
		//still may be null if there was an error fetching file list from users computer
		if(currentDir.contents != null)
			FileListView.setAdapter(new FileSystemEntryAdapter(this, android.R.layout.simple_list_item_1 , currentDir.contents));
		else
			return false;
		return true;
	}
    
    private void openDirectory(FileSystemEntry fse)
    {
    	if(fse.name.equals(".."))
    		currentDir = currentDir.parentDir;
    	else
    		currentDir = fse;
		
		if(!openCurrentDirectory())
			currentDir = currentDir.parentDir;
    }

	protected void refreshDirContents()
	{
		if(connectedService && connectedComputer && serverPluginExists)
		{			
			try
			{
				currentDir.contents = this.getFileList();
			}
			catch (Exception e)
			{
				Toast.makeText(RemotoFileManager.this, "Error opening directory.", Toast.LENGTH_LONG);
			}
		}
	}
	
	
	/**
	 * Checks to see if the server side plugin exists on the users computer. 
	 * Also sets the <b>connectedComputer</b> boolean variable to true or false depending on if there was a connection error or not.
	 */
	private void checkForServerPlugin() 
	{
		if(connectedService)
		{
			try
			{
				serverPluginExists = remotoconService.checkServerPluginExists(pkgName, version);
				connectedComputer = true;
			}
			catch (Exception e)
			{
				//TODO show connection error toast, suggest launching remotocon app and make sure they are able to connect to their computer from there
				serverPluginExists = false;
				connectedComputer = false;
			}				
		}
	}
	
	private void installServerPlugin()
	{
		if(connectedService & connectedComputer)
		{
	 	   try
		   {
			   if(!remotoconService.installServerPlugin(dllURL, dllName))
				   Toast.makeText(RemotoFileManager.this, "Failed to install plugin on computer. Try manually installing on PC.", Toast.LENGTH_LONG).show();
			   else
				   serverPluginExists = true;		   
		   }
		   catch(Exception e)
		   {
			   Toast.makeText(RemotoFileManager.this, "Error installing plugin.", Toast.LENGTH_LONG);
		   }
		}
	}

	@SuppressWarnings("unchecked")
	private ArrayList<FileSystemEntry> getFileList() throws Exception
	{
		ArrayList<Object> filesAL;		
		Object[] params = new Object[] { currentDir.getFullPath() };		
		byte[] paramsBytes = RemotoconServiceHelper.paramsToBytes(params);
		byte[] result = remotoconService.remoteProcedureCall("FileManagerHandler", "ListDirectory", paramsBytes);
		filesAL = (ArrayList<Object>)RemotoconServiceHelper.bytesToObject(result);
		
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