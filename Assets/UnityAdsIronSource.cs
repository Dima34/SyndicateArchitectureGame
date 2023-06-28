using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityAdsIronSource : MonoBehaviour
{
    private const string APP_KEY = "1a8f5cbc5";
    
    private void OnEnable()
    {
        Init();
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
    }

    private void Start()
    {
        IronSource.Agent.validateIntegration();
    }

    private void OnDisable() =>
        IronSourceEvents.onSdkInitializationCompletedEvent -= SdkInitializationCompletedEvent;

    private static void Init() =>
        IronSource.Agent.init(APP_KEY);

    private void SdkInitializationCompletedEvent()
    {
        Debug.Log("Sdk initialized");
        LoadBanner();
    }


    void OnApplicationPause(bool isPaused) =>
        IronSource.Agent.onApplicationPause(isPaused);


    public void LoadBanner() =>
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
}
