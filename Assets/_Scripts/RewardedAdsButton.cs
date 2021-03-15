using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{
    private const float MAXFILL = 300;
    private const float VALUESCALE = 0.002f;

    string gameId = "4000255";
    string myPlacementId = "rewardedVideo";
    bool testMode = true;

    int nBonus1 = 0;
    int nBonus2 = 0;
    int nBonus3 = 0;
    int nBonus4 = 0;

    [Header("Sprites")]
    public Sprite obtenirOn;
    public Sprite obtenirOff;
    public Image imgBonus1;
    public Image imgBonus2;
    public Image imgBonus3;
    public Image imgBonus4;
    public RectTransform rectBonus1;
    public RectTransform rectBonus2;
    public RectTransform rectBonus3;
    public RectTransform rectBonus4;

    public GameObject fillBar1;
    public GameObject fillBar2;
    public GameObject fillBar3;
    public GameObject fillBar4;

    public GameObject blinkCircle;

    public Sprite[] spritesBonus;

    public SoundManager soundManager;

    private int widthToReach = 0;
    private bool iconeReduceHeight1 = true;
    private bool iconeReduceHeight2 = true;
    private bool iconeReduceHeight3 = true;
    private bool iconeReduceHeight4 = true;
    private bool hasBonus = false;
    private int actualBonus = 0;


    // Initialize the Ads listener and service:
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);

        LoadRewards();

        if(PlayerPrefs.GetInt("Ads", 0) == 4)
        {
            ShowRewardedVideo();
            PlayerPrefs.SetInt("Ads", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Ads", PlayerPrefs.GetInt("Ads", 0) + 1);
        }
    }

    public void ShowRewardedVideo()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(myPlacementId))
        {
            Advertisement.Show(myPlacementId);
        }
        else
        {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            AddRewards();
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId)
        {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    public void AddRewards()
    {
        if(nBonus1 < 2)
        {
            nBonus1++;
            widthToReach = (int)Mathf.Floor(nBonus1 * (MAXFILL / 2));
            fillBar1.GetComponent<RectTransform>().sizeDelta = new Vector2(widthToReach, 60f);

            if(nBonus1 == 2)
            {
                imgBonus1.overrideSprite = obtenirOn;
            }
        }

        if (nBonus2 < 3)
        {
            nBonus2++;
            widthToReach = (int)Mathf.Floor(nBonus2 * (MAXFILL / 3));
            fillBar2.GetComponent<RectTransform>().sizeDelta = new Vector2(widthToReach, 60f);

            if (nBonus2 == 3)
            {
                imgBonus2.overrideSprite = obtenirOn;
            }
        }

        if (nBonus3 < 5)
        {
            nBonus3++;
            widthToReach = (int)Mathf.Floor(nBonus3 * (MAXFILL / 5));
            fillBar3.GetComponent<RectTransform>().sizeDelta = new Vector2(widthToReach, 60f);

            if (nBonus3 == 5)
            {
                imgBonus3.overrideSprite = obtenirOn;
            }
        }

        if (nBonus4 < 6)
        {
            nBonus4++;
            widthToReach = (int)Mathf.Floor(nBonus4 * (MAXFILL / 6));
            fillBar4.GetComponent<RectTransform>().sizeDelta = new Vector2(widthToReach, 60f);

            if (nBonus4 == 6)
            {
                imgBonus4.overrideSprite = obtenirOn;
            }
        }

        SaveRewards();
    }

    public void LoadRewards()
    {
        nBonus1 = PlayerPrefs.GetInt("Bonus1", 0);
        nBonus2 = PlayerPrefs.GetInt("Bonus2", 0);
        nBonus3 = PlayerPrefs.GetInt("Bonus3", 0);
        nBonus4 = PlayerPrefs.GetInt("Bonus4", 0);

        widthToReach = (int)Mathf.Floor(nBonus1 * (MAXFILL / 2));
        fillBar1.GetComponent<RectTransform>().sizeDelta = new Vector2(widthToReach, 60f);

        if (nBonus1 == 2)
        {
            imgBonus1.overrideSprite = obtenirOn;
        }


        widthToReach = (int)Mathf.Floor(nBonus2 * (MAXFILL / 3));
        fillBar2.GetComponent<RectTransform>().sizeDelta = new Vector2(widthToReach, 60f);

        if (nBonus2 == 3)
        {
            imgBonus2.overrideSprite = obtenirOn;
        }


        widthToReach = (int)Mathf.Floor(nBonus3 * (MAXFILL / 5));
        fillBar3.GetComponent<RectTransform>().sizeDelta = new Vector2(widthToReach, 60f);

        if (nBonus3 == 5)
        {
            imgBonus3.overrideSprite = obtenirOn;
        }


        widthToReach = (int)Mathf.Floor(nBonus4 * (MAXFILL / 6));
        fillBar4.GetComponent<RectTransform>().sizeDelta = new Vector2(widthToReach, 60f);

        if (nBonus4 == 6)
        {
            imgBonus4.overrideSprite = obtenirOn;
        }

        actualBonus = PlayerPrefs.GetInt("ActualBonus", 0);

        if(actualBonus != 0)
        {
            blinkCircle.SetActive(true);
            hasBonus = true;
            StartCoroutine(BlinkCircleAnim());
            blinkCircle.transform.GetChild(0).gameObject.GetComponent<Image>().overrideSprite = spritesBonus[actualBonus - 1];
        }
    }

    public void SaveRewards()
    {
        PlayerPrefs.SetInt("Bonus1", nBonus1);
        PlayerPrefs.SetInt("Bonus2", nBonus2);
        PlayerPrefs.SetInt("Bonus3", nBonus3);
        PlayerPrefs.SetInt("Bonus4", nBonus4);
    }

    public void RecupRewards1()
    {
        if (hasBonus)
        {
            return;
        }

        if (nBonus1 != 2)
        {
            return;
        }

        soundManager.playAudioClip(14);
        imgBonus1.overrideSprite = obtenirOff;
        fillBar1.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60f);
        nBonus1 = 0;
        SaveRewards();
        blinkCircle.SetActive(true);
        hasBonus = true;
        StartCoroutine(BlinkCircleAnim());
        actualBonus = 1;
        blinkCircle.transform.GetChild(0).gameObject.GetComponent<Image>().overrideSprite = spritesBonus[0];
        PlayerPrefs.SetInt("ActualBonus", 1);
    }

    public void RecupRewards2()
    {
        if (hasBonus)
        {
            return;
        }

        if (nBonus2 != 3)
        {
            return;
        }

        soundManager.playAudioClip(14);
        imgBonus2.overrideSprite = obtenirOff;
        fillBar2.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60f);
        nBonus2 = 0;
        SaveRewards();
        blinkCircle.SetActive(true);
        hasBonus = true;
        StartCoroutine(BlinkCircleAnim());
        actualBonus = 2;
        blinkCircle.transform.GetChild(0).gameObject.GetComponent<Image>().overrideSprite = spritesBonus[1];
        PlayerPrefs.SetInt("ActualBonus", 2);
    }

    public void RecupRewards3()
    {
        if (hasBonus)
        {
            return;
        }

        if (nBonus3 != 5)
        {
            return;
        }

        soundManager.playAudioClip(14);
        imgBonus3.overrideSprite = obtenirOff;
        fillBar3.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60f);
        nBonus3 = 0;
        SaveRewards();
        blinkCircle.SetActive(true);
        hasBonus = true;
        StartCoroutine(BlinkCircleAnim());
        actualBonus = 3;
        hasBonus = true;
        blinkCircle.transform.GetChild(0).gameObject.GetComponent<Image>().overrideSprite = spritesBonus[2];
        PlayerPrefs.SetInt("ActualBonus", 3);
    }

    public void RecupRewards4()
    {
        if (hasBonus)
        {
            return;
        }

        if (nBonus4 != 6)
        {
            return;
        }

        soundManager.playAudioClip(14);
        imgBonus4.overrideSprite = obtenirOff;
        fillBar4.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 60f);
        nBonus4 = 0;
        SaveRewards();
        blinkCircle.SetActive(true);
        hasBonus = true;
        StartCoroutine(BlinkCircleAnim());
        actualBonus = 4;
        blinkCircle.transform.GetChild(0).gameObject.GetComponent<Image>().overrideSprite = spritesBonus[3];
        PlayerPrefs.SetInt("ActualBonus", 4);
    }

    private void Update()
    {
        if(nBonus1 == 2)
        {
            if(iconeReduceHeight1)
            {
                rectBonus1.localScale = new Vector2(rectBonus1.localScale.x + VALUESCALE, rectBonus1.localScale.y - VALUESCALE);
                if (rectBonus1.localScale.x >= 1f)
                {
                    iconeReduceHeight1 = !iconeReduceHeight1;
                }
            }
            else
            {
                rectBonus1.localScale = new Vector2(rectBonus1.localScale.x - VALUESCALE, rectBonus1.localScale.y + VALUESCALE);
                if (rectBonus1.localScale.y >= 1f)
                {
                    iconeReduceHeight1 = !iconeReduceHeight1;
                }
            }
        }

        if (nBonus2 == 3)
        {
            if (iconeReduceHeight2)
            {
                rectBonus2.localScale = new Vector2(rectBonus2.localScale.x + VALUESCALE, rectBonus2.localScale.y - VALUESCALE);
                if (rectBonus2.localScale.x >= 1f)
                {
                    iconeReduceHeight2 = !iconeReduceHeight2;
                }
            }
            else
            {
                rectBonus2.localScale = new Vector2(rectBonus2.localScale.x - VALUESCALE, rectBonus2.localScale.y + VALUESCALE);
                if (rectBonus2.localScale.y >= 1f)
                {
                    iconeReduceHeight2 = !iconeReduceHeight2;
                }
            }
        }

        if (nBonus3 == 5)
        {
            if (iconeReduceHeight3)
            {
                rectBonus3.localScale = new Vector2(rectBonus3.localScale.x + VALUESCALE, rectBonus3.localScale.y - VALUESCALE);
                if (rectBonus3.localScale.x >= 1f)
                {
                    iconeReduceHeight3 = !iconeReduceHeight3;
                }
            }
            else
            {
                rectBonus3.localScale = new Vector2(rectBonus3.localScale.x - VALUESCALE, rectBonus3.localScale.y + VALUESCALE);
                if (rectBonus3.localScale.y >= 1f)
                {
                    iconeReduceHeight3 = !iconeReduceHeight3;
                }
            }
        }

        if (nBonus4 == 6)
        {
            if (iconeReduceHeight4)
            {
                rectBonus4.localScale = new Vector2(rectBonus4.localScale.x + VALUESCALE, rectBonus4.localScale.y - VALUESCALE);
                if (rectBonus4.localScale.x >= 1f)
                {
                    iconeReduceHeight4 = !iconeReduceHeight4;
                }
            }
            else
            {
                rectBonus4.localScale = new Vector2(rectBonus4.localScale.x - VALUESCALE, rectBonus4.localScale.y + VALUESCALE);
                if (rectBonus4.localScale.y >= 1f)
                {
                    iconeReduceHeight4 = !iconeReduceHeight4;
                }
            }
        }
    }

    IEnumerator BlinkCircleAnim()
    {
        int a = 100;

        while (hasBonus)
        {
            yield return new WaitForSeconds(0.01f);

            blinkCircle.GetComponent<RectTransform>().sizeDelta = new Vector2(blinkCircle.GetComponent<RectTransform>().rect.width + 2, blinkCircle.GetComponent<RectTransform>().rect.height + 2);
            a -= 2;
            blinkCircle.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)a);

            if (blinkCircle.GetComponent<RectTransform>().rect.width >= 250)
            {
                blinkCircle.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
                a = 100;
                blinkCircle.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)a);
            }
        }

        blinkCircle.SetActive(false);
    }
}
