using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EyeHelpers
{
    public class VirtualKey : MonoBehaviour
    {
        static public VirtualKeyboard _Keybord = null;
        public enum kType { kCharacter, kOther, kReturn, kSpace, kBackspace, kShift, kTab, kCapsLock, kHangul, kSpeak, kNum }
        public char KeyCharacter;
        public kType KeyType = kType.kCharacter;
        [SerializeField] private Image gaugeImage;
        private float typingTime = 0.5f;

        private bool mKeepPresed;
        public bool KeepPressed
        {
            set { mKeepPresed = value; }
            get { return mKeepPresed; }
        }

        private Timer timer;

        private void Awake()
        {
            timer = new Timer();
        }

        void Update()
        {
            // 버튼 벗어났는지 확인.
            if (timer.GetLastGameTime != 0f && (Time.realtimeSinceStartup - timer.GetLastGameTime) > Time.deltaTime * 3f)
            {
                ResetTimer();
            }
        }

        public void UpdateTimer(float deltaTime)
        {
            timer.Update(deltaTime);
            if (timer.HasPastSince(typingTime))
            {
                Typing();
            }

            UpdateGauge(timer.GetElapsedTime / typingTime);
        }

        private void UpdateGauge(float amount)
        {
            gaugeImage.fillAmount = amount;
        }

        void Typing()
        {
            if (_Keybord != null)
            {
                _Keybord.KeyDown(this);
            }

            EyeTypingManager.Instance.PlayKeySound();
        }

        public void ResetTimer()
        {
            timer.Reset();
            gaugeImage.fillAmount = 0f;
        }
    }
}