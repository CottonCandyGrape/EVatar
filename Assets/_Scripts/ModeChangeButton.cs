using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EyeHelpers
{
    public class ModeChangeButton : MonoBehaviour
    {
        Button button;
        GameObject center, home, videoStreaming, moving, neck, help, keyboard, circleBtn;

        public enum MType { video, keyboard, help, btn_x, controller }
        public MType modeType = MType.video;

        //Color centerColor;

        // Use this for initialization
        void Start()
        {
            button = GetComponent<Button>();

            if (button != null)
            {
                button.onClick.AddListener(OnClick);
            }

            FindModeObject();
            //centerColor = GameObject.Find("Center").GetComponent<Image>().color;
        }

        void Update()
        {
            SetActiveMode();
        }

        private void SetActiveMode()
        {
            home.SetActive(ModeChangeManager.bHome);
            videoStreaming.SetActive(ModeChangeManager.bVideoStreaming);
            moving.SetActive(ModeChangeManager.bMoving);
            neck.SetActive(ModeChangeManager.bNeck);
            help.SetActive(ModeChangeManager.bHelp);
            keyboard.SetActive(ModeChangeManager.bKeyboard);
            circleBtn.SetActive(ModeChangeManager.bCircleBtn);
        }

        private void FindModeObject()
        {
            center = GameObject.Find("Center");
            home = GameObject.Find("Home");
            videoStreaming = GameObject.Find("VideoStreaming");
            moving = GameObject.Find("Moving_Controller");
            neck = GameObject.Find("Neck_Controller");
            help = GameObject.Find("Help");
            keyboard = GameObject.Find("Keyboard");
            circleBtn = GameObject.Find("Circle_Btn");            
        }

        void OnClick()
        {
            MType mode = this.modeType;
            switch (mode)
            {
                case MType.video:
                    if (ModeChangeManager.bHome) //홈화면 상태에서
                    {
                        ModeChangeManager.bHome = false;
                        center.GetComponent<Image>().color = new Color(0.7058824f, 0.1137255f, 0.2313726f);
                        ModeChangeManager.bCircleBtn = true;
                    }
                    else if (!ModeChangeManager.bHome && //비디오만 켜진상태
                        !ModeChangeManager.bKeyboard &&
                        !ModeChangeManager.bHelp)
                    {
                        ModeChangeManager.bHome = true;
                        ModeChangeManager.bCircleBtn = false;
                    }
                    else if (!ModeChangeManager.bHome && //비디오가 켜지고 키보드나 Help 둘중 하나 켜져있을때
                        (ModeChangeManager.bKeyboard ||
                        ModeChangeManager.bHelp))
                    {
                        ModeChangeManager.bHome = true;
                    }


                    ////Home에서의 로봇연결하기버튼. 다른 모드에서 연결해재 눌렀을때 영상이 켜지지 않기 위함.
                    //if (!ModeChangeManager.bVideoStreaming &&
                    //    !ModeChangeManager.bHelp &&
                    //    !ModeChangeManager.bKeyboard)
                    //{
                    //    ModeChangeManager.bVideoStreaming = true;
                    //    ModeChangeManager.bCircleBtn = true;
                    //    ModeChangeManager.bHome = false;
                    //}
                    //else if ((ModeChangeManager.bKeyboard || ModeChangeManager.bHelp)
                    //    && ModeChangeManager.bVideoStreaming)
                    //{//영상위에 keyboard or help 올라가있을때
                    //    ModeChangeManager.bVideoStreaming = false;
                    //}
                    //else if (ModeChangeManager.bVideoStreaming)
                    //{ // 영상만 켜져 있을때 Home으로 가기 위함.
                    //    ModeChangeManager.bVideoStreaming = false;
                    //    ModeChangeManager.bCircleBtn = false;
                    //    ModeChangeManager.bHome = true;
                    //}
                    break;

                case MType.keyboard:
                    if (!ModeChangeManager.bKeyboard)
                    {
                        ModeChangeManager.bKeyboard = true;
                        ModeChangeManager.bCircleBtn = true;
                        center.GetComponent<Image>().color = new Color(0.4392157f, 0.1921569f, 0.4392157f);
                        //ModeChangeManager.bHome = false;
                    }
                    break;

                case MType.help:
                    if (!ModeChangeManager.bHelp)
                    {
                        ModeChangeManager.bHelp = true;
                        ModeChangeManager.bCircleBtn = true;
                        //ModeChangeManager.bHome = false;
                    }
                    break;

                case MType.btn_x:
                    //if (!ModeChangeManager.bHome &&
                    //   (ModeChangeManager.bKeyboard ||
                    //   ModeChangeManager.bHelp))
                    //    ModeChangeManager.bHome = false;
                    //else if (!ModeChangeManager.bHome)
                    //{
                    //    ModeChangeManager.bHome = true;
                    //    ModeChangeManager.bCircleBtn = false;
                    //}

                    if (ModeChangeManager.bKeyboard && ModeChangeManager.bHome)
                    {
                        ModeChangeManager.bKeyboard = false;
                        ModeChangeManager.bCircleBtn = false;
                    }
                    else if (ModeChangeManager.bHelp && ModeChangeManager.bHome)
                    {
                        ModeChangeManager.bHelp = false;
                        ModeChangeManager.bCircleBtn = false;
                    }
                    else if (ModeChangeManager.bKeyboard && !ModeChangeManager.bHome)
                        ModeChangeManager.bKeyboard = false;
                    else if (ModeChangeManager.bHelp && !ModeChangeManager.bHome)
                        ModeChangeManager.bHelp = false;
                    break;

                case MType.controller:
                    if (ModeChangeManager.bMoving)
                    {
                        ModeChangeManager.bMoving = false;
                        ModeChangeManager.bNeck = true;
                    }
                    else
                    {
                        ModeChangeManager.bMoving = true;
                        ModeChangeManager.bNeck = false;
                    }
                    break;
            }
        }
    }
}

//home.GetComponent<Image>().enabled = false;