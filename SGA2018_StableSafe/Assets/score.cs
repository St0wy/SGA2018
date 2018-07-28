using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

    public Text time;
    public Text sc;

    private float current;

    private float secondes;
    private float minutes;

    private string min;
    private string sec;

    public int points = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        current = Time.timeSinceLevelLoad;

        minutes = (float)Math.Floor(current / 60);
        secondes = (float)Math.Round(current % 60);

        if (minutes < 10)
        {
            min = "0" + minutes.ToString();
        }
        else
        {
            min = minutes.ToString();
        }

        if (secondes < 10)
        {
            sec = "0" + Math.Floor(secondes).ToString();
        }
        else
        {
            sec = secondes.ToString();
        }

        time.text = "TIME : " + min + ":" + sec;
        points = ((int)current * 100);
        sc.text = "SCORE : " + ((current * 100)).ToString().PadLeft(5, '0').Substring(0, 5);
	}
}
