using UnityEngine;
using System.Collections;
using SpeechLib;

namespace EyeHelpers
{
    public class VirtualTextInputBox : MonoBehaviour
    {
        AutomateKR mAutomateKR = new AutomateKR();
        protected UnityEngine.UI.InputField mTextField = null;
        protected string TextField
        {
            set
            {
                if (mTextField != null)
                {
                    mTextField.text = value;
                }
            }
            get
            {
                if (mTextField != null)
                {
                    return mTextField.text;
                }
                return "";
            }
        }
        private SpVoice voice;

        void Start()
        {
            mTextField = GetComponent<UnityEngine.UI.InputField>();
            voice = new SpVoice();
        }

        void Update()
        {

        }

        public void TextToSpeech()
        {
            voice.Volume = 100; // Volume (no xml)
            voice.Rate = 0;  //   Rate (no xml)
            voice.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='ko-KO'>"
                        //+"반갑습니다.이부분이 그냥출력"
                        //+ inputField.text
                        + TextField
                        + "</speak>",
                        SpeechVoiceSpeakFlags.SVSFlagsAsync | SpeechVoiceSpeakFlags.SVSFIsXML);
        }

        public void Clear()
        {
            mAutomateKR.Clear();

            TextField = mAutomateKR.completeText + mAutomateKR.ingWord;
        }


        public void KeyDownHangul(char _key)
        {
            mAutomateKR.SetKeyCode(_key);

            TextField = mAutomateKR.completeText + mAutomateKR.ingWord;
        }

        public void KeyDown(char _key)
        {
            mAutomateKR.SetKeyString(_key);

            TextField = mAutomateKR.completeText + mAutomateKR.ingWord;
        }

        public void KeyDown(VirtualKey _key)
        {
            switch (_key.KeyType)
            {
                case VirtualKey.kType.kBackspace:
                    {
                        mAutomateKR.SetKeyCode(AutomateKR.KEY_CODE_BACKSPACE);

                    }
                    break;
                case VirtualKey.kType.kSpace:
                    {
                        mAutomateKR.SetKeyCode(AutomateKR.KEY_CODE_SPACE);
                    }
                    break;
            }

            TextField = mAutomateKR.completeText + mAutomateKR.ingWord;
        }
    }
}

