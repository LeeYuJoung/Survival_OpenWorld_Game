using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour�� ��� �޴� Ŭ������ �ƴ� �Ϲ� C# Ŭ�����ӿ��� �ұ��ϰ� ����Ƽ �ν����Ϳ� ��� �� �Ҵ� �����ϰ� ��
[System.Serializable]
public class ItemEffect
{
    public string itemName;  // ������ �̸�
    [Tooltip("HP, MP, SP, HUNGRY, THIRSTY, SATISFY �� ����")]
    public string[] part;    // ȿ��, ��� �κ��� ȸ���ϰų� Ȥ�� ���� ������
    public int[] num;        // ��ġ, ���� �ϳ��� ��ġ�� ��ġ
}

// ���� ���� �������� �� ���� �����ϱ� ���� ��ũ��Ʈ
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
                                Debug.Log("�߸��� Status ����....");
                                break;
                        }

                        Debug.Log(_item.itemName + "�� ���....");
                    }
                    return;
                }
            }
            Debug.Log("!!! ItemEffectDatabase�� ��ġ�ϴ� itemName�� �����ϴ� !!!");
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
