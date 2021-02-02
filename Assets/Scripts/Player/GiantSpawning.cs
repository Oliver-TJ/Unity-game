using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSpawning : Unit
{
    SpriteRenderer sr; 
    PolygonCollider2D pc;
    bool inBlock = false; 
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        pc = gameObject.GetComponent<PolygonCollider2D>();
        pc.isTrigger = true;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f); 
    }

    public override void initialise() {
        if (!inBlock) {
            pc.isTrigger = false; 
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        } else {
            Destroy(gameObject, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.GetComponent<Rigidbody2D>()?.bodyType == RigidbodyType2D.Kinematic) {
            inBlock = true; 
        }
    }

    void OnTriggerEnter2D() {
        inBlock = false; 
    }
}
