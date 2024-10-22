﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace EyeHelpers
{
    public class HelpButton : MonoBehaviour
    {
        public enum HelpMenu { breathing, defecation, meal, menuDetailText }
        public HelpMenu helpMenu = HelpMenu.breathing;
        public string helpCommand = string.Empty;

        [SerializeField] private Image gaugeImage;
        private Timer timer;
        private float typingTime = 1f;

        [SerializeField] private ChatSend chatSend;

        GameObject breathing, breathingClicked, defecation, defecationClicked, meal, mealClicked;

        // Use this for initialization
        void Start()
        {
            timer = new Timer();
            FindHelpButton();
        }

        // Update is called once per frame
        void Update()
        {
            IsOut();
        }

        private void FindHelpButton()
        {
            breathing = GameObject.Find("Breathing");
            breathingClicked = GameObject.Find("Breathing_Clicked");
            defecation = GameObject.Find("Defecation");
            defecationClicked = GameObject.Find("Defecation_Clicked");
            meal = GameObject.Find("Meal");
            mealClicked = GameObject.Find("Meal_Clicked");
        }

        private void IsOut()
        {   // 버튼 벗어났는지 확인.
            if (timer.GetLastGameTime != 0f && (Time.realtimeSinceStartup - timer.GetLastGameTime) > Time.deltaTime * 3f)
            {
                ResetTimer();
            }
        }

        public void UpdateTimer(float deltaTime)
        {
            timer.Update(deltaTime);
            if (timer.HasPastSince(typingTime))
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

        private void OffCurrentMenu()
        {
            if (!breathing.activeSelf) breathing.SetActive(true);
            if (!defecation.activeSelf) defecation.SetActive(true);
            if (!meal.activeSelf) meal.SetActive(true);
        }

        void Typing()
        {
            HelpMenu clickedMenu = this.helpMenu;
            switch (clickedMenu)
            {
                case HelpMenu.breathing:
                    OffCurrentMenu();
                    breathing.SetActive(false);
                    break;

                case HelpMenu.defecation:
                    OffCurrentMenu();
                    defecation.SetActive(false);
                    break;

                case HelpMenu.meal:
                    OffCurrentMenu();
                    meal.SetActive(false);
                    break;

                case HelpMenu.menuDetailText:
                    //Debug.Log(helpCommand);
                    if (ModeChangeManager.bHome)
                    {
                        EyeTypingManager.Instance.TextToSpeech(helpCommand);
                    }
                    else
                    {
                        chatSend.SendCommandText("<tts=" + helpCommand + ">");
                    }
                    break;
            }

            EyeTypingManager.Instance.PlayKeySound();
        }
    }
}