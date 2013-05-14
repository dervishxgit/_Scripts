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
		
		secs = WorldTime._GetSecondsRM ().ToString ();
		mins = WorldTime._GetMinutesRM ().ToString ();
		hours = WorldTime._GetHoursRM ().ToString ();
		days = WorldTime._GetDaysRM ().ToString ();
		months = WorldTime._GetMonthsRM ().ToString ();
		years = WorldTime._GetYearsR ().ToString ();
		
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
		
		string secs = WorldTime._GetSecondsRM ().ToString ();
		string mins = WorldTime._GetMinutesRM ().ToString ();
		string hours = WorldTime._GetHoursRM ().ToString ();
		string days = WorldTime._GetDaysRM ().ToString ();
		string months = WorldTime._GetMonthsRM ().ToString ();
		string years = WorldTime._GetYearsR ().ToString ();
		
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