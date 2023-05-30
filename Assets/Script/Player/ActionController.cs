using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;  // 아이템 습득이 가능한 최대 거리

    private bool pickupActivated = false;  // 아이템 습득 가능할 시 True 

    private Collider2D hitCollider;  // 충돌한 Collider 저장

    [SerializeField]
    private LayerMask layerMask;  // 특정 레이어를 가진 오브젝트에 대해서만 습득할 수 있어야 한다.

    // [SerializeField]
    // private Text actionText;  // 행동을 보여 줄 텍스트

    private Transform playerTransform;  // 플레이어의 Transform 컴포넌트
    

    private void Start()
    {
        playerTransform = transform;
    }

    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (pickupActivated)
        {
            if (hitCollider != null)
            {
                ItemPickUp itemPickUp = hitCollider.GetComponent<ItemPickUp>();
                if (itemPickUp != null)
                {
                    Debug.Log(itemPickUp.item.itemName + "을(를) 먹었습니다.");
                    Destroy(itemPickUp.gameObject);
                    // ItemInfoDisappear();
                }
            }
        }
    }

    private void CheckItem()
    {
        Vector2 playerPosition = playerTransform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPosition, range, layerMask);

        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Item"))
                {
                    hitCollider = collider;
                    // ItemInfoAppear();
                    return;
                }
            }
        }

        hitCollider = null;
        // ItemInfoDisappear();
    }

    [SerializeField]
    private Inventory theInventory;
    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitCollider.transform != null)
            {
                ItemPickUp itemPickUp = hitCollider.transform.GetComponent<ItemPickUp>();
                if (itemPickUp != null)
                {
                    Debug.Log(itemPickUp.item.itemName + " 획득 했습니다.");
                    theInventory.AcquireItem(itemPickUp.item);
                    Destroy(hitCollider.transform.gameObject);
                    // ItemInfoDisappear();
                }
            }
        }
    }


    /*
    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = "아이템을 먹으려면 E를 누르세요.";
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
    */
}
