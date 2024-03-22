using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Popups
{
    public class PopupFactory
    {
        [Inject] private DiContainer _diContainer;
        private readonly RectTransform _popupParent;
        private List<IPopup> _popups = new List<IPopup>();

        public PopupFactory(RectTransform popupParent)
        {
            _popupParent = popupParent;
        }
        
        public IPopup CreatePopup(PopupData popupData)
        {
            var popup = _diContainer.InstantiatePrefab(popupData.Popup, _popupParent).GetComponent<IPopup>();
            _popups.Add(popup);
            return popup;
        }
        
        public void Release(IPopup popup)
        {
            var popupGameObject = (popup as MonoBehaviour)?.gameObject;
            _popups.Remove(popup);
            Object.Destroy(popupGameObject);
        }
    }
}