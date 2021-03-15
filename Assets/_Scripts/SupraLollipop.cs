using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupraLollipop : MonoBehaviour
{
    private const float delay = 0.7f;

    // ============== VARIABLES ==============

    public GameObject ptcExplosionPrefab;
    public GameObject zoneSupraPrefab;

    private GameObject palette;
    private SoundManager soundManager;

    // =======================================



    public void StartToExplode()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(delay);

        GameObject ptcExplosion;
        ptcExplosion = Instantiate(ptcExplosionPrefab, transform.position, Quaternion.identity, transform);

        yield return new WaitForSeconds(0.8f);

        ptcExplosion.transform.SetParent(null);
        Destroy(ptcExplosion, 7f);

        float pitch = UnityEngine.Random.Range(0.8f, 1.5f);
        soundManager.playAudioClipWithPitch(11, pitch);

        transform.localScale = new Vector3(2.25f, 2.25f, 1);

        GameObject.Find("LevelManager").GetComponent<LevelManager>().EarthQuake();

        yield return new WaitForSeconds(0.01f);

        Destroy(gameObject);
    }
}
