using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gardening
{
    public class Plant : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _growthPrefabs;

        private PlantData _plantData;
        private CancellationTokenSource _cancellationTokenSource;
        private Action<Plant> _releaseCallback;
    
        public bool IsPlantGrew => _plantGrew;
        private bool _plantGrew;
        private float _growTime;
        private float _reduceGrowPercent = 0;

        public void Initialized(PlantData plantData, Action<Plant> callback)
        {
            _releaseCallback = callback;
        
            _plantData = plantData;
            _growTime = plantData.GrowTime;

            GrowingBuffChanges(0);
        }

        public void GrowingBuffChanges(int percent)
        {
            _reduceGrowPercent = percent;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            
            float remainingTime = _growTime * (1f - _reduceGrowPercent / 100f);
            _growTime = remainingTime;
       
            GrowAsync().Forget();
        }

        async UniTaskVoid GrowAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            try
            {
                var timePerPrefab = _growTime / _growthPrefabs.Count;
       
                _growthPrefabs[0].SetActive(true);
        
                for (int i = 0; i < _growthPrefabs.Count; i++)
                {
                    var tCancellationThrow = await UniTask
                        .Delay(TimeSpan.FromSeconds(timePerPrefab), DelayType.DeltaTime, PlayerLoopTiming.Update,
                            _cancellationTokenSource.Token).SuppressCancellationThrow();

                    if (!tCancellationThrow)
                    {
                        if (i > 0)
                        {
                            var previousPrefab = _growthPrefabs[i - 1];
                            var currentPrefab = _growthPrefabs[i];
                    
                            previousPrefab.SetActive(false);
                            currentPrefab.SetActive(true);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            
                _releaseCallback?.Invoke(this);
                _plantGrew = true;
            }
            catch (OperationCanceledException e)
            {
                Debug.Log("Growth canceled " + e.Message);
            }
        }
    }
}
