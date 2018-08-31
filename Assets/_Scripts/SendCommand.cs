using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EyeHelpers
{
    public class SendCommand : MonoBehaviour
    {
        static public string sendText;

        void Awake()
        {
            sendText = "조깟네 이거 시발?";
        }
    }
}