using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "ReduceGrowBuff", menuName = "Buffs/Reduce Grow Buff")]
    public class ReduceGrowBuff : BuffInfoData
    {
        [SerializeField] private int _reduceGrowTimePercent;
        public int ReduceGrowTimePercent => _reduceGrowTimePercent;
    }
}