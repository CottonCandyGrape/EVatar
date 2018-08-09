using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SpeechLib;

namespace EyeHelpers
{
    public class KeyButton : MonoBehaviour
    {
        public string keyName = string.Empty;
        public Sprite hoverImage;

        private Image image;
        private Sprite normalImage;

        private SpVoice voice;
        public InputField inputField;

        private Timer timer;

        private void Awake()
        {
            image = GetComponent<Image>();
            normalImage = image.sprite;

            voice = new SpVoice();

            timer = new Timer();
        }

        void Update()
        {
            // 버튼 벗어났는지 확인.
            if (timer.GetLastGameTime != 0f && (Time.realtimeSinceStartup - timer.GetLastGameTime) > Time.deltaTime * 3f)
            {
                image.sprite = normalImage;
                ResetTimer();
            }   
        }

        public void UpdateTimer(float deltaTime)
        {
            timer.Update(deltaTime);
            if (timer.HasPastSince(1f))
            {
                if(keyName.Equals("speak"))
                {
                    TextToSpeech();
                }
                else
                {
                    KeyboardManager.Instance.Append(keyName);
                }
            }

            image.sprite = hoverImage;
        }

        public void ResetTimer()
        {
            timer.Reset();
        }

        void TextToSpeech()
        {
            voice.Volume = 100; // Volume (no xml)
            voice.Rate = 0;  //   Rate (no xml)
            voice.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='ko-KO'>"
                        //+"반갑습니다.이부분이 그냥출력"
                        + inputField.text
                        + "</speak>",
                        SpeechVoiceSpeakFlags.SVSFlagsAsync | SpeechVoiceSpeakFlags.SVSFIsXML);
        }
    }
}