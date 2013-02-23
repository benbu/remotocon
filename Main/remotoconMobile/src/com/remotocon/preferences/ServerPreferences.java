package com.remotocon.preferences;

import com.remotocon.encryption.Encryptor;
import com.remotocon.globals.Globals;

import com.remotocon.R;

import android.app.Activity;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.KeyEvent;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class ServerPreferences extends Activity implements TextWatcher {  
	public static final String PREF_SERVER = "REMOTOCON_SERVER_PREFS";
	public static final String KEY_SERVER_ADDRESS = "REMOTOCON_KEY_SERVER_ADDRESS";
	public static final String KEY_SERVER_PORT = "REMOTOCON_KEY_SERVER_PORT";
	public static final String KEY_SERVER_USERNAME = "REMOTOCON_KEY_SERVER_USERNAME";
	public static final String KEY_SERVER_PASSWORD = "REMOTOCON_KEY_SERVER_PASSWORD";
	public static final String KEY_SERVER_NAME = "REMOTOCON_KEY_SERVER_NAME"; //make sure cannot be null
	
	SharedPreferences serverPrefs;
	int selectedServerIndex;

	EditText serverName;
	EditText serverAddress;
	EditText serverPort;
	EditText serverUserName;
	EditText serverPassword;
	
	EditText focused;

	Encryptor encryptor = new Encryptor();
	
	@Override
	public void onCreate(Bundle savedInstanceState) { 
		super.onCreate(savedInstanceState); 		
        
        setContentView(R.layout.server_preferences);
        
        selectedServerIndex = Preferences.getSelectedServerIndex(this);
		serverPrefs = getSharedPreferences(PREF_SERVER + selectedServerIndex, MODE_PRIVATE);

		serverName = (EditText) findViewById(R.id.EditTextName);
		serverAddress = (EditText) findViewById(R.id.EditTextServerAddress);
		serverPort = (EditText) findViewById(R.id.EditTextPort);
		serverUserName = (EditText) findViewById(R.id.EditTextUserName);
		serverPassword = (EditText) findViewById(R.id.EditTextPassword);
		
		serverName.addTextChangedListener(this);
		serverAddress.addTextChangedListener(this);
		serverPort.addTextChangedListener(this);
		serverUserName.addTextChangedListener(this);
		serverPassword.addTextChangedListener(this);

		serverName.setText(serverPrefs.getString(KEY_SERVER_NAME, "Computer" + selectedServerIndex));
		serverAddress.setText(serverPrefs.getString(KEY_SERVER_ADDRESS, ""));
		serverPort.setText(Integer.toString(serverPrefs.getInt(KEY_SERVER_PORT, 4646)));
		serverUserName.setText(serverPrefs.getString(KEY_SERVER_USERNAME, ""));
		
		try {
			serverPassword.setText(encryptor.DecryptString64(serverPrefs.getString(KEY_SERVER_PASSWORD, "")));
		} catch (Exception e) {
			Toast.makeText(this, "Error loading saved password", Toast.LENGTH_LONG);
		}
		
		final Button save = (Button) findViewById(R.id.ButtonSavePrefs);
		save.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                savePreferences();
                ServerPreferences.this.finish();
            }
        });
		
		final Button cancel = (Button) findViewById(R.id.ButtonCancelServerPreferences);
		cancel.setOnClickListener(new View.OnClickListener() {
			public void onClick(View v) {
				ServerPreferences.this.finish();				
			}
		});
	}

	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event) {
	    if (keyCode == KeyEvent.KEYCODE_BACK) {
	    	if(savePreferences())
	    		return super.onKeyDown(keyCode, event);
	        return true;
	    }
	    return super.onKeyDown(keyCode, event);
	}
	
	private boolean savePreferences()
	{
		SharedPreferences.Editor editor = serverPrefs.edit();
		
		String sName = serverName.getText().toString();

		editor.putString(KEY_SERVER_NAME, sName == "" ? "Computer" + selectedServerIndex : sName);
		editor.putString(KEY_SERVER_ADDRESS, serverAddress.getText().toString());
		editor.putInt(KEY_SERVER_PORT, Integer.parseInt(serverPort.getText().toString()));
		editor.putString(KEY_SERVER_USERNAME, serverUserName.getText().toString());
		try {
			editor.putString(KEY_SERVER_PASSWORD, encryptor.EncryptString64(serverPassword.getText().toString()));
		} catch (Exception e) {
			Toast.makeText(this, "Error saving password.", Toast.LENGTH_LONG);
		}
		
		editor.commit();
		Globals.reloadClients = true;		
		return true;
	}
	
	private void updateFocusedEditText()
	{
		if(focused == null || !focused.isInputMethodTarget())
		{
			if(serverName.isInputMethodTarget())
				focused = serverName;
			else if (serverAddress.isInputMethodTarget())
				focused = serverAddress;
			else if (serverPort.isInputMethodTarget())
				focused = serverPort;
			else if (serverUserName.isInputMethodTarget())
				focused = serverUserName;
			else if (serverPassword.isInputMethodTarget())
				focused = serverPassword;
		}			
	}

	public void afterTextChanged(Editable s) {
		String value = s.toString();
		updateFocusedEditText();
		if(focused == null)
			return;
		switch(focused.getId())
		{
			case R.id.EditTextName:
				// no name restrictions at this time
				break;
			case R.id.EditTextServerAddress:
				if(value.matches("^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$"))
					serverAddress.setError(null);
				else
					serverAddress.setError("Invalid IP Address: must have the format ###.###.###.###, each number must be between 0-255");
				break;
			case R.id.EditTextPort:
				if(value.length() <= 5 && value.length() > 0)
					if(value.matches("^[0-9]*$"))
					{
						int val = Integer.parseInt(value); 
						if( val > 1023 && val <= 65535)
							serverPort.setError(null);
							return;
					}
				serverPort.setError("Port must be a number between 1024 and 65535");
				break;
			case R.id.EditTextUserName:
				break;
			case R.id.EditTextPassword:
				break;			
		}		
	}

	public void beforeTextChanged(CharSequence s, int start, int count, int after) { }
	public void onTextChanged(CharSequence s, int start, int before, int count) { }
}
