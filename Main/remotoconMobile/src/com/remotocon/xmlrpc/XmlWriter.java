package com.remotocon.xmlrpc;

import java.io.IOException;
import java.io.Writer;
import java.util.Stack;

public class XmlWriter {
	
	private Writer _writer;
	private Stack<String> openTags = new Stack<String>();
	
	public XmlWriter(Writer writer)
	{
		_writer = writer;
	}
	
	public void WriteStartDocument() throws IOException
	{
		_writer.append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
	}
	
	public void WriteStartElement(XmlRpcElement element) throws IOException
	{
		WriteStartElement(element.toString());
	}
	
	public void WriteStartElement(String name) throws IOException
	{
		_writer.append("<");
		_writer.append(name);
		_writer.append(">");
		openTags.push(name);
	}
	
	public void WriteEndElement() throws IOException
	{
		_writer.append("</");
		_writer.append(openTags.pop());
		_writer.append(">");
	}
	
	public void WriteValue(String value) throws IOException
	{
		_writer.append(EscapeXml(value));
	}
	
	public void WriteElementString(XmlRpcElement element, String value)throws IOException
	{
		WriteElementString(element.toString(), value);
	}
	
	public void WriteElementString(String name, String value)throws IOException
	{
		WriteStartElement(name);
		WriteValue(value);
		WriteEndElement();
	}
	
	public String EscapeXml(String value)
	{
		return value.replaceAll("<", "&lt;").replaceAll("&", "&amp;");
	}

	public void WriteBase64(byte[] ba) throws IOException {
		_writer.append(Base64.encodeBytes(ba));
	}

	public void flush() throws IOException {
		_writer.flush();		
	}
}
