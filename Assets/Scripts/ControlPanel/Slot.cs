using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    public static GameObject original;
    public int slotID;
    public Item item;

    /*internal static Slot GetSlotFromId(int id)
	{
		Slot toReturn = null;
		mappedSlots.TryGetValue(id, out toReturn);
		return toReturn;
	}*/

    public static Slot Create(Flag flag, int slotID, Transform parent)
    {
        GameObject toReturnGO = Instantiate(original);
        toReturnGO.transform.SetParent(parent, false);
        Slot toReturn = toReturnGO.GetComponent<Slot>();
        int flagID = -1;
        if (flag is ActFlag)
            flagID = 0;
        if (flag is DieFlag)
            flagID = 1;
        if (flag is JumpFlag)
            flagID = 2;
        if (flag is DirectionFlag)
            switch ((flag as DirectionFlag).direction)
            {
                case DirectionFlag.Direction.Left:
                    flagID = 3;
                    break;
                case DirectionFlag.Direction.Right:
                    flagID = 5;
                    break;
                default:
                    break;
            }
        if (flag is ReverseFlag)
            flagID = 4;
        if (flag is StopFlag)
            flagID = 6;
        GameObject newFlag = Item.Create(flagID).gameObject;
        newFlag.transform.SetParent(toReturnGO.transform, true);
        newFlag.GetComponent<CanvasGroup>().blocksRaycasts = true;
        newFlag.GetComponent<RectTransform>().localPosition = Vector3.zero;
        newFlag.GetComponent<Item>().currentSlot = toReturnGO;
        toReturn.item = newFlag.GetComponent<Item>();
        toReturn.slotID = slotID;
        return toReturn;
    }

    internal static Slot CreateEmpty(int slotID, Transform parent)
    {
        GameObject toReturnGO = Instantiate(original);
        toReturnGO.transform.SetParent(parent, false);
        Slot toReturn = toReturnGO.GetComponent<Slot>();
        toReturn.item = null;
        toReturn.slotID = slotID;
        return toReturn;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Item dragged = eventData.pointerDrag.GetComponent<Item>();
        //bool toRemove = false;

        if (dragged.currentSlot == null)
        { // from right panel to left panel

            //GameObject oldSlot = dragged.currentSlot;
            //Item oldItem = this.item;

            //if (item != null)
            //{
            //    item = null;
            //    //flagStack.RemoveFlagAt(slotID);
            //    Destroy(transform.GetChild(0).gameObject);
            //    toRemove = true;

            //}

            // Recreate right panel Item

            //GameObject draggedPrefab = dragged.gameObject;
            //GameObject rightItem = Instantiate(draggedPrefab, dragged.initialSlot.transform);
            //rightItem.transform.localPosition = Vector3.zero;

            //rightItem.GetComponent<CanvasGroup>().blocksRaycasts = true;


            //Update new Slot item
            // Update new Item Slot
            SlotManager.instance.SetSlotFromRight(slotID, ref dragged);
            //this.item = dragged; // Update Slot's item
            //dragged.currentSlot = this.gameObject; // Update Item's slot


            ////Update FlagStack
            //flagStack.AddFlagAt(dragged.flagPrefab.GetComponent<Flag>(), slotID); // flagStack update

            //if (flagStack.flags.Count < 5 && this.slotID == flagStack.flags.Count - 1 && !toRemove)
            { // needing a new Slot


                // Initialize new betweenSlot
                //GameObject newBetweenSlot = Instantiate(betweenPrefab, transform.parent);

                //// Initialize new Slot
                //GameObject newSlot = Instantiate(original, transform.parent);
                //newSlot.transform.localPosition = Vector3.zero;
                //newSlot.GetComponent<Slot>().slotID = slotID + 1;
                //newSlot.GetComponent<Slot>().flagStack = flagStack;
                //newSlot.GetComponent<Slot>().item = null;


            }


        }
        else
        { // from left panel to left panel

            if (item != null)
            {
                SlotManager.instance.SetSlotFromLeft(slotID, dragged.currentSlot.GetComponent<Slot>().slotID);
                //GameObject oldSlot = dragged.currentSlot;
                //Item oldItem = this.item;

                //this.item = dragged;
                //dragged.currentSlot = this.gameObject;

                //oldItem.currentSlot = oldSlot;
                //oldItem.transform.SetParent(oldSlot.transform,false);
                //oldItem.transform.localPosition = Vector3.zero;

                //flagStack update
                //flagStack.SwapFlag(oldItem.currentSlot.GetComponent<Slot>().slotID, slotID);


            }

        }


    }


    public void MoveAll(int position)
    {

    }

}
