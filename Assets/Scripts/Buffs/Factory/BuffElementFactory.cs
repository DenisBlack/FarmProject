using Effects;
using UnityEngine;
using Zenject;

namespace Buffs
{
    public class BuffElementFactory
    {
        [Inject] private DiContainer _diContainer;
        private readonly BuffElement _buffElement;

        public BuffElementFactory(BuffElement buffElement)
        {
            _buffElement = buffElement;
        }
        
        public BuffElement Create(Buff buff, RectTransform parent)
        {
            var element = _diContainer.InstantiatePrefab(_buffElement, parent).GetComponent<BuffElement>();
            element.Initialize(buff, Release);
            return element;
        }

        private void Release(BuffElement element)
        {
            Object.Destroy(element.gameObject);
        }
    }
}