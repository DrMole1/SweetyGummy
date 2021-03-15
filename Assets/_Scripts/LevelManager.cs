using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private const float MAXWIDTH = 300;
    private const int FONTSIZE = 36;
    private const int MAXFONTSIZE = 60;
    private const float MINDELAY = 0.01f;
    private const float MOVE = 0.06f;

    // ============== VARIABLES ==============
    public float scoreToReach = 0;
    public int maxItems = 0;

    [Header("Parameters to Manage")]
    public GameObject[] palettes;
    public float[] scoresToReach;
    public int[] maxItemsGame;
    public int[] colors;

    [Header("Objects to Drop")]
    public TextMeshProUGUI txtScore;
    public RectTransform scoreBar;
    public GameObject fillBar;
    public RectTransform goldCandy1;
    public RectTransform goldCandy2;
    public RectTransform goldCandy3;
    public Transform palette;
    public LollipopSpawner lollipopSpawner;
    public TextMeshProUGUI txtRemainingItems;
    public SoundManager soundManager;

    public int actualScore = 0;
    private bool isTextGrowing = false;
    private bool isBarGrowing = false;
    private bool isGoldCandy1 = false;
    private bool isGoldCandy2 = false;
    private bool isGoldCandy3 = false;

    private float rot = 0;
    private Vector3 currentRot;
    private Quaternion currentQuaternionRot;
    private int level = 0;

    // =======================================


    public void Awake()
    {
        level = PlayerPrefs.GetInt("ActualLevel", 0);

        GameObject Palette;
        Palette = Instantiate(palettes[level], new Vector3(0, 0, 0), Quaternion.identity);
        Palette.name = "Palette";

        scoreToReach = scoresToReach[level];

        maxItems += maxItemsGame[level];

        lollipopSpawner.nColor = colors[level];
    }

    public void Start()
    {
        palette = GameObject.Find("Palette").transform;
        txtRemainingItems.text = "X" + maxItems.ToString();
    }

    public void AddScore(int _scoreToAdd)
    {
        actualScore += _scoreToAdd;

        txtScore.text = actualScore.ToString();

        int widthToReach = (int)Mathf.Floor(actualScore * (MAXWIDTH / scoreToReach));

        StartCoroutine(FillAnim(widthToReach));

        if (_scoreToAdd == 100 && isTextGrowing == false)
        {
            StartCoroutine(ScoreTextAnim());
        }

        if (_scoreToAdd == 100 && isBarGrowing == false)
        {
            StartCoroutine(BarAnim());
        }

        if(isGoldCandy1 == false && actualScore >= scoreToReach / 3)
        {
            goldCandy1.gameObject.SetActive(true);
            isGoldCandy1 = true;
            StartCoroutine(GrowGoldCandy(goldCandy1));
            soundManager.playAudioClip(13);
        }

        if (isGoldCandy2 == false && actualScore >= scoreToReach / 3 * 2)
        {
            goldCandy2.gameObject.SetActive(true);
            isGoldCandy2 = true;
            StartCoroutine(GrowGoldCandy(goldCandy2));
            soundManager.playAudioClip(13);
        }

        if (isGoldCandy3 == false && actualScore >= scoreToReach)
        {
            goldCandy3.gameObject.SetActive(true);
            isGoldCandy3 = true;
            StartCoroutine(GrowGoldCandy(goldCandy3));
            soundManager.playAudioClip(13);
        }
    }


    IEnumerator FillAnim(int _width)
    {
        yield return new WaitForSeconds(0.05f);

        fillBar.GetComponent<RectTransform>().sizeDelta = new Vector2(fillBar.GetComponent<RectTransform>().rect.width + 1, 60f);

        if(fillBar.GetComponent<RectTransform>().rect.width < _width)
        {
            StartCoroutine(FillAnim(_width));
        }
    }


    IEnumerator ScoreTextAnim()
    {
        isTextGrowing = true;

        while(txtScore.fontSize < MAXFONTSIZE)
        {
            yield return new WaitForSeconds(0.025f);

            txtScore.fontSize++;
        }

        while (txtScore.fontSize > FONTSIZE)
        {
            yield return new WaitForSeconds(0.045f);

            txtScore.fontSize--;
        }

        isTextGrowing = false;
    }


    IEnumerator BarAnim()
    {
        isBarGrowing = true;
        yield return new WaitForSeconds(0.6f);

        while (scoreBar.localScale.x < 1.2f)
        {
            yield return new WaitForSeconds(0.025f);

            scoreBar.localScale = new Vector2(scoreBar.localScale.x + 0.05f, scoreBar.localScale.y + 0.05f);
        }

        while (scoreBar.localScale.x > 1f)
        {
            yield return new WaitForSeconds(0.04f);

            scoreBar.localScale = new Vector2(scoreBar.localScale.x - 0.05f, scoreBar.localScale.y - 0.05f);
        }

        isBarGrowing = false;
    }


    IEnumerator GrowGoldCandy(RectTransform _rect)
    {
        while (_rect.localScale.x < 2f)
        {
            yield return new WaitForSeconds(0.01f);

            _rect.localScale = new Vector2(_rect.localScale.x + 0.05f, _rect.localScale.y + 0.05f);
        }

        while (_rect.localScale.x > 1f)
        {
            yield return new WaitForSeconds(0.015f);

            _rect.localScale = new Vector2(_rect.localScale.x - 0.05f, _rect.localScale.y - 0.05f);
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

    public void EarthQuake()
    {
        for (int i = 0; i < palette.transform.childCount - 1; i++)
        {
            StartCoroutine(StartEarthQuake(palette.transform.GetChild(i)));
        }
    }

    IEnumerator StartEarthQuake(Transform _candy)
    {
        yield return new WaitForSeconds(0.015f);

        if (_candy == null)
            yield return null;
        else
        {
            float xPosInit = _candy.position.x;
            float amplitude = UnityEngine.Random.Range(0.1f, 0.22f);
            int dir = UnityEngine.Random.Range(0, 2);

            if (dir == 0)
            {
                while (_candy.localPosition.x > xPosInit - amplitude)
                {
                    yield return new WaitForSeconds(MINDELAY);

                    _candy.localPosition = new Vector3(_candy.localPosition.x - MOVE, _candy.localPosition.y, 0);
                }

                while (_candy.localPosition.x < xPosInit + amplitude)
                {
                    yield return new WaitForSeconds(MINDELAY);

                    _candy.localPosition = new Vector3(_candy.localPosition.x + MOVE, _candy.localPosition.y, 0);
                }

                while (_candy.localPosition.x > xPosInit - amplitude / 2)
                {
                    yield return new WaitForSeconds(MINDELAY);

                    _candy.localPosition = new Vector3(_candy.localPosition.x - MOVE, _candy.localPosition.y, 0);
                }

                while (_candy.localPosition.x < xPosInit + amplitude / 2)
                {
                    yield return new WaitForSeconds(MINDELAY);

                    _candy.localPosition = new Vector3(_candy.localPosition.x + MOVE, _candy.localPosition.y, 0);
                }

                while (_candy.localPosition.x > xPosInit)
                {
                    yield return new WaitForSeconds(MINDELAY);

                    _candy.localPosition = new Vector3(_candy.localPosition.x - MOVE, _candy.localPosition.y, 0);
                }

                _candy.localPosition = new Vector3(xPosInit, _candy.localPosition.y, 0);
            }
            else
            {
                while (_candy.localPosition.x < xPosInit + amplitude)
                {
                    yield return new WaitForSeconds(MINDELAY);

                    _candy.localPosition = new Vector3(_candy.localPosition.x + MOVE, _candy.localPosition.y, 0);
                }

                while (_candy.localPosition.x > xPosInit - amplitude)
                {
                    yield return new WaitForSeconds(MINDELAY);

                    _candy.localPosition = new Vector3(_candy.localPosition.x - MOVE, _candy.localPosition.y, 0);
                }

                while (_candy.localPosition.x < xPosInit + amplitude / 2)
                {
                    yield return new WaitForSeconds(MINDELAY);

                    _candy.localPosition = new Vector3(_candy.localPosition.x + MOVE, _candy.localPosition.y, 0);
                }

                while (_candy.localPosition.x > xPosInit - amplitude / 2)
                {
                    yield return new WaitForSeconds(MINDELAY);

                    _candy.localPosition = new Vector3(_candy.localPosition.x - MOVE, _candy.localPosition.y, 0);
                }

                while (_candy.localPosition.x < xPosInit)
                {
                    yield return new WaitForSeconds(MINDELAY);

                    _candy.localPosition = new Vector3(_candy.localPosition.x + MOVE, _candy.localPosition.y, 0);
                }

                _candy.localPosition = new Vector3(xPosInit, _candy.localPosition.y, 0);
            }
        }
    }
}
