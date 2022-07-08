using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Window : MonoBehaviour
{

    // Animation curve
    [SerializeField] AnimationCurve animationCurve;
    // Duration of animation in seconds;
    [SerializeField] float animationDuration;
    // Is the window moving
    bool isLerping;
    // Time at which the window started moving
    float startTimestamp;
    // Trigger set to true when a window needs to move
    bool windowLerpTrigger;

    // Targets
    [SerializeField] Transform targetOpen;
    [SerializeField] Transform targetClosed;

    // Positions of the anchors
    Vector3 openPosition;
    Vector3 closedPosition;

    // Rect transform component of this window
    RectTransform rectTransform;

    // References to window's elements
    Button closeButton;
    TextMeshProUGUI titleBarText;

    // Flags for the state of the window
    protected bool isOpen;
    // Last opened Window, used when opening a new one so it closes it
    [SerializeField]
    static Window lastOpenedWindow;
    // Popup reference
    protected Window popup;
    // Number of opened window, used for locking the camera
    static uint openWindowCount;

    // Is the window a popup
    public bool isPopup;

    public virtual void Start() {
        // Assigning references
        closeButton = transform.Find("TitleBarHolder").Find("CloseButton").GetComponent<Button>();
        titleBarText = transform.Find("TitleBarHolder").Find("TitleBar").GetComponentInChildren<TextMeshProUGUI>();

        // Sets Flags
        isOpen = false;
        isLerping = false;
        windowLerpTrigger = false;

        // Converts targets to Vector3
        openPosition = targetOpen.GetComponent<RectTransform>().anchoredPosition;
        closedPosition = targetClosed.GetComponent<RectTransform>().anchoredPosition;

        // Sets rectTransform
        rectTransform = GetComponent<RectTransform>();

        // Sets close Button Action
        closeButton.onClick.AddListener(delegate {CloseWindow();});
    }

    public virtual void Update() {
        if(windowLerpTrigger == true && isLerping == false) {
            startLerping();
            windowLerpTrigger = false;
        }
        if(isLerping == true) {
            WindowLerp();
        }
    }

    // Triggers the lerp animation
    void startLerping() {
        isLerping = true;
        startTimestamp = Time.time;
        if(openWindowCount > 0) {
            CameraController.instance.LockCamera(); 
        } else {
            CameraController.instance.UnlockCamera();
        }
    }

    // Lerping calculations
    void WindowLerp() {
        // Calculates the percentage of the remaining distance based on time and the animation curve 
        float timeSinceStarted = Time.time - startTimestamp;
        float percentageComplete = timeSinceStarted / animationDuration;
        float percentageToMove = animationCurve.Evaluate(percentageComplete);

        // Moves the window 
        if(isOpen == true) {
            rectTransform.anchoredPosition = Vector3.Lerp(closedPosition, openPosition, percentageToMove);
        } else {
            rectTransform.anchoredPosition = Vector3.Lerp(openPosition, closedPosition, percentageToMove);
        }
        if(percentageComplete >= 1.0f) {
            isLerping = false;
        }
    }

    // Opens the window and closes the last opened one 
    public virtual void OpenWindow() {
        // Only processes window opening if it was closed
        if(isOpen == false) {
            // Closes the last opened window if there is one except if it's the parent window of a popup
            if(lastOpenedWindow != null && lastOpenedWindow.isOpen == true && this.isPopup == false) {
                lastOpenedWindow.CloseWindow();
            }
            // Sets this window as the last opened window if it's not a popup
            if(lastOpenedWindow == null || lastOpenedWindow.popup != this)
                lastOpenedWindow = this;
            // Triggers the animation
            isOpen = true;
            windowLerpTrigger = true;
            openWindowCount++;
        }
    }

    // Closes the window and its popup
    public virtual void CloseWindow() {
        // Only processes window closing if it was open
        if(isOpen == true) {
            // Closes window's popup if there is one
            if(popup != null) 
                popup.CloseWindow();
            // Triggers the closing animation
            isOpen = false;
            windowLerpTrigger = true;
            openWindowCount--;
        }
    }

    // Opens a popup from this window
    public void OpenPopup(Window popup) {
        this.popup = popup;
        popup.isPopup = true;
        popup.OpenWindow();
    }

    // Sets the title of the window
    public void SetTitle(string title) {
        titleBarText.SetText(title);
    }
}
