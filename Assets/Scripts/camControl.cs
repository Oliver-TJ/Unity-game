using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camControl : MonoBehaviour
{
    [SerializeField] private float maxCameraSpeed; 
    [SerializeField] private Vector2 Edges;
    private Camera view; 
    float xShift = 0; 
    float yShift = 0; 
    float xRange; 
    float yRange; 
    Vector2 pos; 
    // Start is called before the first frame update
    void Start()
    {
        view = gameObject.GetComponent<Camera>();
        yRange = view.orthographicSize; 
        xRange = (yRange*Screen.width / Screen.height);
        pos = view.transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x < 0.05f * Screen.width || Input.mousePosition.x > 0.95f * Screen.width ||
            Input.mousePosition.y < 0.05f * Screen.height || Input.mousePosition.y > 0.95f * Screen.height) {
            xShift = 10 * (Input.mousePosition.x - (Screen.width * 0.5f)) /
                     ((Screen.width * 0.5f) + (Input.mousePosition.x < 0.5f * Screen.width ? 1 : -1) * 0.9f) *
                     maxCameraSpeed * Time.deltaTime;
            yShift = 10 * (Input.mousePosition.y - (Screen.height * 0.5f)) /
                     ((Screen.height * 0.5f) + (Input.mousePosition.y < 0.5f * Screen.height ? 1 : -1) * 0.9f) *
                     maxCameraSpeed * Time.deltaTime;
        }

        xShift = Mathf.Clamp(xShift,
            xRange - Edges.x - pos.x,
            Edges.x - pos.x - xRange);
        yShift = Mathf.Clamp(yShift, yRange - pos.y - Edges.y, Edges.y - pos.y - yRange);
        view.transform.Translate(xShift, yShift, 0);
        xShift = 0; 
        yShift = 0; 
    }
}
