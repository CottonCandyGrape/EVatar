using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EyeHelpers
{
    public class CircleButton : MonoBehaviour
    {
        GameObject video, keyboard, help;

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
            help = GameObject.Find("Help_Btn");
        }

        private void ChangeCircleButton()
        {
            if (!ModeChangeManager.bHome
                && !ModeChangeManager.bKeyboard
                && !ModeChangeManager.bHelp)
            {//다른 모드에서 Video_Btn이 켜지지 않기 위함
                video.SetActive(true);
                keyboard.SetActive(false);
                help.SetActive(false);                
            }
            else if (ModeChangeManager.bKeyboard)
            {
                video.SetActive(false);
                keyboard.SetActive(true);
                help.SetActive(false);                
            }
            else if (ModeChangeManager.bHelp)
            {
                video.SetActive(false);
                keyboard.SetActive(false);
                help.SetActive(true);                
            }
        }
    }
}