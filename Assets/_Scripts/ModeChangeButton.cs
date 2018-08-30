using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EyeHelpers
{
    public class ModeChangeButton : MonoBehaviour
    {
        public enum MType { video, keyboard, help, btn_x, controller }
        public MType modeType = MType.video;

        public Sprite hoverImage;

        private Image image;
        private Sprite normalImage;
        private Timer timer;

        //Button button;
        Image centerImage;
        GameObject center, home, /*videoStreaming,*/ moving, neck, help, keyboard, circleBtn;

        Color homeColor = new Color(0.7411765f, 0.2117647f, 0.3529412f);
        Color videoColor = new Color(0.7215686f, 0.1176471f, 0.2352941f);
        Color keyboardColor = new Color(0.4392157f, 0.1921569f, 0.4392157f);
        Color helpColor = new Color(0.1372549f, 0.3333333f, 0.4941177f);

        // Use this for initialization
        void Start()
        {
            image = GetComponent<Image>();
            normalImage = image.sprite;
            timer = new Timer();

            FindModeObject();
        }

        void Update()
        {
            // 버튼 벗어났는지 확인.
            if (timer.GetLastGameTime != 0f && (Time.realtimeSinceStartup - timer.GetLastGameTime) > Time.deltaTime * 3f)
            {
                image.sprite = normalImage;
                ResetTimer();
            }

            SetActiveMode();
        }

        public void UpdateTimer(float deltaTime)
        {
            timer.Update(deltaTime);
            if (timer.HasPastSince(1f))
            {
                Typing();
            }

            image.sprite = hoverImage;
        }

        public void ResetTimer()
        {
            timer.Reset();
        }

        void Typing()
        {
            MType mode = this.modeType;
            switch (mode)
            {
                case MType.video:
                    if (!ModeChangeManager.bKeyboard && !ModeChangeManager.bHelp && ModeChangeManager.bHome)
                    {//Keyboard Off, Help Off, Video Off (Home)
                        centerImage.color = videoColor;
                        ModeChangeManager.bHome = false;
                        ModeChangeManager.bCircleBtn = true;
                    }
                    else if (!ModeChangeManager.bKeyboard && !ModeChangeManager.bHelp && !ModeChangeManager.bHome)
                    {//Keyboard Off, Help Off, Video On
                        centerImage.color = homeColor;
                        ModeChangeManager.bHome = true;
                        ModeChangeManager.bCircleBtn = false;
                    }
                    else if (ModeChangeManager.bKeyboard && !ModeChangeManager.bHelp && ModeChangeManager.bHome)
                    {//Keyboard On, Help Off, Video Off
                        centerImage.color = videoColor;
                        ModeChangeManager.bKeyboard = false;
                        ModeChangeManager.bHome = false;
                    }
                    else if (!ModeChangeManager.bKeyboard && ModeChangeManager.bHelp && ModeChangeManager.bHome)
                    {//Keyboard Off, Help On, Video Off
                        centerImage.color = videoColor;
                        ModeChangeManager.bHelp = false;
                        ModeChangeManager.bHome = false;
                    }
                    else if (ModeChangeManager.bKeyboard && !ModeChangeManager.bHelp && !ModeChangeManager.bHome)
                    {//Keyboard On, Help Off, Video On
                        centerImage.color = keyboardColor;
                        keyboard.GetComponent<Image>().enabled = true;
                        ModeChangeManager.bHome = true;
                    }
                    else if (!ModeChangeManager.bKeyboard && ModeChangeManager.bHelp && !ModeChangeManager.bHome)
                    {//Keyboard Off, Help On, Video On
                        centerImage.color = helpColor;
                        help.GetComponent<Image>().enabled = true;
                        ModeChangeManager.bHome = true;
                    }
                    break;

                case MType.keyboard:
                    if (!ModeChangeManager.bKeyboard && ModeChangeManager.bHome)
                    {//Keyboard Off, Video Off
                        centerImage.color = keyboardColor;
                        keyboard.GetComponent<Image>().enabled = true;
                        ModeChangeManager.bKeyboard = true;
                        ModeChangeManager.bCircleBtn = true;
                    }
                    else if (!ModeChangeManager.bKeyboard && !ModeChangeManager.bHome)
                    {//Keyboard Off, Video On
                        centerImage.color = keyboardColor;
                        keyboard.GetComponent<Image>().enabled = false;
                        ModeChangeManager.bKeyboard = true;
                        ModeChangeManager.bCircleBtn = true;
                    }
                    break;

                case MType.help:
                    if (!ModeChangeManager.bHelp && ModeChangeManager.bHome)
                    {//Help Off, Video Off
                        centerImage.color = helpColor;
                        help.GetComponent<Image>().enabled = true;
                        ModeChangeManager.bHelp = true;
                        ModeChangeManager.bCircleBtn = true;
                    }
                    else if (!ModeChangeManager.bKeyboard && !ModeChangeManager.bHome)
                    {//Help Off, Video On
                        centerImage.color = helpColor;
                        help.GetComponent<Image>().enabled = false;
                        ModeChangeManager.bHelp = true;
                        ModeChangeManager.bCircleBtn = true;
                    }
                    break;

                case MType.btn_x:
                    if (ModeChangeManager.bKeyboard && ModeChangeManager.bHome)
                    {//Keyboard On, Video Off
                        ModeChangeManager.bKeyboard = false;
                        ModeChangeManager.bCircleBtn = false;
                        centerImage.color = homeColor;
                    }
                    else if (ModeChangeManager.bHelp && ModeChangeManager.bHome)
                    {//Help On, Video Off
                        ModeChangeManager.bHelp = false;
                        ModeChangeManager.bCircleBtn = false;
                        centerImage.color = homeColor;
                    }
                    else if (ModeChangeManager.bKeyboard && !ModeChangeManager.bHome)
                    {//Keyboard On, Video On
                        ModeChangeManager.bKeyboard = false;
                        centerImage.color = videoColor;
                    }
                    else if (ModeChangeManager.bHelp && !ModeChangeManager.bHome)
                    {//Help On, Video On
                        ModeChangeManager.bHelp = false;
                        centerImage.color = videoColor;
                    }
                    break;

                case MType.controller:
                    if (ModeChangeManager.bMoving)
                    {// Moving On
                        ModeChangeManager.bMoving = false;
                        ModeChangeManager.bNeck = true;
                    }
                    else
                    {// Neck On
                        ModeChangeManager.bMoving = true;
                        ModeChangeManager.bNeck = false;
                    }
                    break;
            }

            EyeTypingManager.Instance.PlayKeySound();
        }

        private void SetActiveMode()
        {
            home.SetActive(ModeChangeManager.bHome);
            //videoStreaming.SetActive(ModeChangeManager.bVideoStreaming);
            moving.SetActive(ModeChangeManager.bMoving);
            neck.SetActive(ModeChangeManager.bNeck);
            help.SetActive(ModeChangeManager.bHelp);
            keyboard.SetActive(ModeChangeManager.bKeyboard);
            circleBtn.SetActive(ModeChangeManager.bCircleBtn);
        }

        private void FindModeObject()
        {
            center = GameObject.Find("Center");
            centerImage = center.GetComponent<Image>();
            home = GameObject.Find("Home");
            //videoStreaming = GameObject.Find("VideoStreaming");
            moving = GameObject.Find("Moving_Controller");
            neck = GameObject.Find("Neck_Controller");
            help = GameObject.Find("Help");
            keyboard = GameObject.Find("Keyboard");
            circleBtn = GameObject.Find("Circle_Btn");
        }
    }
}