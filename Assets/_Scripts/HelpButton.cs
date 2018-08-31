using System.Collections;
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

        public Sprite hoverImage;

        private Image image;
        private Sprite normalImage;
        private Timer timer;

        GameObject breathing, breathingClicked, defecation, defecationClicked, meal, mealClicked;


        // Use this for initialization
        void Start()
        {
            image = GetComponent<Image>();
            normalImage = image.sprite;
            timer = new Timer();

            FindHelpButton();
        }

        // Update is called once per frame
        void Update()
        {
            IsOut();
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

        private void FindHelpButton()
        {
            breathing = GameObject.Find("Breathing");
            breathingClicked = GameObject.Find("Breathing_Clicked");
            defecation = GameObject.Find("Defecation");
            defecationClicked = GameObject.Find("Defecation_Clicked");
            meal = GameObject.Find("Meal");
            mealClicked = GameObject.Find("Meal_Clicked");
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
                    //if (breathing.activeSelf) breathing.SetActive(false);
                    //else breathing.SetActive(true);
                    OffCurrentMenu();
                    breathing.SetActive(false);
                    break;

                case HelpMenu.defecation:
                    //if (defecation.activeSelf) defecation.SetActive(false);
                    //else defecation.SetActive(true);
                    OffCurrentMenu();
                    defecation.SetActive(false);
                    break;

                case HelpMenu.meal:
                    //if (meal.activeSelf) meal.SetActive(false);
                    //else meal.SetActive(true);
                    OffCurrentMenu();
                    meal.SetActive(false);
                    break;

                case HelpMenu.menuDetailText:
                    Debug.Log(helpCommand);
                    break;
            }

            EyeTypingManager.Instance.PlayKeySound();
        }
    }
}