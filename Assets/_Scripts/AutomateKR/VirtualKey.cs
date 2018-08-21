using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EyeHelpers
{
    public class VirtualKey : MonoBehaviour
    {
        static public VirtualKeyboard _Keybord = null;
        public enum kType { kCharacter, kOther, kReturn, kSpace, kBackspace, kShift, kTab, kCapsLock, kHangul, kSpeak }
        public char KeyCharacter;
        public kType KeyType = kType.kCharacter;
        public Sprite hoverImage;

        private bool mKeepPresed;
        public bool KeepPressed
        {
            set { mKeepPresed = value; }
            get { return mKeepPresed; }
        }

        private Image image;
        private Sprite normalImage;
        private Timer timer;
        private AudioSource typingSound;

        private void Awake()
        {
            image = GetComponent<Image>();
            normalImage = image.sprite;
            timer = new Timer();
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
            if (timer.HasPastSince(0.5f))
            {
                Typing();
            }

            image.sprite = hoverImage;
        }

        void Typing()
        {
            if (_Keybord != null)
            {
                _Keybord.KeyDown(this);
            }

            KeyboardManager.Instance.PlayKeySound();
            //typingSound.Play();
        }

        public void ResetTimer()
        {
            timer.Reset();
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


