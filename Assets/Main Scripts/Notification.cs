using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotificationSamples;
using System;
using Unity.Notifications.Android;

public class Notification : MonoBehaviour
{
    //private GameNotificationsManager notificationManager = new GameNotificationsManager();
    // Start is called before the first frame update
    [SerializeField] GameObject _rewardPanel; 

    void Start()
    {
        var notificationID = 10000;
        
        var notification = new AndroidNotification();
        var notificationStatus = AndroidNotificationCenter.CheckScheduledNotificationStatus(notificationID);

        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        notification.Title = "Your reward is ready!";
        notification.Text = "Log into the game NOW and get 10 coins!";
        notification.FireTime = System.DateTime.Now.AddDays(1);
        if (notificationStatus == NotificationStatus.Scheduled)
        {
            // Replace the scheduled notification with a new notification.
            AndroidNotificationCenter.UpdateScheduledNotification(notificationID, notification, "channel_id");
        }
        else if (notificationStatus == NotificationStatus.Delivered)
        {
            // Remove the previously shown notification from the status bar.
            AndroidNotificationCenter.CancelNotification(notificationID);
            _rewardPanel.SetActive(true);
            AndroidNotificationCenter.SendNotificationWithExplicitID(notification, "channel_id", notificationID);
        }
        else
        {
            AndroidNotificationCenter.SendNotificationWithExplicitID(notification, "channel_id", notificationID);
        }
        Debug.Log(notificationStatus);
    }
}
