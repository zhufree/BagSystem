using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draggable : MonoBehaviour{
    bool canMove;   
    public bool dragging;
    BoxCollider2D collider;
    Image image;
    public GameObject rawParent;
    Vector2 rawPosition = new Vector2();
    public bool isSelect = false;

    void Start() {
        collider = GetComponent<BoxCollider2D>();
        image = GetComponent<Image>();
        canMove = false;
        dragging = false;
    }
    // Update is called once per frame
    void Update() {
        Vector2 mousePos = Input.mousePosition;
        if (image.sprite == null || !isSelect) return;

        if (Input.GetMouseButtonDown(0)) {
            // 检测collider是否与某一坐标重叠
            if (collider == Physics2D.OverlapPoint(mousePos)) {
                // 移到物体上才可以拖动
                Debug.Log("can move");
                canMove = true;
                if (rawPosition.x == 0) {
                    rawPosition = transform.position;
                }
                if (transform.parent.gameObject.name == "Slot(Clone)") {
                    transform.SetParent(transform.parent.parent.parent.parent);
                }
            } else {
                canMove = false;
            }
            if (canMove) {
                dragging = true;
            }
        }
        if (dragging) {
            transform.position = mousePos;
        }
        if (Input.GetMouseButtonUp(0) && dragging) {
            if (collider == Physics2D.OverlapPoint(mousePos)) {
                canMove = false;
                dragging = false;
                transform.SetParent(rawParent.transform);
                transform.position = rawPosition;
                if (mousePos.y > 200) {
                    InventoryManager.UseItem();
                }
            }
        }
    }

    public void setSelect(bool select) {
        isSelect = select;
    }
}
