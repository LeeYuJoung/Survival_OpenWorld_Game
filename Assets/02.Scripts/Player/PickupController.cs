using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private PlayerMovement PlayerMovement;
    private Inventory inventory;

    private bool isPickupActivated = false;
    private RaycastHit2D hit;

    private void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {
        CheckItem();
        TryAction();
    }

    public void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CheckItem();
            PickUp();
        }
    }

    public void CheckItem()
    {
        hit = Physics2D.Raycast(transform.position, PlayerMovement.playerDirection, 1.0f, 1 << LayerMask.NameToLayer("Item"));

        if (hit)
        {
            if (hit.transform.CompareTag("Item"))
            {
                ItemInfoAppear();
            }
        }
        else
        {
            ItemInfoDisappear();
        }
    }

    // �������� �ֿ� �� �ִ� ����
    public void ItemInfoAppear()
    {
        isPickupActivated = true;
    }

    // �������� �ֿ� �� ���� ����
    public void ItemInfoDisappear()
    {
        isPickupActivated = false;
    }

    public void PickUp()
    {
        if (isPickupActivated)
        {
            if(hit.transform != null)
            {
                // �κ��丮�� ����
                Debug.Log(hit.transform.GetComponent<ItemObject>().item.itemName + "ȹ���߽��ϴ�.....");
                inventory.AcquireItem(hit.transform.GetComponent<ItemObject>().item);
                Destroy(hit.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }
}
