using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatedWheel : MonoBehaviour
{
    //Déclaration variables
    //===============================
    public float speed;
    //===============================

    void Update()
    {
        transform.Rotate(0, 0, speed, Space.Self);
    }
}
