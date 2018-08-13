using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SpeechLib;

namespace EyeHelpers
{
    public class VirtualKey : MonoBehaviour
    {
        static public VirtualKeyboard _Keybord = null;
        public enum kType { kCharacter, kOther, kReturn, kSpace, kBackspace, kShift, kTab, kCapsLock, kHangul, kSpeak }
        public char KeyCharacter;
        public kType KeyType = kType.kCharacter;
        public Sprite hoverImage;
        public InputField inputField;        

        private bool mKeepPresed;
        public bool KeepPressed
        {
            set { mKeepPresed = value; }
            get { return mKeepPresed; }
        }

        private Image image;
        private Sprite normalImage;
        private Timer timer;
        private SpVoice voice;
        private AudioSource typingSound;

        private void Awake()
        {
            image = GetComponent<Image>();
            normalImage = image.sprite;
            timer = new Timer();
            voice = new SpVoice();
            typingSound = GetComponent<AudioSource>();
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
                //if (keyName.Equals("speak"))
                //{
                //    TextToSpeech();
                //}
                //else
                //{
                //    KeyboardManager.Instance.Append(keyName);
                //}
                Typing();
            }

            image.sprite = hoverImage;
        }

        void Typing()
        {
            //VirtualKeyboard _keybord = GameObject.FindObjectOfType< VirtualKeyboard>();
            if (_Keybord != null)
            {
                _Keybord.KeyDown(this);
            }
            typingSound.Play();
        }

        public void ResetTimer()
        {
            timer.Reset();
        }

        public void TextToSpeech()
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
//void Start()
//{
//    UnityEngine.UI.Button _button = gameObject.GetComponent<UnityEngine.UI.Button>();
//    if (_button != null)
//    {
//        _button.onClick.AddListener(onKeyClick);
//    }
//}

//void onKeyClick()
//{
//    //VirtualKeyboard _keybord = GameObject.FindObjectOfType< VirtualKeyboard>();
//    if (_Keybord != null)
//    {
//        _Keybord.KeyDown(this);
//    }
//}

//void Update()
//{

//    if (KeepPressed)
//    {
//        //do something
//    }
//}


