using System.Collections.Generic;

namespace Popups
{
    public class PopupSystem
    {
        private Queue<IPopup> _popups = new Queue<IPopup>();
        private readonly PopupFactory _popupFactory;

        public PopupSystem( PopupFactory popupFactory)
        {
            _popupFactory = popupFactory;
        }
    
        public void ShownPopup(PopupData popupData)
        {
            var popup = _popupFactory.CreatePopup(popupData);
            popup.Shown();
        
            if (popupData.SinglePopup)
            {
                while (_popups.Count > 0)
                {
                    var prevPopup = _popups.Dequeue();
                    prevPopup.Hide();
                }
            }
        
            _popups.Enqueue(popup);
        }

        public void CloseLastPopup()
        {
            var prevPopup = _popups.Dequeue();
            prevPopup.Hide();
            _popupFactory.Release(prevPopup);
        }
    }
}