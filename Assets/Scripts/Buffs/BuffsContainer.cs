using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "BuffsContainer", menuName = "Effects/Buffs Container")]
    public class BuffsContainer : ScriptableObject
    {
        [SerializeField] private BuffData[] _buffDatas;
        public BuffData[] BuffDatas => _buffDatas;
    }
}