using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continentclicker : MonoBehaviour {


    public WindowManager winMan;
 
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (winMan.GUIopen == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                             Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);
                if (hit)
                {
                    hit.transform.GetComponent<popUp>().call();
                    gameObject.GetComponent<AudioSource>().Play();
                }
            }
        }
    }

}

