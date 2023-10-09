using UnityEngine;

namespace LimbInfo
{
    [System.Serializable]
    public enum LimbType
    {
        LeftLeg,
        RightLeg,
        LeftArm,
        RightArm
    }

    [System.Serializable]
    public struct LimbDetails
    {
        public LimbType Limb;
        public bool IsFromMainStatue;

        public LimbDetails(LimbType limb, bool isFromMainStatue)
        {
            Limb = limb;
            IsFromMainStatue = isFromMainStatue;
        }
    }

}