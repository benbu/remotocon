package remoto.filemanager;

import java.util.ArrayList;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

public class FileSystemEntryAdapter extends ArrayAdapter<FileSystemEntry> {

    public ArrayList<FileSystemEntry> items;
    private LayoutInflater mInflater;

    public FileSystemEntryAdapter(
			Context context, 
			int textViewResourceId, 
			ArrayList<FileSystemEntry> items) {
        super(context, textViewResourceId, items);
        this.items = items;
        mInflater = LayoutInflater.from(context);
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
            View v = convertView;
            if (v == null) {
                v = mInflater.inflate(R.layout.file_system_entry, null);
            }
            FileSystemEntry fileEntry = items.get(position);
            if (fileEntry != null) {
            		ImageView iv = (ImageView) v.findViewById(R.id.FileIcon);
                    TextView tv = (TextView) v.findViewById(R.id.FileText);
                    if (tv != null) {
                          tv.setText(fileEntry.name);
                    }
                    if(iv != null){
                    	if(fileEntry.isDirectory)
                    		iv.setImageResource(R.drawable.folder);
                    	else
                    	{
                    		if(fileEntry.name.matches("^*.exe$"))
                    			iv.setImageResource(R.drawable.exe);
                			else if (fileEntry.name.matches("^*.txt$"))
                    			iv.setImageResource(R.drawable.txt);
                			else
                				iv.setImageResource(R.drawable.blank);
                    	}
                    }
            }
            return v;
    }
}
