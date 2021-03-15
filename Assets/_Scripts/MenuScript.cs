using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{
    private const float MINDELAY = 0.01f;
    private const float VALUESCALE = 0.002f;

    public GameObject[] letters;
    public RectTransform btnQuit;
    public RectTransform btnSuivant;
    public GameObject menu;
    public GameObject btnSound;
    public GameObject btnMusic;
    public GameObject btnPlay;
    public GameObject btnQuitGame;
    public GameObject btnHowToPlay;
    public TextMeshProUGUI txtInfo;
    public Image imgInfo;
    public Sprite[] spriteInfos;
    public SoundManager soundManager;
    public GameObject textCredit;

    private bool iconeReduceHeight = true;
    public bool menuIsOpen = false;
    private bool menuIsOnAnim = false;
    private int slide = 1;



    private void Start()
    {
        StartCoroutine(ShowTitle());
    }

    IEnumerator ShowTitle()
    {
        for(int i = 0; i < letters.Length; i++)
        {
            letters[i].SetActive(true);
            StartCoroutine(ShowLetter(letters[i].transform));

            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator ShowLetter(Transform _letter)
    {
        float initScale;
        initScale = _letter.localScale.x;

        while (_letter.localScale.x < initScale + 0.7f)
        {
            yield return new WaitForSeconds(MINDELAY);

            _letter.localScale = new Vector3(_letter.localScale.x + 0.1f, _letter.localScale.y + 0.1f, 1);
        }

        while (_letter.localScale.x > initScale)
        {
            yield return new WaitForSeconds(MINDELAY);

            _letter.localScale = new Vector3(_letter.localScale.x - 0.06f, _letter.localScale.y - 0.06f, 1);
        }

        StartCoroutine(RotateLetter(_letter));
    }

    IEnumerator RotateLetter(Transform _letter)
    {
        float rot = 0;
        float rot2 = 0;
        Vector3 currentRot;
        Quaternion currentQuaternionRot = Quaternion.identity;

        while (rot < 20)
        {
            yield return new WaitForSeconds(0.01f);

            rot += 1;
            rot2 += 0.4f;
            currentRot = new Vector3(0, 0, rot2);
            currentQuaternionRot.eulerAngles = currentRot;
            _letter.localRotation = currentQuaternionRot;
        }

        while (rot > -20)
        {
            yield return new WaitForSeconds(0.1f);

            rot -= 1;
            rot2 -= 0.4f;
            currentRot = new Vector3(0, 0, rot2);
            currentQuaternionRot.eulerAngles = currentRot;
            _letter.localRotation = currentQuaternionRot;
        }

        while (rot < 0)
        {
            yield return new WaitForSeconds(0.01f);

            rot += 1;
            rot2 += 0.4f;
            currentRot = new Vector3(0, 0, rot2);
            currentQuaternionRot.eulerAngles = currentRot;
            _letter.localRotation = currentQuaternionRot;
        }

        StartCoroutine(RotateLetter(_letter));
    }

    private void Update()
    {
        if (iconeReduceHeight)
        {
            btnQuit.localScale = new Vector2(btnQuit.localScale.x + VALUESCALE, btnQuit.localScale.y - VALUESCALE);
            btnSuivant.localScale = new Vector2(btnSuivant.localScale.x + VALUESCALE, btnSuivant.localScale.y - VALUESCALE);

            if (btnQuit.localScale.x >= 1f)
            {
                iconeReduceHeight = !iconeReduceHeight;
            }
        }
        else
        {
            btnQuit.localScale = new Vector2(btnQuit.localScale.x - VALUESCALE, btnQuit.localScale.y + VALUESCALE);
            btnSuivant.localScale = new Vector2(btnSuivant.localScale.x - VALUESCALE, btnSuivant.localScale.y + VALUESCALE);

            if (btnQuit.localScale.y >= 1f)
            {
                iconeReduceHeight = !iconeReduceHeight;
            }
        }
    }

    public void ShowMenu()
    {
        if (menuIsOnAnim)
        {
            return;
        }

        if (menuIsOpen)
        {
            return;
        }

        menu.SetActive(true);
        menuIsOpen = true;

        soundManager.playAudioClip(9);

        StartCoroutine(DownMenu());
        textCredit.SetActive(false);
    }

    public void HideMenu()
    {
        if (menuIsOnAnim)
        {
            return;
        }

        if (!menuIsOpen)
        {
            return;
        }

        soundManager.playAudioClip(10);

        StartCoroutine(UpMenu());
        textCredit.SetActive(true);
    }

    IEnumerator DownMenu()
    {
        Transform pannel = menu.transform.GetChild(0);
        menuIsOnAnim = true;

        slide = 1;
        btnSuivant.gameObject.SetActive(true);
        txtInfo.text = "Déplacez Gummy en touchant les flèches en gelée. Attrapez les sucettes qui tombent : ce sont vos munitions !";
        imgInfo.overrideSprite = spriteInfos[0];

        btnHowToPlay.SetActive(false);
        btnPlay.SetActive(false);
        btnQuitGame.SetActive(false);
        btnSound.SetActive(false);
        btnMusic.SetActive(false);

        while (pannel.localPosition.x < 100f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x + 55, pannel.localPosition.y, 0);
        }

        while (pannel.localPosition.x > 0f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x - 15f, pannel.localPosition.y, 0);
        }

        menuIsOnAnim = false;
    }

    IEnumerator UpMenu()
    {
        Transform pannel = menu.transform.GetChild(0);
        menuIsOnAnim = true;

        while (pannel.localPosition.x < 100f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x + 15f, pannel.localPosition.y, 0);
        }

        while (pannel.localPosition.x > -1100f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x - 55f, pannel.localPosition.y, 0);
        }

        menuIsOnAnim = false;

        menu.SetActive(false);
        menuIsOpen = false;

        btnHowToPlay.SetActive(true);
        btnPlay.SetActive(true);
        btnQuitGame.SetActive(true);
        btnSound.SetActive(true);
        btnMusic.SetActive(true);
    }

    public void NextSlide()
    {
        slide++;

        soundManager.playAudioClip(6);

        if (slide == 2)
        {
            txtInfo.text = "Touchez le haut de l'écran pour viser et tirer la sucette gobée. Explosez tous les bonbons de chaque niveau !";
            imgInfo.overrideSprite = spriteInfos[1];
        }
        else if (slide == 3)
        {
            txtInfo.text = "Attention. Seules les sucettes d'une couleur donnée peuvent éclater les bonbons de cette même couleur !";
            imgInfo.overrideSprite = spriteInfos[2];
        }
        else if(slide == 4)
        {
            txtInfo.text = "Explosez beaucoup de bonbons avec une même sucette pour obtenir des minutions aux effets suprenants !";
            imgInfo.overrideSprite = spriteInfos[3];
            btnSuivant.gameObject.SetActive(false);
        }
    }
}
