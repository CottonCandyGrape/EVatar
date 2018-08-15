using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenMoveTest : MonoBehaviour
{
    Button button;

    // Use this for initialization
    void Start()
    {
        button = GetComponent<Button>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log(transform.GetChild(i).name);
        }

        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    void OnClick()
    {
        string viewName = button.name;
        switch (viewName)
        {
            case "1":
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}