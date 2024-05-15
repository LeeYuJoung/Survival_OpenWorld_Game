using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    // 획득한 아이템
    public Item item;
    // 획득한 아이템 개수
    public int itemCount;
    // 아이템 이미지
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

    // 아이템 이미지 투명도 조절
    public void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // 인벤토리에 새로운 아이템 슬롯 추가
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

    // 해당 슬롯의 아이템 갯수 업데이트
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // 해당 슬롯 하나 삭제
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;    
        SetColor(0);

        text_Count.text = "0";
        text_Count.gameObject.SetActive(false);
    }

    // IPointerClickHandler 인터페이스 상속 시 마우스 클릭 이벤트 받기 가능
    // PointerEventData는 마우스 혹은 터치 입력 이벤트에 관한 정보 저장 (이벤트가 들어온 버튼, 클릭 수, 마우스 위치 등)
    // 오브젝트에 마우스 클릭 이벤트 발생 시 호출
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(item != null)
            {
                itemEffectDatabase.UseItem(item);

                // 소모성 아이템 사용
                if(item.itemType == Item.ItemType.Used)
                {
                    SetSlotCount(-1);
                }
            }
        }
    }

    // 드래그가 되는 대상에서 호출
    // 마우스 드래그가 시작 됐을 때 발생하는 이벤트
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    // 드래그가 되는 대상에서 계속 호출
    // 마우스 드래그 중일 때 계속 발생하는 이벤트
    public void OnDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    // 나 자신을 드래그 하는 것을 끝냈을 때 드래그 대상 오브젝트에서 호출
    // 마우스 드래그가 끝났을 때 발생하는 이벤트
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

    // 내 자신한테 드롭 된 무언가가 있을 때 호출
    // 해당 슬롯에 무언가의 마우스 드롭 됐을 때 발생하는 이벤트
    public void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
        }
    }
    
    public void ChangeSlot()
    {
        // 현재 슬롯이 가진 정보 저장
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
