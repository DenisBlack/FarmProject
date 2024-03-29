using System;
using UnityEngine;
using Zenject;

namespace Effects
{
    public class ReduceGrowBuffPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        
        private BuffSystem _buffSystem;

        [Inject]
        public void Construct(BuffSystem buffSystem)
        {
            _buffSystem = buffSystem;
        }

        public void Start()
        {
            _buffSystem.OnBuffStarted += OnBuffStarted;
            _buffSystem.OnBuffCompleted += OnBuffCompleted;
        }

        private void OnBuffStarted(Buff buff)
        {
            if (buff.BuffInfo.Type == BuffData.BuffType.ReduceGrow)
            {
                var reduceGrowBuff = buff.BuffInfo.BuffInfoData as ReduceGrowBuff;
                if (reduceGrowBuff != null)
                {
                    _root.SetActive(true);
                }
            }
        }

        private void OnBuffCompleted(Buff buff)
        {
            if (buff.BuffInfo.Type == BuffData.BuffType.ReduceGrow)
            {
                var reduceGrowBuff = buff.BuffInfo.BuffInfoData as ReduceGrowBuff;
                if (reduceGrowBuff != null)
                {
                    _root.SetActive(false);
                }
            }
        }
    }
}