using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berlingot : MonoBehaviour
{
    // ============== VARIABLES ==============

    public GameObject ptcExplosionPrefab;
    public EatLollipop eatLollipop;
    public GameObject[] ptcExplosions;
    public Sprite[] sprites;

    public bool hasGuimauve = false;

    // =======================================



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.tag == gameObject.tag)
        {
            if(!hasGuimauve)
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

        if (other.gameObject.layer == 12 && other.gameObject.tag == gameObject.tag)
        {
            if (!hasGuimauve)
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

        if (other.gameObject.layer == 8 && hasGuimauve)
        {

            GameObject.Find("Palette").GetComponent<Palette>().canSpawnGuimauve = false;
            Destroy(transform.GetChild(0).gameObject);
            hasGuimauve = false;
            GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(20);
        }

        if (other.gameObject.layer == 12 && hasGuimauve)
        {

            GameObject.Find("Palette").GetComponent<Palette>().canSpawnGuimauve = false;
            Destroy(transform.GetChild(0).gameObject);
            hasGuimauve = false;
            GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(20);
        }

        if (other.gameObject.layer == 11 && other.gameObject.tag == "Supra")
        {
            if (hasGuimauve)
            {
                GameObject.Find("Palette").GetComponent<Palette>().canSpawnGuimauve = false;
                Destroy(transform.GetChild(0).gameObject);
                hasGuimauve = false;
                GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(20);
            }
            else
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

        if (other.gameObject.layer == 15 && other.gameObject.tag == "Red")
        {
            gameObject.tag = other.gameObject.tag;

            GetComponent<SpriteRenderer>().sprite = sprites[0];

            ptcExplosionPrefab = ptcExplosions[0];

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(ptcExplosion, 4f);
        }

        if (other.gameObject.layer == 15 && other.gameObject.tag == "Blue")
        {
            gameObject.tag = other.gameObject.tag;

            GetComponent<SpriteRenderer>().sprite = sprites[1];

            ptcExplosionPrefab = ptcExplosions[1];

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(ptcExplosion, 4f);
        }

        if (other.gameObject.layer == 15 && other.gameObject.tag == "Green")
        {
            gameObject.tag = other.gameObject.tag;

            GetComponent<SpriteRenderer>().sprite = sprites[2];

            ptcExplosionPrefab = ptcExplosions[2];

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(ptcExplosion, 4f);
        }

        if (other.gameObject.layer == 15 && other.gameObject.tag == "Pink")
        {
            gameObject.tag = other.gameObject.tag;

            GetComponent<SpriteRenderer>().sprite = sprites[3];

            ptcExplosionPrefab = ptcExplosions[3];

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(ptcExplosion, 4f);
        }

        if (other.gameObject.layer == 15 && other.gameObject.tag == "Orange")
        {
            gameObject.tag = other.gameObject.tag;

            GetComponent<SpriteRenderer>().sprite = sprites[4];

            ptcExplosionPrefab = ptcExplosions[4];

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(ptcExplosion, 4f);
        }
    }
}
