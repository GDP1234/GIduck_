using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject go_InventoryBase; // Inventory_Base 이미지
    [SerializeField]
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting 

    private Slot[] slots;

    private void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
    /*
    //아이템을 담을 리스트 
    public List<Item> items;

    //Slot의 부모가 되는 KeyBag를 담을곳 
    [SerializeField]
    private Transform slotParent;
    //KeyBag의 하위에 등록된 Slot을 담을곳 
    [SerializeField]
    private Slot[] slots;

    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }
    private void Awake()
    {
        FreshSlot();
    }
    public void FreshSlot()
    {
        int i = 0;
        for(; i<items.Count && i<slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for(; i<slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    public void AddItem(Item _item)
    {
        if(items.Count < slots.Length)
        {
            items.Add(_item);
            FreshSlot();
        }
        else
        {
            print("슬롯이 가득 차 있습니다.");
        }
    }
    */
}
