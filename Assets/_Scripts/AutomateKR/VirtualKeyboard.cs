using UnityEngine;
using System.Collections.Generic;

namespace EyeHelpers
{
    public class VirtualKeyboard : MonoBehaviour
    {
        public VirtualTextInputBox TextInputBox = null;
        protected enum kLanguage { kKorean, kEnglish };
        protected bool mPressShift = false;
        protected kLanguage mLanguage = kLanguage.kKorean;
        protected Dictionary<char, char> CHARACTER_TABLE = new Dictionary<char, char>
    {
        {'1', '!'}, {'2', '@'}, {'3', '#'}, {'4', '$'}, {'5', '%'},{'6', '^'}, {'7', '&'}, {'8', '*'}, {'9', '('},{'0', ')'},
        { '`', '~'},   {'-', '_'}, {'=', '+'}, {'[', '{'}, {']', '}'}, {'\\', '|'}, {',', '<'}, {'.', '>'}, {'/', '?'}
    };

        GameObject kor, shiftedKor, num, en, shiftedEn;
        //string currentKeyboard = "bKor";
        ChatSend chatSend;


        void Awake()
        {
            VirtualKey._Keybord = this;
            FindKeyboardModeObject();
            chatSend = new ChatSend();
        }

        void Update()
        {
            SetActiveMode();
        }

        private void FindKeyboardModeObject()
        {
            kor = GameObject.Find("k_Normal");
            shiftedKor = GameObject.Find("k_Shifted");
            num = GameObject.Find("Number");
            en = GameObject.Find("en_Normal");
            shiftedEn = GameObject.Find("en_Shifted");
        }

        private void SetActiveMode()
        {
            kor.SetActive(ModeChangeManager.bKor);
            shiftedKor.SetActive(ModeChangeManager.bShiftedKor);
            num.SetActive(ModeChangeManager.bNum);
            en.SetActive(ModeChangeManager.bEn);
            shiftedEn.SetActive(ModeChangeManager.bShiftedEn);
        }

        private void OffCurrentKeyboard()
        {
            if (ModeChangeManager.bKor)
                ModeChangeManager.bKor = false;
            else if (ModeChangeManager.bShiftedKor)
                ModeChangeManager.bShiftedKor = false;
            else if (ModeChangeManager.bNum)
                ModeChangeManager.bNum = false;
            else if (ModeChangeManager.bEn)
                ModeChangeManager.bEn = false;
            else if (ModeChangeManager.bShiftedEn)
                ModeChangeManager.bShiftedEn = false;
        }

        public void Clear()
        {
            if (TextInputBox != null)
            {
                TextInputBox.Clear();
            }
        }

        void OnGUI()
        {
            //Event e = Event.current;
            //if (e.isKey)
            //  Debug.Log("Detected key code: " + e.keyCode);

        }

        public void KeyDown(VirtualKey _key)
        {
            if (TextInputBox != null)
            {
                switch (_key.KeyType)
                {
                    case VirtualKey.kType.kShift:
                        {
                            mPressShift = true;
                            ChangeShiftKeyboard();
                        }
                        break;
                    case VirtualKey.kType.kHangul:
                        {
                            ChangeKorEnKeyboard();
                        }
                        break;
                    case VirtualKey.kType.kSpace:
                    case VirtualKey.kType.kBackspace:
                        {
                            TextInputBox.KeyDown(_key);
                        }
                        break;
                    case VirtualKey.kType.kReturn:
                        {
                            if (ModeChangeManager.bHome)
                            {// Video Off
                                TextInputBox.TextToSpeech();
                                Clear();
                            }
                            else
                            {
                                Debug.Log("일단 들어옴");
                                TextInputBox.SetSendTextInInputField();
                                Debug.Log("보낼 텍스트 초기화:" + SendCommand.sendText);
                                chatSend.SendCommandText();
                                Debug.Log("보냈다");
                                Clear();
                                Debug.Log("클리어");
                            }
                        }
                        break;
                    case VirtualKey.kType.kCharacter:
                        {
                            char keyCharacter = _key.KeyCharacter;
                            if (mPressShift)
                            {
                                keyCharacter = char.ToUpper(keyCharacter);
                                mPressShift = false;
                                ChangeShiftKeyboard();
                            }

                            if (mLanguage == kLanguage.kKorean)
                            {
                                TextInputBox.KeyDownHangul(keyCharacter);
                            }
                            else if (mLanguage == kLanguage.kEnglish)
                            {
                                TextInputBox.KeyDown(keyCharacter);
                            }
                        }
                        break;
                    case VirtualKey.kType.kOther:
                        {
                            char keyCharacter = _key.KeyCharacter;
                            if (mPressShift)
                            {
                                keyCharacter = CHARACTER_TABLE[keyCharacter];
                                mPressShift = false;
                                ChangeShiftKeyboard();
                            }
                            TextInputBox.KeyDown(keyCharacter);
                        }
                        break;
                    case VirtualKey.kType.kNum:
                        {
                            ChangeNumKeyboard();
                        }
                        break;
                }
            }
        }

        private void ChangeShiftKeyboard()
        {
            if (mPressShift && ModeChangeManager.bKor)
            {
                OffCurrentKeyboard();
                ModeChangeManager.bShiftedKor = true;
            }
            else if (mPressShift && ModeChangeManager.bShiftedKor)
            {
                OffCurrentKeyboard();
                ModeChangeManager.bKor = true;
                mPressShift = false;
            }
            else if (mPressShift && ModeChangeManager.bShiftedEn)
            {
                OffCurrentKeyboard();
                ModeChangeManager.bEn = true;
                mPressShift = false;
            }
            else if (mPressShift && ModeChangeManager.bEn)
            {
                OffCurrentKeyboard();
                ModeChangeManager.bShiftedEn = true;
            }
            else if (!mPressShift && ModeChangeManager.bShiftedKor)
            {
                OffCurrentKeyboard();
                ModeChangeManager.bKor = true;
            }
            else if (!mPressShift && ModeChangeManager.bShiftedEn)
            {
                OffCurrentKeyboard();
                ModeChangeManager.bEn = true;
            }
        }

        private void ChangeKorEnKeyboard()
        {
            if (mLanguage == kLanguage.kKorean)
            {
                mLanguage = kLanguage.kEnglish;
                OffCurrentKeyboard();
                ModeChangeManager.bEn = true;
            }
            else if (mLanguage == kLanguage.kEnglish)
            {
                mLanguage = kLanguage.kKorean;
                OffCurrentKeyboard();
                ModeChangeManager.bKor = true;
            }
        }

        private void ChangeNumKeyboard()
        {
            if (!ModeChangeManager.bNum)
            {
                OffCurrentKeyboard();
                ModeChangeManager.bNum = true;
            }
            else if (mLanguage == kLanguage.kKorean)
            {
                OffCurrentKeyboard();
                ModeChangeManager.bKor = true;
            }
            else if (mLanguage == kLanguage.kEnglish)
            {
                OffCurrentKeyboard();
                ModeChangeManager.bEn = true;
            }
        }
    }
}