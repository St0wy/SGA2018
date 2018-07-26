using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;
    [SerializeField] [Range(1,500)] float speed;
    public GameObject player;
    public Rigidbody2D enemyRigidBody;

    [SerializeField]
    private int lifePoint;


    private Transform target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        target = player.transform;
        Vector2 velocity = (transform.position - target.position).normalized * speed * 10 * Time.deltaTime;

        enemyRigidBody.velocity = -velocity;
    }

    public void MoveEnemy()
    {
        
    }
}