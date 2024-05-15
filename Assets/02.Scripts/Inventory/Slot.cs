using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    // ȹ���� ������
    public Item item;
    // ȹ���� ������ ����
    public int itemCount;
    // ������ �̹���
    public Image itemImage;

    [SerializeField]
    private Text text_Count;

    public Rect baseRect;
    private ItemEffectDatabase itemEffectDatabase;

    private void Start()
    {
        baseRect = transform.parent.parent.GetComponent<RectTransform>().rect;
        itemEffectDatabase = FindObjectOfType<ItemEffectDatabase>();
    }

    // ������ �̹��� ���� ����
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

    // IPointerClickHandler �������̽� ��� �� ���콺 Ŭ�� �̺�Ʈ �ޱ� ����
    // PointerEventData�� ���콺 Ȥ�� ��ġ �Է� �̺�Ʈ�� ���� ���� ���� (�̺�Ʈ�� ���� ��ư, Ŭ�� ��, ���콺 ��ġ ��)
    // ������Ʈ�� ���콺 Ŭ�� �̺�Ʈ �߻� �� ȣ��
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(item != null)
            {
                itemEffectDatabase.UseItem(item);

                // �Ҹ� ������ ���
                if(item.itemType == Item.ItemType.Used)
                {
                    SetSlotCount(-1);
                }
            }
        }
    }

    // �巡�װ� �Ǵ� ��󿡼� ȣ��
    // ���콺 �巡�װ� ���� ���� �� �߻��ϴ� �̺�Ʈ
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    // �巡�װ� �Ǵ� ��󿡼� ��� ȣ��
    // ���콺 �巡�� ���� �� ��� �߻��ϴ� �̺�Ʈ
    public void OnDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    // �� �ڽ��� �巡�� �ϴ� ���� ������ �� �巡�� ��� ������Ʈ���� ȣ��
    // ���콺 �巡�װ� ������ �� �߻��ϴ� �̺�Ʈ
    public void OnEndDrag(PointerEventData eventData)
    {
        if(DragSlot.instance.transform.localPosition.x < baseRect.xMin 
            || DragSlot.instance.transform.localPosition.x > baseRect.xMax
            || DragSlot.instance.transform.localPosition.y < baseRect.yMin
            || DragSlot.instance.transform.localPosition.y > baseRect.yMax)
        {
            GameObject _player = GameObject.Find("Pawn");
            Vector3 _pos = _player.transform.position;
            _pos.y -= 0.2f;

            Instantiate(DragSlot.instance.dragSlot.item.itemPrefab, _pos, Quaternion.identity);
            DragSlot.instance.dragSlot.ClearSlot();
        }

        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    // �� �ڽ����� ��� �� ���𰡰� ���� �� ȣ��
    // �ش� ���Կ� ������ ���콺 ��� ���� �� �߻��ϴ� �̺�Ʈ
    public void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }
    
    public void ChangeSlot()
    {
        // ���� ������ ���� ���� ����
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if(_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }
}
