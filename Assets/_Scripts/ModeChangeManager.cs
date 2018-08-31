using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EyeHelpers
{
    public class ModeChangeManager : MonoBehaviour
    {
        public static bool bHome, bVideoStreaming, bKeyboard, bHelp,
            bCircleBtn, bMoving, bNeck, bBlur, bNum, bKor, bShiftedKor, bEn, bShiftedEn;

        // Use this for initialization
        void Awake()
        {
            bHome = true;

            bVideoStreaming = true;
            bMoving = true;
            bNeck = false;
            bBlur = false;

            bKeyboard = false;
            bKor = true;
            bShiftedKor = false;
            bNum = false;
            bEn = false;
            bShiftedEn = false;

            bHelp = false;

            bCircleBtn = false;            
        }
    }
}