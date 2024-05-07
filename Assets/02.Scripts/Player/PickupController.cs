using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private PlayerMovement PlayerMovement;

    private bool isPickupActivated = false;
    private RaycastHit2D hit;

    private void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
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
                Destroy(hit.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }
}
