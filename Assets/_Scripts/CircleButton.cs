using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EyeHelpers
{
    public class CircleButton : MonoBehaviour
    {
        GameObject video, keyboard, keyboardOnVideo, help, helpOnVideo;

        // Use this for initialization
        void Start()
        {
            FindCircleButton();
        }

        // Update is called once per frame
        void Update()
        {
            ChangeCircleButton();
        }

        private void FindCircleButton()
        {
            video = GameObject.Find("Video_Btn");
            keyboard = GameObject.Find("Keyboard_Btn");
            keyboardOnVideo = GameObject.Find("Keyboard_Btn_OnVideo");
            help = GameObject.Find("Help_Btn");
            helpOnVideo = GameObject.Find("Help_Btn_OnVideo");
        }

        private void OffCurrentCircleButton()
        {
            if (video.activeSelf) video.SetActive(false);
            if (keyboard.activeSelf) keyboard.SetActive(false);
            if (keyboardOnVideo.activeSelf) keyboardOnVideo.SetActive(false);
            if (help.activeSelf) help.SetActive(false);
            if (helpOnVideo.activeSelf) helpOnVideo.SetActive(false);
        }

        public void ChangeCircleButton()
        {
            if (!ModeChangeManager.bHome && !ModeChangeManager.bKeyboard
                && !ModeChangeManager.bHelp)
            {//Video On //다른 모드에서 Video_Btn이 켜지지 않기 위함
                OffCurrentCircleButton();
                video.SetActive(true);
            }
            else if (ModeChangeManager.bKeyboard && ModeChangeManager.bHome)
            {//Keyboard On, Video Off                
                OffCurrentCircleButton();
                keyboard.SetActive(true);
            }
            else if (ModeChangeManager.bKeyboard && !ModeChangeManager.bHome)
            {//Keyboard On, Video On
                OffCurrentCircleButton();
                keyboardOnVideo.SetActive(true);
            }
            else if (ModeChangeManager.bHelp && ModeChangeManager.bHome)
            {//Help On, Video Off
                OffCurrentCircleButton();
                help.SetActive(true);
            }
            else if (ModeChangeManager.bHelp && !ModeChangeManager.bHome)
            {//Help On, Video On
                OffCurrentCircleButton();
                helpOnVideo.SetActive(true);
            }
        }
    }
}