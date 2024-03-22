using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Popups.UI
{
    public class ClosePopupButton : MonoBehaviour
    {
        private Button _button;
        private PopupSystem _popupSystem;
    
        [Inject]
        public void Construct(PopupSystem popupSystem)
        {
            _popupSystem = popupSystem;
        }
    
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _popupSystem.CloseLastPopup();
        }
    }
}
