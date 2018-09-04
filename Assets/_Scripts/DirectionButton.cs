using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EyeHelpers
{
    public class DirectionButton : MonoBehaviour
    {
        public enum Direction { forward, backward, turn_left, turn_right }
        public Direction direction = Direction.forward;

        //Button button;
        public string currentcontrolMode = string.Empty;

        public Sprite hoverImage;

        private Image image;
        private Sprite normalImage;
        private Timer timer;

        // Use this for initialization
        void Start()
        {
            image = GetComponent<Image>();
            normalImage = image.sprite;
            timer = new Timer();
        }        

        private void CurrentMode()
        {
            if (ModeChangeManager.bMoving)
                currentcontrolMode = "Moving";
            else
                currentcontrolMode = "Neck";
        }

        // Update is called once per frame
        void Update()
        {
            IsOut();
            CurrentMode();
        }

        private void IsOut()
        {
            // 버튼 벗어났는지 확인.
            if (timer.GetLastGameTime != 0f && (Time.realtimeSinceStartup - timer.GetLastGameTime) > Time.deltaTime * 3f)
            {
                image.sprite = normalImage;
                ResetTimer();
            }
        }

        public void UpdateTimer(float deltaTime)
        {
            timer.Update(deltaTime);
            if (timer.HasPastSince(1f))
            {
                Typing();
            }

            image.sprite = hoverImage;
        }

        public void ResetTimer()
        {
            timer.Reset();
        }

        void Typing()
        {
            switch (currentcontrolMode)
            {
                case "Moving":
                    if (direction == Direction.forward)
                        EyeTypingManager.Instance.sendText = "<cmd=mobility:go0>";
                        //Debug.Log("직진");
                    else if (direction == Direction.backward)
                        EyeTypingManager.Instance.sendText = "<cmd=mobility:back0>";
                    //Debug.Log("후진");
                    else if (direction == Direction.turn_left)
                        EyeTypingManager.Instance.sendText = "<cmd=mobility:left0>";
                    //Debug.Log("좌회전");
                    else if (direction == Direction.turn_right)
                        EyeTypingManager.Instance.sendText = "<cmd=mobility:right0>";
                    //Debug.Log("우회전");
                    break;

                case "Neck":
                    if (direction == Direction.forward)
                        EyeTypingManager.Instance.sendText = "<cmd=head:up0>";
                    //Debug.Log("위로");
                    else if (direction == Direction.backward)
                        EyeTypingManager.Instance.sendText = "<cmd=head:down0>";
                    //Debug.Log("아래로");
                    else if (direction == Direction.turn_left)
                        EyeTypingManager.Instance.sendText = "<cmd=head:left0>";
                    //Debug.Log("왼쪽으로");
                    else if (direction == Direction.turn_right)
                        EyeTypingManager.Instance.sendText = "<cmd=head:right0>";
                    //Debug.Log("오른쪽으로");
                    break;
            }

            EyeTypingManager.Instance.PlayKeySound();
        }
    }
}