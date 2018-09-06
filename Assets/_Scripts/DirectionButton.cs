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

        public Sprite hoverImage;

        private Image image;
        private Sprite normalImage;
        private Timer timer;
        private int horizontalBase, verticalBase;

        private string currentcontrolMode = string.Empty;
        private string[] commandList = {
            "<cmd=mobility:go0>",
            "<cmd=mobility:back0>",
            "<cmd=mobility:left0>",
            "<cmd=mobility:right0>",
            "<cmd=head:up0>",
            "<cmd=head:down0>",
            "<cmd=head:left0>",
            "<cmd=head:right0>"
        };

        [SerializeField] private ChatSend chatSend;
        [SerializeField] private Text noticeText;

        // Use this for initialization
        void Start()
        {
            image = GetComponent<Image>();
            normalImage = image.sprite;
            timer = new Timer();
            horizontalBase = 0;
            verticalBase = 0;
        }

        // Update is called once per frame
        void Update()
        {
            IsOut();
            CurrentMode();
        }

        private void CurrentMode()
        {
            if (ModeChangeManager.bMoving)
                currentcontrolMode = "Moving";
            else
                currentcontrolMode = "Neck";
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

        private void RobotControl()
        {
            //if () ;
        }

        void Typing()
        {
            switch (currentcontrolMode)
            {
                case "Moving":
                    if (direction == Direction.forward)
                        chatSend.SendCommandText(commandList[0]);
                    else if (direction == Direction.backward)
                        chatSend.SendCommandText(commandList[1]);
                    else if (direction == Direction.turn_left)
                        chatSend.SendCommandText(commandList[2]);
                    else if (direction == Direction.turn_right)
                        chatSend.SendCommandText(commandList[3]);
                    break;

                case "Neck":
                    if (direction == Direction.forward)
                    {
                        chatSend.SendCommandText(commandList[4]);
                    }
                    else if (direction == Direction.backward)
                    {
                        chatSend.SendCommandText(commandList[5]);
                    }
                    else if (direction == Direction.turn_left)
                    {
                        chatSend.SendCommandText(commandList[6]);
                        horizontalBase -= 1;
                    }
                        
                    else if (direction == Direction.turn_right)
                    {
                        chatSend.SendCommandText(commandList[7]);
                        horizontalBase += 1;
                    }                        
                    break;
            }

            EyeTypingManager.Instance.PlayKeySound();
        }
    }
}