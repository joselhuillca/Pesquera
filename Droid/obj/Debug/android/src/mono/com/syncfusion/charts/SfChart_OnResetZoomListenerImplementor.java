package mono.com.syncfusion.charts;


public class SfChart_OnResetZoomListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.syncfusion.charts.SfChart.OnResetZoomListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_OnResetZoomListener:(Lcom/syncfusion/charts/SfChart;Lcom/syncfusion/charts/ChartResetZoomEvent;)V:GetOnResetZoomListener_Lcom_syncfusion_charts_SfChart_Lcom_syncfusion_charts_ChartResetZoomEvent_Handler:Com.Syncfusion.Charts.SfChart/IOnResetZoomListenerInvoker, Syncfusion.SfChart.Android\n" +
			"";
		mono.android.Runtime.register ("Com.Syncfusion.Charts.SfChart+IOnResetZoomListenerImplementor, Syncfusion.SfChart.Android, Version=13.3451.0.7, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", SfChart_OnResetZoomListenerImplementor.class, __md_methods);
	}


	public SfChart_OnResetZoomListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SfChart_OnResetZoomListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Syncfusion.Charts.SfChart+IOnResetZoomListenerImplementor, Syncfusion.SfChart.Android, Version=13.3451.0.7, Culture=neutral, PublicKeyToken=3d67ed1f87d44c89", "", this, new java.lang.Object[] {  });
	}


	public void OnResetZoomListener (com.syncfusion.charts.SfChart p0, com.syncfusion.charts.ChartResetZoomEvent p1)
	{
		n_OnResetZoomListener (p0, p1);
	}

	private native void n_OnResetZoomListener (com.syncfusion.charts.SfChart p0, com.syncfusion.charts.ChartResetZoomEvent p1);

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
