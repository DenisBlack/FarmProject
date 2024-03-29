using System;
using System.Collections.Generic;
using System.Linq;
using Effects;
using UnityEngine;
using Zenject;

namespace Buffs
{
    public class BuffPresenterPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform _root;
        
        private BuffElementFactory _buffElementFactory;
        private BuffSystem _buffSystem;

        private List<BuffElement> _elements = new List<BuffElement>();

        [Inject]
        public void Construct(BuffElementFactory buffElementFactory, BuffSystem buffSystem)
        {
            _buffElementFactory = buffElementFactory;
            _buffSystem = buffSystem;
        }

        private void OnEnable()
        {
            _buffSystem.OnBuffStarted += OnBuffStarted;
            _buffSystem.OnBuffCompleted += OnBuffCompleted;
        }

        private void OnBuffStarted(Buff buff)
        {
            var buffPresenter = _buffElementFactory.Create(buff, _root);
            _elements.Add(buffPresenter);
        }

        private void OnBuffCompleted(Buff buff)
        {
           var element = _elements.FirstOrDefault(x => x.Buff.Equals(buff));
           if(element != null)
               element.Release();
        }

        private void OnDisable()
        {
            _buffSystem.OnBuffStarted -= OnBuffStarted;
            _buffSystem.OnBuffCompleted -= OnBuffCompleted;
        }
    }
}