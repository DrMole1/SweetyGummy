using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LollipopSpawner : MonoBehaviour
{
    private const float XMIN = -1.7f;
    private const float XMAX = 1.7f;
    private const float DELAY = 0.5f;
    private const float DELAYMIN = 2.8f;
    private const float DELAYMAX = 4f;

    // ============== VARIABLES ==============

    public GameObject[] lollipops;
    public float delayToSpawn = 5f;
    public int nColor = 3;
    public bool inMenu = false;

    private Transform palette;
    private int alea = 0;
    private bool check = false;

    // =======================================



    private void Start()
    {
        StartCoroutine(Spawn());

        if(!inMenu)
        {
            StartCoroutine(Palette());
        }
    }

    IEnumerator Palette()
    {
        yield return new WaitForSeconds(1f);

        palette = GameObject.Find("Palette").transform;
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(delayToSpawn);

        delayToSpawn = UnityEngine.Random.Range(DELAYMIN, DELAYMAX);

        float x = UnityEngine.Random.Range(XMIN, XMAX);

        transform.position = new Vector3(x, transform.position.y, transform.position.z);

        if(!inMenu)
        {
            CheckColor();
        }

        GameObject lollipop;
        lollipop = Instantiate(lollipops[alea], transform.position, Quaternion.identity);

        StartCoroutine(Spawn());
    }

    public void CheckColor()
    {
        bool[] possibleColors = new bool[] { false, false, false, false, false };
        check = false;

        for (int i = 0; i < palette.childCount; i++)
        {
            if(possibleColors[0] == false && palette.GetChild(i).gameObject.tag == "Red")
            {
                possibleColors[0] = true;
            }
            if (possibleColors[1] == false && palette.GetChild(i).gameObject.tag == "Blue")
            {
                possibleColors[1] = true;
            }
            if (nColor >= 3 && possibleColors[2] == false && palette.GetChild(i).gameObject.tag == "Green")
            {
                possibleColors[2] = true;
            }
            if (nColor >= 4 && possibleColors[3] == false && palette.GetChild(i).gameObject.tag == "Pink")
            {
                possibleColors[3] = true;
            }
            if (nColor >= 5 && possibleColors[4] == false && palette.GetChild(i).gameObject.tag == "Orange")
            {
                possibleColors[4] = true;
            }
        }

        do
        {
            alea = UnityEngine.Random.Range(0, nColor);

            if (possibleColors[alea])
            {
                check = true;
            }

            if (palette.childCount == 0)
            {
                return;
            }
        } while (check == false);
    }
}
