using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SpeechLib;

namespace EyeHelpers
{
    public class EyeTypingManager : Singleton<EyeTypingManager>
    {
        public GraphicRaycaster raycaster;

        EventSystem eventSystem;
        PointerEventData data;
        List<RaycastResult> results;

        AudioSource audioSource;

        VirtualKey lastKey = null;

        private SpVoice voice;

        // 테스트 할 때 사용하는 불리언 값. true = 마우스 위치로 동작 / false = 토비 위치로 동작.
        [SerializeField] private bool isDebugMode = false;

        private void Awake()
        {
            eventSystem = EventSystem.current;
            data = new PointerEventData(eventSystem);
            audioSource = GetComponent<AudioSource>();
            voice = new SpVoice();
        }

        private void Update()
        {
            // 디버그 모드인지 확인해서 마우스 / 토비 위치로 동작할 지 확인.
            if (isDebugMode)
                RaycastByMouse();
            else
                RayCastByTobii();
        }

        // 테스트용 (마우스 포인터 위치를 기반으로 키보드 타이핑 하는 메소드).
        void RaycastByMouse()
        {
            data = new PointerEventData(eventSystem);
            data.position = Input.mousePosition;

            if (!IsNaN(data.position))
            {
                results = new List<RaycastResult>();
                raycaster.Raycast(data, results);

                if (results.Count > 0)
                {
                    GameObject result = results[0].gameObject;
                    ModeChangeButton modeChangeButton = result.GetComponent<ModeChangeButton>();
                    VirtualKey keyButton = result.GetComponent<VirtualKey>();
                    DirectionButton directionButton = result.GetComponent<DirectionButton>();
                    HelpButton helpButton = result.GetComponent<HelpButton>();

                    if (modeChangeButton != null) modeChangeButton.UpdateTimer(Time.deltaTime);
                    if (keyButton != null) keyButton.UpdateTimer(Time.deltaTime);
                    if (directionButton != null) directionButton.UpdateTimer(Time.deltaTime);
                    if (helpButton != null) helpButton.UpdateTimer(Time.deltaTime);
                }
            }
        }

        void RayCastByTobii() // 토비 좌표 기준으로 Ray 발사해서 동작 구동.
        {
            data = new PointerEventData(eventSystem);
            data.position = TobiiAPI.GetGazePoint().Screen;

            // 토비 좌표가 화면 좌표 안에 있을 때만 실행되도록.
            if (!IsNaN(data.position))
            {
                // Ray 발사.
                results = new List<RaycastResult>();
                raycaster.Raycast(data, results);

                if (results.Count > 0)
                {
                    ModeChangeButton modeChangeButton = results[0].gameObject.GetComponent<ModeChangeButton>();
                    VirtualKey keyButton = results[0].gameObject.GetComponent<VirtualKey>();
                    DirectionButton directionButton = results[0].gameObject.GetComponent<DirectionButton>();
                    HelpButton helpButton = results[0].gameObject.GetComponent<HelpButton>();

                    if (modeChangeButton != null) modeChangeButton.UpdateTimer(Time.deltaTime);
                    if (keyButton != null) keyButton.UpdateTimer(Time.deltaTime);
                    if (directionButton != null) directionButton.UpdateTimer(Time.deltaTime);
                    if (helpButton != null) helpButton.UpdateTimer(Time.deltaTime);
                }
            }
        }

        bool IsNaN(Vector2 input)
        {
            return float.IsNaN(input.x) || float.IsNaN(input.y);
        }

        public void PlayKeySound()
        {
            audioSource.PlayOneShot(audioSource.clip);
        }

        public void TextToSpeech(string ttsText)
        {
            voice.Volume = 100; // Volume (no xml)
            voice.Rate = 0;  //   Rate (no xml)
            voice.Speak("<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='ko-KO'>"
                        + ttsText
                        + "</speak>",
                        SpeechVoiceSpeakFlags.SVSFlagsAsync | SpeechVoiceSpeakFlags.SVSFIsXML);
        }

        //void UpdateObjectPosition()
        //{
        //    GazePoint pointer = TobiiAPI.GetGazePoint();
        //    if (pointer.IsValid)
        //    {
        //        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pointer.Screen);
        //        sphere.transform.position = worldPos;
        //    }
        //}
    }
}