using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSpawning : Unit
{
    SpriteRenderer sr; 
    PolygonCollider2D pc; 
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        pc = gameObject.GetComponent<PolygonCollider2D>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f); 
        pc.enabled = false; 
    }

    public override void initialise() {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        pc.enabled = true; 
    }
}
