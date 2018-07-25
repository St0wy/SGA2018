using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private float Vertical;
    private float VerticalFire;
    private float Horizontal;
    private float HorizontalFire;
    private float Special;

    public int range = 3;
    public GameObject projectile;

    private const int FACTOR = 5;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vertical = Input.GetAxis("Vertical");
        VerticalFire = Input.GetAxis("VerticalFire");
        Horizontal = Input.GetAxis("Horizontal");
        HorizontalFire = Input.GetAxis("HorizontalFire");
        Special = Input.GetAxis("Special");

        transform.position += new Vector3(Horizontal/FACTOR, Vertical/FACTOR, 0);
        transform.rotation = Quaternion.Euler(0, 0, 180);
        //special
        if (VerticalFire > 0 && Special > 0)
        {
            
        }

        if (VerticalFire > 0)
        {

        }
        else if (VerticalFire < 0)
        {

        }
        else if (HorizontalFire > 0)
        {

        }
        else if (HorizontalFire < 0)
        {

        }
    }
}
