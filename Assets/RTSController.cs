using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSController : MonoBehaviour
{
    [SerializeField] private GameObject King; 
    [SerializeField] private GameObject pathPointer; 
    [SerializeField] private Camera view; 
    private Actions kingMethods; 
    // Start is called before the first frame update
    void Start()
    {
        kingMethods = King.GetComponent<Actions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            // Add ability functionality in future for Q, W, R, E
            Debug.Log($"Q down at {view.ScreenToWorldPoint(Input.mousePosition)}");
        }

        if (Input.GetMouseButtonDown(1)) {
            Vector2 intent = (Vector2)view.ScreenToWorldPoint(Input.mousePosition); 
            Instantiate(pathPointer, new Vector3(intent.x, intent.y, 0), Quaternion.identity);
            kingMethods.setIntent(intent); 
            Debug.Log($"Mouse button down at {Input.mousePosition}");


        }
    }
}
