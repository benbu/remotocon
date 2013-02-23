package com.remotocon.plugins;

import com.remotocon.xmlrpc.XmlRpcClient;

import android.content.Context;
import android.content.Intent;

public class BuiltInPlugin extends Plugin {
	private Class<?> cls;
	
	public BuiltInPlugin(String Name, String Description, String Author, String Version, String MobilePluginName, Class<?> cls) {
		super(Name, Version);

		this.cls = cls;
	}

	@Override
	public void launchPlugin(Context context, XmlRpcClient client) {
		context.startActivity(new Intent(context, cls));
	}
}
