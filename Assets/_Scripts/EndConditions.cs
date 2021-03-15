using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class EndConditions : MonoBehaviour
{
    private const float XMIN = -1.6f;
    private const float XMAX = 1.6f;
    private const float YMIN1 = -3.5f;
    private const float YMAX1 = 2f;
    private const float YMIN2 = 2f;
    private const float YMAX2 = 3.5f;
    private const int FONTSIZE = 36;
    private const int MAXFONTSIZE = 60;
    private const float VALUESCALE = 0.002f;
    private const float MAXWIDTH = 300;

    // ============== VARIABLES ==============

    [Header("Objects to Drop")]
    public GameObject bgEnd;
    public GameObject btnLeft;
    public GameObject btnRight;
    public GameObject btnOptions;
    public GameObject lollipopSpawner;
    public LevelManager levelManager;
    public TextMeshProUGUI txtRemainingItems;
    public SoundManager soundManager;
    public RectTransform transitionPanel;

    [Header("Prefabs")]
    public GameObject ptcRedPrefab;
    public GameObject ptcBluePrefab;
    public GameObject ptcGreenPrefab;
    public GameObject ptcPinkPrefab;
    public GameObject ptcOrangePrefab;
    public GameObject ptcEndLevelPrefab;
    public GameObject supraLollipopPrefab;

    [Header("EndPannel")]
    public RectTransform endPannel;
    public TextMeshProUGUI txtCondition;
    public RectTransform btnReturn;
    public GameObject fillBar;
    public RectTransform goldCandy1;
    public RectTransform goldCandy2;
    public RectTransform goldCandy3;
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtLevel;


    public bool isFinished = false;
    private float xPos = 0;
    private float yPos = 0;
    private int isUp = 0;
    private bool iconeReduceHeight = true;
    private bool isGoldCandy1 = false;
    private bool isGoldCandy2 = false;
    private bool isGoldCandy3 = false;
    private float rot = 0;
    private Vector3 currentRot;
    private Quaternion currentQuaternionRot;
    private int nGoldCandy = 0;
    private bool isWon = false;

    // =======================================

    public void CheckWinCondition()
    {
        StartCoroutine(CheckAfterDelayWin());
    }

    public void CheckLoseCondition()
    {
        isFinished = true;

        DesactiveInputs();

        StartCoroutine(ShowEndPannel());
    }

    IEnumerator CheckAfterDelayWin()
    {
        yield return new WaitForSeconds(1f);

        GameObject palette = GameObject.Find("Palette");

        if (palette.transform.childCount == 0 && isFinished == false)
        {
            isFinished = true;

            DesactiveInputs();

            StartCoroutine(CheckRemainingItems());
        }
    }

    IEnumerator CheckRemainingItems()
    {
        isWon = true;

        for (int i = 0; i < levelManager.maxItems; i++)
        {
            txtRemainingItems.text = "X" + (levelManager.maxItems - (i + 1)).ToString();

            int scoreToAdd = 0;
            scoreToAdd = UnityEngine.Random.Range(80, 121);
            scoreToAdd *= 10;

            levelManager.AddScore(scoreToAdd);

            ShootSupraLollipop();

            while (txtRemainingItems.fontSize < MAXFONTSIZE)
            {
                yield return new WaitForSeconds(0.01f);

                txtRemainingItems.fontSize += 4;
            }

            while (txtRemainingItems.fontSize > FONTSIZE)
            {
                yield return new WaitForSeconds(0.015f);

                txtRemainingItems.fontSize -= 4;
            }

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1f);

        EndTransition();
        StartCoroutine(StartFirework());
    }

    public void EndTransition()
    {
        bgEnd.SetActive(true);

        StartCoroutine(FinishedLevel());
    }

    IEnumerator FinishedLevel()
    {
        RectTransform pannel = bgEnd.transform.GetChild(0).GetComponent<RectTransform>();

        while (pannel.localPosition.y < 20f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x, pannel.localPosition.y + 4.5f, 0);
        }

        while (pannel.localPosition.y > 0f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x, pannel.localPosition.y - 2.5f, 0);
        }

        GameObject ptcEndLevel;
        ptcEndLevel = Instantiate(ptcEndLevelPrefab, new Vector2(0f, 0f), Quaternion.identity);
        Destroy(ptcEndLevel, 5f);

        soundManager.playAudioClip(16);

        yield return new WaitForSeconds(4f);

        while (pannel.localPosition.y < 20f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x, pannel.localPosition.y + 2.5f, 0);
        }

        while (pannel.localPosition.y > -65f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x, pannel.localPosition.y - 4.5f, 0);
        }

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ShowEndPannel());
        txtCondition.text = "VICTOIRE !";
    }

    IEnumerator StartFirework()
    {
        yield return new WaitForSeconds(1f);

        for(int i = 0; i < 8; i++)
        {
            xPos = UnityEngine.Random.Range(XMIN, XMAX);
            isUp = UnityEngine.Random.Range(0, 2);

            if(isUp == 0)
            {
                yPos = UnityEngine.Random.Range(YMIN1, YMAX1);
            }
            else
            {
                yPos = UnityEngine.Random.Range(YMIN2, YMAX2);
            }

            GameObject ptcRed;
            ptcRed = Instantiate(ptcRedPrefab, new Vector2(xPos, yPos), Quaternion.identity);
            ptcRed.transform.localScale = new Vector2(ptcRed.transform.localScale.x * 1.5f, ptcRed.transform.localScale.y * 1.5f);
            Destroy(ptcRed, 5f);

            xPos = UnityEngine.Random.Range(XMIN, XMAX);
            isUp = UnityEngine.Random.Range(0, 2);

            if (isUp == 0)
            {
                yPos = UnityEngine.Random.Range(YMIN1, YMAX1);
            }
            else
            {
                yPos = UnityEngine.Random.Range(YMIN2, YMAX2);
            }

            GameObject ptcBlue;
            ptcBlue = Instantiate(ptcBluePrefab, new Vector2(xPos, yPos), Quaternion.identity);
            ptcBlue.transform.localScale = new Vector2(ptcBlue.transform.localScale.x * 1.5f, ptcBlue.transform.localScale.y * 1.5f);
            Destroy(ptcBlue, 5f);

            xPos = UnityEngine.Random.Range(XMIN, XMAX);
            isUp = UnityEngine.Random.Range(0, 2);

            if (isUp == 0)
            {
                yPos = UnityEngine.Random.Range(YMIN1, YMAX1);
            }
            else
            {
                yPos = UnityEngine.Random.Range(YMIN2, YMAX2);
            }

            GameObject ptcGreen;
            ptcGreen = Instantiate(ptcGreenPrefab, new Vector2(xPos, yPos), Quaternion.identity);
            ptcGreen.transform.localScale = new Vector2(ptcGreen.transform.localScale.x * 1.5f, ptcGreen.transform.localScale.y * 1.5f);
            Destroy(ptcGreen, 5f);

            xPos = UnityEngine.Random.Range(XMIN, XMAX);
            isUp = UnityEngine.Random.Range(0, 2);

            if (isUp == 0)
            {
                yPos = UnityEngine.Random.Range(YMIN1, YMAX1);
            }
            else
            {
                yPos = UnityEngine.Random.Range(YMIN2, YMAX2);
            }

            GameObject ptcPink;
            ptcPink = Instantiate(ptcPinkPrefab, new Vector2(xPos, yPos), Quaternion.identity);
            ptcPink.transform.localScale = new Vector2(ptcPink.transform.localScale.x * 1.5f, ptcPink.transform.localScale.y * 1.5f);
            Destroy(ptcPink, 5f);

            xPos = UnityEngine.Random.Range(XMIN, XMAX);
            isUp = UnityEngine.Random.Range(0, 2);

            if (isUp == 0)
            {
                yPos = UnityEngine.Random.Range(YMIN1, YMAX1);
            }
            else
            {
                yPos = UnityEngine.Random.Range(YMIN2, YMAX2);
            }

            GameObject ptcOrange;
            ptcOrange = Instantiate(ptcOrangePrefab, new Vector2(xPos, yPos), Quaternion.identity);
            ptcOrange.transform.localScale = new Vector2(ptcOrange.transform.localScale.x * 1.5f, ptcOrange.transform.localScale.y * 1.5f);
            Destroy(ptcOrange, 5f);

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void DesactiveInputs()
    {
        btnLeft.SetActive(false);
        btnRight.SetActive(false);
        btnOptions.SetActive(false);
        Destroy(lollipopSpawner);
    }

    public void ShootSupraLollipop()
    {
        GameObject spittedlollipop;
        spittedlollipop = Instantiate(supraLollipopPrefab, new Vector2(0f, -6f), Quaternion.identity);
        Rigidbody2D rb;
        Vector3 dir;
        float xPosRandom = 0;
        xPosRandom = UnityEngine.Random.Range(-0.5f, 0.5f);
        rb = spittedlollipop.GetComponent<Rigidbody2D>();

        rb.gravityScale = 1f;
        dir = (new Vector2(xPosRandom, 0f) - new Vector2(0f, -6f)).normalized;

        spittedlollipop.layer = 11;
        spittedlollipop.GetComponent<SupraLollipop>().StartToExplode();
        rb.AddForce(dir * 600);
    }

    IEnumerator ShowEndPannel()
    {
        txtLevel.text = "Niveau " + (PlayerPrefs.GetInt("ActualLevel", 0) + 1).ToString();

        endPannel.gameObject.SetActive(true);

        soundManager.playAudioClip(9);

        while (endPannel.localScale.x < 1f)
        {
            yield return new WaitForSeconds(0.025f);

            endPannel.localScale = new Vector2(endPannel.localScale.x + 0.05f, endPannel.localScale.y + 0.05f);
        }

        int widthToReach = (int)Mathf.Floor(levelManager.actualScore * (MAXWIDTH / levelManager.scoreToReach));
        StartCoroutine(FillAnim(widthToReach));

        StartCoroutine(ScoreTextAnim());

        yield return new WaitForSeconds(0.5f);
    }

    private void Update()
    {
        if (isFinished == false)
        {
            return;
        }

        if (iconeReduceHeight)
        {
            btnReturn.localScale = new Vector2(btnReturn.localScale.x + VALUESCALE, btnReturn.localScale.y - VALUESCALE);

            if (btnReturn.localScale.x >= 1f)
            {
                iconeReduceHeight = !iconeReduceHeight;
            }
        }
        else
        {
            btnReturn.localScale = new Vector2(btnReturn.localScale.x - VALUESCALE, btnReturn.localScale.y + VALUESCALE);

            if (btnReturn.localScale.y >= 1f)
            {
                iconeReduceHeight = !iconeReduceHeight;
            }
        }
    }

    public void ReturnMenu()
    {
        if (PlayerPrefs.GetInt("MaxLevel", 0) == PlayerPrefs.GetInt("ActualLevel", 0) && isWon)
        {
            PlayerPrefs.SetInt("MaxLevel", PlayerPrefs.GetInt("MaxLevel", 0) + 1);
        }

        int level = PlayerPrefs.GetInt("ActualLevel", 0);
        string ppName = "GoldCandy" + level.ToString();

        if (PlayerPrefs.GetInt(ppName, 0) < nGoldCandy)
        {
            PlayerPrefs.SetInt(ppName, nGoldCandy);
        }

        soundManager.playAudioClip(5);

        StartCoroutine(ShowTransitionPanel());
    }

    IEnumerator FillAnim(int _width)
    {
        yield return new WaitForSeconds(0.05f);

        fillBar.GetComponent<RectTransform>().sizeDelta = new Vector2(fillBar.GetComponent<RectTransform>().rect.width + 4, 60f);

        if (isGoldCandy1 == false && _width >= 100)
        {
            goldCandy1.gameObject.SetActive(true);
            isGoldCandy1 = true;
            StartCoroutine(GrowGoldCandy(goldCandy1));
            nGoldCandy++;
            soundManager.playAudioClip(13);
        }

        if (isGoldCandy2 == false && _width >= 200)
        {
            goldCandy2.gameObject.SetActive(true);
            isGoldCandy2 = true;
            StartCoroutine(GrowGoldCandy(goldCandy2));
            nGoldCandy++;
            soundManager.playAudioClip(13);
        }

        if (isGoldCandy3 == false && _width >= 300)
        {
            goldCandy3.gameObject.SetActive(true);
            isGoldCandy3 = true;
            StartCoroutine(GrowGoldCandy(goldCandy3));
            nGoldCandy++;
            soundManager.playAudioClip(13);
        }

        if (fillBar.GetComponent<RectTransform>().rect.width < _width)
        {
            StartCoroutine(FillAnim(_width));
        }
    }

    IEnumerator GrowGoldCandy(RectTransform _rect)
    {
        while (_rect.localScale.x < 2f)
        {
            yield return new WaitForSeconds(0.01f);

            _rect.localScale = new Vector2(_rect.localScale.x + 0.1f, _rect.localScale.y + 0.1f);
        }

        while (_rect.localScale.x > 1f)
        {
            yield return new WaitForSeconds(0.015f);

            _rect.localScale = new Vector2(_rect.localScale.x - 0.1f, _rect.localScale.y - 0.1f);
        }

        RectTransform _rect2 = _rect.transform.parent.GetComponent<RectTransform>();
        rot = 0;

        while (rot < 20)
        {
            yield return new WaitForSeconds(0.01f);

            rot += 2;
            currentRot = new Vector3(0, 0, rot);
            currentQuaternionRot.eulerAngles = currentRot;
            _rect2.localRotation = currentQuaternionRot;
        }

        while (rot > 0)
        {
            yield return new WaitForSeconds(0.01f);

            rot -= 2;
            currentRot = new Vector3(0, 0, rot);
            currentQuaternionRot.eulerAngles = currentRot;
            _rect2.localRotation = currentQuaternionRot;
        }
    }

    IEnumerator ScoreTextAnim()
    {
        txtScore.text = levelManager.actualScore.ToString();

        while (txtScore.fontSize < MAXFONTSIZE)
        {
            yield return new WaitForSeconds(0.025f);

            txtScore.fontSize++;
        }

        while (txtScore.fontSize > FONTSIZE)
        {
            yield return new WaitForSeconds(0.045f);

            txtScore.fontSize--;
        }
    }

    IEnumerator ShowTransitionPanel()
    {
        transitionPanel.gameObject.SetActive(true);

        while (transitionPanel.localScale.x < 24)
        {
            transitionPanel.localScale = new Vector2(transitionPanel.localScale.x + 1, transitionPanel.localScale.y + 1);

            yield return new WaitForSeconds(0.01f);
        }

        transitionPanel.GetChild(1).gameObject.SetActive(true);
        transitionPanel.GetChild(2).gameObject.SetActive(true);

        int count = 0;
        float speed = 1;

        while (count < 140)
        {
            for (int i = 0; i < transitionPanel.GetChild(1).childCount; i++)
            {
                if (i % 2 == 0)
                {
                    speed = 1;
                }
                else
                {
                    speed = 1.4f;
                }

                transitionPanel.GetChild(1).GetChild(i).localPosition = new Vector3(transitionPanel.GetChild(1).GetChild(i).localPosition.x, transitionPanel.GetChild(1).GetChild(i).localPosition.y - speed, 0f);
                transitionPanel.GetChild(2).GetChild(i).localPosition = new Vector3(transitionPanel.GetChild(2).GetChild(i).localPosition.x, transitionPanel.GetChild(2).GetChild(i).localPosition.y + speed, 0f);
            }

            yield return new WaitForSeconds(0.01f);

            count++;
        }

        yield return new WaitForSeconds(0.4f);

        SceneManager.LoadScene("MenuSelectionLevels", LoadSceneMode.Single);
    }
}
