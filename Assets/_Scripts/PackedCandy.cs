using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackedCandy : MonoBehaviour
{
    // ============== VARIABLES ==============

    public GameObject ptcExplosionPrefab;
    public GameObject candySpawnerPrefab;

    [HideInInspector]public EatLollipop eatLollipop;

    private float rot = 0;
    private Vector3 currentRot;
    private Quaternion currentQuaternionRot;

    // =======================================

    private void Start()
    {
        StartCoroutine(Anim());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.tag == gameObject.tag)
        {
            GameObject.Find("Palette").GetComponent<Palette>().Check();

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(ptcExplosion, 4f);

            eatLollipop = GameObject.Find("Gummy").GetComponent<EatLollipop>();
            eatLollipop.TouchBerlingotExplosif(transform.position);

            GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(17);

            GameObject candySpawner;
            candySpawner = Instantiate(candySpawnerPrefab, transform.position, Quaternion.identity);
            Destroy(candySpawner, 4f);

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

            GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(17);

            GameObject candySpawner;
            candySpawner = Instantiate(candySpawnerPrefab, transform.position, Quaternion.identity);
            Destroy(candySpawner, 4f);

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

            GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(17);

            GameObject candySpawner;
            candySpawner = Instantiate(candySpawnerPrefab, transform.position, Quaternion.identity);
            Destroy(candySpawner, 4f);

            Destroy(gameObject);
        }
    }

    IEnumerator Anim()
    {
        float delay = 0f;
        delay = UnityEngine.Random.Range(8f, 20f);

        rot = 0;

        yield return new WaitForSeconds(delay);

        while (transform.localScale.x < 0.1f)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.0045f, transform.localScale.y + 0.0045f, 1f);

            yield return new WaitForSeconds(0.1f);
        }

        while (rot < 20)
        {
            yield return new WaitForSeconds(0.01f);

            rot += 1;
            currentRot = new Vector3(0, 0, rot);
            currentQuaternionRot.eulerAngles = currentRot;
            transform.localRotation = currentQuaternionRot;
        }

        while (rot > -20)
        {
            yield return new WaitForSeconds(0.01f);

            rot -= 1;
            currentRot = new Vector3(0, 0, rot);
            currentQuaternionRot.eulerAngles = currentRot;
            transform.localRotation = currentQuaternionRot;
        }

        while (rot < 0)
        {
            yield return new WaitForSeconds(0.01f);

            rot += 1;
            currentRot = new Vector3(0, 0, rot);
            currentQuaternionRot.eulerAngles = currentRot;
            transform.localRotation = currentQuaternionRot;
        }

        while (transform.localScale.x > 0.092f)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.0045f, transform.localScale.y - 0.0045f, 1f);

            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(Anim());
    }
}
