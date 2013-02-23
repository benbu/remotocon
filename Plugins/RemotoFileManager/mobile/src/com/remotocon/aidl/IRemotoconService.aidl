package com.remotocon.aidl;

interface IRemotoconService {
	byte[] remoteProcedureCall(in String objectName, in String methodName, in byte[] objectArray); 
	boolean checkServerPluginExists(in String packageName, in String version);
	boolean installServerPlugin(in String dllURL, in String dllName);
}