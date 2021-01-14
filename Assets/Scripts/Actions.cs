using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    [SerializeField] private Vector3 startPos; 
    [SerializeField] private float speed; 
    private Vector3 intention; 
    private Vector2[] movements; 
    private int moveIndex = 0; // remaining iterations (ri*20 = remaining time in milliseconds)
    private Rigidbody2D rb;
    private bool complete = true; 
    Transform t; 
    // Start is called before the first frame update
    private void Start()
    {
        intention = startPos; 
        rb = gameObject.GetComponent<Rigidbody2D>();
        t = gameObject.GetComponent<Transform>();
        rb.position = startPos; 
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!complete) 
            approachPoint(intention);
    }

    private void approachPoint(Vector2 position) {
        if (moveIndex < movements.Length) {
            // rb.AddForce(movements[moveIndex]);
            rb.velocity = 50*movements[moveIndex];
            // t.Translate(movements[moveIndex]);
            moveIndex++;
        } else {
            rb.velocity = Vector2.zero; 
            moveIndex = 0; 
            complete = true; 
        }
    }

    public void setIntent(Vector2 intent) {
        // Determine the sequence of movements 
        float m = (intent-rb.position).magnitude;
        int ti = (int)(m/speed)+1; // Gets the number of iterations (final iteration may be 0,0)
        Vector2 s = (intent - rb.position) / m; // Unit direction (s.magnitude = 1)
        movements = new Vector2[ti]; 
        movements[ti-1] = s*speed*(m/speed-(ti-1));
        for (int i = 0; i < ti-1; i++) {
            movements[i] = s*speed;
        }
        moveIndex = 0; 
        complete = false; 
        intention = intent; 
        foreach (Vector2 v in movements) { 
            Debug.Log(v);
        }
        Debug.Log($"Moving to {intent} from {rb.position}"); 
    }
}
