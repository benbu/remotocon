package remoto.filemanager;

import java.util.ArrayList;

public class FileSystemEntry {
	
	String name;
	FileSystemEntry parentDir;
	boolean isDirectory;
	ArrayList<FileSystemEntry> contents;
	
	public static final FileSystemEntry root = new FileSystemEntry("", null, true);
	
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
