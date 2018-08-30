using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

        private void Awake()
        {
            eventSystem = EventSystem.current;
            data = new PointerEventData(eventSystem);
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            RayCastByTobii();
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