using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeTest : MonoBehaviour
{
    const int verticalUpEnd = 4;
    const int verticalDownEnd = -2;
    const int horizontalLeftEnd = -2;
    const int horizontalRightEnd = 2;

    int horizontalLocation = 0;
    int verticalLocation = 0;

    public Image noticeImage;
    public Text noticeText;

    public float animTime = 0.5f;

    //private float start = 0f;
    //private float end = 1f;
    private float time = 0f;

    private bool isPlaying = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PressDirectionKey();
    }

    public void StartFadeAnim()
    {
        if (isPlaying == true) return;

        StartCoroutine("PlayFadeOut");
        StartCoroutine("PlayFadeIn");
    }

    IEnumerator PlayFadeOut()
    {
        isPlaying = true;

        Color imageColor = noticeImage.color;
        Color textColor = noticeText.color;
        imageColor.a = Mathf.Lerp(0f, 1f, time);
        textColor.a = Mathf.Lerp(0f, 1f, time);

        while (imageColor.a < 1f)
        {
            time += Time.deltaTime / animTime;

            imageColor.a = Mathf.Lerp(0f, 1f, time);
            textColor.a = Mathf.Lerp(0f, 1f, time);

            noticeImage.color = imageColor;
            noticeText.color = textColor;

            yield return null;                
        }

        isPlaying = false;
    }

    IEnumerator PlayFadeIn()
    {
        isPlaying = true;

        Color imageColor = noticeImage.color;
        Color textColor = noticeText.color;
        imageColor.a = Mathf.Lerp(1f, 0f, time);
        textColor.a = Mathf.Lerp(1f, 0f, time);

        while (imageColor.a > 0f)
        {
            time += Time.deltaTime / animTime;

            imageColor.a = Mathf.Lerp(1f, 0f, time);
            textColor.a = Mathf.Lerp(1f, 0f, time);

            noticeImage.color = imageColor;
            noticeText.color = textColor;

            yield return null;
        }

        isPlaying = false;
    }

    void PressDirectionKey()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            PressedUp();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            PressedDown();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            PressedLeft();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            PressedRight();
        }
    }

    void PressedUp()
    {
        if (verticalLocation != verticalUpEnd)
        {
            verticalLocation += 1;
            Debug.Log(verticalLocation);
        }
        else
        {
            noticeText.text = "목을 더이상 위로 움직일 수 없습니다.";
            StartFadeAnim();
        }
    }

    void PressedDown()
    {
        if (verticalLocation != verticalDownEnd)
            verticalLocation -= 1;
        else
        {
            noticeText.text = "목을 더이상 아래로 움직일 수 없습니다.";
        }
    }

    void PressedLeft()
    {
        if (horizontalLocation != horizontalLeftEnd)
            horizontalLocation -= 1;
        else
        {
            noticeText.text = "목을 더이상 왼쪽으로 움직일 수 없습니다.";
        }
    }

    void PressedRight()
    {
        if (horizontalLocation != horizontalRightEnd)
            horizontalLocation += 1;
        else
        {
            noticeText.text = "목을 더이상 오른쪽으로 움직일 수 없습니다.";
        }
    }
}
