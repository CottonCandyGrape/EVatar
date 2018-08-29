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

        private void ChangeCircleButton()
        {
            if (!ModeChangeManager.bHome
                && !ModeChangeManager.bKeyboard
                && !ModeChangeManager.bHelp)
            {//다른 모드에서 Video_Btn이 켜지지 않기 위함
                video.SetActive(true);
                keyboard.SetActive(false);
                keyboardOnVideo.SetActive(false);
                help.SetActive(false);
                helpOnVideo.SetActive(false);
            }
            else if (ModeChangeManager.bKeyboard && ModeChangeManager.bHome)
            {//Keyboard On, Video Off
                video.SetActive(false);
                keyboard.SetActive(true);
                keyboardOnVideo.SetActive(false);
                help.SetActive(false);
                helpOnVideo.SetActive(false);
            }
            else if (ModeChangeManager.bKeyboard && !ModeChangeManager.bHome)
            {//Keyboard On, Video On
                video.SetActive(false);
                keyboard.SetActive(false);
                keyboardOnVideo.SetActive(true);
                help.SetActive(false);
                helpOnVideo.SetActive(false);
            }
            else if (ModeChangeManager.bHelp && ModeChangeManager.bHome)
            {//Help On, Video Off
                video.SetActive(false);
                keyboard.SetActive(false);
                keyboardOnVideo.SetActive(false);
                help.SetActive(true);
                helpOnVideo.SetActive(false);
            }
            else if (ModeChangeManager.bHelp && !ModeChangeManager.bHome)
            {//Help On, Video On
                video.SetActive(false);
                keyboard.SetActive(false);
                keyboardOnVideo.SetActive(false);
                help.SetActive(false);
                helpOnVideo.SetActive(true);
            }
        }
    }
}