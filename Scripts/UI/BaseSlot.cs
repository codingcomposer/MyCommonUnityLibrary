using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DKCommon.UI
{
    public class BaseSlot : MonoBehaviour, IPointerClickHandler
    {
        protected ISlotHandler slotHandler;
        public void SetHandler(ISlotHandler _slotHandler)
        {
            slotHandler = _slotHandler;
        }
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (slotHandler != null)
            {
                slotHandler.SlotClicked(this);
            }
        }
    }
}