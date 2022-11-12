using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draggable : MonoBehaviour{
    bool canMove;   
    bool dragging;
    BoxCollider2D collider;
    Image image;
    public GameObject rawParent;
    Vector2 rawPosition = new Vector2();
    bool isSelecct = false;
    void Start() {
        collider = GetComponent<BoxCollider2D>();
        image = GetComponent<Image>();
        canMove = false;
        dragging = false;
    }
    // Update is called once per frame
    void Update() {
        Vector2 mousePos = Input.mousePosition;
        if (image.sprite == null) return;

        if (Input.GetMouseButtonDown(0)) {
            // 检测collider是否与某一坐标重叠
            if (collider == Physics2D.OverlapPoint(mousePos)) {
                // 移到物体上才可以拖动
                canMove = true;
                if (rawPosition.x == 0) {
                    rawPosition = transform.position;
                    Debug.Log(rawPosition);
                }
                Debug.Log(transform.parent.gameObject.name);
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
        if (Input.GetMouseButtonUp(0)) {
            if (collider == Physics2D.OverlapPoint(mousePos)) {
                if (mousePos.y > 200) {
                    canMove = false;
                    dragging = false;
                } else {
                    dragging = false;
                    transform.SetParent(rawParent.transform);
                    transform.position = rawPosition;
                    Debug.Log(rawPosition);
                }
            }
        }
    }
}
