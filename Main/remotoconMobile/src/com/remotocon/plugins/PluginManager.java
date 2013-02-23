package com.remotocon.plugins;

import java.util.ArrayList;
import java.util.Hashtable;
import java.util.List;

import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.content.pm.PackageManager.NameNotFoundException;

public class PluginManager 
{
	List<ResolveInfo> mobilePluginList = new ArrayList<ResolveInfo>();
	public List<Plugin> plugins = new ArrayList<Plugin>();
	private PackageManager packageManager;
	
	public PluginManager(Context context)
	{
		final Intent mainIntent = new Intent(Intent.ACTION_MAIN, null);
		mainIntent.addCategory("com.remotocon.category.PLUGIN");
		packageManager = context.getPackageManager();
		mobilePluginList = packageManager.queryIntentActivities(mainIntent, 
				PackageManager.GET_META_DATA);
	}
	
	@SuppressWarnings({ "rawtypes" })
	public void buildPluginList(ArrayList<Object> serverPlugins) throws Exception
	{
		//add server plugins
		for(Object o : serverPlugins)
		{
			if(o instanceof Hashtable)
			{
				String packageName = (String)((Hashtable)o).get("AndroidPackageName");
				String version = (String)((Hashtable)o).get("Version");
				String name = (String)((Hashtable)o).get("Name");

				ResolveInfo info = null;
				for(ResolveInfo inf : mobilePluginList)
					if(inf.activityInfo.packageName.equalsIgnoreCase(packageName))
					{
						info = inf;
						break;
					}
				
				plugins.add(new ThirdPartyPlugin(name, version, packageName, info));
			}
		}
		
		//add mobile plugins that didn't get linked to a server plugin
		for(ResolveInfo info : mobilePluginList)
		{
			boolean found = false;
			for(Plugin p : plugins)
				if(p instanceof ThirdPartyPlugin)
					if(info.activityInfo.packageName.equals(((ThirdPartyPlugin)p).packageName))
					{
						found = true;
						break;
					}
			
			if(!found)
			{
				try
				{
					PackageInfo pi = packageManager.getPackageInfo( info.activityInfo.packageName, 0);
					ThirdPartyPlugin tpp = new ThirdPartyPlugin(info.loadLabel(packageManager), pi.versionName, pi.packageName, info);
					tpp.NoServerPluginFound = true;
					plugins.add(tpp);
				}
				catch(NameNotFoundException nnfe)
				{
					throw new Exception("Error loading plugin " + info.activityInfo.applicationInfo.name + "\n\n" 
							+ info.activityInfo.packageName);
				}
				catch(Exception e)
				{
					throw e;
				}
			}
		}
	}
	
	public String[] getPluginNameArray()
	{
		int size = plugins.size();
		
		String[] names = new String[size];

		for(int i = 0; i < size; i++)
			names[i] = plugins.get(i).Name;
		
		return names;
	}

	public void clear() {
		plugins.clear();
	}
}
