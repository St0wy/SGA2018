using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {

    public bool enemy = true;
    private int health = 0;
    public ParticleSystem Blood_onDeath;
    public ParticleSystem Blood_onHit;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (health == 0)
        {
            Die();
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.enemy == collision.otherCollider.gameObject.GetComponent<Health>().enemy)
            return;

        Blood_onHit.gameObject.SetActive(true);
        if (Blood_onHit.isStopped)
            Blood_onHit.gameObject.SetActive(false);
        health -= 1;
        Debug.Log(health + " --- " + this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.enemy == collider.gameObject.GetComponent<Health>().enemy)
            return;

        Blood_onHit.gameObject.SetActive(true);
        if (Blood_onHit.isStopped)
            Blood_onHit.gameObject.SetActive(false);
        health -= 1;
        Debug.Log(health + " --- " + this.gameObject);
    }

    private void Die()
    {
        if (this.enemy)
        {
            Blood_onDeath.gameObject.SetActive(true);
            Destroy(this.gameObject,0.23f);
            return;
        }
        Blood_onDeath.gameObject.SetActive(true);
        if (Blood_onDeath.isStopped)
            SceneManager.LoadScene("Death");
    }
}
