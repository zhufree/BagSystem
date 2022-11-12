using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Item slotItem;
    public Image slotImage;
    public Image slotBg;
    public Sprite unSelectedBg;
    public Sprite selectedBg;
    public TMP_Text slotName;
    public TMP_Text slotCount;

    public void SetItem(Item item) {
        slotItem = item;
        if (item != null) {
            slotBg.sprite = unSelectedBg;
            slotImage.sprite = item.itemImage;
            slotName.text = item.itemName;
            slotCount.text = item.itemCount.ToString();
        } else {
            slotBg.sprite = unSelectedBg;
            slotImage.sprite = null;
            slotName.text = "";
            slotCount.text = "";
        }
    }

    public void SetBg(bool select) {
        if (select) {
            slotBg.sprite = selectedBg;
        } else {
            slotBg.sprite = unSelectedBg;
        }
    }

    public void ItemOnClicked() {
        InventoryManager.SelectItem(slotItem);
    }
    
    public void onSelect(bool select) {
        slotImage.GetComponent<Draggable>().setSelect(select);
    }

    public bool isDragging() {
        return slotImage.GetComponent<Draggable>().dragging;
    }

    public void useItem() {
        SetItem(null);
    }
}
