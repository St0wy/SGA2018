using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Vector2 direction = Vector2.zero;
    public float range = 3;
    public bool enemy = true;
    [SerializeField]
    private float speedMultiplier = 2f;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().AddForce(direction/50 * speedMultiplier);
        Destroy(this.gameObject, range);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
