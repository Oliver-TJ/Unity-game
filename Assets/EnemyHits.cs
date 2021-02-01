using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHits : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float findRange;
    [SerializeField] Transform player;
    [SerializeField] float stoppingDistance;
    [SerializeField] float dashSpeed;
    private Vector2 tempPos;
    private Vector2 targetPos;
    private Rigidbody2D rb;
    private bool detected;
    private bool primed;
    
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 tempPos = new Vector2(transform.position.x, transform.position.y);
        detected = false;
        primed = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 tempPos = new Vector2(transform.position.x, transform.position.y);

        if (detected == false && Vector2.Distance(transform.position, player.position) < findRange)
        {
            detected = true;
        }

        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                targetPos = player.position - transform.position;
                rb.MovePosition((Vector2)transform.position + targetPos * speed * Time.deltaTime);
                
                if (primed == false)
                {
                    primed = true;
                }
            }
            /*
            else
            {
                if (primed == true && Vector2.Distance(transform.position, player.position) <= stoppingDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, dashSpeed * Time.deltaTime);
                }
            }
            */
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        primed = false;
        rb.velocity = new Vector2(0.0f, 0.0f);
        rb.angularVelocity = (0.0f);
    }
}