using System;
using Cysharp.Threading.Tasks;
using Effects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Buffs
{
    public class BuffElement : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _timerTMP;
        private float _duration;

        public Buff Buff { get; private set; }

        public Action<BuffElement> _releaseAction;

        public void Initialize(Buff buff, Action<BuffElement> release)
        {
            Buff = buff;
            _image.sprite = buff.BuffInfo.Icon;
            _duration = buff.BuffInfo.Duration;
            _releaseAction = release;
            
            StartTimer().Forget();
        }

        async UniTask StartTimer()
        {
            while (_duration > 0)
            {
                await UniTask.Delay(1000, DelayType.DeltaTime);
                _duration -= 1;
                _timerTMP.text = $"{_duration}s";
            }
        }

        public void Release()
        {
            _releaseAction?.Invoke(this);
        }
    }
}