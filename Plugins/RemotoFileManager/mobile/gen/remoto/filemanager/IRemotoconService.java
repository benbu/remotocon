/*
 * This file is auto-generated.  DO NOT MODIFY.
 * Original file: C:\\svn\\benjabu\\trunk\\RemotoFileManager\\src\\remoto\\filemanager\\IRemotoconService.aidl
 */
package remoto.filemanager;
import java.lang.String;
import android.os.RemoteException;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Binder;
import android.os.Parcel;
public interface IRemotoconService extends android.os.IInterface
{
/** Local-side IPC implementation stub class. */
public static abstract class Stub extends android.os.Binder implements remoto.filemanager.IRemotoconService
{
private static final java.lang.String DESCRIPTOR = "remoto.filemanager.IRemotoconService";
/** Construct the stub at attach it to the interface. */
public Stub()
{
this.attachInterface(this, DESCRIPTOR);
}
/**
 * Cast an IBinder object into an IRemotoconService interface,
 * generating a proxy if needed.
 */
public static remoto.filemanager.IRemotoconService asInterface(android.os.IBinder obj)
{
if ((obj==null)) {
return null;
}
android.os.IInterface iin = (android.os.IInterface)obj.queryLocalInterface(DESCRIPTOR);
if (((iin!=null)&&(iin instanceof remoto.filemanager.IRemotoconService))) {
return ((remoto.filemanager.IRemotoconService)iin);
}
return new remoto.filemanager.IRemotoconService.Stub.Proxy(obj);
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
}
return super.onTransact(code, data, reply, flags);
}
private static class Proxy implements remoto.filemanager.IRemotoconService
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
}
static final int TRANSACTION_remoteProcedureCall = (IBinder.FIRST_CALL_TRANSACTION + 0);
}
public byte[] remoteProcedureCall(java.lang.String objectName, java.lang.String methodName, byte[] objectArray) throws android.os.RemoteException;
}
