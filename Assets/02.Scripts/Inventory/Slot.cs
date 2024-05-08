using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    // ȹ���� ������
    public Item item;
    // ȹ���� ������ ����
    public int itemCount;
    // ������ �̹���
    public Image itemImage;

    [SerializeField]
    private Text text_Count;

    // ������ �̹��� ������ ����
    public void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // �κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if(item.itemType != Item.ItemType.Equipment)
        {
            text_Count.gameObject.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            text_Count.gameObject.SetActive(false);
        }

        SetColor(1);
    }

    // �ش� ������ ������ ���� ������Ʈ
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // �ش� ���� �ϳ� ����
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;    
        SetColor(0);

        text_Count.text = "0";
        text_Count.gameObject.SetActive(false);
    }
}