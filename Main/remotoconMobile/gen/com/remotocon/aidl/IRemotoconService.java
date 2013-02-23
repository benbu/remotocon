/*
 * This file is auto-generated.  DO NOT MODIFY.
 * Original file: C:\\svn\\beanstalk\\remotocon\\trunk\\Main\\remotoconMobile\\src\\com\\remotocon\\aidl\\IRemotoconService.aidl
 */
package com.remotocon.aidl;
import java.lang.String;
import android.os.RemoteException;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Binder;
import android.os.Parcel;
public interface IRemotoconService extends android.os.IInterface
{
/** Local-side IPC implementation stub class. */
public static abstract class Stub extends android.os.Binder implements com.remotocon.aidl.IRemotoconService
{
private static final java.lang.String DESCRIPTOR = "com.remotocon.aidl.IRemotoconService";
/** Construct the stub at attach it to the interface. */
public Stub()
{
this.attachInterface(this, DESCRIPTOR);
}
/**
 * Cast an IBinder object into an IRemotoconService interface,
 * generating a proxy if needed.
 */
public static com.remotocon.aidl.IRemotoconService asInterface(android.os.IBinder obj)
{
if ((obj==null)) {
return null;
}
android.os.IInterface iin = (android.os.IInterface)obj.queryLocalInterface(DESCRIPTOR);
if (((iin!=null)&&(iin instanceof com.remotocon.aidl.IRemotoconService))) {
return ((com.remotocon.aidl.IRemotoconService)iin);
}
return new com.remotocon.aidl.IRemotoconService.Stub.Proxy(obj);
}
public android.os.IBinder asBinder()
{
return this;
}
public boolean onTransact(int code, android.os.Parcel data, android.os.Parcel reply, int flags) throws android.os.RemoteException
{
switch (code)
{
case INTERFACE_TRANSACTION:
{
reply.writeString(DESCRIPTOR);
return true;
}
case TRANSACTION_remoteProcedureCall:
{
data.enforceInterface(DESCRIPTOR);
java.lang.String _arg0;
_arg0 = data.readString();
java.lang.String _arg1;
_arg1 = data.readString();
byte[] _arg2;
_arg2 = data.createByteArray();
byte[] _result = this.remoteProcedureCall(_arg0, _arg1, _arg2);
reply.writeNoException();
reply.writeByteArray(_result);
return true;
}
case TRANSACTION_checkServerPluginExists:
{
data.enforceInterface(DESCRIPTOR);
java.lang.String _arg0;
_arg0 = data.readString();
java.lang.String _arg1;
_arg1 = data.readString();
boolean _result = this.checkServerPluginExists(_arg0, _arg1);
reply.writeNoException();
reply.writeInt(((_result)?(1):(0)));
return true;
}
case TRANSACTION_installServerPlugin:
{
data.enforceInterface(DESCRIPTOR);
java.lang.String _arg0;
_arg0 = data.readString();
java.lang.String _arg1;
_arg1 = data.readString();
boolean _result = this.installServerPlugin(_arg0, _arg1);
reply.writeNoException();
reply.writeInt(((_result)?(1):(0)));
return true;
}
}
return super.onTransact(code, data, reply, flags);
}
private static class Proxy implements com.remotocon.aidl.IRemotoconService
{
private android.os.IBinder mRemote;
Proxy(android.os.IBinder remote)
{
mRemote = remote;
}
public android.os.IBinder asBinder()
{
return mRemote;
}
public java.lang.String getInterfaceDescriptor()
{
return DESCRIPTOR;
}
public byte[] remoteProcedureCall(java.lang.String objectName, java.lang.String methodName, byte[] objectArray) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
byte[] _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeString(objectName);
_data.writeString(methodName);
_data.writeByteArray(objectArray);
mRemote.transact(Stub.TRANSACTION_remoteProcedureCall, _data, _reply, 0);
_reply.readException();
_result = _reply.createByteArray();
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
public boolean checkServerPluginExists(java.lang.String packageName, java.lang.String version) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
boolean _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeString(packageName);
_data.writeString(version);
mRemote.transact(Stub.TRANSACTION_checkServerPluginExists, _data, _reply, 0);
_reply.readException();
_result = (0!=_reply.readInt());
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
public boolean installServerPlugin(java.lang.String dllURL, java.lang.String dllName) throws android.os.RemoteException
{
android.os.Parcel _data = android.os.Parcel.obtain();
android.os.Parcel _reply = android.os.Parcel.obtain();
boolean _result;
try {
_data.writeInterfaceToken(DESCRIPTOR);
_data.writeString(dllURL);
_data.writeString(dllName);
mRemote.transact(Stub.TRANSACTION_installServerPlugin, _data, _reply, 0);
_reply.readException();
_result = (0!=_reply.readInt());
}
finally {
_reply.recycle();
_data.recycle();
}
return _result;
}
}
static final int TRANSACTION_remoteProcedureCall = (IBinder.FIRST_CALL_TRANSACTION + 0);
static final int TRANSACTION_checkServerPluginExists = (IBinder.FIRST_CALL_TRANSACTION + 1);
static final int TRANSACTION_installServerPlugin = (IBinder.FIRST_CALL_TRANSACTION + 2);
}
public byte[] remoteProcedureCall(java.lang.String objectName, java.lang.String methodName, byte[] objectArray) throws android.os.RemoteException;
public boolean checkServerPluginExists(java.lang.String packageName, java.lang.String version) throws android.os.RemoteException;
public boolean installServerPlugin(java.lang.String dllURL, java.lang.String dllName) throws android.os.RemoteException;
}
