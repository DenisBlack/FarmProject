using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Order", menuName = "Orders/Order Data")]
public class OrderData : ScriptableObject
{
    [Header("Requirement")]
    [SerializeField] private ItemData itemData;
    [SerializeField] private int _quantity;
    [Header("Reward")]
    [SerializeField] private int _rewardCoins;


    public ItemData ItemData => itemData;
    public int Quantity => _quantity;
    public int RewardCoins => _rewardCoins;
}
