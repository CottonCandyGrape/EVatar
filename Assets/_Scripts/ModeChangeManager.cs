using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EyeHelpers
{
    public class ModeChangeManager : MonoBehaviour
    {
        public static bool bHome, bVideoStreaming, bKeyboard, bHelp,
            bCircleBtn, bMoving, bNeck;

        // Use this for initialization
        void Awake()
        {
            bHome = true;
            bVideoStreaming = false;
            bMoving = true;
            bNeck = false;
            bKeyboard = false;
            bHelp = false;
            bCircleBtn = false;            
        }
    }
}