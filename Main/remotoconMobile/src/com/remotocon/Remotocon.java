package com.remotocon;

import java.util.ArrayList;

import com.remotocon.globals.Globals;
import com.remotocon.plugins.PluginManager;
import com.remotocon.preferences.*;
import com.remotocon.xmlrpc.XmlRpcClient;

import android.app.*;
import android.content.*;
import android.graphics.Color;
import android.net.Uri;
import android.os.*;
import android.view.*;
import android.view.View.OnClickListener;
import android.widget.*;
import android.widget.AdapterView.*;

// TODO Figure out how to manage plugins and version inconsistencies.
	//plugin developer specifies plugin .dll url in meta-data
	//launch market intent for a specific app or for remotocon plugins. might be able to use stuff related to the intent category

// TODO -Add some sort of menu when the menu button is clicked.
// Ideas
	// Use better icons for these.
	//  -Manage computers
	//  -Settings
	//  - Refresh plugins
	//  -Connection timeout
	//  -Search for more plugins
	//  -look at other apps for more ideas

// TODO Make a custom layout for the client spinner items so they look better

// TODO TEST APP

// TODO -go over application and think of what still needs/could be done

public class Remotocon extends Activity {
	
	//private Spinner clientSpinner;
	private PluginManager pluginManager;
	private ArrayList<XmlRpcClient> clientArray;
	private int clientIndex = -1;
	private TextView statusBar;
	
    /** Called when the activity is first created. */
    @Override
    public void onCreate(Bundle savedInstanceState) {
    	Eula.show(this);    	
        super.onCreate(savedInstanceState);
        
        pluginManager = new PluginManager(this);

        setContentView(R.layout.main);        

        final Spinner clientSpinner = (Spinner) findViewById(R.id.ClientSpinner);    	
        clientSpinner.setOnItemSelectedListener(new OnClientSelectedListener());
        
        statusBar = (TextView) findViewById(R.id.TextViewStatus);
        
        ImageButton addButton = (ImageButton) findViewById(R.id.RefreshPluginsButton);
        addButton.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View v) {
				refreshPluginList();
			}
        	
        });        

        ListView lv = (ListView)findViewById(R.id.ListViewPlugins);	         
        lv.setOnItemClickListener(new OnPluginClickedListener());
    }

	private void refreshPluginList() {		
    	
		statusBar.setText("");
		statusBar.setBackgroundColor(Color.YELLOW);
		
    	final XmlRpcClient client = clientArray.get(clientIndex);   	
    	pluginManager.clear();

        final ListView lv = (ListView)findViewById(R.id.ListViewPlugins);
        lv.setAdapter(new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, new String[]{}));
    	
    	if(client != null)
    	{	        
    		final ArrayList<Object> plugins = new ArrayList<Object>();
    		final ProgressDialog dlg = new ProgressDialog(this);
    		dlg.setTitle("Loading Plugins");
    		dlg.setMessage("Please Wait...");
    		dlg.setIndeterminate(true);
    		dlg.setCancelable(true);
    		
			final Handler handler = new Handler() {
			   public void handleMessage(Message msg) {		
			        dlg.dismiss();	        
			        if(msg.what == 0)
			        {			        
				        try
				        {
				        	pluginManager.buildPluginList(plugins);
				        }
				        catch(Exception e)
				        {
				        	Toast.makeText(Remotocon.this, "Error Building Plugin List: " + e.toString(), Toast.LENGTH_LONG).show();
				        	statusBar.setText("Unable to get plugin list.");
				        	statusBar.setBackgroundColor(Color.RED);
				        }	        
				        
				        String[] pluginNameArray = pluginManager.getPluginNameArray();
				        int pluginCount = pluginNameArray.length;
				               
				        lv.setAdapter(new ArrayAdapter<String>(Remotocon.this, android.R.layout.simple_list_item_1, pluginNameArray));
				        
				        if(pluginCount > 0)
				        {
				        	statusBar.setText("Successfully loaded " + pluginCount + " plugin" + (pluginCount > 1 ? "s." : "."));
				        	statusBar.setBackgroundColor(Color.GREEN);
				        }
				        else
				        {
				        	statusBar.setText("No plugins found.");
				        	statusBar.setBackgroundColor(Color.GREEN);
				        }
			        }
			        else
			        {
			        	Toast.makeText(Remotocon.this, "Error connecting to computer.", Toast.LENGTH_LONG).show();
			        	statusBar.setText("Unable to connect to computer.");
			        	statusBar.setBackgroundColor(Color.RED);
			        }
			      }
			};
			
			final Thread checkUpdate = new Thread() {
				public void run()
				{
					int result = 0;
					try {
						getServerPlugins(client, plugins);
					} catch (Exception e) {
						result = 1;
					}
					
					handler.sendEmptyMessage(result);
				}
			};			

    		dlg.setButton(DialogInterface.BUTTON_NEGATIVE, "Cancel", new DialogInterface.OnClickListener(){
    			
				@Override
				public void onClick(DialogInterface dialog, int which) {
					dlg.dismiss();
					if(checkUpdate.isAlive())
						checkUpdate.stop();
					Toast.makeText(Remotocon.this, "Canceled loading plugins", Toast.LENGTH_LONG).show();
				}
    		});
			
			dlg.show();
			checkUpdate.start();
    	}
    	else // client is null
    	{
    		statusBar.setText("No computer to connect to");
    		statusBar.setBackgroundColor(Color.RED);
    	}
	}

	private void getServerPlugins(XmlRpcClient client, ArrayList<Object> plugins) throws Exception {
		ArrayList<Object> temp;

		temp = client.getServerPlugins();
		
		for(Object o : temp)
			plugins.add(o);
	}
    
    private boolean loadXmlRpcClients() {
    	
    	clientArray = Preferences.getXmlRpcClients(this);
    	//String[] clientNames = getClientNameArray(clientArray);
    	
    	Spinner clientSpinner = (Spinner)findViewById(R.id.ClientSpinner);
    	XmlRpcClientAdapter mAdapter = new XmlRpcClientAdapter(this, R.layout.xmlrpc_client, clientArray);
    	mAdapter.setDropDownViewResource(R.layout.xmlrpc_client_spinner_item);
    	//clientSpinner.setAdapter(new XmlRpcClientAdapter(this, R.layout.xmlrpc_client, clientArray));
    	clientSpinner.setAdapter(mAdapter);
    	
    	if(clientArray.isEmpty()) //no preferences have been set
    	{
    		clientIndex = -1;
    		Preferences.setSelectedServerIndex(this, -1);
    		return false;
    	}
    	else
    	{          
    		clientIndex = Preferences.getSelectedServerIndex(this);
    		if(clientIndex >= clientArray.size())
    		{
        		clientIndex = -1;
        		Preferences.setSelectedServerIndex(this, -1);
    		}
    		else
    			clientSpinner.setSelection(clientIndex);
    		return true;
    	}
	}

