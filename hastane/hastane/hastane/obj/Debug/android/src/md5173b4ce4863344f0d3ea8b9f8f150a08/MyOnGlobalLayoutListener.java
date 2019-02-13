package md5173b4ce4863344f0d3ea8b9f8f150a08;


public class MyOnGlobalLayoutListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.ViewTreeObserver.OnGlobalLayoutListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onGlobalLayout:()V:GetOnGlobalLayoutHandler:Android.Views.ViewTreeObserver/IOnGlobalLayoutListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Me.ITheBK.MyOnGlobalLayoutListener, BarChart, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null", MyOnGlobalLayoutListener.class, __md_methods);
	}


	public MyOnGlobalLayoutListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MyOnGlobalLayoutListener.class)
			mono.android.TypeManager.Activate ("Me.ITheBK.MyOnGlobalLayoutListener, BarChart, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onGlobalLayout ()
	{
		n_onGlobalLayout ();
	}

	private native void n_onGlobalLayout ();

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
