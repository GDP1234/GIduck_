using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Equipment,   // 무기 장비
        Used,       // 소모품
        Ingredient, // 재료
        ETC,        //기타
    }
    public string itemName; //아이템 이름
    public ItemType itemType; // 아이템 유형
    public Sprite itemImage; // 아이템 이미지
    public GameObject itemPrefab; //아이템의 프리팹
}
