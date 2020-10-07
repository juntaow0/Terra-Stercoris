using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : MonoBehaviour {

    public void SetText(string text) {
        Notification.instance.SetText(text);
    }

    public void Show() {
        Notification.instance.Show();
    }

    public void Hide() {
        Notification.instance.Hide();
    }
}
