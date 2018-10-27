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

        [SerializeField]private Image gaugeImage;
        private Image centerImage;
        private Timer timer;
        private float typingTime = 1f;

        GameObject center, home, controller, moving, neck, blur, help, keyboard, circleBtn;

        Color homeColor = new Color(0.7411765f, 0.2117647f, 0.3529412f);
        Color videoColor = new Color(0.7215686f, 0.1176471f, 0.2352941f);
        Color keyboardColor = new Color(0.4392157f, 0.1921569f, 0.4392157f);
        Color helpColor = new Color(0.1372549f, 0.3333333f, 0.4941177f);

        [SerializeField] private CircleButton circleButton;

        // Use this for initialization
        void Start()
        {
            timer = new Timer();
            FindModeObject();
        }

        void Update()
        {
            IsOut();
            SetActiveMode();
            OnOffBlur();
            OnOffController();
        }

        private void IsOut()
        {   // 버튼 벗어났는지 확인.
            if (timer.GetLastGameTime != 0f && (Time.realtimeSinceStartup - timer.GetLastGameTime) > Time.deltaTime * 3f)
            {
                ResetTimer();
            }
        }

        public void UpdateTimer(float deltaTime)
        {
            timer.Update(deltaTime);
            if (timer.HasPastSince(typingTime))
            {
                Typing();
            }

            UpdateGauge(timer.GetElapsedTime / typingTime);
        }

        private void UpdateGauge(float amount)
        {
            gaugeImage.fillAmount = amount;
        }

        public void ResetTimer()
        {
            timer.Reset();
            gaugeImage.fillAmount = 0f;
        }

        void Typing()
        {
            MType mode = this.modeType;
            switch (mode)
            {
                case MType.video:
                    if (!ModeChangeManager.bKeyboard && !ModeChangeManager.bHelp && ModeChangeManager.bHome)
                    {//Keyboard Off, Help Off, Video Off //Home에서 Video 켜기
                        centerImage.color = videoColor;
                        ModeChangeManager.bHome = false;
                        ModeChangeManager.bCircleBtn = true;
                        circleButton.ChangeCircleButton();
                    }
                    else if (!ModeChangeManager.bKeyboard && !ModeChangeManager.bHelp && !ModeChangeManager.bHome)
                    {//Keyboard Off, Help Off, Video On //Video에서 연결해제
                        centerImage.color = homeColor;
                        ModeChangeManager.bHome = true;
                        ModeChangeManager.bCircleBtn = false;
                    }
                    else if (ModeChangeManager.bKeyboard && !ModeChangeManager.bHelp && ModeChangeManager.bHome)
                    {//Keyboard On, Help Off, Video Off //Keyboard에서 연결하기
                        centerImage.color = videoColor;
                        ModeChangeManager.bKeyboard = false;
                        ModeChangeManager.bHome = false;
                        circleButton.ChangeCircleButton();
                    }
                    else if (!ModeChangeManager.bKeyboard && ModeChangeManager.bHelp && ModeChangeManager.bHome)
                    {//Keyboard Off, Help On, Video Off //Help에서 연결하기
                        centerImage.color = videoColor;
                        ModeChangeManager.bHelp = false;
                        ModeChangeManager.bHome = false;
                        circleButton.ChangeCircleButton();
                    }
                    else if (ModeChangeManager.bKeyboard && !ModeChangeManager.bHelp && !ModeChangeManager.bHome)
                    {//Keyboard On, Help Off, Video On //Keyboard에서 연결해제
                        centerImage.color = keyboardColor;
                        keyboard.GetComponent<Image>().enabled = true;
                        ModeChangeManager.bHome = true;
                        circleButton.ChangeCircleButton();
                    }
                    else if (!ModeChangeManager.bKeyboard && ModeChangeManager.bHelp && !ModeChangeManager.bHome)
                    {//Keyboard Off, Help On, Video On //Help에서 연결해제
                        centerImage.color = helpColor;
                        help.GetComponent<Image>().enabled = true;
                        ModeChangeManager.bHome = true;
                        circleButton.ChangeCircleButton();
                    }
                    break;

                case MType.keyboard:
                    if (!ModeChangeManager.bKeyboard && ModeChangeManager.bHome)
                    {//Keyboard Off, Video Off //Home에서 Keyboard 켜기
                        centerImage.color = keyboardColor;
                        keyboard.GetComponent<Image>().enabled = true;
                        ModeChangeManager.bKeyboard = true;
                        ModeChangeManager.bCircleBtn = true;
                        circleButton.ChangeCircleButton();
                    }
                    else if (!ModeChangeManager.bKeyboard && !ModeChangeManager.bHome)
                    {//Keyboard Off, Video On //Video에서 Keyboard 켜기
                        centerImage.color = keyboardColor;
                        keyboard.GetComponent<Image>().enabled = false;
                        ModeChangeManager.bKeyboard = true;
                        ModeChangeManager.bCircleBtn = true;
                        circleButton.ChangeCircleButton();
                    }
                    break;

                case MType.help:
                    if (!ModeChangeManager.bHelp && ModeChangeManager.bHome)
                    {//Help Off, Video Off //Home에서 Help 켜기
                        centerImage.color = helpColor;
                        help.GetComponent<Image>().enabled = true;
                        ModeChangeManager.bHelp = true;
                        ModeChangeManager.bCircleBtn = true;
                        circleButton.ChangeCircleButton();
                    }
                    else if (!ModeChangeManager.bKeyboard && !ModeChangeManager.bHome)
                    {//Help Off, Video On //Video에서 Help 켜기
                        centerImage.color = helpColor;
                        help.GetComponent<Image>().enabled = false;
                        ModeChangeManager.bHelp = true;
                        ModeChangeManager.bCircleBtn = true;
                        circleButton.ChangeCircleButton();
                    }
                    break;

                case MType.btn_x:
                    if (ModeChangeManager.bKeyboard && ModeChangeManager.bHome)
                    {//Keyboard On, Video Off //Keyboard 끄기
                        ModeChangeManager.bKeyboard = false;
                        ModeChangeManager.bCircleBtn = false;
                        centerImage.color = homeColor;
                    }
                    else if (ModeChangeManager.bHelp && ModeChangeManager.bHome)
                    {//Help On, Video Off //Help 끄기
                        ModeChangeManager.bHelp = false;
                        ModeChangeManager.bCircleBtn = false;
                        centerImage.color = homeColor;
                    }
                    else if (ModeChangeManager.bKeyboard && !ModeChangeManager.bHome)
                    {//Keyboard On, Video On 
                        ModeChangeManager.bKeyboard = false;
                        centerImage.color = videoColor;
                        circleButton.ChangeCircleButton();
                    }
                    else if (ModeChangeManager.bHelp && !ModeChangeManager.bHome)
                    {//Help On, Video On
                        ModeChangeManager.bHelp = false;
                        centerImage.color = videoColor;
                        circleButton.ChangeCircleButton();
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
            moving.SetActive(ModeChangeManager.bMoving);
            neck.SetActive(ModeChangeManager.bNeck);
            blur.SetActive(ModeChangeManager.bBlur);
            help.SetActive(ModeChangeManager.bHelp);
            keyboard.SetActive(ModeChangeManager.bKeyboard);
            circleBtn.SetActive(ModeChangeManager.bCircleBtn);
        }

        private void FindModeObject()
        {
            center = GameObject.Find("Center");
            centerImage = center.GetComponent<Image>();
            home = GameObject.Find("Home");
            controller = GameObject.Find("Controller");
            moving = GameObject.Find("Moving_Controller");
            neck = GameObject.Find("Neck_Controller");
            blur = GameObject.Find("BLUR");
            help = GameObject.Find("Help");
            keyboard = GameObject.Find("Keyboard");
            circleBtn = GameObject.Find("Circle_Btn");
        }

        private void OnOffBlur()
        {//Video On, Keyboard On OR Video On, Help On
            if ((!ModeChangeManager.bHome && ModeChangeManager.bKeyboard) ||
                (!ModeChangeManager.bHome && ModeChangeManager.bHelp))
                ModeChangeManager.bBlur = true;
            else
                ModeChangeManager.bBlur = false;
        }

        private void OnOffController()
        {
            if ((!ModeChangeManager.bHome && ModeChangeManager.bKeyboard) ||
                (!ModeChangeManager.bHome && ModeChangeManager.bHelp))
                controller.SetActive(false);
            else
                controller.SetActive(true);
        }
    }
}