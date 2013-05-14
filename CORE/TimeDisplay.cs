using UnityEngine;
using System.Collections;

public class TimeDisplay : MonoBehaviour
{
	
	public GUIText timetext;
	
	//Time display
	string seconds, minutes, hours, days, months, years;

	Vector2 clockrectsize = new Vector2(200, 25);
	
	public string _DisplayTime (out string secs, 
		out string mins, 
		out string hours, 
		out string days, 
		out string months, 
		out string years)
	{
		
		secs = _WorldTime_._GetSecondsRM ().ToString ();
		mins = _WorldTime_._GetMinutesRM ().ToString ();
		hours = _WorldTime_._GetHoursRM ().ToString ();
		days = _WorldTime_._GetDaysRM ().ToString ();
		months = _WorldTime_._GetMonthsRM ().ToString ();
		years = _WorldTime_._GetYearsR ().ToString ();
		
		string rs = years + "y::" +
					months + "m::" +
					days + "d::" +
					hours + "h::" +
					mins + "m::" +
					secs + "s::";
		
		return rs;
		
	}
	
	public string _DisplayTime ()
	{
		
		string secs = _WorldTime_._GetSecondsRM ().ToString ();
		string mins = _WorldTime_._GetMinutesRM ().ToString ();
		string hours = _WorldTime_._GetHoursRM ().ToString ();
		string days = _WorldTime_._GetDaysRM ().ToString ();
		string months = _WorldTime_._GetMonthsRM ().ToString ();
		string years = _WorldTime_._GetYearsR ().ToString ();
		
		string rs = years + "y::" +
					months + "m::" +
					days + "d::" +
					hours + "h::" +
					mins + "m::" +
					secs + "s";
		
		return rs;
		
	}
	
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log (_DisplayTime ());
	}
	
	void OnGUI ()
	{
		Rect clockrect = new Rect(Screen.width/2 - clockrectsize.x/2, 0, clockrectsize.x, clockrectsize.y);
		
		GUI.Box(clockrect, _DisplayTime() );
	}
}