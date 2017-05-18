using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contract : MonoBehaviour {


    public int jobLevel;
    public int wantedLevel;
    public float timer;

    public Contract(int jobLevel_, int wantedLevel_)
    {
        jobLevel = jobLevel_;
        wantedLevel = wantedLevel_;
        timer = 0.0f;

    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void incrementTimer()
    {
        timer += Time.deltaTime;
    }
}
