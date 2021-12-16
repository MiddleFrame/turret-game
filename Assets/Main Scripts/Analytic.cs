using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Messaging;
using Firebase;
using Firebase.Analytics;
using UnityEngine;

public class Analytic : MonoBehaviour
{

    public static Analytic instance;
    // Start is called before the first frame update
    void Start()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        Analytic.instance = this;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        });
    }


    public void StartNewGame()
    {
        Debug.Log("StartNewGame ");
        FirebaseAnalytics.LogEvent("StartNewGame");
    }

    public void GameContinue(int Score =0 )
    {
        Debug.Log("GameContinue " + "Score " + Score);
        FirebaseAnalytics.LogEvent("GameContinue", "Score", Score);
    }

    public void EndGame( int Score = 0)
    {
        Debug.Log("EndGame " + "Score " + Score);
        FirebaseAnalytics.LogEvent("EndGame", "Score", Score);
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        Debug.Log("Received Reg Token:" + token.Token);
    }
    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new Message from:" + e.Message.From);
    }

}
