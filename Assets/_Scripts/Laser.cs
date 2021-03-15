using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public SpriteRenderer sr;


    void Start()
    {
        StartCoroutine(Explode());
        Destroy(gameObject, 2f);
    }

    IEnumerator Explode()
    {
        float a = 0.4f;

        while(a < 1f)
        {
            sr.color = new Color(1f, 1f, 1f, a);

            yield return new WaitForSeconds(0.1f);

            a += 0.1f;
        }

        while (a > 0f)
        {
            sr.color = new Color(1f, 1f, 1f, a);

            yield return new WaitForSeconds(0.1f);

            a -= 0.25f;
        }
    }
}
