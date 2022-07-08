using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrafterInfoWindow : Window
{
    // Singleton instance
    public static CrafterInfoWindow instance;

    // Action Button
    public Button actionButton;
    public TextMeshProUGUI actionButtonText;

    // Singleton logic
    void Awake() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    new void Start() {
        base.Start();
        // Get actionButton and disables it
        actionButton = transform.Find("Action").GetComponent<Button>();
        actionButton.gameObject.SetActive(false);
        actionButtonText = actionButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    // Sets the camera back to freecam when closing the window
    public override void CloseWindow() {
        base.CloseWindow();
        CameraController.instance.UnselectTarget();
    }
}
