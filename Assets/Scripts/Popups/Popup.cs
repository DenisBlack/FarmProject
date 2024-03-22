using UnityEngine;

namespace Popups
{
    public abstract class PopupBase : MonoBehaviour, IPopup
    {
        [SerializeField] protected GameObject _root;

        public virtual void Shown()
        {
            _root.SetActive(true);
        }

        public virtual void Hide()
        {
            _root.SetActive(false);
        }
    }
}