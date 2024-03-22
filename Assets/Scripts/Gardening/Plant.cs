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

        public void Initialized(PlantData plantData, Action<Plant> callback)
        {
            _releaseCallback = callback;
        
            _plantData = plantData;
        
            _cancellationTokenSource = new CancellationTokenSource();
            GrowAsync().Forget();
        }

        async UniTaskVoid GrowAsync()
        {
            try
            {
                var timePerPrefab = _plantData.GrowTime / _growthPrefabs.Count;
       
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