//	private String[] getClientNameArray(ArrayList<XmlRpcClient> clientArray) {
//		String[] result = new String[clientArray.size()];
//		for(int i = 0; i < clientArray.size(); i++)
//			result[i] = clientArray.get(i).serverName;
//		return result;
//	}

	@Override
    public void onResume()
    {
    	super.onResume();

        boolean hasClients = true;
        
        if(Globals.reloadClients)
        {
        	hasClients = loadXmlRpcClients();
        	Globals.reloadClients = false;
        }
        
        if(!hasClients)
        {
        	AlertDialog.Builder builder = new AlertDialog.Builder(Remotocon.this);
        	builder.setMessage("You have not added any computers, would you like to add one now?")
        	       .setCancelable(false)
        	       .setPositiveButton("Yes", new DialogInterface.OnClickListener() {
        	           public void onClick(DialogInterface dialog, int id) {
        	        	   Preferences.setSelectedServerIndex(Remotocon.this, 0);
        	        	   startActivity(new Intent(Remotocon.this, ServerPreferences.class));
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
    }
	
	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
	    MenuInflater inflater = getMenuInflater();
	    inflater.inflate(R.menu.main_menu, menu);
	    return true;
	}
	
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
	    // Handle item selection
	    switch (item.getItemId()) {
	    case R.id.menu_manage_computers:
	    	startActivity(new Intent(Remotocon.this, ClientManager.class));
	        return true;
	    case R.id.menu_find_plugins:
	        startActivity(new Intent(Intent.ACTION_VIEW, Uri.parse("market://search?q=Remotocon%20plugin")));
	        return true;
	    default:
	        return super.onOptionsItemSelected(item);
	    }
	}
    
    private class OnClientSelectedListener implements OnItemSelectedListener
    {
		@Override
		public void onItemSelected(AdapterView<?> adapterView, View view, int position, long id) {
			clientIndex = position;
			Preferences.setSelectedServerIndex(Remotocon.this, position);
			refreshPluginList();
		}

		@Override
		public void onNothingSelected(AdapterView<?> arg0) { }	
    }
	
	private class OnPluginClickedListener implements OnItemClickListener
	{
		@Override
		public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
			pluginManager.plugins.get(position).launchPlugin(Remotocon.this, clientArray.get(clientIndex));			
		}		
	}

	private class XmlRpcClientAdapter extends ArrayAdapter<XmlRpcClient> implements SpinnerAdapter{

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
	                    	if(client.serverName.length() > 8)
	                    		tv.setText(client.serverName.substring(0, 8));
	                    	else
	                    		tv.setText(client.serverName);
	                    }
	            }
	            return v;
	    }
	}
}