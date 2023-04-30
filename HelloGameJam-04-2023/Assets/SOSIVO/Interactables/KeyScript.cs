using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : InteractableObject
{
    [SerializeField]
    int keyIndex;
    [SerializeField]
    Inventory inventory;
    public void Start()
    {
        if (inventory == null)
            return;
        inventory.keys.Clear();
    }
    public override void onInteraction()
    {
        if (inventory == null)
            return;
        inventory.keys.Add(keyIndex);
        Destroy(gameObject);
    }
}
