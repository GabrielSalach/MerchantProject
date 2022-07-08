using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public enum CameraMode {HubView, MapView};

    // Singleton instance
    public static CameraController instance;

    CameraMode currentMode = CameraMode.HubView;

    // FreeCam orthographic size
    float newCameraSize;

    // Min and Max FreeCam orthographic size
    float minCameraSize = 1.5f;
    float maxCameraSize = 999;

    // FreeCam zoom scroll sensitivity
    public float zoomSensitivity = 0.1f;
    // Switches to 0 when camera is locked and back to zoomSensitivity when unlocked
    float currentZoomSensitivity;

    // Player cursor (WorldSpace transform)
    public Transform cursor;
    // FreeCam
    public CinemachineVirtualCamera hubFreeCam;
    // Object Selection cam
    public CinemachineVirtualCamera lockOnCam;
    // Camera Blending animator
    Animator cinemachineAnimator;
    // Reference to main Camera
    public Camera mainCamera;

    // Singleton logic
    void Awake() {
        if(instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    void Start() {
        // Calculates the maximum orthographic size so the camera can't zoom out of bounds
        Bounds bounds = hubFreeCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D.bounds;
        maxCameraSize = Mathf.Min(bounds.extents.y, bounds.extents.x/mainCamera.aspect);
        // Sets values and references
        currentZoomSensitivity = zoomSensitivity;
        cinemachineAnimator = hubFreeCam.GetComponentInParent<Animator>();
        newCameraSize = Mathf.Clamp(hubFreeCam.m_Lens.OrthographicSize, minCameraSize, maxCameraSize);
    }

    // Called when Zoom Input is performed
    public void OnZoom(InputAction.CallbackContext context) {
        // Zooms camera In and Out
        if(context.ReadValue<Vector2>().y > 0) {
            newCameraSize -= currentZoomSensitivity;
        }
        if(context.ReadValue<Vector2>().y < 0) {
            newCameraSize += currentZoomSensitivity;
        }
        
        // Clamps the value between minCameraSize and maxCameraSize and applies it to the main Virtual Camera 
        newCameraSize = Mathf.Clamp(newCameraSize, minCameraSize, maxCameraSize);
        hubFreeCam.m_Lens.OrthographicSize = newCameraSize;
    }

    // Called by Cursor when clicking an Interactable object and sets the camera to LockOn cam
    public void SelectTarget(Transform selectedTarget) {
        if(currentMode == CameraMode.MapView) {
            Bounds bounds = selectedTarget.GetComponent<Collider2D>().bounds;
            lockOnCam.m_Lens.OrthographicSize = Mathf.Max(bounds.extents.y, bounds.extents.x/mainCamera.aspect);
        }
        lockOnCam.Follow = selectedTarget;
        cinemachineAnimator.SetBool("LockOn", true);
    }
    
    // Called when Unselect input is performed
    public void OnUnselectTarget(InputAction.CallbackContext context) {
        UnselectTarget();
    }

    // Sets back the camera to FreeCam
    public void UnselectTarget() {
        lockOnCam.Follow = mainCamera.transform;
        cinemachineAnimator.SetBool("LockOn", false);
    }

    // Locks the FreeCam in place (Used when UI is shown)
    public void LockCamera() {
        currentZoomSensitivity = 0;
        hubFreeCam.Follow = null;
    }

    // Unlocks the FreeCam 
    public void UnlockCamera() {
        currentZoomSensitivity = zoomSensitivity;
        hubFreeCam.Follow = cursor;
    }


    // Maybe refactor these two function into one with an enum parameter like enum Destination{Hub, Map}
    public void SwitchToMapCamera() {
        currentMode = CameraMode.MapView;
        cinemachineAnimator.SetBool("LockOn", false);
        LockCamera();
        cinemachineAnimator.SetBool("MapCamera", true);
        lockOnCam.GetComponent<CinemachineConfiner>().enabled = false;
    }

    public void SwitchToHubView() {
        currentMode = CameraMode.HubView;
        cinemachineAnimator.SetBool("MapCamera", false);
        UnlockCamera();
        lockOnCam.GetComponent<CinemachineConfiner>().enabled = true;
    }
}
