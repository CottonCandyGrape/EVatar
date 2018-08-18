using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class ScreenMoveTest : MonoBehaviour
    {
        Button button;
        GameObject videoStreaming, helpMe, keyboard;

        // Use this for initialization
        void Start()
        {
            button = GetComponent<Button>();

            if (button != null)
            {
                button.onClick.AddListener(OnClick);
            }

            videoStreaming = GameObject.Find("VideoStreaming");
            helpMe = GameObject.Find("HelpMe");
            keyboard = GameObject.Find("Keyboard");
            //Debug.Log(obj.transform.parent.name);
            //obj.SetActive(false);
        }

        void OnClick()
        {
            string viewName = button.name;
            switch (viewName)
            {
                case "VideoStreamingBtn":
                    if (!ScreenMoveTest2.bVideoStreaming)
                        ScreenMoveTest2.bVideoStreaming = true;
                    else
                        ScreenMoveTest2.bVideoStreaming = false;
                    break;
                case "HelpMeBtn":
                    if (!ScreenMoveTest2.bHelpMe)
                        ScreenMoveTest2.bHelpMe = true;
                    else
                        ScreenMoveTest2.bHelpMe = false;
                    break;
                case "KeyboardBtn":
                    if (!ScreenMoveTest2.bKeyboard)
                        ScreenMoveTest2.bKeyboard = true;
                    else
                        ScreenMoveTest2.bKeyboard = false;
                    break;
            }

        }

        void Update()
        {
            videoStreaming.SetActive(ScreenMoveTest2.bVideoStreaming);
            helpMe.SetActive(ScreenMoveTest2.bHelpMe);
            keyboard.SetActive(ScreenMoveTest2.bKeyboard);
        }
    }
}