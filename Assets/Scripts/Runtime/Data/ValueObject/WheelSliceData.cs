using System;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct WheelSlice
    {
        public Sprite spriteObject;
        public string sliceName;
        public bool isBomb;
        public int rewardPoints;
        public string rewardedText;
    }
}