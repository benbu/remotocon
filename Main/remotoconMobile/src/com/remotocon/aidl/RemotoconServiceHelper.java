package com.remotocon.aidl;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.StreamCorruptedException;

public class RemotoconServiceHelper {
	
	public static byte[] paramsToBytes(Object[] params)
	{
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		try {
			ObjectOutputStream oos = new ObjectOutputStream(baos);
			oos.writeObject(params);
		} catch (IOException e) {
		}
		return baos.toByteArray();
	}
	
	public static Object bytesToObject(byte[] bytes) throws ClassNotFoundException
	{
		ByteArrayInputStream bais = new ByteArrayInputStream(bytes);
		Object o = null;
		try {
			ObjectInputStream ois = new ObjectInputStream(bais);
			o = ois.readObject();
		} catch (StreamCorruptedException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
		return o;
	}
}
