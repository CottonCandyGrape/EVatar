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
        
        [SerializeField] private Image gaugeImage;
        private Timer timer;
        private float typingTime = 1f;

        private string currentcontrolMode = string.Empty;
        private string[] commandList = {
            "<cmd=mobility:go1>",
            "<cmd=mobility:back1>",
            "<cmd=mobility:left1>",
            "<cmd=mobility:right1>",
            "<cmd=head:up1>",
            "<cmd=head:down1>",
            "<cmd=head:left1>",
            "<cmd=head:right1>"
        };

        [SerializeField] private ChatSend chatSend;
        [SerializeField] private RobotControl robotControl;

        // Use this for initialization
        void Start()
        {
            timer = new Timer();
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

            UpdateGauge(timer.GetElapsedTime / typingTime);
        }

        private void UpdateGauge(float amount)
        {
            gaugeImage.fillAmount = amount;
        }

        public void ResetTimer()
        {
            timer.Reset();
            gaugeImage.fillAmount = 0f;
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
                        robotControl.TypedUp();
                        if (robotControl.SendState)
                            chatSend.SendCommandText(commandList[4]);
                    }
                    else if (direction == Direction.backward)
                    {
                        robotControl.TypedDown();
                        if (robotControl.SendState)
                            chatSend.SendCommandText(commandList[5]);
                    }
                    else if (direction == Direction.turn_left)
                    {
                        robotControl.TypedLeft();
                        if (robotControl.SendState)
                            chatSend.SendCommandText(commandList[6]);
                    }

                    else if (direction == Direction.turn_right)
                    {
                        robotControl.TypedRight();
                        if (robotControl.SendState)
                            chatSend.SendCommandText(commandList[7]);
                    }
                    break;
            }

            EyeTypingManager.Instance.PlayKeySound();
        }

    }
}