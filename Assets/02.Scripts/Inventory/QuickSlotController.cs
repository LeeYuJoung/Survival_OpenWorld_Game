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

    // Ȱ��ȭ�� �����Կ� �ִ� �������� ������ ���
    public void Execute(int _num)
    {
        if (quickSlots[_num].item != null)
        {
            if (quickSlots[_num].item.itemType == Item.ItemType.Equipment)
            {
                Debug.Log("���� ���");

            }
            else if (quickSlots[_num].item.itemType == Item.ItemType.Used)
            {
                Debug.Log("������ ���");
                itemEffectDatabase.UseItem(quickSlots[_num].item);

                // �Ҹ� ������ ���
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
            Debug.Log("������ ���� ....");
        }
    }
}
