using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bin : MonoBehaviour, IDropHandler{
	
		public void OnDrop(PointerEventData eventData)
		{
			Item dragged = eventData.pointerDrag.GetComponent<Item>();
		if (dragged.currentSlot != null) {
            SlotManager.instance.RemoveItem(dragged);
			//dragged.currentSlot.GetComponent<Slot>().item = null;
			////dragged.currentSlot.GetComponent<Slot>().flagStack.RemoveFlagAt (dragged.currentSlot.GetComponent<Slot>().slotID);
			//Destroy (dragged.currentSlot.GetComponent<Slot>().transform.GetChild (0).gameObject);
		}

		}
}

