using UnityEngine;

namespace Popups
{
    [CreateAssetMenu(fileName = "PopupData", menuName = "Popups/Popup Data")]
    public class PopupData : ScriptableObject
    {
        [SerializeField] private GameObject _popupGameObject;
        [SerializeField] private bool _singlePopup;
        
        public GameObject Popup => _popupGameObject;
        public bool SinglePopup => _singlePopup;
    }
}