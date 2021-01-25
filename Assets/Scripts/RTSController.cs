using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RTSController : MonoBehaviour
{
    [SerializeField] private GameObject King; 
    [SerializeField] private GameObject pathPointer; 
    [SerializeField] private GameObject aimIndicator;
    [SerializeField] private GameObject fireball;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject archers;
    [SerializeField] private Tile highlightTile;
    [SerializeField] private Camera view;
    [SerializeField] private Grid grid;
    [SerializeField] private KeyCode[] keyset; 
    
    private Ability[] abilitySet; 
    private Vector3[] abilityPos; 
    private List<GameObject>[] abilityUI;
    private Actions kingMethods; 
    private Rigidbody2D kingRB;
    private Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        kingMethods = King.GetComponent<Actions>();
        kingRB = King.GetComponent<Rigidbody2D>();
        abilitySet = new Ability[] { Fireball, Archers };
        abilityPos = new Vector3[] { Vector3.zero, Vector3.zero };
        abilityUI = new List<GameObject>[] { new List<GameObject>(), new List<GameObject>() };
        tilemap = grid.transform.GetChild(0).gameObject.GetComponent<Tilemap>();
        tilemap.SetTile(new Vector3Int(1, 1, 0), highlightTile);
        Debug.Log($"Set tile at {tilemap.WorldToCell(new Vector3Int(1000, 1000, 10))}.");
        Debug.Log($"Set tile at {tilemap.WorldToCell(new Vector3Int(1, 1, -10))}.");
        Debug.Log($"Set tile at {tilemap.WorldToCell(new Vector3Int(1, 1, 0))}.");
        Debug.Log($"Set tile at {tilemap.WorldToCell(new Vector3Int(-1, 1, 0))}.");
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

    void Archers(int keyInd)
    {
        bool validTile = false; // This is a value that will never be reached
        if (Input.GetKey(keyset[keyInd])) {
            Vector3 pos = view.ScreenToWorldPoint(Input.mousePosition); // Get current mouse position 
            // Get the square corresponding to the mouse position 
            pos = new Vector3((float)Math.Floor(pos.x), (float)Math.Floor(pos.y), 0); 
            Debug.Log(pos);
            Vector3Int intPos = Vector3Int.FloorToInt(pos);
            if (pos != abilityPos[keyInd] && tilemap.GetTile(intPos) == null) // Check that the position has changed and it is now only a tile which isn't an obstacle
            {
                if (validTile) 
                    tilemap.SetTile(Vector3Int.FloorToInt(abilityPos[keyInd]), null); // Set previous tile to null 
                tilemap.SetTile(tilemap.WorldToCell(intPos), highlightTile); // Set current tile to be highlighted
                abilityPos[keyInd] = pos;
                validTile = true; 
                Debug.Log($"Created tile at {pos}");
            }
        }

        if (Input.GetKeyUp(keyset[keyInd]))
        {
            Vector2Int pos = Vector2Int.FloorToInt(view.ScreenToWorldPoint(Input.mousePosition));
            tilemap.SetTile(Vector3Int.FloorToInt(abilityPos[keyInd]), null);
            Instantiate(archers,  new Vector3(pos.x+0.5f, pos.y+0.5f, 0), Quaternion.identity);
            Debug.Log($"Instantiated archers at {new Vector3(pos.x, pos.y, 0)}");
        }
    }
}