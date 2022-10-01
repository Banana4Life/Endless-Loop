using UnityEngine;

[CreateAssetMenu(fileName = "PickupData", menuName = "ScriptableObjects/PickupData")]
public class PickupData : ScriptableObject
{
    public string itemName;
    public GameObject model;
}