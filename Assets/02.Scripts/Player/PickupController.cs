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

    // 아이템을 주울 수 있는 상태
    public void ItemInfoAppear()
    {
        isPickupActivated = true;
    }

    // 아이템을 주울 수 없는 상태
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
                // 인벤토리에 저장
                Debug.Log(hit.transform.GetComponent<ItemObject>().item.itemName + "획득했습니다.....");
                Destroy(hit.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }
}
