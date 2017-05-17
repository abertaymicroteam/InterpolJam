using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colourChanger : MonoBehaviour {

	public Image characterImage;
	private float startTime;

	private Color blue;
	private Color red;

	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;

		blue = new Color32 (0, 66, 124, 255);
		red = new Color32 (193, 3, 3, 255);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ((Time.time - startTime) > 0 && (Time.time - startTime) < 5)
		{
			characterImage.color = Color.white;
		}
		else if ((Time.time - startTime) > 5 && (Time.time - startTime) < 10)
		{
			characterImage.color = blue;
		}
		else if ((Time.time - startTime) > 10)
		{
			characterImage.color = red;
		}
	}
}
