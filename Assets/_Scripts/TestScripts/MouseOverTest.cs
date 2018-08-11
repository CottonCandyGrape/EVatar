using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EyeHelpers
{
    public class MouseOverTest : MonoBehaviour
    {
        public Renderer sprite;

        public Color color = Color.white;

        private Timer timer = new Timer();
        private float time = 2f;

        // Use this for initialization
        void Start()
        {
            sprite = GetComponent<Renderer>();
            UpdateOutline(true);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void UpdateOutline(bool outline)
        {
            MaterialPropertyBlock mpb = new MaterialPropertyBlock();
            sprite.GetPropertyBlock(mpb);
            mpb.SetFloat("_Outline", outline ? 1f : 0);
            mpb.SetColor("_OutklineColor", color);
            sprite.SetPropertyBlock(mpb);
        }

        void OnMouseOver()
        {
            timer.Update(Time.deltaTime);
            //Debug.Log(timer.GetElapsedTime);
            //UpdateOutline(true);
            if (timer.HasPastSince(time))
            {
                sprite.material.color = Color.green;
                Debug.Log("끝");
            }
        }

        void OnMouseExit()
        {
            timer.Reset();
            //Debug.Log(timer.GetElapsedTime);
            UpdateOutline(false);
            sprite.material.color = Color.white;
        }
    }
}