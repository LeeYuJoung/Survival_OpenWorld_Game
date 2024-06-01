using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC
    }

    public string itemName;
    [TextArea]  // ���� �� �ۼ� ����
    public string itemDesc;
    public ItemType itemType;
    public Sprite itemImage;
    public Sprite interactionImage;
    public GameObject itemPrefab;
    public int damage;
}
