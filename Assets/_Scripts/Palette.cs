using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palette : MonoBehaviour
{
    private const int MAXDELAY = 50;

    // ============== VARIABLES ==============

    public int maxSteps = 0;
    public int actualStep = 0;
    public bool hasGuimauve = false;
    public GameObject guimauvePrefab;
    public GameObject[] jellyBeans;

    [System.Serializable]
    public struct CandyPalette
    {
        public int step;
        public int place;
        public GameObject candy;
    }

    public CandyPalette[] candyPalette = new CandyPalette[255];

    private bool stop = false;
    [HideInInspector] public bool canSpawnGuimauve = true;

    // =======================================


    private void Awake()
    {
        int maxPlaces = 7;
        int nCandy = 0;

        for(int i = 0; i < maxSteps; i++)
        {
            if(i != 0)
            {
                maxPlaces = CalculateMaxPlaces(i);
            }

            for(int j = 0; j < maxPlaces; j++)
            {
                candyPalette[nCandy].step = i;
                candyPalette[nCandy].place = j;
                candyPalette[nCandy].candy = transform.GetChild(nCandy).gameObject;
                nCandy++;
            }
        }

        for(int k = 0; k < transform.childCount; k++)
        {
            transform.GetChild(k).gameObject.SetActive(false);
            transform.GetChild(k).localScale = new Vector3(0.07f, 0.07f, 1f);
        }
    }

    private void Start()
    {
        StartCoroutine(ShowAllCandies());
    }

    public int CalculateMaxPlaces(int _step)
    {
        if (_step % 2 == 0)
        {
            return 7;
        }
        else
        {
            return 6;
        }
    }

    public void Check()
    {
        if (hasGuimauve)
        {
            CheckForNougat();
        }

        int maxPlaces = 7;
        int startPlace = 0;

        if (actualStep != 0)
        {
            maxPlaces = CalculateMaxPlaces(actualStep);
            startPlace = (int)Mathf.Ceil(actualStep / 2) * 7 + (int)Mathf.Floor(actualStep / 2) * 6;
        }

        bool stepIsEmpty = true;

        for(int i = startPlace; i < startPlace + maxPlaces; i++)
        {
            if(candyPalette[i].candy != null)
            {
                stepIsEmpty = false;
            }
        }

        if(stepIsEmpty && stop == false)
        {
            StartCoroutine(GoNextStep());
            print(actualStep);
            stop = true;
        }
    }

    IEnumerator GoNextStep()
    {
        float initPosY = transform.position.y;
        float nextPosY = initPosY - 0.75f;

        while (transform.position.y > nextPosY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);

            yield return new WaitForSeconds(0.01f);
        }

        actualStep++;

        yield return new WaitForSeconds(1f);

        stop = false;

        Check();
    }

    IEnumerator ShowAllCandies()
    {
        for (int k = 0; k < transform.childCount; k++)
        {
            transform.GetChild(k).gameObject.SetActive(true);

            if(transform.GetChild(k).gameObject.layer == 7 || transform.GetChild(k).gameObject.layer == 14 || transform.GetChild(k).gameObject.layer == 16 || transform.GetChild(k).gameObject.layer == 18)
                StartCoroutine(GrowCandy(transform.GetChild(k)));
            if (transform.GetChild(k).gameObject.layer == 13)
                StartCoroutine(GrowCandyExplosive(transform.GetChild(k)));

            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator GrowCandy(Transform tr)
    {
        while(tr.localScale.x < 0.1f)
        {
            tr.localScale = new Vector3(tr.localScale.x + 0.006f, tr.localScale.y + 0.006f, 1f);

            yield return new WaitForSeconds(0.1f);
        }

        while (tr.localScale.x > 0.092f)
        {
            tr.localScale = new Vector3(tr.localScale.x - 0.006f, tr.localScale.y - 0.006f, 1f);

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator GrowCandyExplosive(Transform tr)
    {
        while (tr.localScale.x < 0.45f)
        {
            tr.localScale = new Vector3(tr.localScale.x + 0.085f, tr.localScale.y + 0.085f, 1f);

            yield return new WaitForSeconds(0.1f);
        }

        while (tr.localScale.x > 0.35f)
        {
            tr.localScale = new Vector3(tr.localScale.x - 0.085f, tr.localScale.y - 0.085f, 1f);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void CheckForNougat()
    {
        if(GameObject.Find("Guimauve") == null)
        {
            hasGuimauve = false;
            return;
        }

        if(!canSpawnGuimauve)
        {
            return;
        }

        bool check = false;
        GameObject candy;

        do
        {
            int alea = UnityEngine.Random.Range(0, transform.childCount);
            candy = transform.GetChild(alea).gameObject;

            if(candy.layer == 7 && candy.transform.childCount == 0)
            {
                check = true;
            }

        } while (!check);

        candy.GetComponent<Berlingot>().hasGuimauve = true;
        
        GameObject guimauve;
        guimauve = Instantiate(guimauvePrefab, candy.transform.position, Quaternion.identity, candy.transform);
        guimauve.name = "Guimauve";
        GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(19);
        StartCoroutine(GrowGuimauve(guimauve.transform));
    }

    IEnumerator GrowGuimauve(Transform tr)
    {
        while (tr.localScale.x < 13f)
        {
            tr.localScale = new Vector3(tr.localScale.x + 0.6f, tr.localScale.y + 0.6f, 1f);

            yield return new WaitForSeconds(0.1f);
        }

        while (tr.localScale.x > 11f)
        {
            tr.localScale = new Vector3(tr.localScale.x - 0.6f, tr.localScale.y - 0.6f, 1f);

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void CountDownJellyBeans()
    {
        if(jellyBeans.Length == 0)
        {
            return;
        }

        for(int i = 0; i < jellyBeans.Length; i++)
        {
            if(jellyBeans[i].gameObject != null)
                jellyBeans[i].GetComponent<JellyBeans>().CountDown();
        }
    }
}
