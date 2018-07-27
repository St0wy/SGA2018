using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resume : MonoBehaviour {

    public Canvas escMenu;

	public void resumeGame()
    {
        Debug.Log("awsd");
        GameObject.FindGameObjectWithTag("pause").GetComponent<Canvas>().enabled = true;
        Time.timeScale = 1;
    }
}
