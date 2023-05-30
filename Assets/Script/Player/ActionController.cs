using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;  // ������ ������ ������ �ִ� �Ÿ�

    private bool pickupActivated = false;  // ������ ���� ������ �� True 

    private Collider2D hitCollider;  // �浹�� Collider ����

    [SerializeField]
    private LayerMask layerMask;  // Ư�� ���̾ ���� ������Ʈ�� ���ؼ��� ������ �� �־�� �Ѵ�.

    // [SerializeField]
    // private Text actionText;  // �ൿ�� ���� �� �ؽ�Ʈ

    private Transform playerTransform;  // �÷��̾��� Transform ������Ʈ
    

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
                    Debug.Log(itemPickUp.item.itemName + "��(��) �Ծ����ϴ�.");
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
                    Debug.Log(itemPickUp.item.itemName + " ȹ�� �߽��ϴ�.");
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
        actionText.text = "�������� �������� E�� ��������.";
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
    */
}
