using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "BuffData", menuName = "Buffs/Buff Data")]
    public class BuffData : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _duration;
        [SerializeField] private BuffType _buffType;
        [SerializeField] private BuffInfoData _buffInfoData;
        [SerializeField] private string _buffDescription;
        public Sprite Icon => _icon;
        public float Duration => _duration;
        public string BuffDescription => _buffDescription;
        public BuffInfoData BuffInfoData => _buffInfoData;

        public BuffType Type => _buffType;
        public enum BuffType
        {
            ReduceGrow,
            IncreaseReward
        }
    }
}