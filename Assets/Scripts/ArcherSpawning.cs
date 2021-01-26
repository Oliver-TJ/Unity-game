using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherSpawning : MonoBehaviour
{
    Rigidbody2D[] rbs;
    SpriteRenderer[] srs;
    PolygonCollider2D[] pcs; 
    // Start is called before the first frame update
    void Start()
    {
        rbs = this.transform.GetComponentsInChildren<Rigidbody2D>();
        srs = this.transform.GetComponentsInChildren<SpriteRenderer>();
        pcs = this.transform.GetComponentsInChildren<PolygonCollider2D>();
        foreach (SpriteRenderer sr in srs) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f); 
        }
        foreach(PolygonCollider2D pc in pcs) { 
            pc.enabled = false; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initialise() {
        foreach (SpriteRenderer sr in srs) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        }
        foreach(PolygonCollider2D pc in pcs) { 
            pc.enabled = true; 
        }
    }
}
