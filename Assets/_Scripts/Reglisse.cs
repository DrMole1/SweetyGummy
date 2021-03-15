using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reglisse : MonoBehaviour
{
    // =============== VARIABLES ===============

    public GameObject[] nearestCandies;
    public GameObject ptcExplosionPrefab;
    public GameObject similiReglissePrefab;

    [HideInInspector] public EatLollipop eatLollipop;

    private bool canExplode = false;
    private bool check = true;

    // =========================================

    private void Start()
    {
        StartCoroutine(Check());
    }

    IEnumerator Check()
    {
        yield return new WaitForSeconds(1f);

        canExplode = true;

        for(int i = 0; i < nearestCandies.Length; i++)
        {
            if(nearestCandies[i] != null)
            {
                canExplode = false;
            }
        }

        if(check)
        {
            if (canExplode)
            {
                Explode();
            }
            else
            {
                StartCoroutine(Check());
            }
        }
    }

    public void Explode()
    {
        check = false;

        GameObject.Find("Palette").GetComponent<Palette>().Check();

        GameObject ptcExplosion;
        ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(ptcExplosion, 4f);

        eatLollipop = GameObject.Find("Gummy").GetComponent<EatLollipop>();
        eatLollipop.TouchBerlingotExplosif(transform.position);

        GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(17);

        gameObject.layer = 17;

        GameObject similiReglisse;
        similiReglisse = Instantiate(similiReglissePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

    IEnumerator Grow()
    {
        while (transform.localScale.x < 0.1f)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.0045f, transform.localScale.y + 0.0045f, 1f);

            yield return new WaitForSeconds(0.1f);
        }

        while (transform.localScale.x > 0.092f)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.0045f, transform.localScale.y - 0.0045f, 1f);

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(check)
        {
            if (other.gameObject.layer == 8)
            {
                StartCoroutine(Grow());
                GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(18);
            }

            if (other.gameObject.layer == 12)
            {
                StartCoroutine(Grow());
                GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(18);
            }

            if (other.gameObject.layer == 11)
            {
                StartCoroutine(Grow());
                GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(18);
            }
        }
    }
}
