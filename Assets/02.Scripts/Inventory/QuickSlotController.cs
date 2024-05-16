using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotController : MonoBehaviour
{
    [SerializeField]
    private Slot[] quickSlots;
    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private ItemEffectDatabase itemEffectDatabase;

    private int selectedSlot;

    void Start()
    {
        quickSlots = parentTransform.GetComponentsInChildren<Slot>();
        selectedSlot = 0;
    }

    void Update()
    {
        TryInputNumber();
    }

    public void TryInputNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeSlot(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeSlot(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeSlot(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangeSlot(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ChangeSlot(9);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ChangeSlot(0);
        }
    }

    public void ChangeSlot(int _num)
    {
        SelectedSlot(_num);
        Execute();
    }

    public void SelectedSlot(int _num)
    {
        selectedSlot = _num;
    }

    // 활성화된 퀵슬롯에 있는 아이템을 실제로 사용
    public void Execute()
    {
        if (quickSlots[selectedSlot].item != null)
        {
            if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Equipment)
            {

            }
            else if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Used)
            {

            }
            else
            {

            }
        }
        else
        {

        }
    }

    public void IsActivatedQuickSlot(int _num)
    {
        if(selectedSlot == _num)
        {
            Execute();
            return;
        }
        if(DragSlot.instance != null)
        {
            if(DragSlot.instance.dragSlot != null)
            {
                if(DragSlot.instance.dragSlot.GetQuickSlotNumber() == selectedSlot)
                {
                    Execute();
                    return;
                }
            }
        }
    }
}
