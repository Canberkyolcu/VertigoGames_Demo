using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Manager
{
    public class UIManager : MonoBehaviour
    {
        #region SerializeField

        [SerializeField] private Button spinButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Button collectRewardsButton;
        [SerializeField] private Button reviveButton;
        [SerializeField] private Button giveupButton;
        [SerializeField] private Image losePanel;
        [SerializeField] private Image exitPanelGameObject;

        #endregion


       [SerializeField] private List<GameObject> _objects;

        private void Awake()
        {
            _objects = new List<GameObject>(3)
            {
                spinButton.gameObject,
                exitButton.gameObject,
                losePanel.gameObject
            };
        }

        private void OnEnable()
        {
            WheelGameSignals.Instance.onGameObjectList += ButtonList;
        }

        private void ButtonList(byte index,bool arg0)
        {
            _objects[index].gameObject.SetActive(arg0);
           
        }


        private void OnDisable()
        {
            WheelGameSignals.Instance.onGameObjectList -= ButtonList;
        }

        internal void SpinWheel()
        {
            
            WheelGameSignals.Instance.onSpin?.Invoke();
          
        }
        internal void ReviveButton()
        {
            losePanel.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(true);
            WheelGameSignals.Instance.onNextZone?.Invoke();
        }
        internal void ExitButton()
        {
            exitPanelGameObject.gameObject.SetActive(true);
        }

        internal void BackButton()
        {
            exitPanelGameObject.gameObject.SetActive(false);
        }
        
        #region OnValidate

        private void OnValidate()
        {
            spinButton = GameObject.Find("UI_Button_SpinButton").GetComponent<Button>();
            exitButton = GameObject.Find("UI_Button_ExitButton").GetComponent<Button>();
            backButton = GameObject.Find("UI_Button_GoBackButton").GetComponent<Button>();
            reviveButton = GameObject.Find("UI_Wheel_Button_ReviveButton").GetComponent<Button>();
            giveupButton = GameObject.Find("UI_Wheel_Button_GiveupButton").GetComponent<Button>();
            collectRewardsButton = GameObject.Find("UI_Button_CollectableButton").GetComponent<Button>();
        }

        #endregion
    }
}