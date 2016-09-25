using UnityEngine;
using System.Collections;


public class TestPlotter : MonoBehaviour {
	public int xPos, yPos;
	public int thickness;
	public Listener listener;

	// Use this for initialization
	void Start () {

		thickness = 50;
		//listener = new Listener();

		// Create a main graph to store info
		PlotManager.Instance.PlotCreate("Valence", 0, 1000, Color.green, new Vector2(xPos,yPos));
		//Clone to increase thickness
		for (int i = 1; i < thickness; i++) {
			string plotId = "Valence"+i;
			PlotManager.Instance.PlotCreate(plotId, Color.green, "Valence");
		}

		/*// Create a new child "Arousal" graph.  Colour is red and parent is "valence"
		PlotManager.Instance.PlotCreate("Arousal", Color.red, "Valence");
		// Clone to increase thickness
		for (int i = 1; i < thickness; i++) {
			string plotId = "Arousal"+i;
			PlotManager.Instance.PlotCreate(plotId, Color.red, "Valence");
		}

		// Create a new child "Arousal" graph.  Colour is red and parent is "valence"
		PlotManager.Instance.PlotCreate("Mouse", Color.blue, "Valence");
		// Clone to increase thickness
		for (int i = 1; i < thickness; i++) {
			string plotId = "Mouse"+i;
			PlotManager.Instance.PlotCreate(plotId, Color.blue, "Valence");
		}*/
	}

	// Update is called once per frame
	void Update () {

		float valence = Listener.currentValence*5+500;//listener.currentValence;

		// Add data to graphs
		PlotManager.Instance.PlotAdd("Valence", valence);
		// Clone to increase thickness
		for (int i = -thickness/2 ; i < (thickness/2)+1; i++) {
			string plotId = "Valence"+i;
			PlotManager.Instance.PlotAdd (plotId, valence + i );
		}

		/*float arousal = Listener.currentArousal*5+200;//Input.mousePosition.y;

		// Add data to graphs
		PlotManager.Instance.PlotAdd("Arousal", arousal);
		// Clone to increase thickness
		for (int i = -thickness/2 ; i < (thickness/2)+1; i++) {
			string plotId = "Arousal"+i;
			PlotManager.Instance.PlotAdd (plotId, arousal + i );
		}
		
		float yData = Input.mousePosition.x;//Input.mousePosition.y;

		// Add data to graphs
		PlotManager.Instance.PlotAdd("Mouse", yData);
		// Clone to increase thickness
		for (int i = -thickness/2 ; i < (thickness/2)+1; i++) {
			string plotId = "Mouse"+i;
			PlotManager.Instance.PlotAdd (plotId, yData + i );
		}*/
	}
}
