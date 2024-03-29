using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "IncreaseRewardBuff", menuName = "Buffs/Increase Reward Buff")]
    public class IncreaseRewardBuff : BuffInfoData
    {
        [SerializeField] private int _increaseRewardAmount;
        public int IncreaseRewardAmount => _increaseRewardAmount;
    }
}