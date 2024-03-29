using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Effects
{
    public class Buff
    {
        private BuffData _data;
        private CancellationTokenSource _cancellationTokenSource;

        public BuffData BuffInfo => _data;
   
        public void Initialized(BuffData data)
        {
            _data = data;
            
            _cancellationTokenSource = new CancellationTokenSource();
            PerformBuff().Forget();
        }

        public async UniTask PerformBuff()
        {
            var tCancellationThrow =await UniTask
                .Delay(TimeSpan.FromSeconds(_data.Duration), DelayType.DeltaTime, PlayerLoopTiming.Update,
                    _cancellationTokenSource.Token).SuppressCancellationThrow();

            if (!tCancellationThrow)
            {
                
            }
        }
    }
}