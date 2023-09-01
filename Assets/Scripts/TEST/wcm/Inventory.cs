using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    //����� �κ��丮 ����Ʈ
    public string[] itemList;
    public Slot[] slots;
    
    //�κ��丮�� ũ��
    public const int numSlots = 4;

    private void Awake()
    {
        itemList = new string[numSlots];
        slots = GetComponentsInChildren<Slot>();
    }

    //����
    private void OnEnable()
    {
        foreach (Slot slot in slots)
        {
            slot.AddItemEvent?.AddListener(AddItem);
            slot.RemoveItemEvent?.AddListener(RemoveItem);
        }
    }

    //�̺�Ʈ����
    private void OnDisable()
    {
        foreach(Slot slot in slots)
        {
            slot.AddItemEvent?.RemoveListener(AddItem);
            slot.RemoveItemEvent?.RemoveListener(RemoveItem);
        }
    }

    public void AddItem(int slotnum, string itemName)
    {
        itemList[slotnum] = itemName;
    }

    public void RemoveItem(int slotnum)
    {
        itemList[slotnum] = null;
    }
}