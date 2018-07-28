using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;

    [SerializeField] [Range(1, 500)] private float speed;

    private Transform target;
    private float tempX = 0f;
    
    // Use this for initialization
    void Start()
    {
        tempX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D enemyRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 velocity = (transform.position - target.position).normalized * speed * 10 * Time.deltaTime;

        enemyRigidBody.velocity = -velocity;
    }
}