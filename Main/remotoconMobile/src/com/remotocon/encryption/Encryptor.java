package com.remotocon.encryption;

import java.io.IOException;

import javax.crypto.Cipher;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;

import com.remotocon.xmlrpc.Base64;

public class Encryptor {

    private static final String DefaultKey = "!N5g(g3f37k.;''$RNB<>spe";
    private static final String DefaultIV = "12345678";
    private IvParameterSpec iv = new IvParameterSpec(DefaultIV.getBytes());
    private SecretKeySpec key;
    
    public Encryptor(String userName, String password)
    {
        String longKey = userName + password + Encryptor.DefaultKey;
        String shortKey = longKey.substring(0, Encryptor.DefaultKey.length());
        key = new SecretKeySpec(shortKey.getBytes(), "DESede");
    }

    public Encryptor() {
		key = new SecretKeySpec(Encryptor.DefaultKey.getBytes(), "DESede");
	}
    
    public String EncryptString64(String input) throws Exception
    {
    	return new String(encryptToBase64(input.getBytes()), "UTF8");
    }
    
    public String DecryptString64(String input) throws Exception
    {
    	return new String(decryptFromBase64(input.getBytes()), "UTF8");
    }

	public byte[] encrypt(byte[] input) throws Exception
    {
      Cipher c3des = Cipher.getInstance("DESede/CBC/PKCS5Padding");
      c3des.init(Cipher.ENCRYPT_MODE, key, iv);
      return c3des.doFinal(input);
    }
    
    public byte[] encryptToBase64(byte[] input) throws Exception
    {
        return Base64.encodeBytesToBytes(this.encrypt(input));
    }
    
    public byte[] decryptFromBase64(byte[] encryptedBase64Bytes) throws IOException, Exception
    {
    	return decrypt(Base64.decode(encryptedBase64Bytes));
    }
    
    public byte[] decrypt(byte[] encryptedBytes) throws Exception
    {
        Cipher c3des = Cipher.getInstance("DESede/CBC/PKCS5Padding");
        c3des.init(Cipher.DECRYPT_MODE, key, iv);
        return c3des.doFinal(encryptedBytes);    	
    }

}
