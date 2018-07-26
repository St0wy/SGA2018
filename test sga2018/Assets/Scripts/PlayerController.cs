using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private float Vertical;
    private float VerticalFire;
    private float Horizontal;
    private float HorizontalFire;
    private float Special;
    [SerializeField]
    private float shotPerSecond;
    private float rateOfFire;
    private float nextFireAllowed;

    private bool canFire = false;

    public int range = 3;
    public GameObject projectile;

    private const int FACTOR = 5;
    

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        rateOfFire = 1 / shotPerSecond;
        Vertical = Input.GetAxis("Vertical");
        VerticalFire = Input.GetAxis("VerticalFire");
        Horizontal = Input.GetAxis("Horizontal");
        HorizontalFire = Input.GetAxis("HorizontalFire");
        Special = Input.GetAxis("Special");

        transform.position += new Vector3(Horizontal/FACTOR, Vertical/FACTOR, 0);
        //special
        if (VerticalFire > 0 && Special > 0)
        {
            
        }

      
        canFire = false;
        if (Time.time >= nextFireAllowed)
        {
            nextFireAllowed = Time.time + rateOfFire;   
            canFire = true;
        }
        if (canFire)
        {
            if (VerticalFire == 0 && HorizontalFire == 0)
                return;
            GameObject projectil = Instantiate(projectile, transform.position, Quaternion.identity);
            //up
            if (VerticalFire > 0 && Special == 0)
            {
               
                projectil.GetComponent<Projectile>().direction = Vector2.up;
                projectil.GetComponent<Projectile>().range = range;
            }
            //down
            else if (VerticalFire < 0 && Special == 0)
            {
                projectil.GetComponent<Projectile>().direction = Vector2.down;
                projectil.GetComponent<Projectile>().range = range;
            }
            //right
            else if (HorizontalFire > 0 && Special == 0)
            {
                projectil.GetComponent<Projectile>().direction = Vector2.right;
                projectil.GetComponent<Projectile>().range = range;
            }
            //left
            else if (HorizontalFire < 0 && Special == 0)
            {
                projectil.GetComponent<Projectile>().direction = Vector2.left;
                projectil.GetComponent<Projectile>().range = range;
            }
          
        }
    }
}
