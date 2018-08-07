using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.UI;

namespace EyeHelpers
{
    public class EyeTyping : MonoBehaviour
    {
        public Color normalColor = Color.white;
        public Color focusedColor = Color.red;

        public GazeAware gazeAware;
        public Renderer targetRenderer;
        public InputField inputField;


        //private bool hasFocus = false;
        private Timer typingTimer = new Timer();
        private float typingTime = 2f;
        private AudioSource audioSource;

        void Awake()
        {
            if (gazeAware == null)
                gazeAware = GetComponent<GazeAware>();

            if (targetRenderer == null)
                targetRenderer = GetComponent<Renderer>();

            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            IsGazing();
        }

        void IsGazing()
        {
            if (gazeAware.HasGazeFocus) //쳐다보고 있을때
            {
                typingTimer.Update(Time.deltaTime);
                if (typingTimer.HasPastSince(typingTime)) //타이핑 시간이 지났을때
                    Typing();                
            }
            else if (!gazeAware.HasGazeFocus) //안 쳐다보고 있을때
            {
                typingTimer.Reset();
                ChangeColor(gazeAware.HasGazeFocus);
            }
        }

        void Typing()
        {
            inputField.text += targetRenderer.name;
            ChangeColor(gazeAware.HasGazeFocus);
            audioSource.Play();
        }

        void ChangeColor(bool state)
        {
            if (state) targetRenderer.material.color = focusedColor;
            else targetRenderer.material.color = normalColor;
        }
    }
}