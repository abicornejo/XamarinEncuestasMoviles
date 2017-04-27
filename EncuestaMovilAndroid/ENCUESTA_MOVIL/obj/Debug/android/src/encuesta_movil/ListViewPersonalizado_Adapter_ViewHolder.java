package encuesta_movil;


public class ListViewPersonalizado_Adapter_ViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ENCUESTA_MOVIL.ListViewPersonalizado_Adapter/ViewHolder, ENCUESTA_MOVIL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ListViewPersonalizado_Adapter_ViewHolder.class, __md_methods);
	}


	public ListViewPersonalizado_Adapter_ViewHolder ()
	{
		super ();
		if (getClass () == ListViewPersonalizado_Adapter_ViewHolder.class)
			mono.android.TypeManager.Activate ("ENCUESTA_MOVIL.ListViewPersonalizado_Adapter/ViewHolder, ENCUESTA_MOVIL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	java.util.ArrayList refList;
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
