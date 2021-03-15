using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimiliReglisse : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GrowExplode());
    }

    IEnumerator GrowExplode()
    {
        while (transform.localScale.x < 0.1f)
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.006f, transform.localScale.y + 0.006f, 1f);

            yield return new WaitForSeconds(0.1f);
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        rb.AddTorque(1500, ForceMode2D.Impulse);

        GameObject.Find("LevelManager").GetComponent<EndConditions>().CheckWinCondition();

        Destroy(gameObject, 5f);
    }
}
