using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class archerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject enemyCheck() {
        GameObject enemy = null; 
        List<Collider2D> enemies = new List<Collider2D>();  
        int n = Physics2D.OverlapCircle((Vector2)transform.position, 5, new ContactFilter2D().NoFilter(), enemies);
        float minDist = float.PositiveInfinity;
        if (n > 0) {
            foreach (Collider2D e in enemies) { 
                float dist = (e.transform.position-transform.position).magnitude;
                if (e.tag == "Enemy" && dist < minDist ) {
                    minDist = dist;
                    enemy = e.gameObject;
                }
            }
        }
        return enemy; 
    }

    void TrackEnemy(GameObject e) {
        
    }
}
