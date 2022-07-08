using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour {

    [SerializeField]
    Camera mainCamera;
    Vector3 position;
    Vector2 mousePos;

    Interactable hoveringInteractable;

    Collider2D cursorCollider;

    void Start() {
        cursorCollider = GetComponent<Collider2D>();
    }

    public void OnCursorPrimaryAction(InputAction.CallbackContext context) {
        if(context.performed) {
            cursorCollider.enabled = false;
            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            // if it hits something
            if(hit.collider != null)
            {
                //Debug.Log(hit.transform);
                // Gets IInteractable 
                Interactable interactableObject = hit.transform.GetComponent<Interactable>();
                // If the object has an IInteractable component and isn't part of the UI
                if (interactableObject != null && !EventSystem.current.IsPointerOverGameObject())
                    // Calls OnClick()
                    interactableObject.OnClick();
            }
            cursorCollider.enabled = true;
        }
    }

    public void OnCursorMovement(InputAction.CallbackContext context) {
        mousePos = context.ReadValue<Vector2>();
        position = mainCamera.ScreenToWorldPoint(mousePos);
        position.z = 0;
        transform.position = position;
    }

    void OnTriggerEnter2D(Collider2D other) {
        hoveringInteractable = other.GetComponent<Interactable>();
        if(hoveringInteractable != null) {
            hoveringInteractable.HoverOn();
        }
    }
    
    void OnTriggerExit2D(Collider2D other) {
        hoveringInteractable = other.GetComponent<Interactable>();
        if(hoveringInteractable != null) {
            hoveringInteractable.HoverOff();
        }
    }
}
