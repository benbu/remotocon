package com.remotocon.xmlrpc;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Dictionary;
import java.util.Hashtable;
import java.util.Stack;

import org.xml.sax.Attributes;
import org.xml.sax.helpers.DefaultHandler;

public class XmlRpcResponseSax extends DefaultHandler {
	
	boolean isFault = false;
	Object Result;
	String Errors = "";	
	
	private Stack<Object> CollectionStack = new Stack<Object>();
	private Stack<XmlRpcElement> ElementStack = new Stack<XmlRpcElement>();
	
	private StringBuilder text = new StringBuilder();
	private String memberName;
	private Object value;
	
	private SimpleDateFormat dateFormat = new SimpleDateFormat("yyyyMMdd'T'HH:mm:ss");	
	
	public XmlRpcResponseSax()
	{
		super();
	}
	
    public void startDocument ()
    {
    	
    }


    public void endDocument ()
    {
        if (!CollectionStack.isEmpty())
            Errors += "\nCollectionStack not empty after parsing.";
        if (!ElementStack.isEmpty())
        	Errors += "\nNode Stack not empty after parsing.";       

        if(Result == null)
        	Errors += "\nNo value returned in response.";
    }


    public void startElement (String uri, String name, String qName, Attributes atts)
    {
    	ElementStack.push(XmlRpcElement.Parse(qName));
    	switch(ElementStack.peek())
    	{
    	case ARRAY:
    		CollectionStack.push(new ArrayList<Object>());
    		break;
    	case STRUCT:
    		CollectionStack.push(new Hashtable<String, Object>());
    		break;
    	case FAULT:
    		this.isFault = true;
    		break;
    	}
    }

	@SuppressWarnings("unchecked")
    public void endElement (String uri, String name, String qName)
    {    	
    	try
    	{
	    	switch(ElementStack.pop())
	    	{
	    	case I4:
	    	case INT: value = Integer.parseInt(text.toString()); break;
	    	case BOOLEAN: value = text.toString().equals("1"); break;
	    	case STRING: value = text.toString(); break;
	    	case DOUBLE: value = Double.parseDouble(text.toString()); break;
	    	case DATETIMEISO8601: value = dateFormat.parseObject(text.toString()); break;
	    	case BASE64: value = Base64.decodeToObject(text.toString()); break;
	    	
	    	case STRUCT:
	    	case ARRAY: value = CollectionStack.pop(); break;
	    	
	    	case NAME: memberName = text.toString(); break;
	    	case MEMBER: ((Dictionary<String, Object>)CollectionStack.peek()).put(memberName, value); break;
	    	
	    	case VALUE:
	    		if(CollectionStack.isEmpty())
	    		{
	    			if(Result != null)
	    				Errors += "\nMore than one value returned in response.";
	    			Result = value;
	    		}
	    		else if(CollectionStack.peek() instanceof ArrayList)
	    			((ArrayList<Object>)CollectionStack.peek()).add(value);
	    		break;
	    	}
    	}
    	catch(Exception e)
    	{
    		Errors += "\n" + e.toString();
    	}
    }

    public void characters (char ch[], int start, int length)
    {
    	text.delete(0, text.length());
    	for(int i = start; i < start + length; i++)
    		text.append(ch[i]);
    }
}
