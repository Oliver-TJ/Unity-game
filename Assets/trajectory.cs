using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trajectory : MonoBehaviour
{
    [SerializeField] float speed; 
    [SerializeField] float activeTime; 
    private Rigidbody2D rb; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.forward*speed; 
    }

    void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider other) {
        
    }
}
