using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

namespace EyeHelpers
{
    public class GazePointer : MonoBehaviour
    {
        public float frame = 10f;

        private float fps = 0f;
        private bool hasStarted = false;
        private float startedTime = 0f;
        private Timer timer = new Timer();
        private float distance = 10f;

        void Awake()
        {
            fps = 1f / frame;
        }

        void InitTobii(GazePoint gazePoint)
        {
            hasStarted = true;
            startedTime = gazePoint.Timestamp;
        }

        void Update()
        {
            timer.Update(Time.deltaTime);
            if (timer.HasPastSince(fps))
            {
                UpdateGazePoint();
            }
        }

        void UpdateGazePoint()
        {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            if (gazePoint.IsValid)
            {
                if (!hasStarted) InitTobii(gazePoint);
                UpdatePosition(gazePoint);
            }
        }

        void UpdatePosition(GazePoint gazePoint)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(gazePoint.Screen);
            worldPos += transform.forward * distance;
            transform.position = worldPos;

            //Debug.Log(gazePoint.Timestamp - startedTime);
        }
    }
}