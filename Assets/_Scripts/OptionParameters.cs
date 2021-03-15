using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionParameters : MonoBehaviour
{
    private const float VALUESCALE = 0.002f;

    // ============== VARIABLES ==============

    [Header("Parameters")]
    public static bool isMusic = true;
    public static bool isSound = true;
    public bool isFirstMenu = false;

    [Header("Objects to Drop")]
    public GameObject menu;
    public AudioSource audioSource;
    public Image soundImage;
    public Image musicImage;
    public RectTransform btnQuit;
    public RectTransform btnSound;
    public RectTransform btnMusic;
    public RectTransform btnQuitLevel;
    public SoundManager soundManager;
    public RectTransform transitionPanel;

    [Header("Sprites")]
    public Sprite soundOn;
    public Sprite soundOff;
    public Sprite musicOn;
    public Sprite musicOff;

    public bool menuIsOpen = false;
    private bool menuIsOnAnim = false;
    private bool iconeReduceHeight = true;

    // =======================================

    private void Start()
    {
        if(!isFirstMenu)
            StartCoroutine(UnshowTransitionPanel());
        else
            transitionPanel.localScale = new Vector2(0, 0);

        if (PlayerPrefs.GetInt("Sound", 1) == 0)
        {
            isSound = false;
            audioSource.volume = 0;
            soundImage.overrideSprite = soundOff;
        }

        if (PlayerPrefs.GetInt("Music", 1) == 0)
        {
            isMusic = false;
            GameObject.Find("MusicManager").GetComponent<AudioSource>().volume = 0;
            musicImage.overrideSprite = musicOff;
        }
    }

    public void ShowMenu()
    {
        if(menuIsOnAnim)
        {
            return;
        }

        if (menuIsOpen)
        {
            return;
        }

        soundManager.playAudioClip(9);
        menu.SetActive(true);
        menuIsOpen = true;

        StartCoroutine(DownMenu());
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
    }

    public void UpdateSound()
    {
        if(isSound)
        {
            soundManager.playAudioClip(8);
            isSound = false;
            audioSource.volume = 0;
            soundImage.overrideSprite = soundOff;
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            soundManager.playAudioClip(7);
            isSound = true;
            audioSource.volume = 1;
            soundImage.overrideSprite = soundOn;
            PlayerPrefs.SetInt("Sound", 1);
        }
    }

    public void UpdateMusic()
    {
        if (isMusic)
        {
            soundManager.playAudioClip(8);
            isMusic = false;
            GameObject.Find("MusicManager").GetComponent<AudioSource>().volume = 0;
            musicImage.overrideSprite = musicOff;
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            soundManager.playAudioClip(7);
            isMusic = true;
            GameObject.Find("MusicManager").GetComponent<AudioSource>().volume = 0.6f;
            musicImage.overrideSprite = musicOn;
            PlayerPrefs.SetInt("Music", 1);
        }
    }

    IEnumerator DownMenu()
    {
        Transform pannel = menu.transform.GetChild(0);
        menuIsOnAnim = true;

        while (pannel.localPosition.y > -8f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(0, pannel.localPosition.y - 3, 0);
        }

        while (pannel.localPosition.y < 0f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(0, pannel.localPosition.y + 1.5f, 0);
        }

        menuIsOnAnim = false;

        Time.timeScale = 0f;
    }

    IEnumerator UpMenu()
    {
        Time.timeScale = 1f;

        Transform pannel = menu.transform.GetChild(0);
        menuIsOnAnim = true;

        while (pannel.localPosition.y > -8f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(0, pannel.localPosition.y - 1.5f, 0);
        }

        while (pannel.localPosition.y < 63f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(0, pannel.localPosition.y + 3f, 0);
        }

        menuIsOnAnim = false;

        menu.SetActive(false);
        menuIsOpen = false;
    }

    private void Update()
    {
        if (menuIsOpen)
        {
            if(iconeReduceHeight)
            {
                btnQuit.localScale = new Vector2(btnQuit.localScale.x + VALUESCALE, btnQuit.localScale.y - VALUESCALE);
                btnSound.localScale = new Vector2(btnSound.localScale.x + VALUESCALE, btnSound.localScale.y - VALUESCALE);
                btnMusic.localScale = new Vector2(btnMusic.localScale.x + VALUESCALE, btnMusic.localScale.y - VALUESCALE);
                btnQuitLevel.localScale = new Vector2(btnQuitLevel.localScale.x + VALUESCALE, btnQuitLevel.localScale.y - VALUESCALE);

                if(btnQuit.localScale.x >= 1f)
                {
                    iconeReduceHeight = !iconeReduceHeight;
                }
            }
            else
            {
                btnQuit.localScale = new Vector2(btnQuit.localScale.x - VALUESCALE, btnQuit.localScale.y + VALUESCALE);
                btnSound.localScale = new Vector2(btnSound.localScale.x - VALUESCALE, btnSound.localScale.y + VALUESCALE);
                btnMusic.localScale = new Vector2(btnMusic.localScale.x - VALUESCALE, btnMusic.localScale.y + VALUESCALE);
                btnQuitLevel.localScale = new Vector2(btnQuitLevel.localScale.x - VALUESCALE, btnQuitLevel.localScale.y + VALUESCALE);

                if (btnQuit.localScale.y >= 1f)
                {
                    iconeReduceHeight = !iconeReduceHeight;
                }
            }
        }
    }

    public void Quitter()
    {
        soundManager.playAudioClip(5);
        Application.Quit();
    }

    public void Jouer()
    {
        Time.timeScale = 1f;
        soundManager.playAudioClip(6);

        StartCoroutine(ShowTransitionPanel());
    }

    IEnumerator UnshowTransitionPanel()
    {
        transitionPanel.gameObject.SetActive(true);

        while(transitionPanel.localScale.x > 0)
        {
            transitionPanel.localScale = new Vector2(transitionPanel.localScale.x - 1, transitionPanel.localScale.y - 1);

            yield return new WaitForSeconds(0.01f);
        }

        transitionPanel.gameObject.SetActive(false);
    }

    IEnumerator ShowTransitionPanel()
    {
        transitionPanel.gameObject.SetActive(true);

        while (transitionPanel.localScale.x < 30)
        {
            transitionPanel.localScale = new Vector2(transitionPanel.localScale.x + 1, transitionPanel.localScale.y + 1);

            yield return new WaitForSeconds(0.01f);
        }

        transitionPanel.GetChild(1).gameObject.SetActive(true);
        transitionPanel.GetChild(2).gameObject.SetActive(true);

        int count = 0;
        float speed = 1;

        while(count < 140)
        {
            for(int i = 0; i < transitionPanel.GetChild(1).childCount; i++)
            {
                if(i % 2 == 0)
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
