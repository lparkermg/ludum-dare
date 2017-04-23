using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public CanvasGroup BannerGroup;

    public Text FlowerAmount;
    public Text SeedAmount;
    public Text CoinAmount;

    private int _flowerCount;
    private int _seedCount;
    private float _coinCount;

    public Color ErrorBkgColour;
    public Color SuccessBkgColour;
    public Color ErrorTextColour;
    public Color SuccessTextColour;
    public Text BannerText;
    public Image BannerBackground;

    private float _maxShowTime = 1.0f;
    private float _currentTime = 0.0f;

    private bool _showingBanner;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTextValues();
	    TimeCheck();
	}

    private void TimeCheck()
    {
        if (_showingBanner)
        {
            if (_currentTime >= _maxShowTime)
            {
                HideBanner();
            }
            else
            {
                _currentTime += Time.deltaTime;
            }
        }
        else
        {
            _currentTime = 0.0f;
        }

    }

    #region UI Updating


    private void UpdateTextValues()
    {
        FlowerAmount.text = "Flowers: " + _flowerCount;
        SeedAmount.text = "Seeds: " + _seedCount;
        CoinAmount.text = "Coins: " + _coinCount.ToString("#.##");
    }

    public void ShowBannerForX(bool error, string message, float time)
    {
        BannerBackground.color = error ? ErrorBkgColour : SuccessBkgColour;
        BannerText.color = error ? ErrorTextColour : SuccessTextColour;

        BannerText.text = message;
        _maxShowTime = time;
        ShowBanner();
    }

    private void ShowBanner()
    {
        BannerGroup.alpha = 1.0f;
        BannerGroup.interactable = true;
        BannerGroup.blocksRaycasts = true;
        _showingBanner = true;
    }

    private void HideBanner()
    {
        BannerGroup.alpha = 0.0f;
        BannerGroup.interactable = false;
        BannerGroup.blocksRaycasts = false;
        _showingBanner = false;
    }
    #endregion

    #region Value Updating

    public void UpdateFlowerCount(int newCount)
    {
        _flowerCount = newCount;
    }

    public void UpdateSeedCount(int newCount)
    {
        _seedCount = newCount;
    }

    public void UpdateCoinCount(float coinCount)
    {
        _coinCount = coinCount;
    }
    #endregion
}
