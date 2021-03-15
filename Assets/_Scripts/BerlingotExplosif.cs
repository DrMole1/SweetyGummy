using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerlingotExplosif : MonoBehaviour
{
    // ============== VARIABLES ==============

    public GameObject ptcExplosionPrefab;
    public GameObject ptcExplosionPrefab2;
    public GameObject zoneExplosionPrefab;

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
            ptcExplosion.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            Destroy(ptcExplosion, 4f);

            GameObject ptcExplosion2;
            ptcExplosion2 = Instantiate(ptcExplosionPrefab2, transform.position, Quaternion.identity);
            ptcExplosion2.transform.localScale = new Vector3(1f, 1f, 1f);
            Destroy(ptcExplosion2, 4f);

            GameObject zoneExplosion;
            zoneExplosion = Instantiate(zoneExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(zoneExplosion, 0.3f);

            eatLollipop = GameObject.Find("Gummy").GetComponent<EatLollipop>();
            eatLollipop.TouchBerlingotExplosif(transform.position);

            GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(15);

            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == 12 && other.gameObject.tag == gameObject.tag)
        {
            GameObject.Find("Palette").GetComponent<Palette>().Check();

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            ptcExplosion.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            Destroy(ptcExplosion, 4f);

            GameObject ptcExplosion2;
            ptcExplosion2 = Instantiate(ptcExplosionPrefab2, transform.position, Quaternion.identity);
            ptcExplosion2.transform.localScale = new Vector3(1f, 1f, 1f);
            Destroy(ptcExplosion2, 4f);

            GameObject zoneExplosion;
            zoneExplosion = Instantiate(zoneExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(zoneExplosion, 0.3f);

            eatLollipop = GameObject.Find("Gummy").GetComponent<EatLollipop>();
            eatLollipop.TouchBerlingotExplosif(transform.position);

            GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(15);

            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == 11 && other.gameObject.tag == "Supra")
        {
            GameObject.Find("Palette").GetComponent<Palette>().Check();

            GameObject ptcExplosion;
            ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity);
            ptcExplosion.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            Destroy(ptcExplosion, 4f);

            GameObject ptcExplosion2;
            ptcExplosion2 = Instantiate(ptcExplosionPrefab2, transform.position, Quaternion.identity);
            ptcExplosion2.transform.localScale = new Vector3(1f, 1f, 1f);
            Destroy(ptcExplosion2, 4f);

            GameObject zoneExplosion;
            zoneExplosion = Instantiate(zoneExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(zoneExplosion, 0.3f);

            eatLollipop = GameObject.Find("Gummy").GetComponent<EatLollipop>();
            eatLollipop.TouchBerlingotExplosif(transform.position);

            GameObject.Find("SoundManager").GetComponent<SoundManager>().playAudioClip(15);

            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    IEnumerator Anim()
    {
        float delay = 0f;
        delay = UnityEngine.Random.Range(8f, 20f);

        rot = 0;

        yield return new WaitForSeconds(delay);

        while (transform.localScale.x < 0.45f)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.085f, transform.localScale.y + 0.085f, 1f);

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

        while (transform.localScale.x > 0.35f)
        {
            transform.localScale = new Vector3(transform.localScale.x - 0.085f, transform.localScale.y - 0.085f, 1f);

            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(Anim());
    }
}
