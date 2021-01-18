using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trajectory : MonoBehaviour
{
    [SerializeField] float speed; 
    [SerializeField] float activeTime; 
    [SerializeField] GameObject explosion; 
    private Rigidbody2D rb; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up*speed; 
    }

    void FixedUpdate()
    {
        activeTime -= Time.fixedDeltaTime; 
        if (activeTime <= 0) { 
            Explode();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Collision detected"); 
        Explode();
    }

    private void Explode() {
        Destroy(gameObject, 0);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
