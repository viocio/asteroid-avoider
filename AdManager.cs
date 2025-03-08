using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    [SerializeField] private bool testMode = false;

    public static AdManager Instance;

    private GameOverHandler gameOverHandler;
#if UNITY_ANDROID
    private string gameId = "5122054";
#elif UNITY_IOS
    private string gameId = "5122055";
#endif
    
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Advertisement.Initialize(gameId, testMode, this);
        }
    }

    public void ShowAd(GameOverHandler gameOverHandler)
    {
        this.gameOverHandler = gameOverHandler; 
        
        Advertisement.Show("rewardedVideo", this); 
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad Loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Unity Ads Error: {message}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Continue Button Pressed");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Advertisement.Load("rewardedVideo", this);
        switch(showCompletionState)
        {
            case UnityAdsShowCompletionState.COMPLETED:
                gameOverHandler.ContinueGame();
                break;
            
            case UnityAdsShowCompletionState.SKIPPED:
                // Ad was skipped
                break;

            case UnityAdsShowCompletionState.UNKNOWN:
                Debug.Log("Warning! Ad Failed");
                break;
        }  
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Unity Ads Error: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Ad Start");
    }

    public void OnInitializationComplete()
    {
        Advertisement.Load("rewardedVideo", this);
        Debug.Log("Ad Initialized");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Initialization Failed: {message}");
    }
}

