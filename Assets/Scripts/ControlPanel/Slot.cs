using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

	public class Slot : MonoBehaviour, IDropHandler
	{
		public GameObject prefab;
		public GameObject betweenPrefab;
		public int slotID;
		public Item item;
		public FlagStack flagStack;

	/*internal static Slot GetSlotFromId(int id)
	{
		Slot toReturn = null;
		mappedSlots.TryGetValue(id, out toReturn);
		return toReturn;
	}*/

		/*public static Slot Create(int slotID, GameObject parent)
		{
			GameObject toReturnGO = Instantiate(prefab);
			toReturnGO.transform.SetParent(parent.transform, false);
			Slot toReturn = toReturnGO.GetComponent<Slot>();
			toReturn.slotID = slotID;
			return toReturn;
		}*/

		public void OnDrop(PointerEventData eventData)
		{

			Item dragged = eventData.pointerDrag.GetComponent<Item>();
		bool toRemove = false;


		if (dragged.currentSlot == null) { // from right panel to left panel


			GameObject oldSlot = dragged.currentSlot;
			Item oldItem = this.item;

			if (item != null) {
				item = null;
				flagStack.RemoveFlagAt (slotID);
				Destroy (transform.GetChild (0).gameObject);
				toRemove = true;

			}

			// Recreate right panel Item

			GameObject draggedPrefab = dragged.gameObject;
			GameObject rightItem = Instantiate (draggedPrefab, dragged.initialSlot.transform);
			rightItem.transform.localPosition = Vector3.zero;

			rightItem.GetComponent<CanvasGroup>().blocksRaycasts = true;


			//Update new Slot item
			// Update new Item Slot

			this.item = dragged; // Update Slot's item
			dragged.currentSlot = this.gameObject; // Update Item's slot


			//Update FlagStack
			flagStack.AddFlagAt (dragged.flagprefab.GetComponent<Flag> (), slotID); // flagStack update

			if (flagStack.flags.Count < 5 && this.slotID == flagStack.flags.Count - 1 && !toRemove) { // needing a new Slot


				// Initialize new betweenSlot
				GameObject newBetweenSlot = Instantiate(betweenPrefab,transform.parent);

				// Initialize new Slot
				GameObject newSlot = Instantiate(prefab,transform.parent);
				newSlot.transform.localPosition = Vector3.zero;
				newSlot.GetComponent<Slot> ().slotID = slotID + 1;
				newSlot.GetComponent<Slot> ().flagStack = flagStack;
				newSlot.GetComponent<Slot> ().item = null;


			} 


		} else { // from left panel to left panel

			if (item != null){
				
				GameObject oldSlot = dragged.currentSlot;
				Item oldItem = this.item;

				this.item = dragged;
				dragged.currentSlot = this.gameObject;

				// New Position update
				flagStack.RemoveFlagAt (slotID); // flagStack update
				flagStack.AddFlagAt (dragged.flagprefab.GetComponent<Flag> (), slotID); // flagStack update
				

				oldItem.currentSlot = oldSlot;
				oldItem.transform.parent = oldSlot.transform;
				oldItem.transform.localPosition = Vector3.zero;

				// Old position update
				flagStack.RemoveFlagAt (oldItem.currentSlot.GetComponent<Slot> ().slotID); // flagStack update
				flagStack.AddFlagAt (oldItem.flagprefab.GetComponent<Flag> (), oldItem.currentSlot.GetComponent<Slot> ().slotID); // flagStack update
			

				
			}

		}


		}

	public void MoveAll(int position){
		
	}

}
