package com.remotocon.xmlrpc;

import java.io.IOException;
import java.io.InputStream;
import java.util.Dictionary;

import org.xml.sax.InputSource;
import org.xml.sax.SAXException;
import org.xml.sax.XMLReader;
import org.xml.sax.helpers.XMLReaderFactory;

public class XmlRpcResponse {
	Object Result;
	int FaultCode;
	String FaultMessage;
	boolean isFault;	
	
	public XmlRpcResponse()
	{}
	
	@SuppressWarnings("unchecked")
	public static XmlRpcResponse ParseResponse(InputStream inputStream) throws SAXException, IOException
	{	
		System.setProperty("org.xml.sax.driver","org.xmlpull.v1.sax2.Driver");

		XMLReader xr = XMLReaderFactory.createXMLReader();
		XmlRpcResponseSax handler = new XmlRpcResponseSax();
		xr.setContentHandler(handler);
		xr.setErrorHandler(handler);
		
		xr.parse(new InputSource(inputStream));
		
		if(handler.Errors.length() > 0)
			throw new IOException(handler.Errors);

		XmlRpcResponse response = new XmlRpcResponse();
		response.isFault = handler.isFault;
		
		if(handler.isFault)
		{
			Dictionary<String, Object> faultDict = (Dictionary<String, Object>)handler.Result;
			response.FaultCode = (Integer)faultDict.get("faultCode");
			response.FaultMessage = (String)faultDict.get("faultString");
		}
		else
			response.Result = handler.Result;
		
		return response;
	}
}
