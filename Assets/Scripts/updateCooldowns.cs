using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateCooldowns : MonoBehaviour
{
    RTSController playerScript; 
    Text dashText; 
    // Start is called before the first frame update
    void Start()
    {
        playerScript = transform.parent.parent.parent.GetComponentInChildren<RTSController>();
        dashText = gameObject.GetComponent<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        dashText.text = playerScript.getDashCD; 
    }
}
