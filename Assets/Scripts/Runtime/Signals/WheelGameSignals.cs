using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime
{
    public class WheelGameSignals : MonoBehaviour
    {
        #region Singleton

        public static WheelGameSignals Instance;

        private void Awake()
        {
            if (Instance != this && Instance!=null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        #endregion


        public UnityAction<string, int, Sprite> onAddReward = delegate { };
        public UnityAction onSpin = delegate { };
        public UnityAction onNextZone = delegate { };
        public UnityAction<byte,bool> onGameObjectList = delegate { };
    }
}