using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public bool enemy = true;
    public bool dead = false;
    public int health_point = 0;
    public GameObject Blood_onDeath;
    public GameObject Blood_onHit;

    // Use this for initialization
    void Start()
    {
        if (enemy)
        {
            health_point = 2;
        }
        else if (!enemy)
        {
            health_point = 6;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            return;
        }

        if (this.enemy == collision.otherCollider.gameObject.GetComponent<Health>().enemy && collision.gameObject.tag == "Player")
            return;

        Blood_onHit.SetActive(true);
        Blood_onHit.GetComponent<ParticleSystem>().Play();
        health_point -= 1;

        if (Blood_onHit.GetComponent<ParticleSystem>().isStopped)
            Blood_onHit.SetActive(false);
    }

    public void Die()
    {
        
        if (this.enemy)
        {
            Blood_onDeath.SetActive(true);
            Blood_onDeath.GetComponent<ParticleSystem>().Play();

            if (Blood_onDeath.GetComponent<ParticleSystem>().isStopped)
                Blood_onDeath.SetActive(false);

            gameObject.SetActive(false);
            return;
        }
        
        SceneManager.LoadScene("Death");
    }

    
}
