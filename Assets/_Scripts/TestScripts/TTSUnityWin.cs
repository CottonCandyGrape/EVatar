using UnityEngine;
using UnityEngine.UI;
using SpeechLib;
//내부에 있는 customMarshalers.dll이랑 Interop.SpeechLib.dll같은파일을 같은 폴더 내에 놔둬야함
//Resources폴더 내에있는 파일도 마찬가지

public class TTSUnityWin : MonoBehaviour
{
    private SpVoice voice;
    public InputField input;

    void Start()
    {
        voice = new SpVoice();
        input = GameObject.Find("InputField1").GetComponent<InputField>();
    }

    void Ttsstar()
    {
        voice.Volume = 100; // Volume (no xml)
        voice.Rate = 0;  //   Rate (no xml)
        voice.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='ko-KO'>"
                    //+"반갑습니다.이부분이 그냥출력"
                    + input.text
                    + "</speak>",
                    SpeechVoiceSpeakFlags.SVSFlagsAsync | SpeechVoiceSpeakFlags.SVSFIsXML);
    }
}