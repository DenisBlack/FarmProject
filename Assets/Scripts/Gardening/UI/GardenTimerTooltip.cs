using System;
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
            //private Transform _targetTransform;
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
            
            UpdateTimerDisplay().Forget();
        }

        private async UniTaskVoid UpdateTimerDisplay()
        {
            while (_growTime > 0)
            {
                string formattedTime = FormatTime(_growTime);
                _text.text = $"{_plantData.ProductReference.GetAsset().ItemName}: { formattedTime}";
                await UniTask.Delay(1000); // оновлення кожну секунду
                _growTime -= 1; // віднімання секунди від часу зростання
            }
            _text.text = String.Empty; //
            
            _releaseCallback?.Invoke(this);
        }
        
        private string FormatTime(float seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);

            if (time.TotalHours >= 1)
            {
                return string.Format("{0:0}h {1:0}m", time.Hours + time.Days * 24, time.Minutes);
            }
            else if (time.TotalMinutes >= 1)
            {
                return string.Format("{0:0}m {1:0}s", time.Minutes, time.Seconds);
            }
            else
            {
                return string.Format("{0:0}s", time.Seconds);
            }
        }
        
    }
}