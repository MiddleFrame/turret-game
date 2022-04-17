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
    private void Awake()
    {
        Analytic.instance = this;
    }
    void Start()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        
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

    public void GoldPrice()
    {
        Debug.Log("GoldPrice " + "Money " + GameManager.playerStats.Money);
        FirebaseAnalytics.LogEvent("EndGame", "Total_money", GameManager.playerStats.Money);
    }

    /*  public void BuyBallCount()
      {
          Debug.Log("GoldPrice " + "Money " + GameManager.playerStats.Money);
          FirebaseAnalytics.LogEvent("EndGame", "Total_money", GameManager.playerStats.Money);
      } 
      public void BuyRotateTime()
      {
          Debug.Log("GoldPrice " + "Money " + GameManager.playerStats.Money);
          FirebaseAnalytics.LogEvent("EndGame", "Total_money", GameManager.playerStats.Money);
      }
      public void BuyShootCooldown()
      {
          Debug.Log("GoldPrice " + "Money " + GameManager.playerStats.Money);
          FirebaseAnalytics.LogEvent("EndGame", "Total_money", GameManager.playerStats.Money);
      }
      public void BuyBulletSpeed()
      {
          Debug.Log("GoldPrice " + "Money " + GameManager.playerStats.Money);
          FirebaseAnalytics.LogEvent("EndGame", "Total_money", GameManager.playerStats.Money);
      }
    public void BuyLifes()
      {
          Debug.Log("GoldPrice " + "Money " + GameManager.playerStats.Money);
          FirebaseAnalytics.LogEvent("EndGame", "Total_money", GameManager.playerStats.Money);
      }*/

    public void BuyShopUpgrade(string upgrade)
    {
        Debug.Log("upgrade buy " + "Upgrade " + upgrade);
        FirebaseAnalytics.LogEvent("BuyShopUpgrade", "Upgrade", upgrade);
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
