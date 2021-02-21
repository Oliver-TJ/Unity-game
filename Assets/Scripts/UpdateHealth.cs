using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealth : MonoBehaviour
{
    RTSController playerScript; 
    Text healthText; 
    // Start is called before the first frame update
    void Start()
    {
        playerScript = transform.parent.parent.GetComponentInChildren<RTSController>();
        healthText = gameObject.GetComponent<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = playerScript.getHealth.ToString(); 
    }
}
