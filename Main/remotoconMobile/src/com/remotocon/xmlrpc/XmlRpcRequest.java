package com.remotocon.xmlrpc;

import java.io.IOException;
import java.io.Writer;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Dictionary;
import java.util.Enumeration;

public class XmlRpcRequest {

      public String MethodName;
      public String ObjectName;
      public Object[] Params;
  	
      private SimpleDateFormat dateFormat = new SimpleDateFormat("yyyyMMdd'T'HH:mm:ss");	

      public XmlRpcRequest(Object[] params)
      {    	  
    	  Params = (params == null ? new Object[]{} : params);
      }
      
      private String methodName()
      {
    	  if(ObjectName != null)
    		  return ObjectName + '.' + MethodName;
    	  return MethodName;
      }

      public void WriteXml(Writer w) throws IOException
      {
    	  XmlWriter writer = new XmlWriter(w);
    	  writer.WriteStartDocument();
          writer.WriteStartElement(XmlRpcElement.METHODCALL);
          writer.WriteElementString(XmlRpcElement.METHODNAME, methodName());

          writer.WriteStartElement(XmlRpcElement.PARAMS);
          writer.WriteStartElement(XmlRpcElement.PARAM);
          
          for(Object o : Params)
        	  WriteXmlValue(writer, o);

          writer.WriteEndElement();
          writer.WriteEndElement();

          writer.WriteEndElement();
          
          writer.flush();
      }

      @SuppressWarnings({ "unchecked", "rawtypes" })
      private void WriteXmlValue(XmlWriter writer, Object value) throws IOException
      {
          writer.WriteStartElement(XmlRpcElement.VALUE);

          if (value != null)
          {
              if (value instanceof byte[])
              {
                  byte[] ba = (byte[])value;
                  writer.WriteStartElement(XmlRpcElement.BASE64);
                  writer.WriteBase64(ba);
                  writer.WriteEndElement();
              }
              else if (value instanceof String) writer.WriteElementString(XmlRpcElement.STRING, value.toString());
              else if (value instanceof Integer) writer.WriteElementString(XmlRpcElement.I4, value.toString());
              else if (value instanceof Date) writer.WriteElementString(XmlRpcElement.DATETIMEISO8601, dateFormat.format(value));
              else if (value instanceof Double || value instanceof Float) writer.WriteElementString(XmlRpcElement.DOUBLE, value.toString());
              else if (value instanceof Boolean) writer.WriteElementString(XmlRpcElement.BOOLEAN, (Boolean)value ? "1" : "0");
              else if (value instanceof Iterable)
              {
                  writer.WriteStartElement(XmlRpcElement.ARRAY);
                  writer.WriteStartElement(XmlRpcElement.DATA);
                  
                  for (Object member : (Iterable)value)
                      WriteXmlValue(writer, member);
                          
                  writer.WriteEndElement();
                  writer.WriteEndElement();
              }
              else if (value instanceof Object[])
              {
                  writer.WriteStartElement(XmlRpcElement.ARRAY);
                  writer.WriteStartElement(XmlRpcElement.DATA);
                  
                  for (Object member : (Object[])value)
                      WriteXmlValue(writer, member);
                          
                  writer.WriteEndElement();
                  writer.WriteEndElement();
              }
              else if (value instanceof Dictionary)
              {
                  Dictionary<String,Object> h = (Dictionary<String, Object>)value;
                  writer.WriteStartElement(XmlRpcElement.STRUCT);
                  Enumeration<String> keys = h.keys();
                  while(keys.hasMoreElements())
                  {
                	  String key = keys.nextElement();
                      writer.WriteStartElement(XmlRpcElement.MEMBER);
                      writer.WriteElementString(XmlRpcElement.NAME, key);
                      WriteXmlValue(writer, h.get(key));
                      writer.WriteEndElement();
                      writer.WriteEndElement();
                  }
                  writer.WriteEndElement();
              }
          }
          writer.WriteEndElement();
      }
}
