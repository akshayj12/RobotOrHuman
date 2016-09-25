using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Networking;

public class PavlokRest
{
	private static string baseUrl = "https://pavlok.herokuapp.com/api";
	private static string objectID = "Tu1AwK45tQ";

	public PavlokRest(){
		Debug.Log ("Pavlok up and running!");
	}

	public void getFeedback(string feedbackType, int intensity)
	{
		/*
		 * Feedback type = "vibrate", "beep", "shock", "led"
		 * For vibrate and shock, value is anywhere from 0-255
		 * For beep and led, value is anywhere from 1-4
		 */

		string url = baseUrl+"/"+objectID+"/"+feedbackType+"/"+intensity;
		//Debug.Log("Pavlok request:" + url);
		UnityWebRequest www = UnityWebRequest.Get(url);
		www.Send ();
	}
}

