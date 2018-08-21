using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EyeHelpers
{
    public class KeyboardManager : Singleton<KeyboardManager>
    {
        public InputField inputField;
        public GraphicRaycaster raycaster;
        public GameObject sphere;

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

        //public void Append(string input)
        //{
        //    inputField.text += input;
        //}

        private void Update()
        {
            // 물체 이동.
            UpdateObjectPosition();

            // 토비 좌표 기준으로 Ray 발사해서 키버튼 동작 구동.
            RayCastByTobii();
        }

        void RayCastByTobii()
        {
            data = new PointerEventData(eventSystem);
            data.position = TobiiAPI.GetGazePoint().Screen;

            // 토비 좌표가 화면 좌표 안에 있을 때만 실행되도록.
            if (!IsNaN(data.position))
            {
                // Ray 발사.
                results = new List<RaycastResult>();
                raycaster.Raycast(data, results);

                foreach (RaycastResult result in results)
                {
                    VirtualKey keyButton = result.gameObject.GetComponent<VirtualKey>();
                    if (keyButton == null) continue;
                    
                    keyButton.UpdateTimer(Time.deltaTime);
                }
            }
        }

        bool IsNaN(Vector2 input)
        {
            return float.IsNaN(input.x) || float.IsNaN(input.y);
        }

        void UpdateObjectPosition()
        {
            GazePoint pointer = TobiiAPI.GetGazePoint();
            if (pointer.IsValid)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(pointer.Screen);
                sphere.transform.position = worldPos;
            }
        }

        public void PlayKeySound()
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}