using Affdex;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmotions : ImageResultsListener
{

	public float currentSmile;
	public float currentInterocularDistance;
	public float currentContempt;
	public float currentValence;
	public float currentAnger;
	public float currentFear;
	public FeaturePoint[] featurePointsList;


	public override void onFaceFound(float timestamp, int faceId)
	{
		Debug.Log("Found the face");
	}

	public override void onFaceLost(float timestamp, int faceId)
	{
		Debug.Log("Lost the face");
	}

	public override void onImageResults(Dictionary<int, Face> faces)
	{
		Debug.Log ("Got face results");
	}

}
