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
    public Animation body;
    public Animation head;
    public string walk;
    public string head_bank;

    private const int FACTOR = 10;

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

        //transform.position += new Vector3(Horizontal/FACTOR, Vertical/FACTOR, 0);
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        rb.velocity = new Vector3(Horizontal * FACTOR, Vertical * FACTOR, 0);

        if (Vertical != 0 || Horizontal != 0)
        {
            //body.Play(walk);
            //head.Play(head_bank);
        }
        else
        {
            //body.enabled = false;
            //head.enabled = false;
        }

        //special
        if (VerticalFire > 0 && Special > 0)
        {
            
            
        }
        if (VerticalFire > 0)
        {
            GameObject go = Instantiate(projectile, transform);
            //go.GetComponent<Rigidbody2D>().AddForce(transform.up*750);
            //Destroy(go, range);

        }
    }
}
