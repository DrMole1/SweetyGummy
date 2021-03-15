using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyBeans : MonoBehaviour
{
    // ============== VARIABLES ==============

    public GameObject ptcExplosionPrefab;
    public Sprite[] spritesCount;
    public int count = 4;

    [HideInInspector] public EatLollipop eatLollipop;
    private bool isActivated = false;

    // =======================================

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 19 && isActivated == false)
        {
            isActivated = true;
        }

        if (other.gameObject.layer == 8 && other.gameObject.tag == gameObject.tag)
        {
            GameObject.Find("Palette").GetComponent<Palette>().Check();

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(ptcExplosion, 4f);

            eatLollipop = GameObject.Find("Gummy").GetComponent<EatLollipop>();
            eatLollipop.TouchBerlingotExplosif(transform.position);

            Destroy(gameObject);
        }

        if (other.gameObject.layer == 12 && other.gameObject.tag == gameObject.tag)
        {
            GameObject.Find("Palette").GetComponent<Palette>().Check();

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(ptcExplosion, 4f);

            eatLollipop = GameObject.Find("Gummy").GetComponent<EatLollipop>();
            eatLollipop.TouchBerlingotExplosif(transform.position);

            Destroy(gameObject);
        }

        if (other.gameObject.layer == 11 && other.gameObject.tag == "Supra")
        {
            GameObject.Find("Palette").GetComponent<Palette>().Check();

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(ptcExplosion, 4f);

            eatLollipop = GameObject.Find("Gummy").GetComponent<EatLollipop>();
            eatLollipop.TouchBerlingotExplosif(transform.position);

            Destroy(gameObject);
        }
    }

    public void CountDown()
    {
        if (!isActivated)
        {
            return;
        }

        StartCoroutine(SetCountDown());
    }

    IEnumerator SetCountDown()
    {
        count--;
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = spritesCount[count];

        while (transform.localScale.x < 0.1f)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.006f, transform.localScale.y + 0.006f, 1f);

            yield return new WaitForSeconds(0.1f);
        }

        while (transform.localScale.x > 0.092f)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.006f, transform.localScale.y - 0.006f, 1f);

            yield return new WaitForSeconds(0.1f);
        }

        if (count == 0)
        {
            yield return new WaitForSeconds(0.5f);

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            ptcExplosion.transform.localScale = new Vector3(ptcExplosion.transform.localScale.x * 2, ptcExplosion.transform.localScale.y * 2, ptcExplosion.transform.localScale.z * 2);
            Destroy(ptcExplosion, 4f);

            GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

            GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(21);

            yield return new WaitForSeconds(1.5f);

            GameObject.Find("LevelManager").GetComponent<EndConditions>().CheckLoseCondition();
        }
    }
}
