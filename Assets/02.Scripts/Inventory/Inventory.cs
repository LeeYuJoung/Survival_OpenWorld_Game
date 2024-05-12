using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // 인벤토리 활성화 여부
    public static bool isInventoryActivated = false;

    [SerializeField]
    private GameObject slotParent;
    private Slot[] slots;

    void Start()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }

    void Update()
    {
        TryOpenInventory();
    }

    // 인벤토리 활성화/비활성화
    public void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isInventoryActivated = !isInventoryActivated;

            if (isInventoryActivated)
            {
                slotParent.SetActive(true);
            }
            else
            {
                slotParent.SetActive(false);
            }
        }
    }

    // 아이템 저장
    public void AcquireItem(Item _item, int _count = 1)
    {
        // 아이템 타입이 장비가 아니라면
        if(Item.ItemType.Equipment != _item.itemType)
        {
            for(int i = 0; i < slots.Length; i++)
            {
                // 현재 슬롯이 비어있지 않다면
                if (slots[i].item != null)
                {
                    // 현재 슬롯에 있는 아이템과 저장할 아이템이 같다면
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
