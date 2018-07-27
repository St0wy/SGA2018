using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public int damage;
    public GameObject player;

    [SerializeField] [Range(1, 100)] private float speed;

    private Transform target;
    private int maxHealth;
    private Health scriptHealth;
    private bool isAngry = false;

    // Use this for initialization
    void Start()
    {
        scriptHealth = GetComponent<Health>();
        maxHealth = scriptHealth.health;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D enemyRigidBody = GetComponent<Rigidbody2D>();
        target = player.transform;
        
        int health = scriptHealth.health;

        if(health <= maxHealth / 2 && !isAngry)
        {
            speed += 20;
            isAngry = true;
        }
        Vector2 velocity = (transform.position - target.position).normalized * speed * 10 * Time.deltaTime;

        enemyRigidBody.velocity = -velocity;
    }

}