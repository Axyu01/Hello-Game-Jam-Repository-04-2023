using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory",menuName ="Player Data/Inventory")]
public class Inventory : ScriptableObject
{
    public List<int> keys= new List<int>();
    // Start is called before the first frame update
    public bool hasKey(int key)
    {
        foreach(int i in keys) { if (i== key) return true; }
        return false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
