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

    public void TtsButton()
    {
        Invoke("Ttsstar", 2);
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

//void OnGUI()
//{
//    SpObjectTokenCategory tokenCat = new SpObjectTokenCategory();
//    tokenCat.SetId(SpeechLib.SpeechStringConstants.SpeechCategoryVoices, false);
//    ISpeechObjectTokens tokens = tokenCat.EnumerateTokens(null, null);

//    int n = 0;
//    foreach (SpObjectToken item in tokens)
//    {
//        GUILayout.Label("Voice" + n + " ---> " + item.GetDescription(0));
//        n++;
//    }
//    GUILayout.Label("There are " + n + " SAPI voices installed in your OS | Press SPACE for start TEST");
//}

//public void GetInput(string guess)
//{
//    Debug.Log("You Entered " + guess);
//    input.text = "";
//}

//string BuiltAsset = "";