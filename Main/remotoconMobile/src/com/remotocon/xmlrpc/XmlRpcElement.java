package com.remotocon.xmlrpc;

import java.util.Dictionary;
import java.util.Hashtable;

enum XmlRpcElement 
{	
	METHODCALL      ,
	METHODNAME      ,
	PARAMS          ,
	PARAM           ,
	VALUE           ,
	I4              ,
	INT             ,
	BOOLEAN         ,
	STRING          ,
	DOUBLE          ,
	DATETIMEISO8601 ,
	BASE64          ,
	STRUCT          ,
	ARRAY           ,
	MEMBER          ,
	NAME            ,
	DATA            ,
	METHODRESPONSE  ,
	FAULT           ,
	OTHER			;
	
	private static Dictionary<String, XmlRpcElement> StringElement;
	private static Dictionary<XmlRpcElement, String> ElementString;
	
	private static void InitializeStringElement() {
		StringElement = new Hashtable<String, XmlRpcElement>();
		StringElement.put("methodCall"        ,      METHODCALL);     
		StringElement.put("methodName"        ,      METHODNAME);     
		StringElement.put("params"            ,      PARAMS);         
		StringElement.put("param"             ,      PARAM);          
		StringElement.put("value"             ,      VALUE);          
		StringElement.put("i4"                ,      I4);             
		StringElement.put("int"               ,      INT);            
		StringElement.put("boolean"           ,      BOOLEAN);        
		StringElement.put("string"            ,      STRING);         
		StringElement.put("double"            ,      DOUBLE);         
		StringElement.put("dateTime.iso8601"  ,      DATETIMEISO8601);
		StringElement.put("base64"            ,      BASE64);         
		StringElement.put("struct"            ,      STRUCT);         
		StringElement.put("array"             ,      ARRAY);          
		StringElement.put("member"            ,      MEMBER);         
		StringElement.put("name"              ,      NAME);           
		StringElement.put("data"              ,      DATA);           
		StringElement.put("methodResponse"    ,      METHODRESPONSE); 
		StringElement.put("fault"             ,      FAULT);       
	}

	private static void InitializeElementString() {
		ElementString = new Hashtable<XmlRpcElement, String>();
		ElementString.put( METHODCALL          ,  "methodCall");       
		ElementString.put( METHODNAME          ,  "methodName");       
		ElementString.put( PARAMS              ,  "params");           
		ElementString.put( PARAM               ,  "param");            
		ElementString.put( VALUE               ,  "value");            
		ElementString.put( I4                  ,  "i4");               
		ElementString.put( INT                 ,  "int");              
		ElementString.put( BOOLEAN             ,  "boolean");          
		ElementString.put( STRING              ,  "string");           
		ElementString.put( DOUBLE              ,  "double");           
		ElementString.put( DATETIMEISO8601     ,  "dateTime.iso8601"); 
		ElementString.put( BASE64              ,  "base64");           
		ElementString.put( STRUCT              ,  "struct");           
		ElementString.put( ARRAY               ,  "array");            
		ElementString.put( MEMBER              ,  "member");           
		ElementString.put( NAME                ,  "name");             
		ElementString.put( DATA                ,  "data");             
		ElementString.put( METHODRESPONSE      ,  "methodResponse");   
		ElementString.put( FAULT      		   ,  "fault");            
	}
	
	public static XmlRpcElement Parse(String s)
	{
		if(StringElement == null)
			InitializeStringElement();
		XmlRpcElement result =  StringElement.get(s);
		return result == null ? XmlRpcElement.OTHER : result;
	}

	@Override
	public String toString()
	{
		if(ElementString == null)
			InitializeElementString();
		return ElementString.get(this);
	}
}