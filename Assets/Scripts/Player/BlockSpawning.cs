using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawning : MonoBehaviour
{
    [SerializeField] float duration; 
    SpriteRenderer sr;
    float remainingTime; 
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f); 
        remainingTime = duration; 
    }

    void Update() { 
        remainingTime -= Time.deltaTime; 
        if (remainingTime <= 0) 
            Destroy(gameObject, 0);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.3f + 0.7f*remainingTime/duration); 
    }
}
