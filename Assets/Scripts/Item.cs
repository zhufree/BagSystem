using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item", order = 0)]
public class Item : ScriptableObject {
    public string itemName;
    public Sprite itemImage;
    [TextArea]
    public string itemInformation;
    public int itemCount = 1;
}

