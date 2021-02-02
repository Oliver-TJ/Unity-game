using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    // This refers to anything which the player may place down that will remain present
    public abstract void initialise();
}
