using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {

	public Text moneyDisplay;
	public GameObject bankruptText;
	private float money;
	private float timeEntered;
	private int difficulty;
	private float jobTimer;
	private bool jobActive;
	// Use this for initialization
	void Start () 
	{
	// TO DO: Clicking more than one button fucks everything up
		money = 1000;
		moneyDisplay.text = "$" + money;
		jobActive = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if ((Time.time - timeEntered) > 3)
		{
			bankruptText.SetActive(false);
		}

		if (((Time.time - jobTimer) > 5) && jobActive == true)
		{
			jobComplete();
			jobActive = false;
		}

		moneyDisplay.text = "$" + money;
	}

	private void resourcePurchased (float resourceCost)
	{
		float checkCost = money - resourceCost;
		if (checkCost >= 0)
		{
			money -= resourceCost;
			jobActive = true;
		}
		else
		{
			bankruptcyEntered();
		}
	}

	private void resourceGained (float resourceValue)
	{
		money += resourceValue;
	}

	private void bankruptcyEntered()
	{
		timeEntered = Time.time;
		bankruptText.SetActive(true);
	}

	public void jobSelected ()
	{
		switch (difficulty)
		{
			case 1:
					resourcePurchased(250);
					break;
			case 2:
					resourcePurchased(500);
					break;
			case 3:
					resourcePurchased(1000);
					break;
		}

	}

	public void jobComplete ()
	{
		switch (difficulty)
		{
			case 1:
					resourceGained(500);
					break;
			case 2:
					resourceGained(1250);
					break;
			case 3:
					resourceGained(3000);
					break;
		}
	}

	public void setEasy()
	{
		difficulty = 1;
		jobSelected();

		jobTimer = Time.time;
	}

	public void setMedium()
	{
		difficulty = 2;
		jobSelected();

		jobTimer = Time.time;
	}

	public void setHard()
	{
		difficulty = 3;
		jobSelected();

		jobTimer = Time.time;
	}
}
