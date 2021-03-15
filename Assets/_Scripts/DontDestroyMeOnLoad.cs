using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dont destroy objetcs when this script is attached to
public class DontDestroyMeOnLoad : MonoBehaviour
{
    private static DontDestroyMeOnLoad _instance;

    void Awake()
    {

        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
