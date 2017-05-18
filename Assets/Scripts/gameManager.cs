using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {

	public Text moneyDisplay;
	public GameObject bankruptText;
	public float money;
	private float timeEntered;
	private int difficulty;
	private float jobTimer;
	private bool jobActive;
    public WindowManager WinMan;
    public int[,] Workers = new int[6,3];
    public int[,] prevOnJob = new int[6,3];
	// Use this for initialization
	void Start () 
	{
        
		money = 300;
		moneyDisplay.text = "$" + money;
		jobActive = false;

        for(int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                prevOnJob[i,j] = 0;
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
        checkWorkers();

        if ((Time.time - timeEntered) > 3)
        {
            bankruptText.SetActive(false);
        }


        if ((Workers[0,0] + Workers[0,1] + Workers[0, 2] + Workers[1, 0] + Workers[1, 1] + Workers[1, 2] + Workers[2, 0] + Workers[2, 1] + Workers[2, 2] + Workers[3, 0] + Workers[3, 1] + Workers[3, 2] + Workers[4, 0] + Workers[4, 1] + Workers[4, 2] + Workers[5, 0] + Workers[5, 1] + Workers[5, 2]) > 0)
        {
            if(jobActive == false)
            {
                jobTimer = Time.time;
            }
            jobActive = true;
        }

        if (((Time.time - jobTimer) > 5) && jobActive == true)
        {
            jobComplete();
            jobActive = false;
        }

        for ( int i = 0; i < 6; i ++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Workers[i,j] > prevOnJob[i,j])
                {
                    jobSelected(j);
                }
            }
        }

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                prevOnJob[i,j] = Workers[i,j];
            }
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

	public void bankruptcyEntered()
	{
		timeEntered = Time.time;
		bankruptText.SetActive(true);
	}

    public void jobSelected(int difficulty)
    {
        switch (difficulty) {
            case 0:
                resourcePurchased(250);
                break;
            case 1:
                resourcePurchased(500);
                break;
            case 2:
                resourcePurchased(1000);
                break;
        }
	}

	public void jobComplete ()
	{
        for (int i = 0; i < 6; i++)
        {
            resourceGained((25 * Workers[i, 0]) + (50 * Workers[i, 1]) + (100 * Workers[i, 2]));
        }
	}

    public void checkWorkers()
    {
        for (int i = 0; i < 6; i++)
        {
            Workers[i, 0] = WinMan.getEasy(i);
            Workers[i, 1] = WinMan.getMedium(i);
            Workers[i, 2] = WinMan.getHard(i);
        }
    }

}
