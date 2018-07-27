using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour {

    public Canvas escMenu;

	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Cancel") > 0)
        {
            escMenu.enabled = true;
            Time.timeScale = 0;
        }
	}
}
