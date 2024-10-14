using System;
using Runtime.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Handlers
{
    public class UIEventSubscriber : MonoBehaviour
    {
        [SerializeField] private UIEventSubscriptionTypes tpye;
        [SerializeField] private Button button;

        private UIManager _manager;

        private void Awake()
        {
            _manager = FindObjectOfType<UIManager>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            switch (tpye)
            {
                case UIEventSubscriptionTypes.OnSpin:
                    button.onClick.AddListener(_manager.SpinWheel);
                    break;
                case UIEventSubscriptionTypes.OnExit:
                    button.onClick.AddListener(_manager.ExitButton);
                    break;
                case UIEventSubscriptionTypes.OnGiveUp:
                    break;
                case UIEventSubscriptionTypes.Onback:
                    button.onClick.AddListener(_manager.BackButton);
                    break;
                case UIEventSubscriptionTypes.OnCollect:
                    break;
                case UIEventSubscriptionTypes.OnRevive:
                    button.onClick.AddListener(_manager.ReviveButton);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UnSubscribeEvents()
        {
            switch (tpye)
            {
                case UIEventSubscriptionTypes.OnSpin:
                    button.onClick.RemoveListener(_manager.SpinWheel);
                    break;
                case UIEventSubscriptionTypes.OnExit:
                    button.onClick.RemoveListener(_manager.ExitButton);
                    break;
                case UIEventSubscriptionTypes.OnGiveUp:
                    break;
                case UIEventSubscriptionTypes.Onback:
                    button.onClick.RemoveListener(_manager.BackButton);
                    break;
                case UIEventSubscriptionTypes.OnCollect:
                    break;
                case UIEventSubscriptionTypes.OnRevive:
                    button.onClick.RemoveListener(_manager.ReviveButton);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}