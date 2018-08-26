using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class ModeChangeManager : MonoBehaviour
    {
        public static bool bHome, bVideoStreaming, bKeyboard, bHelp, bCircleBtn;

        // Use this for initialization
        void Awake()
        {
            bHome = true;
            bVideoStreaming = false;
            bKeyboard = false;
            bHelp = false;
            bCircleBtn = false;
        }

        //void update()
        //{
        //    ToggleHelpAndText();
        //}

        //void ToggleHelpAndText()
        //{
        //    if (!ModeChangeManager.bKeyboard)
        //        ModeChangeManager.bHelp = true;
        //    else
        //        ModeChangeManager.bHelp = false;
        //}

        //void ToggleHomeAndCircleBtn()
        //{
        //    if (!ModeChangeManager.bHome)
        //        ModeChangeManager.bCircleBtn = true;
        //    else
        //        ModeChangeManager.bCircleBtn = false;
        //}
    }
}