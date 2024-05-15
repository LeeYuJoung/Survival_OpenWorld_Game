using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField]
    private GameObject _base;

    [SerializeField]
    private Text itemNameText;
    [SerializeField]
    private Text itemDescText;
    [SerializeField]
    private Text itemToUsedText;

    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        _base.SetActive(true);
        _pos += new Vector3(_base.GetComponent<RectTransform>().rect.width * 0.5f,
            _base.GetComponent<RectTransform>().rect.height * 0.5f, 0.0f);

        _base.transform.position = _pos;

        itemNameText.text = _item.itemName;
        itemDescText.text = _item.itemDesc;

        if (_item.itemType == Item.ItemType.Equipment)
        {
            itemToUsedText.text = "우 클릭 - 장착";
        }
        else if (_item.itemType == Item.ItemType.Used)
        {
            itemToUsedText.text = "우 클릭 - 사용";
        }
        else
        {
            itemToUsedText.text = "";
        }
    }

    public void HideToolTip()
    {
        _base.SetActive(false);
    }
}
