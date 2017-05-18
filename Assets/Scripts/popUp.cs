using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popUp : MonoBehaviour {


    public WindowManager winMan;
    public int value;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void call()
    {
        winMan.chooseWindow(value);
    
       
    }


}
