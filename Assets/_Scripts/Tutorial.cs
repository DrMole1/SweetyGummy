using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // ============== VARIABLES ==============

    public RectTransform btnLeft;
    public RectTransform btnRight;
    public EatLollipop eatLollipop;
    public SpriteRenderer sr;

    private bool isCheck = false;

    // =======================================



    void Start()
    {
        if(PlayerPrefs.GetInt("ActualLevel", 0) != 0)
        {
            return;
        }

        StartCoroutine(StartTutorial());
    }

    IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(3f);

        float initScaleX;
        initScaleX = btnLeft.localScale.x;

        while (btnLeft.localScale.x < initScaleX + 0.2f)
        {
            yield return new WaitForSeconds(0.01f);

            btnLeft.localScale = new Vector3(btnLeft.localScale.x + 0.02f, btnLeft.localScale.y + 0.02f, 1);
            btnRight.localScale = new Vector3(btnRight.localScale.x + 0.02f, btnRight.localScale.y + 0.02f, 1);
        }

        while (btnLeft.localScale.x > initScaleX)
        {
            yield return new WaitForSeconds(0.01f);

            btnLeft.localScale = new Vector3(btnLeft.localScale.x - 0.02f, btnLeft.localScale.y - 0.02f, 1);
            btnRight.localScale = new Vector3(btnRight.localScale.x - 0.02f, btnRight.localScale.y - 0.02f, 1);
        }

        while (btnLeft.localScale.x < initScaleX + 0.2f)
        {
            yield return new WaitForSeconds(0.01f);

            btnLeft.localScale = new Vector3(btnLeft.localScale.x + 0.02f, btnLeft.localScale.y + 0.02f, 1);
            btnRight.localScale = new Vector3(btnRight.localScale.x + 0.02f, btnRight.localScale.y + 0.02f, 1);
        }

        while (btnLeft.localScale.x > initScaleX)
        {
            yield return new WaitForSeconds(0.01f);

            btnLeft.localScale = new Vector3(btnLeft.localScale.x - 0.02f, btnLeft.localScale.y - 0.02f, 1);
            btnRight.localScale = new Vector3(btnRight.localScale.x - 0.02f, btnRight.localScale.y - 0.02f, 1);
        }

        while (btnLeft.localScale.x < initScaleX + 0.2f)
        {
            yield return new WaitForSeconds(0.01f);

            btnLeft.localScale = new Vector3(btnLeft.localScale.x + 0.02f, btnLeft.localScale.y + 0.02f, 1);
            btnRight.localScale = new Vector3(btnRight.localScale.x + 0.02f, btnRight.localScale.y + 0.02f, 1);
        }

        while (btnLeft.localScale.x > initScaleX)
        {
            yield return new WaitForSeconds(0.01f);

            btnLeft.localScale = new Vector3(btnLeft.localScale.x - 0.02f, btnLeft.localScale.y - 0.02f, 1);
            btnRight.localScale = new Vector3(btnRight.localScale.x - 0.02f, btnRight.localScale.y - 0.02f, 1);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("ActualLevel", 0) != 0)
        {
            return;
        }

        if(isCheck)
        {
            return;
        }

        if(eatLollipop.hasEaten)
        {
            isCheck = true;

            StartCoroutine(TouchTutorial());
        }
    }

    IEnumerator TouchTutorial()
    {
        float a = 0f;

        while (a < 0.8f)
        {
            sr.color = new Color(1f, 1f, 1f, a);

            yield return new WaitForSeconds(0.1f);

            a += 0.1f;
        }

        yield return new WaitForSeconds(1.5f);

        while (a > 0f)
        {
            sr.color = new Color(1f, 1f, 1f, a);

            yield return new WaitForSeconds(0.1f);

            a -= 0.1f;
        }
    }
}
