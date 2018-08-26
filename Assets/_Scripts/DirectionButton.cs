using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EyeHelpers
{
    public class DirectionButton : MonoBehaviour
    {
        Button button;
        string currentMode = string.Empty;

        public enum Direction { forward, backward, turn_left, turn_right }
        public Direction direction = Direction.forward;
        //GameObject up, down, left, right;       

        // Use this for initialization
        void Start()
        {
            button = GetComponent<Button>();

            if (button != null)
            {
                button.onClick.AddListener(OnClick);
            }

            //FindDirectionObject();
        }

        //private void FindDirectionObject()
        //{
        //    up = GameObject.Find("up");
        //    down = GameObject.Find("down");
        //    left = GameObject.Find("left");
        //    right = GameObject.Find("right");
        //}

        private void CurrentMode()
        {
            if (ModeChangeManager.bMoving)
                currentMode = "Moving";
            else
                currentMode = "Neck";
        }

        // Update is called once per frame
        void Update()
        {
            CurrentMode();
        }

        void OnClick()
        {
            switch (currentMode)
            {
                case "Moving":
                    if (direction == Direction.forward)
                        Debug.Log("직진");
                    else if (direction == Direction.backward)
                        Debug.Log("후진");
                    else if (direction == Direction.turn_left)
                        Debug.Log("좌회전");
                    else if (direction == Direction.turn_right)
                        Debug.Log("우회전");
                    break;

                case "Neck":
                    break;
            }
        }
    }
}