using UnityEngine;

namespace LimbInfo
{
    [System.Serializable]
    public enum LimbType
    {
        Leg,
        Arm
    }

    [System.Serializable]
    public struct MyStruct
    {
        public bool isEnabled;
        public LimbType limbType;
    }

}