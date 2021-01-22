using System.Collections;
using System.Collections.Generic;
using System; 
using UnityEngine;

public class RTSController : MonoBehaviour
{
    [SerializeField] private GameObject King; 
    [SerializeField] private GameObject pathPointer; 
    [SerializeField] private GameObject aimIndicator;
    [SerializeField] private GameObject fireball;
    [SerializeField] private GameObject arrow; 
    [SerializeField] private Camera view; 
    [SerializeField] private KeyCode[] keyset; 
    
    private Ability[] abilitySet; 
    private Vector3[] abilityPos; 
    private List<GameObject>[] abilityUI;
    private Actions kingMethods; 
    private Rigidbody2D kingRB;
    // Start is called before the first frame update
    void Start()
    {
        kingMethods = King.GetComponent<Actions>();
        kingRB = King.GetComponent<Rigidbody2D>();
        abilitySet = new Ability[] { Fireball };
        abilityPos = new Vector3[] { new Vector3(0, 0, 0) };
        abilityUI = new List<GameObject>[] { new List<GameObject>() };
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < abilitySet.Length; i++) {
            abilitySet[i].DynamicInvoke(i);
        }

        if (Input.GetMouseButtonDown(1)) {
            Vector2 intent = (Vector2)view.ScreenToWorldPoint(Input.mousePosition); 
            Instantiate(pathPointer, new Vector3(intent.x, intent.y, 0), Quaternion.identity);
            kingMethods.setIntent(intent); 
            Debug.Log($"Mouse button down at {Input.mousePosition}");
        }

        if (Input.GetKey(KeyCode.Space)) {
            view.transform.Translate(kingRB.position - (Vector2)view.transform.position);
        }
    }

    delegate void Ability(int keyInd); 

    void Fireball(int keyInd) {
        if (Input.GetKeyDown(keyset[keyInd])) {
            Vector2 pos = view.ScreenToWorldPoint(Input.mousePosition);
            abilityPos[keyInd] = new Vector3(pos.x, pos.y, 0);
            GameObject tc = Instantiate(aimIndicator, abilityPos[keyInd], Quaternion.identity);
            GameObject ta = Instantiate(arrow, abilityPos[keyInd], Quaternion.identity);
            abilityUI[keyInd] = new List<GameObject>() { tc, ta };
            
        } else if (Input.GetKey(keyset[keyInd])) {
            Vector2 dp = view.ScreenToWorldPoint(Input.mousePosition) - abilityPos[keyInd];
            abilityUI[keyInd][1].transform.rotation = Quaternion.Euler(0, 0, 360f*(float)Math.Atan2(-(double)dp.x, (double)dp.y) / (2*(float)Math.PI));
            if (dp.magnitude > 1.75) {
                abilityUI[keyInd][1].transform.localScale = new Vector3(0.2f + 0.65f * (1 - (float)Math.Exp(-(dp.magnitude-1.8)/10)), 0.582f, 1);
            } else {
                abilityUI[keyInd][1].transform.localScale = new Vector3(0.2f, dp.magnitude / 1.8f * 0.582f, 1);
            }
        }

        if (Input.GetKeyUp(keyset[keyInd])) {
            Vector2 dp = view.ScreenToWorldPoint(Input.mousePosition) - abilityPos[keyInd];
            Instantiate(fireball, abilityPos[keyInd], Quaternion.Euler(0, 0, 360f*(float)Math.Atan2(-(double)dp.x, (double)dp.y) / (2*(float)Math.PI)));
            foreach (GameObject g in abilityUI[keyInd]) {
                Destroy(g);
            }
        }
    }
}