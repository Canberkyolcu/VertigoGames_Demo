using System.Collections.Generic;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "NewWheelData", menuName = "Wheel Data", order = 1)]
    public class CD_WheelData : ScriptableObject
    {
        public string SpinName;
        public Sprite MainObject;
        public Sprite SpinIndicator;
        public List<WheelSlice> WheelSlicesData;
        
    }
}