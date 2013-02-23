package com.remotocon.plugins;

import com.remotocon.xmlrpc.XmlRpcClient;

import android.app.AlertDialog;
import android.content.ComponentName;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.ResolveInfo;
import android.net.Uri;
import android.widget.Toast;

public class ThirdPartyPlugin extends Plugin {
	public ResolveInfo rInfo;
	public String packageName;
	
	public ThirdPartyPlugin(String Name, String Version, String packageName, ResolveInfo info) {
		super(Name, Version);
		
		this.packageName = packageName;

		if(info == null)
			NoMobilePluginFound = true;
		else
			rInfo = info;		
	}

	public ThirdPartyPlugin(CharSequence Name, String Version, String packageName, ResolveInfo info) {
		this(Name.toString(), Version, packageName, info);
	}

	@Override
	public void launchPlugin(final Context context, XmlRpcClient xmlClient) {
		final Context c = context;
		final XmlRpcClient client = xmlClient;
		if(NoMobilePluginFound)
		{
        	AlertDialog.Builder builder = new AlertDialog.Builder(c);
        	builder.setMessage("You have not installed the android app for this plugin. "
        			+ "Install plugin app now?")
        	       .setCancelable(false)
        	       .setPositiveButton("Yes", new DialogInterface.OnClickListener() {
        	           public void onClick(DialogInterface dialog, int id) {
        	        	   Intent goToMarket = null;
        	        	   goToMarket = new Intent(Intent.ACTION_VIEW,Uri.parse("market://details?id=" + packageName));
        	        	   c.startActivity(goToMarket);
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
		else if (NoServerPluginFound)
		{        	
			AlertDialog.Builder builder = new AlertDialog.Builder(c);
			builder.setMessage("Computer plugin not found. Would you like to download and"
					+ " install the plugin on your computer?")
    	       .setCancelable(false)
    	       .setPositiveButton("Yes", new DialogInterface.OnClickListener() {
    	           public void onClick(DialogInterface dialog, int id) {
    	        	   String dll = rInfo.activityInfo.metaData.getString("ServerDll");
    	        	   String url = rInfo.activityInfo.metaData.getString("ServerDllUrl");
    	        	   try {
    	        		   if(!(Boolean)client.CallRemoteMethod("Server.DownloadInstallPlugin", url))
    	        			   throw new Exception();
    	        	   } catch (Exception e) {
    	        		   Toast.makeText(c, "Error installing plugin on computer.", Toast.LENGTH_LONG).show();
    	        		   e.printStackTrace();
    	        	   }
    	           }
    	       })
    	       .setNegativeButton("No", new DialogInterface.OnClickListener() {
    	           public void onClick(DialogInterface dialog, int id) {
    	                dialog.cancel();
    	           }
    	       });
	    	AlertDialog alert = builder.create();
	    	alert.show();
			//TODO show no server plugin found dialog
			// only one version of a server plugin may exist in the Plugins folder.
		}
		else
		{
			Intent intent = new Intent(Intent.ACTION_MAIN);
			intent.setComponent(new ComponentName(rInfo.activityInfo.packageName, rInfo.activityInfo.name));
			c.startActivity(intent);
		}
	}
	
}
