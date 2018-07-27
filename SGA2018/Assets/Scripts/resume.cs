using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resume : MonoBehaviour {

    public Canvas escMenu;

	public void resumeGame()
    {
        Debug.Log("awsd");
        escMenu.enabled = false;
        Time.timeScale = 1;
    }
}
