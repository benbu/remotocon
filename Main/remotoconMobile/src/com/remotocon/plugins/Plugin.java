package com.remotocon.plugins;

import com.remotocon.xmlrpc.XmlRpcClient;

import android.content.Context;

public abstract class Plugin {
    String Name;
    String Version;
    
    Boolean NoServerPluginFound = false;
    Boolean NoMobilePluginFound = false;
    
    public Plugin( String Name, String Version)
    {
        this.Name = Name;
        this.Version = Version;
    }
    
    public abstract void launchPlugin(Context context, XmlRpcClient client);
}
