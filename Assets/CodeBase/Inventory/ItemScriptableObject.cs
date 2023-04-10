using UnityEngine;

public enum ItemType
{
    Default, Turret, Weapon, Building, Ore, Clip, Module, EnergyBuilding, Tool
}
public class ItemScriptableObject : ScriptableObject
{
    public string ItemName;
    public int MaximumAmount;
    public string ItemDescription;
    public ItemType ItemType;
    public GameObject ItemPrefab;
}
