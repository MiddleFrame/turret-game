using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yodo1.MAS;
public class Adversisment : MonoBehaviour
{
    [SerializeField] GameObject _backgroundBlur;
    private Yodo1U3dBannerAdView bannerAdView;
    private int _typeReward = 0;
    private void Start()
    {
        Yodo1AdBuildConfig config = new Yodo1AdBuildConfig().enableUserPrivacyDialog(true);
        Yodo1U3dMas.SetAdBuildConfig(config);
        Yodo1U3dMas.InitializeSdk();
        InitializeRewardedAds();
        this.RequestBanner();
    }

    private void InitializeRewardedAds()
    {
        // Добавить события
        Yodo1U3dMasCallback.Rewarded.OnAdOpenedEvent += OnRewardedAdOpenedEvent;
        Yodo1U3dMasCallback.Rewarded.OnAdClosedEvent += OnRewardedAdClosedEvent;
        Yodo1U3dMasCallback.Rewarded.OnAdReceivedRewardEvent += OnAdReceivedRewardEvent;
        Yodo1U3dMasCallback.Rewarded.OnAdErrorEvent += OnRewardedAdErorEvent;
    }

    private void OnRewardedAdOpenedEvent()
    {
        Debug.Log("[Yodo1 Mas] Объявление о вознаграждении открыто");
    }

    private void OnAdReceivedRewardEvent()
    {
        if (_typeReward == 0)
            ContinueGame();
        else if (_typeReward == 1)
            MoneyReward();
    }
   private void OnRewardedAdClosedEvent()
    {
        
        
    }

    private void OnRewardedAdErorEvent(Yodo1U3dAdError adError)
    { 
    }


    private void ContinueGame()
    {
        _backgroundBlur.SetActive(false);
        GameStats.ContinueGame();
    }

    private void MoneyReward()
    {
        GameManager.playerStats.Money += 10;
        ChangeTextValue.instance.UpdateMoney();
        Analytic.instance.GoldPrice();
    }

    public void ShowReward(int i =0)
    {
        bool isLoaded = Yodo1U3dMas.IsRewardedAdLoaded();
        if (isLoaded)
        {
            _typeReward = i;
            Yodo1U3dMas.ShowRewardedAd();
        }
    }




    private void RequestBanner()
    {
        // Clean up banner before reusing
        if (bannerAdView != null)
        {
            bannerAdView.Destroy();
        }

        // Create a 320x50 banner at top of the screen
        bannerAdView = new Yodo1U3dBannerAdView(Yodo1U3dBannerAdSize.Banner, Yodo1U3dBannerAdPosition.BannerTop | Yodo1U3dBannerAdPosition.BannerHorizontalCenter);
        // Ad Events
        bannerAdView.OnAdLoadedEvent += OnBannerAdLoadedEvent;
        bannerAdView.OnAdFailedToLoadEvent += OnBannerAdFailedToLoadEvent;
        bannerAdView.OnAdOpenedEvent += OnBannerAdOpenedEvent;
        bannerAdView.OnAdFailedToOpenEvent += OnBannerAdFailedToOpenEvent;
        bannerAdView.OnAdClosedEvent += OnBannerAdClosedEvent;
        // Load banner ads, the banner ad will be displayed automatically after loaded
        bannerAdView.LoadAd();
    }

    private void OnBannerAdLoadedEvent(Yodo1U3dBannerAdView adView)
    {
        // Banner ad is ready to be shown.
        Debug.Log("[Yodo1 Mas] OnBannerAdLoadedEvent event received");
    }

    private void OnBannerAdFailedToLoadEvent(Yodo1U3dBannerAdView adView, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnBannerAdFailedToLoadEvent event received with error: " + adError.ToString());
    }

    private void OnBannerAdOpenedEvent(Yodo1U3dBannerAdView adView)
    {
        Debug.Log("[Yodo1 Mas] OnBannerAdOpenedEvent event received");
    }

    private void OnBannerAdFailedToOpenEvent(Yodo1U3dBannerAdView adView, Yodo1U3dAdError adError)
    {
        Debug.Log("[Yodo1 Mas] OnBannerAdFailedToOpenEvent event received with error: " + adError.ToString());
    }

    private void OnBannerAdClosedEvent(Yodo1U3dBannerAdView adView)
    {
        Debug.Log("[Yodo1 Mas] OnBannerAdClosedEvent event received");
    }
}

