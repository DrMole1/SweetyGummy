using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimlodMovement : MonoBehaviour
{
    private const float XMIN = -1.6f;
    private const float XMAX = 1.6f;

    // ============== VARIABLES ==============

    public float currentSpeed = 10f;
    public Animator animFoot;

    private int inputX = 0;
    private float xPos = 0f;

    // =======================================

    void FixedUpdate()
    {
        transform.position += new Vector3(inputX * currentSpeed * Time.deltaTime, 0f);

        // Clamp the position with the screen
        // ==================================================================

        xPos = Mathf.Clamp(transform.position.x, XMIN, XMAX);

        transform.position = new Vector3(xPos, transform.position.y, 0f);
        // ==================================================================

        if(transform.position.x > -1.6f && transform.position.x < 1.6f)
        {
            animFoot.SetBool("OnMovement", true);
        }
        else
        {
            animFoot.SetBool("OnMovement", false);
        }
    }


    public void MovePlatform(int _direction)
    {
        inputX = _direction;
    }
}
