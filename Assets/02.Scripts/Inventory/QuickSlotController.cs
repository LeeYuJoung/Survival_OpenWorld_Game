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
            Execute(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Execute(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Execute(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Execute(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Execute(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Execute(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Execute(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Execute(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Execute(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Execute(9);
        }
    }

    // 활성화된 퀵슬롯에 있는 아이템을 실제로 사용
    public void Execute(int _num)
    {
        if (quickSlots[_num].item != null)
        {
            if (quickSlots[_num].item.itemType == Item.ItemType.Equipment)
            {
                Debug.Log("무기 사용");

            }
            else if (quickSlots[_num].item.itemType == Item.ItemType.Used)
            {
                Debug.Log("아이템 사용");
                itemEffectDatabase.UseItem(quickSlots[_num].item);

                // 소모성 아이템 사용
                if (quickSlots[_num].item.itemType == Item.ItemType.Used)
                {
                    quickSlots[_num].SetSlotCount(-1);
                }
            }
            else
            {

            }
        }
        else
        {
            Debug.Log("아이템 없음 ....");
        }
    }
}
