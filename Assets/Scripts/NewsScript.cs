using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsScript : MonoBehaviour {

    public WindowManager winMan;
    public AudioSource audioSource;
    public Sprite escape, suspicious, caught;
	// Use this for initialization
	void Start () {
		
	}

	
	// Update is called once per frame
	void Update () {

	}

    public void dismiss()
    {
        audioSource.Play();
        gameObject.SetActive(false);
        winMan.DismissedNewsEvent();       
    }

    public void show(int level)
    {

        switch (level)
        {
            case 1:
                gameObject.GetComponent<SpriteRenderer>().sprite = suspicious;
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = caught;
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().sprite = escape;
                break;
        }
        gameObject.SetActive(true);
    }
}
