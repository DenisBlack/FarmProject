using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Gardening.UI
{
    public class GardenTimerTooltip : Tooltip
    {
        private TMP_Text _text;
        private PlantData _plantData;
        private float _growTime;

        private Action<GardenTimerTooltip> _releaseCallback;

        private CancellationTokenSource _cancellationTokenSource;

        private float _reduceGrowPercent = 0;
        void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void Initialized(PlantData plantData, Transform targetTransform, Action<GardenTimerTooltip> callback)
        {
            _releaseCallback = callback;
            
            _plantData = plantData;
            _growTime = _plantData.GrowTime;
            
            AttachToTransform(targetTransform, 0.9f);
            
            StartTimer().Forget();
        }

        private async UniTaskVoid StartTimer()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            while (_growTime > 0)
            {
                string formattedTime = FormatTime(_growTime);
                
                _text.color = _reduceGrowPercent > 0 ? Color.green : Color.white;
                _text.text = $"{_plantData.ProductReference.GetAsset().ItemName}: { formattedTime}";
               
                await UniTask.Delay(1000, cancellationToken: _cancellationTokenSource.Token);
         
                if (_cancellationTokenSource.Token.IsCancellationRequested)
                    return;
                
                _growTime -= 1;
            }
            _text.text = String.Empty;
            
            _releaseCallback?.Invoke(this);
        }

        public void GrowingBuffChanges(int percent)
        {
            _reduceGrowPercent = percent;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

            float remainingTime = _growTime * (1f - _reduceGrowPercent / 100f);
            _growTime = remainingTime;
  
            StartTimer().Forget();
        }
        
        private string FormatTime(float seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);

            if (time.TotalHours >= 1)
            {
                return string.Format("{0:0}h {1:0}m", time.Hours + time.Days * 24, time.Minutes);
            }

            if (time.TotalMinutes >= 1)
            {
                return string.Format("{0:0}m {1:0}s", time.Minutes, time.Seconds);
            }

            return string.Format("{0:0}s", time.Seconds);
        }
        
    }
}