using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    float range = 1f;
	// Use this for initialization
	void Start () {
        Destroy(this, range);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
