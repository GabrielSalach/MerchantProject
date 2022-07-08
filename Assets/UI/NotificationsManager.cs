using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class NotificationsManager : MonoBehaviour
{

    // Notifications slots
    List<GameObject> notifications;
    // Queue of messages waiting to be displayed
    static Queue<string> notificationsQueue;
    // Available notifications slots
    Queue<GameObject> availableNotifications;


    // Start is called before the first frame update
    void Start()
    {
        // Gets all notification slots
        notifications = GameObject.FindGameObjectsWithTag("Notification").ToList();

        notificationsQueue = new Queue<string>();
        availableNotifications = new Queue<GameObject>();

        // Adds all the notifications slots to the available queue
        foreach(GameObject notification in notifications) {
            availableNotifications.Enqueue(notification);
        }

    }

/*     void bite () {
        foreach (Notification n) {
            if(n.available == true)
                n.diplay();
        }
    }
 */
    // Update is called once per frame
    void Update()
    {   
        // If there is a message to be displayed and if there is an available notification slot
        if(notificationsQueue.Count > 0 && availableNotifications.Count > 0) {
            StartCoroutine(displayNotification(notificationsQueue.Dequeue(), availableNotifications.Dequeue())); 
        }
    }

    // Gets called when a message needs to be displayed
    public static void TriggerNotification(string message) {
        notificationsQueue.Enqueue(message);
    }

    // Coroutine that animates and sets the text for each notification when called
    IEnumerator displayNotification(string message, GameObject notification) {
        // Set text
        notification.GetComponentInChildren<Text>().text = message;

        // Variable that holds the transform 
        RectTransform notificationTransform = notification.GetComponent<RectTransform>();


        // While notification isn't in position, move it to the right place
        while(notificationTransform.anchoredPosition.x > -4.9f) {
            notificationTransform.anchoredPosition = Vector2.Lerp(notificationTransform.anchoredPosition, Vector2.left * 5 + Vector2.up * notificationTransform.anchoredPosition.y, 0.01f);
            yield return null;
        }

        yield return new WaitForSeconds(5);

        // Waits for the notification slot to fade out
        while(notification.GetComponent<CanvasRenderer>().GetAlpha() > 0.1f) {
            notification.GetComponent<Image>().CrossFadeAlpha(0, 2, true);
            notification.GetComponentInChildren<Text>().CrossFadeAlpha(0, 2, true);
            yield return null;
        }

        // Reset the notification slot's position and alpha, and adds it back to the available slots
        notificationTransform.anchoredPosition = new Vector2(250, notificationTransform.anchoredPosition.y);
        notification.GetComponent<Image>().CrossFadeAlpha(1, 0.01f, true);
        notification.GetComponentInChildren<Text>().CrossFadeAlpha(1, 0.01f, true);
        availableNotifications.Enqueue(notification);
    }
}
