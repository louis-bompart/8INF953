using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


	public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
	{
	
	public GameObject flagprefab;
	public GameObject currentSlot;
	public GameObject initialSlot;


		public void OnBeginDrag(PointerEventData eventData)
		{
			transform.SetParent(transform.parent.transform.root, true);
			this.transform.position = eventData.position;
			this.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}

		public void OnDrag(PointerEventData eventData)
		{
			this.transform.position = eventData.position;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
		if (currentSlot != null) {
			transform.SetParent (currentSlot.transform, true);
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
			GetComponent<RectTransform> ().localPosition = Vector3.zero;
		} else {
			transform.SetParent (initialSlot.transform, true);
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
			GetComponent<RectTransform> ().localPosition = Vector3.zero;
		}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			/*if (isStandalone)
				return;
			Tooltip.instance.Activate(Slot.GetSlotFromId(currentSlot.slotID).item);*/
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			/*if (isStandalone)
				return;
			Tooltip.instance.Desactivate();*/
		}

}