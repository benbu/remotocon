package com.remotocon.plugins.FileManager;

public class FileSystemEntry {
	
	String name;
	FileSystemEntry parentDir;
	boolean isDirectory;
	
	public FileSystemEntry(String name, FileSystemEntry parentDir, boolean isDirectory)
	{
		this.name = name;
		this.parentDir = parentDir;
		this.isDirectory = isDirectory;
	}
	
	public String getFullPath()
	{
		if(parentDir == null || parentDir.name == "")
			return name;
		return parentDir.getFullPath() + "\\" + name;
	}
}
