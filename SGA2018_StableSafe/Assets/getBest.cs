using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getBest : MonoBehaviour {

    public Text best;

	// Use this for initialization
	void Start () {
        best.text = "BEST : " + PlayerPrefs.GetInt("Score").ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
