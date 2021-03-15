using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonetizationBonus : MonoBehaviour
{
    private const float XMIN = -1.7f;
    private const float XMAX = 1.7f;

    public GameObject[] augmentedLollipops;
    public GameObject supraLollipopPrefab;
    public LevelManager levelManager;

    private int choice = 0;
    private float xPos = 0f;
    private Vector3 pos;


    private void Awake()
    {
        if(PlayerPrefs.GetInt("ActualBonus", 0) != 0)
        {
            if(PlayerPrefs.GetInt("ActualBonus", 0) == 1)
            {
                choice = UnityEngine.Random.Range(0, 5);
                xPos = UnityEngine.Random.Range(-1.7f, -1f);
                pos = new Vector3(xPos, 6f, transform.position.z);

                GameObject lollipop1;
                lollipop1 = Instantiate(augmentedLollipops[choice], pos, Quaternion.identity);

                choice = UnityEngine.Random.Range(0, 5);
                xPos = UnityEngine.Random.Range(-0.7f, 0.7f);
                pos = new Vector3(xPos, 6f, transform.position.z);

                GameObject lollipop2;
                lollipop2 = Instantiate(augmentedLollipops[choice], pos, Quaternion.identity);

                choice = UnityEngine.Random.Range(0, 5);
                xPos = UnityEngine.Random.Range(1f, 1.7f);
                pos = new Vector3(xPos, 6f, transform.position.z);

                GameObject lollipop3;
                lollipop3 = Instantiate(augmentedLollipops[choice], pos, Quaternion.identity);
            }
            else if (PlayerPrefs.GetInt("ActualBonus", 0) == 2)
            {
                levelManager.maxItems = levelManager.maxItems + 3;
            }
            else if (PlayerPrefs.GetInt("ActualBonus", 0) == 3)
            {
                xPos = UnityEngine.Random.Range(-1.7f, 1.7f);
                pos = new Vector3(xPos, 6f, transform.position.z);

                GameObject supralollipop;
                supralollipop = Instantiate(supraLollipopPrefab, pos, Quaternion.identity);
            }
            else if (PlayerPrefs.GetInt("ActualBonus", 0) == 4)
            {
                levelManager.maxItems = levelManager.maxItems + 6;
            }

            PlayerPrefs.SetInt("ActualBonus", 0);
        }
    }
}