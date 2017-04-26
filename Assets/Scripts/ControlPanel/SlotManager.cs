using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager instance;
    List<Slot> slots;
    public FlagStack flagStack;
    public Tile tile;
    // Use this for initialization
    private void Awake()
    {
        if (FindObjectsOfType<SlotManager>().Length > 1)
            DestroyImmediate(this.gameObject);
        else
        {
            slots = new List<Slot>();
            instance = this;
        }
    }

    internal void RemoveItem(Item item)
    {
        FlagStack.GetFS(flagStack).RemoveFlagAt(item.currentSlot.GetComponent<Slot>().slotID); // flagStack update   
		
        RefreshSlots(); 
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadFlagStack(Tile tile)
    {
        flagStack = tile.data.flagStack;
        this.tile = tile;
        RefreshSlots();
        Debug.Log("FlagStack loaded");
    }

    void AddSlot()
    {
        GameObject newSlot = Instantiate(Slot.original, transform.parent);
        newSlot.transform.localPosition = Vector3.zero;
        newSlot.GetComponent<Slot>().slotID = slots.Count;
        newSlot.GetComponent<Slot>().item = null;
    }

    public void SetSlotFromRight(int slotID, ref Item item)
    {

        //slots[slotID].item = item; // Update Slot's item
        //item.currentSlot = this.gameObject; // Update Item's slot
        //Update FlagStack
        if (flagStack.flags.Count < 5)
		FlagStack.GetFS(flagStack).AddFlagAt(item.flagPrefab.GetComponent<Flag>(), slotID); // flagStack update
        RefreshSlots();
        //if (slots.Count < 5 && slotID == flagStack.flags.Count - 1)

    }
    public void SetSlotFromLeft(int fromID, int toID)
    {
        FlagStack.GetFS(flagStack).SwapFlag(fromID, toID);
        RefreshSlots();
    }

    private void RefreshSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Destroy(slots[i].gameObject);
        }
        slots.Clear();
        //FlagStack flagStack = null;
        if (FlagStack.mapFlagStack.TryGetValue(this.flagStack.id, out flagStack))
        {
            for (int i = 0; i < flagStack.flags.Count; i++)
            {
                Flag flag = flagStack.flags[i];
                slots.Add(Slot.Create(flag, slots.Count, transform));
            }
        }
        if (slots.Count < 5)
        {
            slots.Add(Slot.CreateEmpty(slots.Count, transform));
        }
        tile.RefreshFlagStack();

    }
}
