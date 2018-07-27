using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;
    public GameObject player;
    public int lifePoint;

    [SerializeField] [Range(1, 100)] private float speed;

    private Transform target;
    
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D enemyRigidBody = GetComponent<Rigidbody2D>();
        target = player.transform;
        Vector2 velocity = (transform.position - target.position).normalized * speed * 10 * Time.deltaTime;

        enemyRigidBody.velocity = -velocity;
    }

}