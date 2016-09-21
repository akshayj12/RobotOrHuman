using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoryUpdater : MonoBehaviour {

	public Text textArea;

	public int space = 0;

	public static Dictionary<int, string> narratives;
	public static Dictionary<int, Dictionary<Affdex.Emotions, float>> ratings;

	// Use this for initialization
	void Start () {
		space = 0;
		Image humanButton = GameObject.Find("HumanButton").GetComponent<Image>();
		humanButton.color = Color.clear;

		narratives = new Dictionary<int, string> ();
		narratives.Add (0, "It is your birthday. Someone gives you a wallet made from an endangered animal.");
		narratives.Add (2, "You are watching television. Suddenly, you realize there is a wasp crawling on your arm.");
		narratives.Add (4, "You are giving a presentation at Emotion Lab 16. You can't get the projector to work.");
		narratives.Add (6, "Describe in single words, only the good things that come into your mind about your mother");

		ratings = new Dictionary<int, Dictionary<Affdex.Emotions, float>> ();

		Dictionary<Affdex.Emotions, float> narrative_0 = new Dictionary<Affdex.Emotions, float> ();
		narrative_0.Add (Affdex.Emotions.Anger, 100);
		narrative_0.Add (Affdex.Emotions.Contempt, 50);
		narrative_0.Add (Affdex.Emotions.Fear, 0);
		narrative_0.Add (Affdex.Emotions.Disgust, 100);
		narrative_0.Add (Affdex.Emotions.Joy, 0);
		narrative_0.Add (Affdex.Emotions.Surprise, 50);
		narrative_0.Add (Affdex.Emotions.Sadness, 0);
		ratings.Add (0, narrative_0);

		Dictionary<Affdex.Emotions, float> narrative_2 = new Dictionary<Affdex.Emotions, float> ();
		narrative_2.Add (Affdex.Emotions.Anger, 0);
		narrative_2.Add (Affdex.Emotions.Contempt, 0);
		narrative_2.Add (Affdex.Emotions.Fear, 100);
		narrative_2.Add (Affdex.Emotions.Disgust, 100);
		narrative_2.Add (Affdex.Emotions.Joy, 0);
		narrative_2.Add (Affdex.Emotions.Surprise, 50);
		narrative_2.Add (Affdex.Emotions.Sadness, 0);
		ratings.Add (2, narrative_2);

		Dictionary<Affdex.Emotions, float> narrative_6 = new Dictionary<Affdex.Emotions, float> ();
		narrative_6.Add (Affdex.Emotions.Anger, 0);
		narrative_6.Add (Affdex.Emotions.Contempt, 0);
		narrative_6.Add (Affdex.Emotions.Fear, 0);
		narrative_6.Add (Affdex.Emotions.Disgust, 0);
		narrative_6.Add (Affdex.Emotions.Joy, 100);
		narrative_6.Add (Affdex.Emotions.Surprise, 50);
		narrative_6.Add (Affdex.Emotions.Sadness, 20);
		ratings.Add (6, narrative_6);

		Dictionary<Affdex.Emotions, float> narrative_4 = new Dictionary<Affdex.Emotions, float> ();
		narrative_4.Add (Affdex.Emotions.Anger, 50);
		narrative_4.Add (Affdex.Emotions.Contempt, 0);
		narrative_4.Add (Affdex.Emotions.Fear, 100);
		narrative_4.Add (Affdex.Emotions.Disgust,0);
		narrative_4.Add (Affdex.Emotions.Joy, 0);
		narrative_4.Add (Affdex.Emotions.Surprise, 50);
		narrative_4.Add (Affdex.Emotions.Sadness, 50);
		ratings.Add (4, narrative_4);

	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown ("Jump"))
		{ 
			// Double narrative size to account for processing steps. 
			if (space == narratives.Count * 2) {
				space = 0;
			}
			//Debrief mode (odd numbers!)
			if (space % 2 == 1) {
				textArea.text = Listener.RunEmotionAnalysis (ratings[space-1]);

				bool hri = Listener.isHuman();
				if (hri){
					Image humanButton = GameObject.Find("HumanButton").GetComponent<Image>();
					humanButton.color = Color.green;			
				}
				else{
					
					Image humanButton = GameObject.Find("HumanButton").GetComponent<Image>();
					humanButton.color = Color.red;
				}
			}
			//otherwise proceed with the narrative (even numbers!)
			if (space % 2 == 0) {
				Listener.reinitialize ();
				textArea.text = narratives [space];
				Image humanButton = GameObject.Find("HumanButton").GetComponent<Image>();
				humanButton.color = Color.clear;
			}
			space++;
	}
}
}
