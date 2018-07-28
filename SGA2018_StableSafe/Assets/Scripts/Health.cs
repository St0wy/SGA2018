using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public bool enemy = true;
    public bool dead = false;
    public int health_point = 0;
    public GameObject Blood_onDeath;
    public GameObject Blood_onHit;

    public Image pv1;
    public Image pv2;
    public Image pv3;
    public Image pv4;
    public Image pv5;
    public Image pv6;

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
        if (this.health_point <= 0 && !enemy)
            Die();
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

        if (gameObject.tag == "Player")
        {
            switch (health_point)
            {
                case 6:
                    pv1.enabled = true;
                    pv2.enabled = true;
                    pv3.enabled = true;
                    pv4.enabled = true;
                    pv5.enabled = true;
                    pv6.enabled = true;
                    break;
                case 5:
                    pv1.enabled = true;
                    pv2.enabled = true;
                    pv3.enabled = true;
                    pv4.enabled = true;
                    pv5.enabled = true;
                    pv6.enabled = false;
                    break;
                case 4:
                    pv1.enabled = true;
                    pv2.enabled = true;
                    pv3.enabled = true;
                    pv4.enabled = true;
                    pv5.enabled = false;
                    pv6.enabled = false;
                    break;
                case 3:
                    pv1.enabled = true;
                    pv2.enabled = true;
                    pv3.enabled = true;
                    pv4.enabled = false;
                    pv5.enabled = false;
                    pv6.enabled = false;
                    break;
                case 2:
                    pv1.enabled = true;
                    pv2.enabled = true;
                    pv3.enabled = false;
                    pv4.enabled = false;
                    pv5.enabled = false;
                    pv6.enabled = false;
                    break;
                case 1:
                    pv1.enabled = true;
                    pv2.enabled = false;
                    pv3.enabled = false;
                    pv4.enabled = false;
                    pv5.enabled = false;
                    pv6.enabled = false;
                    break;
            }
        }

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

        if (GetComponent<score>().points > PlayerPrefs.GetInt("Score"))
        {
            PlayerPrefs.SetInt("Score", GetComponent<score>().points);
            PlayerPrefs.SetString("Time", GetComponent<score>().time.text);
        }

        SceneManager.LoadScene("Death");
    }


}
