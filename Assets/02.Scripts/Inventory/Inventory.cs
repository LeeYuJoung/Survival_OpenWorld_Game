using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // �κ��丮 Ȱ��ȭ ����
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

    // �κ��丮 Ȱ��ȭ/��Ȱ��ȭ
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

    // ������ ����
    public void AcquireItem(Item _item, int _count = 1)
    {
        // ������ Ÿ���� ��� �ƴ϶��
        if(Item.ItemType.Equipment != _item.itemType)
        {
            for(int i = 0; i < slots.Length; i++)
            {
                // ���� ������ ������� �ʴٸ�
                if (slots[i].item != null)
                {
                    // ���� ���Կ� �ִ� �����۰� ������ �������� ���ٸ�
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
