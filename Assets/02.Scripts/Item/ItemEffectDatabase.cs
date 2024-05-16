using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour를 상속 받는 클래스가 아닌 일반 C# 클래스임에도 불구하고 유니티 인스펙터에 띄워 값 할당 가능하게 함
[System.Serializable]
public class ItemEffect
{
    public string itemName;  // 아이템 이름
    [Tooltip("HP, MP, SP, HUNGRY, THIRSTY, SATISFY 만 가능")]
    public string[] part;    // 효과, 어느 부분을 회복하거나 혹은 깎을 것인지
    public int[] num;        // 수치, 포션 하나당 미치는 수치
}

// 여러 포션 아이템을 한 번에 관리하기 위한 스크립트
public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;

    private const string HP = "HP", MP = "MP", SP = "SP", HUNGRY = "HUNGRY", THIRSTY = "THIRSTY", SATISFY = "SATISFY";

    [SerializeField]
    private StatusController statusController;
    [SerializeField]
    private SlotToolTip SlotToolTip;
    [SerializeField]
    private QuickSlotController quickSlotController;

    private void Start()
    {
        statusController = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusController>();
    }

    public void UseItem(Item _item)
    {
        if(_item.itemType == Item.ItemType.Equipment)
        {

        }
        if(_item.itemType == Item.ItemType.Used)
        {
            for(int i = 0; i < itemEffects.Length; i++)
            {
                if (itemEffects[i].itemName == _item.itemName)
                {
                    for(int j = 0; j < itemEffects[i].part.Length; j++)
                    {
                        switch (itemEffects[i].part[j])
                        {
                            case HP:
                                statusController.IncreaseHP(itemEffects[i].num[j]);
                                break;
                            case MP:
                                statusController.IncreaseMP(itemEffects[i].num[j]);
                                break;
                            case SP:
                                statusController.IncreaseSP(itemEffects[i].num[j]);
                                break;
                            case HUNGRY:
                                statusController.IncreaseHungry(itemEffects[i].num[j]);
                                break;
                            case THIRSTY:
                                statusController.IncreaseThirsty(itemEffects[i].num[j]);
                                break;
                            case SATISFY:
                                break;
                            default:
                                Debug.Log("잘못된 Status 부위....");
                                break;
                        }

                        Debug.Log(_item.itemName + "을 사용....");
                    }
                    return;
                }
            }
            Debug.Log("!!! ItemEffectDatabase에 일치하는 itemName이 없습니다 !!!");
        }
    }

    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        SlotToolTip.ShowToolTip(_item, _pos);
    }

    public void HideToolTip()
    {
        SlotToolTip.HideToolTip();
    }
}
