using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotControl : MonoBehaviour
{
    public GameObject notice;
    public Text noticeText;

    const int verticalUpEnd = 5;
    const int verticalDownEnd = -3;
    const int horizontalLeftEnd = -3;
    const int horizontalRightEnd = 3;

    int horizontalLocation = 0;
    int verticalLocation = 0;

    private bool sendState = true;

    public bool SendState
    {
        set { sendState = value; }
        get { return sendState; }
    }


    // Use this for initialization
    void Start()
    {
        notice.SetActive(false);
    }

    IEnumerator PopUpDown()
    {
        notice.SetActive(true);
        yield return new WaitForSeconds(2f);
        notice.SetActive(false);
    }

    public void TypedUp()
    {
        if (verticalLocation != verticalUpEnd)
        {
            SendState = true;
            verticalLocation += 1;
        }
        else
        {
            SendState = false;
            noticeText.text = "목을 더이상 위로 움직일 수 없습니다.";
            StartCoroutine("PopUpDown");
        }
    }

    public void TypedDown()
    {
        if (verticalLocation != verticalDownEnd)
        {
            SendState = true;
            verticalLocation -= 1;
        }
        else
        {
            SendState = false;
            noticeText.text = "목을 더이상 아래로 움직일 수 없습니다.";
            StartCoroutine("PopUpDown");
        }
    }

    public void TypedLeft()
    {
        if (horizontalLocation != horizontalLeftEnd)
        {
            SendState = true;
            horizontalLocation -= 1;
        }
        else
        {
            SendState = false;
            noticeText.text = "목을 더이상 왼쪽으로 움직일 수 없습니다.";
            StartCoroutine("PopUpDown");
        }
    }

    public void TypedRight()
    {
        if (horizontalLocation != horizontalRightEnd)
        {
            SendState = true;
            horizontalLocation += 1;
        }
        else
        {
            SendState = false;
            noticeText.text = "목을 더이상 오른쪽으로 움직일 수 없습니다.";
            StartCoroutine("PopUpDown");
        }
    }
}
