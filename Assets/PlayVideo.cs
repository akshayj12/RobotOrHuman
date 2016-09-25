using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayVideo : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//CanvasRenderer r = GetComponent<CanvasRenderer>();
		//MovieTexture movie = (MovieTexture)	if (!movie.isPlaying) {

		RawImage rim = GetComponent<RawImage>();
		MovieTexture movie = (MovieTexture)rim.mainTexture;
		movie.Play ();

	}
}