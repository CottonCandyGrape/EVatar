using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace EyeHelpers
{
    public class EyeTyping : MonoBehaviour
    {
        public Color normalColor = Color.white;
        public Color focusedColor = Color.red;

        public GazeAware gazeAware;
        public Image targetImage;
        public InputField inputField;


        //private bool hasFocus = false;
        private Timer typingTimer = new Timer();
        private float typingTime = 2f;
        private AudioSource audioSource;
        private RectTransform rectTransform;
        private GazePoint gazePoint;

        void Awake()
        {
            if (gazeAware == null)
                gazeAware = GetComponent<GazeAware>();

            if (targetImage == null)
                targetImage = GetComponent<Image>();

            rectTransform = GetComponent<RectTransform>();

            audioSource = GetComponent<AudioSource>();

            gazePoint = TobiiAPI.GetGazePoint();
        }

        void Update()
        {
            IsGazing();
        }

        void IsGazing()
        {
            if (gazeAware.HasGazeFocus)
            //if (Math.Abs(rectTransform.localPosition.x) >= Math.Abs(gazePoint.Screen.x)
            //    && Math.Abs(rectTransform.localPosition.y) >= Math.Abs(gazePoint.Screen.y)) //쳐다보고 있을때
            {
                typingTimer.Update(Time.deltaTime);
                Debug.Log("쳐다본다");
                if (typingTimer.HasPastSince(typingTime)) //타이핑 시간이 지났을때
                    Typing();
            }
            else /*if (!gazeAware.HasGazeFocus) *///안 쳐다보고 있을때
            {
                typingTimer.Reset();
                ChangeColor(gazeAware.HasGazeFocus);
            }
        }

        void Typing()
        {
            inputField.text += targetImage.name;
            ChangeColor(gazeAware.HasGazeFocus);
            //audioSource.Play();
        }

        void ChangeColor(bool state)
        {
            if (state) targetImage.material.color = focusedColor;
            else targetImage.material.color = normalColor;
        }
    }
}