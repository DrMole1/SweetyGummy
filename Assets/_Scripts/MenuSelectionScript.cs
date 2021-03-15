using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuSelectionScript : MonoBehaviour
{
    private const float VALUESCALE = 0.002f;


    public GameObject menu;

    public GameObject btnReturn;
    public GameObject btnSound;
    public GameObject btnMusic;
    public GameObject buttons;

    public Sprite successedLevel;
    public Sprite actualLevel;

    public bool menuIsOpen = false;

    public GameObject ptcActualLevelPrefab;
    public Transform map;

    public Transform playPannel;
    public TextMeshProUGUI txtNiveau;
    public GameObject goldCandy1;
    public GameObject goldCandy2;
    public GameObject goldCandy3;
    public SoundManager soundManager;

    public RectTransform next;
    public RectTransform back;
    public Transform totalMap;
    public RectTransform marquePage;
    public TextMeshProUGUI txtMarquePage;
    public Transform panneaux;
    public RectTransform transitionPanel;

    private bool menuIsOnAnim = false;
    private RectTransform btnActualLevel;
    private bool isActualButtonCanAnim = false;
    private bool iconeReduceHeight = true;
    private int maxPage = 0;
    private int actualPage = 0;
    private bool isMarquePageOnAnim = false;

    public void Start()
    {
        StartMusic();

        maxPage = (int)Mathf.Floor(PlayerPrefs.GetInt("MaxLevel", 0) / 10);

        for (int i = 0; i <= maxPage; i++)
        {
            if(i != maxPage)
            {
                for(int j = 0; j < 10; j++)
                {
                    MakeSuccessedLevel(buttons.transform.GetChild(i).GetChild(j).gameObject);
                }
            }
            else
            {
                int level = PlayerPrefs.GetInt("MaxLevel", 0) - maxPage * 10;

                for (int k = 0; k < level; k++)
                {
                    MakeSuccessedLevel(buttons.transform.GetChild(i).GetChild(k).gameObject);
                }

                MakeActualLevel(buttons.transform.GetChild(i).GetChild(level).gameObject);
            }
        }

        StartCoroutine(ActiveAllButtons());
        StartCoroutine(MoveNavigationButtons());

        if (actualPage == maxPage)
        {
            marquePage.gameObject.SetActive(false);
            marquePage.localPosition = new Vector3(marquePage.localPosition.x, 915f, marquePage.localPosition.z);
        }

        txtMarquePage.text = (PlayerPrefs.GetInt("MaxLevel", 0) + 1).ToString();

        ShowPanneaux();
    }

    public void Return()
    {
        soundManager.playAudioClip(5);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ShowMenu()
    {
        if (menuIsOnAnim)
        {
            return;
        }

        if (menuIsOpen)
        {
            soundManager.playAudioClip(9);
            StartCoroutine(UpMenu());
        }
        else
        {
            menuIsOpen = true;
            soundManager.playAudioClip(10);
            StartCoroutine(DownMenu());
        }
    }


    IEnumerator DownMenu()
    {
        RectTransform pannel = menu.transform.GetChild(0).GetComponent<RectTransform>();
        menuIsOnAnim = true;

        btnReturn.SetActive(false);
        btnMusic.SetActive(false);
        btnSound.SetActive(false);
        buttons.SetActive(false);

        while (pannel.localPosition.y < -17f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x, pannel.localPosition.y + 5f, 0);
        }

        while (pannel.localPosition.y > -20f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x, pannel.localPosition.y - 2f, 0);
        }

        menuIsOnAnim = false;
    }

    IEnumerator UpMenu()
    {
        RectTransform pannel = menu.transform.GetChild(0).GetComponent<RectTransform>();
        menuIsOnAnim = true;

        while (pannel.localPosition.y < -17f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x, pannel.localPosition.y + 2f, 0);
        }

        while (pannel.localPosition.y > -58.5f)
        {
            yield return new WaitForSeconds(0.01f);

            pannel.localPosition = new Vector3(pannel.localPosition.x, pannel.localPosition.y - 5f, 0);
        }

        menuIsOnAnim = false;

        menuIsOpen = false;

        btnReturn.SetActive(true);
        btnSound.SetActive(true);
        btnMusic.SetActive(true);
        buttons.SetActive(true);
    }

    public void SelectLevel(int _level)
    {
        if(PlayerPrefs.GetInt("MaxLevel", 0) < _level)
        {
            return;
        }

        PlayerPrefs.SetInt("ActualLevel", _level);

        soundManager.playAudioClip(9);

        StartCoroutine(ShowPlayPannel());
    }

    public void MakeSuccessedLevel(GameObject _button)
    {
        _button.GetComponent<Image>().overrideSprite = successedLevel;
        _button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
        _button.GetComponent<RectTransform>().localScale = new Vector2(1f, 0.8f);
    }

    public void MakeActualLevel(GameObject _button)
    {
        _button.GetComponent<Image>().overrideSprite = actualLevel;
        _button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
        _button.GetComponent<RectTransform>().localScale = new Vector2(0.95f, 1f);

        btnActualLevel = _button.GetComponent<RectTransform>();
    }

    IEnumerator ActiveAllButtons()
    {
        if ((int)Mathf.Floor(PlayerPrefs.GetInt("MaxLevel", 0) / 10) == actualPage)
        {
            GameObject ptcActualLevel;
            ptcActualLevel = Instantiate(ptcActualLevelPrefab, map.GetChild(PlayerPrefs.GetInt("MaxLevel", 0)).position, Quaternion.identity);
            ptcActualLevel.name = "PtcActualLevel";
        }
        else
        {
            Destroy(GameObject.Find("PtcActualLevel"));
        }

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.2f);

            buttons.transform.GetChild(actualPage).GetChild(i).gameObject.SetActive(true);
            StartCoroutine(GrowAnimButtons(buttons.transform.GetChild(actualPage).GetChild(i).gameObject.GetComponent<RectTransform>()));
        }

        yield return new WaitForSeconds(0.6f);

        isActualButtonCanAnim = true;

        CheckForNavigation();
    }

    IEnumerator GrowAnimButtons(RectTransform _button)
    {
        float initScaleX;
        initScaleX = _button.localScale.x;

        while (_button.localScale.x < initScaleX + 0.5f)
        {
            yield return new WaitForSeconds(0.01f);

            _button.localScale = new Vector3(_button.localScale.x + 0.07f, _button.localScale.y + 0.07f, 1);
        }

        while (_button.localScale.x > initScaleX)
        {
            yield return new WaitForSeconds(0.01f);

            _button.localScale = new Vector3(_button.localScale.x - 0.04f, _button.localScale.y - 0.04f, 1);
        }
    }

    private void Update()
    {
        if(isActualButtonCanAnim == false)
        {
            return;
        }

        if (iconeReduceHeight)
        {
            btnActualLevel.localScale = new Vector2(btnActualLevel.localScale.x + VALUESCALE, btnActualLevel.localScale.y - VALUESCALE);

            if (btnActualLevel.localScale.x >= 1f)
            {
                iconeReduceHeight = !iconeReduceHeight;
            }
        }
        else
        {
            btnActualLevel.localScale = new Vector2(btnActualLevel.localScale.x - VALUESCALE, btnActualLevel.localScale.y + VALUESCALE);

            if (btnActualLevel.localScale.y >= 1f)
            {
                iconeReduceHeight = !iconeReduceHeight;
            }
        }
    }

    IEnumerator ShowPlayPannel()
    {
        playPannel.parent.gameObject.SetActive(true);

        txtNiveau.text = "Niveau " + (PlayerPrefs.GetInt("ActualLevel", 0) + 1).ToString();

        int level = PlayerPrefs.GetInt("ActualLevel", 0);
        string ppName = "GoldCandy" + level.ToString();

        if (PlayerPrefs.GetInt(ppName, 0) >= 1)
        {
            goldCandy1.SetActive(true);
        }

        if (PlayerPrefs.GetInt(ppName, 0) >= 2)
        {
            goldCandy2.SetActive(true);
        }

        if (PlayerPrefs.GetInt(ppName, 0) >= 3)
        {
            goldCandy3.SetActive(true);
        }

        while (playPannel.localScale.x < 0.05f)
        {
            yield return new WaitForSeconds(0.025f);

            playPannel.localScale = new Vector2(playPannel.localScale.x + 0.0025f, playPannel.localScale.y + 0.0025f);
        }
    }

    public void Jouer()
    {
        soundManager.playAudioClip(6);

        StartCoroutine(ShowTransitionPanel());
    }

    public void Quitter()
    {
        soundManager.playAudioClip(5);
        StartCoroutine(UnshowPlayPannel());
    }

    IEnumerator UnshowPlayPannel()
    {
        while (playPannel.localScale.x > 0.01f)
        {
            yield return new WaitForSeconds(0.025f);

            playPannel.localScale = new Vector2(playPannel.localScale.x - 0.005f, playPannel.localScale.y - 0.005f);
        }

        playPannel.parent.gameObject.SetActive(false);

        goldCandy1.SetActive(false);
        goldCandy2.SetActive(false);
        goldCandy3.SetActive(false);
    }

    public void CheckForNavigation()
    {
        if(actualPage < maxPage)
        {
            next.gameObject.SetActive(true);
            marquePage.gameObject.SetActive(true);
        }

        if(actualPage != 0)
        {
            back.gameObject.SetActive(true);
        }

        if(actualPage == maxPage)
        {
            marquePage.gameObject.SetActive(false);
            marquePage.localPosition = new Vector3(marquePage.localPosition.x, 915f, marquePage.localPosition.z);
        }
    }

    IEnumerator MoveNavigationButtons()
    {
        int count = 0;

        while (count < 20)
        {
            yield return new WaitForSeconds(0.01f);
            count++;

            back.localPosition = new Vector3(back.localPosition.x + 2, back.localPosition.y, 0);
            next.localPosition = new Vector3(next.localPosition.x - 2, next.localPosition.y, 0);

            if(!isMarquePageOnAnim)
            {
                marquePage.localPosition = new Vector3(marquePage.localPosition.x, marquePage.localPosition.y - 1, 0);
            }
        }

        while (count > 0)
        {
            yield return new WaitForSeconds(0.01f);
            count--;

            back.localPosition = new Vector3(back.localPosition.x - 2, back.localPosition.y, 0);
            next.localPosition = new Vector3(next.localPosition.x + 2, next.localPosition.y, 0);

            if (!isMarquePageOnAnim)
            {
                marquePage.localPosition = new Vector3(marquePage.localPosition.x, marquePage.localPosition.y + 1, 0);
            }
        }

        StartCoroutine(MoveNavigationButtons());
    }

    public void Back()
    {
        DesactiveAllButtons();
        actualPage--;
        StartCoroutine(ScrollMapBack());
    }

    public void Next()
    {
        DesactiveAllButtons();
        actualPage++;
        StartCoroutine(ScrollMapNext());
    }

    public void DesactiveAllButtons()
    {
        if(GameObject.Find("PtcActualLevel") != null)
        { 
            Destroy(GameObject.Find("PtcActualLevel")); 
        }

        next.gameObject.SetActive(false);
        back.gameObject.SetActive(false);

        for (int i = 0; i < 10; i++)
        {
            buttons.transform.GetChild(actualPage).GetChild(i).gameObject.SetActive(false);
        }
    }

    IEnumerator ScrollMapBack()
    {
        float xPos = actualPage * -5f;

        while (totalMap.localPosition.x < xPos)
        {
            yield return new WaitForSeconds(0.01f);

            totalMap.localPosition = new Vector3(totalMap.localPosition.x + 0.2f, totalMap.localPosition.y, 0);
        }

        StartCoroutine(ActiveAllButtons());
    }

    IEnumerator ScrollMapNext()
    {
        float xPos = actualPage * -5f;

        while (totalMap.localPosition.x > xPos)
        {
            yield return new WaitForSeconds(0.01f);

            totalMap.localPosition = new Vector3(totalMap.localPosition.x - 0.2f, totalMap.localPosition.y, 0);
        }

        StartCoroutine(ActiveAllButtons());
    }

    public void ActiveMarquePage()
    {
        if(actualPage == maxPage)
        {
            return;
        }

        if(isMarquePageOnAnim)
        {
            return;
        }

        DesactiveAllButtons();
        actualPage = maxPage;
        StartCoroutine(MarquePageTransition());
    }

    IEnumerator MarquePageTransition()
    {
        isMarquePageOnAnim = true;

        while (marquePage.localPosition.y > 850f)
        {
            yield return new WaitForSeconds(0.01f);

            marquePage.localPosition = new Vector3(marquePage.localPosition.x, marquePage.localPosition.y - 5f, 0);
        }

        while (marquePage.localPosition.y < 1200f)
        {
            yield return new WaitForSeconds(0.01f);

            marquePage.localPosition = new Vector3(marquePage.localPosition.x, marquePage.localPosition.y + 12f, 0);
        }

        isMarquePageOnAnim = false;

        float xPos = actualPage * -5f;

        while (totalMap.localPosition.x > xPos)
        {
            yield return new WaitForSeconds(0.01f);

            totalMap.localPosition = new Vector3(totalMap.localPosition.x - 0.2f, totalMap.localPosition.y, 0);
        }

        StartCoroutine(ActiveAllButtons());
    }

    public void ShowPanneaux()
    {
        bool active = true;

        for(int i = 0; i < panneaux.childCount; i++)
        {
            active = true;

            for(int j = 0; j < 10; j++)
            {
                string ppName = "GoldCandy" + (i * 10 + j).ToString();
                if(PlayerPrefs.GetInt(ppName, 0) != 3)
                {
                    active = false;
                }
            }

            if(active)
            {
                panneaux.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void StartMusic()
    {
        MusicManager musicManager;

        if(GameObject.Find("MusicManager") != null)
        {
            musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
            musicManager.PlayMusicMenu();
        }
        else
        {
            print("No Music Manager found.");
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

        SceneManager.LoadScene("Game0", LoadSceneMode.Single);
    }
}