using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour {
    static InventoryManager instance; // 单例管理

    public GameObject myBag; // canvas下的bag game object UI
    public GameObject slotGrid; // 放物品的网格game Object UI

    public Inventory playerBag; // 背包scriptableObject DATA
    public Item selectedItem; // 背包里物品的scriptableObject DATA

    public Slot slotPrefab; // 与Prefab对应的数据结构 DATA

    public TMP_Text itemInformation;

    public List<Slot> slotList = new List<Slot>();

    void Awake() {
        if (instance != null) {
            Destroy(this);
        }
        instance = this;
    }
    private void OnEnable() {
        for (int i = 0; i < 8; i++) {
            CreateNewSlot();
        }
        RefreshItem();
        instance.itemInformation.text = "";
    }

    void Update()
    {
        OpenMyBag();
    }

    void OpenMyBag() {
        if (Input.GetKeyDown(KeyCode.O)) {
            myBag.SetActive(!myBag.activeSelf);
        }
    }

    public static void AddBagItem(Item item) {
        // add item
        if (instance.playerBag.itemList.Contains(item)) {
            item.itemCount += 1;
        } else {
            instance.playerBag.itemList.Add(item);
            instance.itemInformation.text = item.itemInformation;
        }
        InventoryManager.RefreshItem();
    }

    public static void RefreshItem() {
        for (int i = 0; i < instance.slotList.Count; i++) {
            if (instance.playerBag.itemList.Count > i) {
                instance.slotList[i].SetItem(instance.playerBag.itemList[i]);
            } else {
                instance.slotList[i].SetItem(null);
            }
            if (instance.slotList[i].slotItem != null && instance.slotList[i].slotItem == instance.selectedItem ) {
                instance.slotList[i].SetBg(true);
            }
        }
    }

    public static void CreateNewSlot() {
        Slot newSlot = Instantiate(instance.slotPrefab, 
            instance.slotGrid.transform.position, 
            Quaternion.identity);
        newSlot.SetBg(false);
        newSlot.gameObject.transform.SetParent(instance.slotGrid.transform);
        instance.slotList.Add(newSlot);
    }

    public static void SelectItem(Item item) {
        if (item == null) return;
        instance.selectedItem = item;
        instance.itemInformation.text = item.itemInformation;
        InventoryManager.RefreshItem();
    }

    public static void UseItem() {
        if (!instance.selectedItem) return;
        if (instance.selectedItem.itemCount > 1) {
            instance.selectedItem.itemCount -= 1;
        } else {
            instance.playerBag.itemList.Remove(instance.selectedItem);
            instance.selectedItem = null;
        }
        InventoryManager.RefreshItem();
    }
}
