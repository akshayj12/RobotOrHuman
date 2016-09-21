using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Affdex;
using System.Collections;
using System.Linq;

public class Listener : ImageResultsListener
{
	// Access to Emotions
//	public float currentSmile;
//	public float currentInterocularDistance;
//	public float currentContempt;
	public static float currentValence,currentArousal;
//	public float currentAnger;
//	public float currentFear;

	// Link to Pavlok
	public PavlokRest pavlok;

	// Debug info (left panel)
    public Text textArea;

    //Var to store the emotion through time
	//public static Dictionary<int, Dictionary<Emotions, float>> timeEmotions = new Dictionary<int, Dictionary<Emotions, float>> ();
	public static List<Dictionary<Affdex.Emotions, float>> timeEmotions = new List<Dictionary<Emotions, float>>();

    //Timer
    public static int timer = 0;
    public static int recordThreshold = 1; //record emotion every 10 steps

	// Threshold isHuman
	public static float threshold_human = 30;
	// Delta Human/Replicant
	public static float agreement;

    public override void onFaceFound(float timestamp, int faceId)
    {
        Debug.Log("Found the face");
		pavlok.getFeedback ("beep", 3);
    }

    public override void onFaceLost(float timestamp, int faceId)
    {
        Debug.Log("Lost the face");
		pavlok.getFeedback ("vibrate", 255);
    }

    public override void onImageResults(Dictionary<int, Face> faces)
    {
		// Print the debug panel on the left
        if(faces.Count > 0)
        {
//            DebugFeatureViewer dfv = GameObject.FindObjectOfType<DebugFeatureViewer>();
//
//            if (dfv != null)
//                dfv.ShowFace(faces[0]);
//            textArea.text = faces[0].ToString();
//            textArea.CrossFadeColor(Color.white, 0.2f, true, false);
        }
        else
        {
            textArea.CrossFadeColor(new Color(1, 0.7f, 0.7f), 0.2f, true, false);
        }

		// Record the emotion information
		if (timer % recordThreshold == 0)
		{
			if (faces.Count > 0)
			{
				Face face = faces[0];

				//Retrieve the Valence Score
				face.Emotions.TryGetValue (Emotions.Valence, out currentValence);

				//Retrieve the Emotion Score
				float currentAnger;
				float currentContempt;
				float currentDisgust;
				float currentFear;
				float currentJoy;
				float currentSadness;
				float currentSurprise;
				face.Emotions.TryGetValue (Emotions.Anger, out currentAnger);
				face.Emotions.TryGetValue (Emotions.Contempt, out currentContempt);
				face.Emotions.TryGetValue (Emotions.Disgust, out currentDisgust);
				face.Emotions.TryGetValue (Emotions.Fear, out currentFear);
				face.Emotions.TryGetValue (Emotions.Joy, out currentJoy);
				face.Emotions.TryGetValue (Emotions.Sadness, out currentSadness);
				face.Emotions.TryGetValue (Emotions.Surprise, out currentSurprise);

				Dictionary<Affdex.Emotions, float> temp = new Dictionary<Affdex.Emotions, float> ();
				temp.Add (Emotions.Anger, currentAnger);
				temp.Add (Emotions.Contempt, currentContempt);
				temp.Add (Emotions.Disgust, currentDisgust);
				temp.Add (Emotions.Fear, currentFear);
				temp.Add (Emotions.Joy, currentJoy);
				temp.Add (Emotions.Sadness, currentSadness);
				temp.Add (Emotions.Surprise, currentSurprise);

				timeEmotions.Add(temp);
			}
			//Debug.Log(debugEmotion());
		}
    }

	//public static string RunEmotionAnalysis()
	public static string RunEmotionAnalysis(Dictionary<Affdex.Emotions, float> ratings)
	{
		// Aggregate the results in a dictionnary
		Dictionary<Affdex.Emotions, float> emotionsSummary = new Dictionary<Affdex.Emotions, float>();

		//foreach(KeyValuePair<int, Dictionary<Affdex.Emotions, float>> e in timeEmotions)
		//{
		foreach (Dictionary<Affdex.Emotions, float> e in timeEmotions) {
			foreach (KeyValuePair<Affdex.Emotions, float> emo in e)
			{
				float value;
				if(emotionsSummary.TryGetValue(emo.Key, out value))
				{
					emotionsSummary[emo.Key]=(value + emo.Value/(timer/recordThreshold));
				}
				else // initialize the dictionary
				{
					emotionsSummary.Add(emo.Key, 0);
				}
			}
		}

		// Order by Value
		var items = from pair in emotionsSummary
					orderby pair.Value descending
					select pair;

		// Compare the ratings to the user's behavior
		agreement = 0;
		foreach (KeyValuePair<Affdex.Emotions, float> bhv in emotionsSummary) 
		{
			float delta = Mathf.Abs(ratings [bhv.Key] - bhv.Value/timer);
			Debug.Log (bhv.Key + ":" + delta);
			agreement += delta/emotionsSummary.Count;
		}

		// Create the string to display
		string result = "";
		//result += "Interaction time: " + timer + "\n";
		foreach (KeyValuePair<Affdex.Emotions, float> emo in items)
		{
			result += emo.Key + ":" + Mathf.Floor(emo.Value) + "% -- (" + ratings[emo.Key] +")% \n";
		}

		//result += "agreement:" + agreement + "\n";
		//result += "isHuman:" + isHuman ();

		//Debug.Log (result);
		Debug.Log(agreement);
		return result;
	}

	public static bool isHuman(){
		if (agreement < threshold_human)
			return false;
		else
			return true;
	}

	public static void reinitialize()
	{
		timeEmotions = new List<Dictionary<Emotions, float>>();
		timer = 0;
	}


	string debugEmotion ()
	{
		string s = "timeEmotion\n";
		foreach(Dictionary<Affdex.Emotions, float> e in timeEmotions)
		{
			foreach (KeyValuePair<Affdex.Emotions, float> emo in e)
			{
				s += emo.Key + ": " + emo.Value+"\n";
			}
			s += "\n";
		}
		return s;
	}

    // Use this for initialization
    void Start () {
		pavlok = new PavlokRest ();
	}

	// Update is called once per frame
	void Update () {
        timer++;
		/*if (currentValence >= 0){
			Image humanButton = GameObject.Find("HumanButton").GetComponent<Image>();
			humanButton.color = Color.green;
			
		}
		else{
			Image humanButton = GameObject.Find("HumanButton").GetComponent<Image>();
			humanButton.color = Color.red;
		}*/
	}


}
