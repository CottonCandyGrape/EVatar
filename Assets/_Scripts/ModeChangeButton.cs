using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class ModeChangeButton : MonoBehaviour
    {
        Button button;
        GameObject home, videoStreaming, help, keyboard, circleBtn;

        public enum mType { video, keyboard, help, btn_x, moving, neck }

        public mType modeType = mType.video;

        // Use this for initialization
        void Start()
        {
            button = GetComponent<Button>();

            if (button != null)
            {
                button.onClick.AddListener(OnClick);
            }

            FindModeObject();
        }

        void Update()
        {
            SetActiveMode();
        }

        private void SetActiveMode()
        {
            home.SetActive(ModeChangeManager.bHome);
            videoStreaming.SetActive(ModeChangeManager.bVideoStreaming);
            help.SetActive(ModeChangeManager.bHelp);
            keyboard.SetActive(ModeChangeManager.bKeyboard);
            circleBtn.SetActive(ModeChangeManager.bCircleBtn);
        }

        private void FindModeObject()
        {
            home = GameObject.Find("Home");
            videoStreaming = GameObject.Find("VideoStreaming");
            help = GameObject.Find("Help");
            keyboard = GameObject.Find("Keyboard");
            circleBtn = GameObject.Find("Circle_Btn");
        }

        void OnClick()
        {
            mType mode = this.modeType;
            switch (mode)
            {
                case mType.video:
                    if (!ModeChangeManager.bVideoStreaming &&
                        !ModeChangeManager.bHelp &&
                        !ModeChangeManager.bKeyboard)
                    {
                        ModeChangeManager.bVideoStreaming = true;
                        ModeChangeManager.bCircleBtn = true;
                        ModeChangeManager.bHome = false;
                    }
                    else if ((ModeChangeManager.bKeyboard || ModeChangeManager.bHelp)
                        && ModeChangeManager.bVideoStreaming)
                    {//영상위에 keyboard or help 올라가있을때
                        ModeChangeManager.bVideoStreaming = false;
                    }
                    else if (ModeChangeManager.bVideoStreaming)
                    { // 영상만 켜져 있을때
                        ModeChangeManager.bVideoStreaming = false;
                        ModeChangeManager.bCircleBtn = false;
                        ModeChangeManager.bHome = true;
                    }
                    break;

                case mType.keyboard:
                    if (!ModeChangeManager.bKeyboard)
                    {
                        ModeChangeManager.bKeyboard = true;
                        ModeChangeManager.bCircleBtn = true;
                        ModeChangeManager.bHome = false;
                    }                    
                    break;

                case mType.help:
                    if (!ModeChangeManager.bHelp)
                    {
                        ModeChangeManager.bHelp = true;
                        ModeChangeManager.bCircleBtn = true;
                        ModeChangeManager.bHome = false;
                    }
                    break;

                case mType.btn_x:
                    if (ModeChangeManager.bKeyboard)
                        ModeChangeManager.bKeyboard = false;
                    else if (ModeChangeManager.bHelp)
                        ModeChangeManager.bHelp = false;

                    if (!ModeChangeManager.bVideoStreaming)
                    {
                        ModeChangeManager.bHome = true;
                        ModeChangeManager.bCircleBtn = false;
                    }
                    break;
            }
        }
    }
}