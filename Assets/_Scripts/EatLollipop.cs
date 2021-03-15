using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EatLollipop : MonoBehaviour
{
    private const int FONTSIZE = 36;
    private const int MAXFONTSIZE = 60;


    // ============== VARIABLES ==============

    public SpriteRenderer tongue;
    public SpriteRenderer head;
    public Sprite headWithoutLollipop;
    public Sprite headWithLollipop;
    public GameObject[] lollipopsPrefab;
    public Transform tr;
    public float power = 280f;
    public GameObject ptcEatingPrefab;
    public SoundManager soundManager;
    public GameObject ptcSpitingPrefab;
    public int actualCombo = 0;
    public GameObject[] ptcScorePrefab;
    public LevelManager levelManager;
    public GameObject blinkCircle;
    public Sprite[] items;
    public Sprite[] augmentedItems;
    public TextMeshProUGUI txtRemainingItems;
    public GameObject[] bonusLollipops;
    public EndConditions endConditions;

    public bool hasEaten = false;
    private Vector3 fingerPos;
    private Vector3 realWorldPos;
    private int color;
    private float x = 0;
    private Rigidbody2D rb;
    private Vector3 dir;
    private bool canSpit = true;
    private GameObject spittedlollipop;
    private bool isAugmentedLollipop = false;
    private Palette palette;

    // =======================================



    private void Start()
    {
        StartMusic();
        StartCoroutine(SetPalette());
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 6 && hasEaten == false)
        {
            hasEaten = true;

            switch (other.gameObject.tag)
            {
                case "Red":
                    color = 0;
                    break;
                case "Green":
                    color = 1;
                    break;
                case "Blue":
                    color = 2;
                    break;
                case "Pink":
                    color = 3;
                    break;
                case "Orange":
                    color = 4;
                    break;
                default:
                    print("Incorrect tag. Error.");
                    break;
            }

            Destroy(other.gameObject);

            tongue.enabled = false;
            head.sprite = headWithLollipop;

            GameObject ptcEating;
            ptcEating = Instantiate(ptcEatingPrefab, transform.position, Quaternion.identity);
            Destroy(ptcEating, 4f);

            int sound = UnityEngine.Random.Range(0, 3);
            soundManager.playAudioClip(sound);

            blinkCircle.SetActive(true);
            blinkCircle.transform.GetChild(0).gameObject.GetComponent<Image>().overrideSprite = items[color];
            StartCoroutine(BlinkCircleAnim());

            isAugmentedLollipop = false;

        }

        if (other.gameObject.layer == 9 && hasEaten == false)
        {
            hasEaten = true;

            switch (other.gameObject.tag)
            {
                case "Red":
                    color = 0;
                    break;
                case "Green":
                    color = 1;
                    break;
                case "Blue":
                    color = 2;
                    break;
                case "Pink":
                    color = 3;
                    break;
                case "Orange":
                    color = 4;
                    break;
                case "Supra":
                    color = 5;
                    break;
                default:
                    print("Incorrect tag. Error.");
                    break;
            }

            Destroy(other.gameObject);

            tongue.enabled = false;
            head.sprite = headWithLollipop;

            GameObject ptcEating;
            ptcEating = Instantiate(ptcEatingPrefab, transform.position, Quaternion.identity);
            Destroy(ptcEating, 4f);

            int sound = UnityEngine.Random.Range(0, 3);
            soundManager.playAudioClip(sound);

            blinkCircle.SetActive(true);
            blinkCircle.transform.GetChild(0).gameObject.GetComponent<Image>().overrideSprite = augmentedItems[color];
            StartCoroutine(BlinkCircleAnim());

            isAugmentedLollipop = true;

        }
    }

    private void Update()
    {
        if(endConditions.isFinished)
        {
            return;
        }

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            fingerPos = Input.GetTouch(0).position;
            fingerPos.z = 8;
            realWorldPos = Camera.main.ScreenToWorldPoint(fingerPos);

            if(realWorldPos.y > -2f && realWorldPos.y < 5f)
            {
                Spit();
            }
        }
    }


    public void Spit()
    {
        if(hasEaten == false)
        {
            return;
        }
        if (canSpit == false)
        {
            return;
        }

        // Works with the gameplay "Guimauve"
        palette.canSpawnGuimauve = true;
        palette.CountDownJellyBeans();

        canSpit = false;
        tongue.enabled = true;
        head.sprite = headWithoutLollipop;
        actualCombo = 0;

        GameObject ptcSpiting;
        ptcSpiting = Instantiate(ptcSpitingPrefab, transform.position, Quaternion.Euler(realWorldPos - tr.position));
        Destroy(ptcSpiting, 4f);
        soundManager.playAudioClip(3);

        if(isAugmentedLollipop == false)
        {
            ThrowLollipop();
        }
        else
        {
            ThrowAugmentedLollipop();
        }
    }


    public void ThrowLollipop()
    {
        x = tr.position.x;
        Vector3 pos = new Vector3(x, tr.position.y, 0f);
        spittedlollipop = Instantiate(lollipopsPrefab[color], pos, Quaternion.identity);
        rb = spittedlollipop.GetComponent<Rigidbody2D>();

        rb.gravityScale = 0f;
        dir = (realWorldPos - tr.position).normalized;
        rb.AddForce(dir * power);
        spittedlollipop.layer = 8;

        StartCoroutine(UpdateRemainingItems());

        StartCoroutine(DelayToStartEat());
    }


    IEnumerator DelayToStartEat()
    {
        yield return new WaitForSeconds(0.5f);

        hasEaten = false;
        canSpit = true;
    }


    public void TouchBerlingot()
    {
        actualCombo++;

        soundManager.playAudioClipWithPitch(4, 0.45f + 0.05f * actualCombo);

        GameObject ptcScore;

        endConditions.CheckWinCondition();

        if(actualCombo == 1)
        {
            ptcScore = Instantiate(ptcScorePrefab[0], spittedlollipop.transform.position, Quaternion.identity);
            Destroy(ptcScore, 5f);
        }
        else if (actualCombo == 2)
        {
            ptcScore = Instantiate(ptcScorePrefab[1], spittedlollipop.transform.position, Quaternion.identity);
            Destroy(ptcScore, 5f);
        }
        else if (actualCombo == 3)
        {
            ptcScore = Instantiate(ptcScorePrefab[2], spittedlollipop.transform.position, Quaternion.identity);
            Destroy(ptcScore, 5f);
        }
        else if (actualCombo == 4)
        {
            ptcScore = Instantiate(ptcScorePrefab[3], spittedlollipop.transform.position, Quaternion.identity);
            Destroy(ptcScore, 5f);

            SpawnAugmentedLollipop();
        }
        else if (actualCombo >= 5)
        {
            ptcScore = Instantiate(ptcScorePrefab[4], spittedlollipop.transform.position, Quaternion.identity);
            Destroy(ptcScore, 5f);

            if (actualCombo == 8)
            {
                SpawnSupraLollipop();
            }
        }

        levelManager.AddScore(actualCombo * 100);
    }


    IEnumerator BlinkCircleAnim()
    {
        int a = 75;

        while(canSpit)
        {
            yield return new WaitForSeconds(0.01f);

            blinkCircle.GetComponent<RectTransform>().sizeDelta = new Vector2(blinkCircle.GetComponent<RectTransform>().rect.width + 2, blinkCircle.GetComponent<RectTransform>().rect.height + 2);
            a -= 3;
            blinkCircle.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)a);

            if(blinkCircle.GetComponent<RectTransform>().rect.width >= 200)
            {
                blinkCircle.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
                a = 75;
                blinkCircle.GetComponent<Image>().color = new Color32(255, 255, 255, (byte)a);
            }
        }

        blinkCircle.SetActive(false);
    }


    IEnumerator UpdateRemainingItems()
    {
        levelManager.maxItems--;

        txtRemainingItems.text = "X" + levelManager.maxItems.ToString();

        while (txtRemainingItems.fontSize < MAXFONTSIZE)
        {
            yield return new WaitForSeconds(0.01f);

            txtRemainingItems.fontSize += 4;
        }

        while (txtRemainingItems.fontSize > FONTSIZE)
        {
            yield return new WaitForSeconds(0.015f);

            txtRemainingItems.fontSize -= 4;
        }

        if(levelManager.maxItems == 0)
        {
            endConditions.CheckLoseCondition();
        }
    }

    public void SpawnAugmentedLollipop()
    {
        if(color == 5)
        {
            return;
        }

        GameObject augmentedLollipop;
        augmentedLollipop = Instantiate(bonusLollipops[color], spittedlollipop.transform.position, Quaternion.identity);
    }

    public void SpawnSupraLollipop()
    {
        if (color == 5)
        {
            return;
        }

        GameObject supraLollipop;
        supraLollipop = Instantiate(bonusLollipops[5], spittedlollipop.transform.position, Quaternion.identity);
    }

    public void ThrowAugmentedLollipop()
    {
        x = tr.position.x;
        Vector3 pos = new Vector3(x, tr.position.y, 0f);
        spittedlollipop = Instantiate(bonusLollipops[color], pos, Quaternion.identity);
        rb = spittedlollipop.GetComponent<Rigidbody2D>();

        rb.gravityScale = 1f;
        dir = (realWorldPos - tr.position).normalized;

        spittedlollipop.layer = 12;
        rb.AddForce(dir * 600);

        if (color == 5)
        {
            spittedlollipop.layer = 11;
            spittedlollipop.GetComponent<SupraLollipop>().StartToExplode();
        }
        else
        {
            spittedlollipop.GetComponent<AugmentedLollipop>().StartToExplode();
        }

        StartCoroutine(UpdateRemainingItems());

        StartCoroutine(DelayToStartEat());
    }

    public void StartMusic()
    {
        MusicManager musicManager;

        if (GameObject.Find("MusicManager") != null)
        {
            musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
            musicManager.PlayMusicGame();
        }
        else
        {
            print("No Music Manager found.");
        }
    }

    public void TouchBerlingotExplosif(Vector3 _pos)
    {
        actualCombo++;

        soundManager.playAudioClipWithPitch(4, 0.45f + 0.05f * actualCombo);

        GameObject ptcScore;

        endConditions.CheckWinCondition();

        if (actualCombo == 1)
        {
            ptcScore = Instantiate(ptcScorePrefab[0], _pos, Quaternion.identity);
            Destroy(ptcScore, 5f);
        }
        else if (actualCombo == 2)
        {
            ptcScore = Instantiate(ptcScorePrefab[1], _pos, Quaternion.identity);
            Destroy(ptcScore, 5f);
        }
        else if (actualCombo == 3)
        {
            ptcScore = Instantiate(ptcScorePrefab[2], _pos, Quaternion.identity);
            Destroy(ptcScore, 5f);
        }
        else if (actualCombo == 4)
        {
            ptcScore = Instantiate(ptcScorePrefab[3], _pos, Quaternion.identity);
            Destroy(ptcScore, 5f);

            if (color == 5)
            {
                return;
            }

            GameObject augmentedLollipop;
            augmentedLollipop = Instantiate(bonusLollipops[color], _pos, Quaternion.identity);
        }
        else if (actualCombo >= 5)
        {
            ptcScore = Instantiate(ptcScorePrefab[4], _pos, Quaternion.identity);
            Destroy(ptcScore, 5f);

            if (actualCombo == 8)
            {
                if (color == 5)
                {
                    return;
                }

                GameObject supraLollipop;
                supraLollipop = Instantiate(bonusLollipops[5], _pos, Quaternion.identity);
            }
        }

        levelManager.AddScore(actualCombo * 100);
    }

    IEnumerator SetPalette()
    {
        yield return new WaitForSeconds(2f);

        if (GameObject.Find("Palette") != null)
            palette = GameObject.Find("Palette").GetComponent<Palette>();
        else
            print("There is no palette in game");
    }
}
