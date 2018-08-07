using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EyeHelpers
{
    public class MouseOverTest : MonoBehaviour
    {
        public Renderer sprite;

        private Timer timer = new Timer();
        private float time = 2f;

        // Use this for initialization
        void Start()
        {
            sprite = GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseOver()
        {
            timer.Update(Time.deltaTime);
            Debug.Log(timer.GetElapsedTime);
            if (timer.HasPastSince(time))
            {
                sprite.material.color = Color.green;
                Debug.Log("끝");
            }
        }

        void OnMouseExit()
        {
            timer.Reset();
            Debug.Log(timer.GetElapsedTime);
            sprite.material.color = Color.white;
        }
    }
}