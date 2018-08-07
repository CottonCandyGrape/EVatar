using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EyeHelpers
{
    public class Timer
    {
        private float elapsedTime = 0f;

        public void Update(float deltaTime)
        {
            elapsedTime += deltaTime;
        }

        public float GetElapsedTime
        {
            get { return elapsedTime; }
        }

        public void Reset()
        {
            elapsedTime = 0f;
        }

        public bool HasPastSince(float time)
        {
            bool retValue = elapsedTime >= time;
            if (retValue) elapsedTime = 0f;

            return retValue;
        }
    }
}