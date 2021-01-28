using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour 
{
    // This class should be inherited by any member that can take damage. All specifics should be handled within the damaging object, this merely acts as an interface. 
    protected float _maxHealth; 
    protected float health; 
    protected Transform t; 
    protected GameObject[] _respawnList; 
    protected int checkPoint; 
    public void takeDamage(float d) { 
        health -= d;
        Debug.Log(health); 
        if (health <= 0) 
            Death(); 
    }

    protected void Death() {
        t.position = _respawnList[checkPoint].transform.position;
        health = _maxHealth; 
    }
}
