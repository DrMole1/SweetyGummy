using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnCollision : MonoBehaviour
{
    // ============== VARIABLES ==============

    public int nLayer;

    // =======================================

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == nLayer)
        {
            Destroy(other.gameObject);
        }
    }
}
