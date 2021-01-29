using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapturePoint : Interactable
{
    public bool isCaptured = false;
    public float timeToCap = 10;
    [SerializeField] public Slider slider;
    private float _duration;

    void Update()
    {
        slider.value = _duration;
        if (playerInRange && timeToCap > 0)
        {
            timeToCap -= Time.deltaTime;
            _duration += Time.deltaTime/10;
        }

        if (!playerInRange)
        {
            timeToCap = 10;
            _duration = 0;
        }
    }

    public void Capping(float timeToCap)
    {
        if(timeToCap > 0)
        {
            isCaptured = true;
            Debug.Log("Captured");
        }
    }
}
