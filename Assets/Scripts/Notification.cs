using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {

    public Text text;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
                text.text = "";
                break;
            case 2:
                text.text = "";
                break;
            case 3:
                text.text = "";
                break;
        }
        
    }
}
