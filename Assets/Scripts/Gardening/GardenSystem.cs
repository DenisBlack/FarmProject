using System;
using Effects;
using Zenject;

namespace Gardening
{
    public class GardenSystem : IInitializable
    {
        private readonly BuffSystem _buffSystem;

        public Action<int> ReduceGrowTimerAction;
        private int _reduceGrowTimePercent;

        public GardenSystem(BuffSystem buffSystem)
        {
            _buffSystem = buffSystem;
        }

        public bool BuffActivated { get; private set; }
        public int BuffPercent { get; private set; }

        public void Initialize()
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
                    _reduceGrowTimePercent = reduceGrowBuff.ReduceGrowTimePercent;
                    BuffPercent = _reduceGrowTimePercent;
                    ReduceGrowTimerAction?.Invoke(_reduceGrowTimePercent);
                    BuffActivated = true;
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
                    _reduceGrowTimePercent = 0;
                    BuffPercent = _reduceGrowTimePercent;
                    ReduceGrowTimerAction?.Invoke(_reduceGrowTimePercent);
                    BuffActivated = false;
                }
            }
        }
    }
}