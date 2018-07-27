using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    public bool enemy = true;
    [SerializeField] private int health = 0;
    public GameObject Blood_onDeath;
    public GameObject Blood_onHit;

    // Use this for initialization
    void Start()
    {
        if (enemy)
        {
            health = 2;
        }
        else if (!enemy)
        {
            health = 6;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            Die();
        }
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
        health -= 1;



        Debug.Log(health + " --- " + this.gameObject);

        if (Blood_onHit.GetComponent<ParticleSystem>().isStopped)
            Blood_onHit.SetActive(false);
    }

    private void Die()
    {
        if (this.enemy)
        {
            Blood_onDeath.SetActive(true);
            Blood_onDeath.GetComponent<ParticleSystem>().Play();
            Destroy(this.gameObject, 0.23f);

            if (Blood_onDeath.GetComponent<ParticleSystem>().isStopped)
                Blood_onDeath.SetActive(false);

            return;
        }

        
            SceneManager.LoadScene("Death");
    }
}
