using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour {
    private bool _isDragging = false;
    private Vector2 _screenPosition;
    private Vector2 _worldPosition;
    private Draggable _lastDragItem;

    public static DragController instance; // 单例管理

    // void Awake() {
    //     DragController[] controllers = FindObjectsOfType<DragController>();
    //     if (controllers.length > 1) {
    //         Destroy(gameObject)
    //     }
    // }

    void Awake() {
        if (instance != null) {
            Destroy(this);
        }
        instance = this;
    }
    // Update is called once per frame
    void Update() {
        // if (Input.GetMouseButton(0)) {
        //     Vector2 mousePos = Input.mousePosition;
        //     _screenPosition = new Vector2(mousePos.x, mousePos.y);
        // } else if (Input.touchCount > 0) {
        //     _screenPosition = Input.GetTouch(0).position;
        // } else {
        //     return;
        // }

        // // _worldPosition = Camera.main.ScreenToWorldPoint(_screenPosition);

        // if (_isDragging) {
        //     Drag();
        // } else {
        //     RaycastHit2D hit = Physiscs2D.Raycast(_worldPosition, Vector128.zero);
        //     if (HttpListener.collider != null) {
        //         if (draag)
        //     }
        // }
    }

    void InitDrag() {

    }

    void Drag() {

    }

    void Drop() {

    }
}
