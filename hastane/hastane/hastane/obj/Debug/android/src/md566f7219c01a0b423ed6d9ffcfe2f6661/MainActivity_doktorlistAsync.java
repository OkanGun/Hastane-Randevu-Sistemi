package md566f7219c01a0b423ed6d9ffcfe2f6661;


public class MainActivity_doktorlistAsync
	extends android.os.AsyncTask
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPreExecute:()V:GetOnPreExecuteHandler\n" +
			"n_doInBackground:([Ljava/lang/Object;)Ljava/lang/Object;:GetDoInBackground_arrayLjava_lang_Object_Handler\n" +
			"";
		mono.android.Runtime.register ("hastane.MainActivity+doktorlistAsync, hastane, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MainActivity_doktorlistAsync.class, __md_methods);
	}


	public MainActivity_doktorlistAsync () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity_doktorlistAsync.class)
			mono.android.TypeManager.Activate ("hastane.MainActivity+doktorlistAsync, hastane, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MainActivity_doktorlistAsync (md566f7219c01a0b423ed6d9ffcfe2f6661.MainActivity p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == MainActivity_doktorlistAsync.class)
			mono.android.TypeManager.Activate ("hastane.MainActivity+doktorlistAsync, hastane, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "hastane.MainActivity, hastane, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void onPreExecute ()
	{
		n_onPreExecute ();
	}

	private native void n_onPreExecute ();


	public java.lang.Object doInBackground (java.lang.Object[] p0)
	{
		return n_doInBackground (p0);
	}

	private native java.lang.Object n_doInBackground (java.lang.Object[] p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
