using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : Shootable
{
    [SerializeField] private Vector3 startPos; 
    [SerializeField] private float speed; 
    [SerializeField] private GameObject[] respawnList; 
    [SerializeField] private float pMaxHealth; 
    [SerializeField] private float dashSpeed; 
    private Transform t;
    private Vector3 intention; 
    private Vector2[] movements; 
    private int moveIndex = 0; // remaining iterations (ri*20 = remaining time in milliseconds)
    private Rigidbody2D rb;
    private bool complete = true; 
    private int checkpoint = 0;
    // Start is called before the first frame update
    private void Start()
    {
        intention = startPos; 
        rb = gameObject.GetComponent<Rigidbody2D>();
        t = gameObject.GetComponent<Transform>();
        rb.position = startPos;
        maxHealth = pMaxHealth; 
        health = maxHealth; 
    }

    private void FixedUpdate()
    {
        if (!complete) 
            approachPoint(intention);
    }

    private void approachPoint(Vector2 position) {
        if (moveIndex < movements.Length) {
            // rb.AddForce(movements[moveIndex]);
            rb.velocity = 50*movements[moveIndex];
            Debug.Log(rb.velocity);
            // t.Translate(movements[moveIndex]);
            moveIndex++;
        } else {
            rb.velocity = Vector2.zero; 
            moveIndex = 0; 
            complete = true; 
        }
    }

    public void setIntent(Vector2 intent, bool dashing = false) {
        // Determine the sequence of movements 
        float ts = dashing ? dashSpeed : speed; 
        float m = (intent-rb.position).magnitude;
        int ti = dashing ? 5 : (int)(m/ts)+1; // Gets the number of iterations (final iteration may be 0,0)
        Vector2 s = (intent - rb.position) / m; // Unit direction (s.magnitude = 1)
        movements = new Vector2[ti]; 
        movements[ti-1] = dashing? Vector2.zero : s*ts*(m/ts-(ti-1));
        for (int i = 0; i < ti-1; i++) {
            movements[i] = s*ts;
        }
        moveIndex = 0; 
        complete = false; 
        intention = intent; 
    }

    override protected void Death() { 
        t.position = respawnList[checkpoint].transform.position;
        health = maxHealth;
        rb.velocity = Vector2.zero; 
    }
}
