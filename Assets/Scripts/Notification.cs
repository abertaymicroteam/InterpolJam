using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {

    public Text text;
    public SpriteRenderer rend;
    public Sprite[] pics = new Sprite[4];
    public Sprite horry, jock, jimmy, michel;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        pics[0] = horry;
        pics[1] = jock;
        pics[2] = jimmy;
        pics[3] = michel;
    }

    public void dismiss()
    {
        gameObject.SetActive(false);

    }

    public void show(int level)
    {
        gameObject.SetActive(true);
        switch (level)
        {
            case 1:
                rend.sprite = horry;
                text.text = "Boss, one of our workers spotted the authorities raiding warehouses near one of our supply spots. What should we do? \n A : Get The Supplies Out. \n B : Burn Down The Building.";
                break;
            case 2:
                rend.sprite = jock;
                text.text = "Boss, I’ve got an investigator tailing me towards our stash. What should I do? \n A : Try To Lose Them. \n B : Pay Them Off.";
                break;
            case 3:
                rend.sprite = michel;
                text.text = "Boss, there’s a rumour that the customer I’m about to meet might be a snitch. How should I handle them? \n A : Continue As Planned. \n B: Chase Them Off.";
                break;
        }
        
    }
}
