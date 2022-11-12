using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;    

public class InventoryManager : MonoBehaviour {
    public static InventoryManager instance; // 单例管理

    public Image canvasBg;
    public GameObject myBag; // canvas下的bag game object UI
    public GameObject slotGrid; // 放物品的网格game Object UI
    public GameObject operationPanel;
    public TMP_Text itemName;
    public TMP_Text itemInformation;
    public GameObject useItemNotice;
    Transform operationPanelTransform;

    public Inventory playerBag = new Inventory(); // 背包Inventory Class DATA
    public Item selectedItem; // 背包里物品的Item Class DATA

    public Slot slotPrefab; // 与Prefab对应的数据结构 DATA

    public int maxSize = 16;

    public List<Slot> slotList = new List<Slot>();

    void Awake() {
        if (instance != null) {
            Destroy(this);
        }
        instance = this;
    }
    private void OnEnable() {
        for (int i = 0; i < maxSize; i++) {
            CreateNewSlot();
        }
        RefreshItem();
        instance.itemName.text = "";
        instance.itemInformation.text = "";
        instance.operationPanel.SetActive(false);
        instance.useItemNotice.SetActive(false);
        instance.operationPanelTransform = instance.operationPanel.GetComponent<Transform>();
    }

    void Update()
    {
        // OpenBag();
    }

    public static void OpenBag() {
        // if (Input.GetKeyDown(KeyCode.O)) {
        instance.myBag.SetActive(!instance.myBag.activeSelf);
        if (instance.myBag.activeSelf) {
            instance.useItemNotice.SetActive(true);
            instance.canvasBg.color = new Color(0.5f,0.5f,0.5f,1);
        } else {
            instance.selectedItem = null;
            instance.operationPanel.SetActive(false);
            instance.useItemNotice.SetActive(false);
            InventoryManager.RefreshItem();
            instance.canvasBg.color = new Color(1,1,1,1);
        }
        // }
    }

    public static void AddBagItem(string name) {
        // add item
        Item item = new Item(name);
        instance.playerBag.AddItem(item);
        InventoryManager.RefreshItem();
    }

    public static void RefreshItem() {
        var showOperationPanel = false;
        for (int i = 0; i < instance.slotList.Count; i++) {
            if (instance.playerBag.itemList.Count > i) {
                instance.slotList[i].SetItem(instance.playerBag.itemList[i]);
            } else {
                instance.slotList[i].SetItem(null);
            }
            if (instance.slotList[i].slotItem != null && instance.selectedItem != null && 
            instance.slotList[i].slotItem.itemName == instance.selectedItem.itemName ) {
                instance.operationPanelTransform.position = 
                    new Vector2(instance.slotList[i].GetComponent<Transform>().position.x, instance.operationPanelTransform.position.y);
                instance.slotList[i].SetBg(true);
                showOperationPanel = true;
            }
        }
        if (!showOperationPanel) {
            instance.operationPanel.SetActive(false);
        }
    }

    public static void LoadSaveData(List<ItemData> itemDataList) {
        instance.playerBag.LoadSaveData(itemDataList);
        InventoryManager.RefreshItem();
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
        instance.selectedItem = item;
        if (item == null) {
            instance.operationPanel.SetActive(false);
        } else {
            instance.itemInformation.text = item.itemInformation;
            instance.itemName.text = item.itemName;
            instance.operationPanel.SetActive(true);
        }
        InventoryManager.RefreshItem();
    }

    public static void DropItem() {
        if (instance.selectedItem == null) return;
        if (instance.selectedItem.itemCount > 1) {
            instance.selectedItem.itemCount -= 1;
        } else {
            instance.playerBag.RemoveItem(instance.selectedItem);
            instance.selectedItem = null;
        }
        InventoryManager.RefreshItem();
    }
}
