using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePoint : Interactable
{
    public bool isCaptured = false;
    public float timeToCap = 10;

    void Update(float timeToCap)
    {
        if (playerInRange && timeToCap > 0)
        {
            timeToCap -= Time.deltaTime;
        }
        if (!playerInRange)
            timeToCap = 10;

    }

    public void Capping(float timeToCap)
    {
        if(timeToCap > 0)
        {
            isCaptured == true;
            Debug.Log("Captured");
        }
    }



}
