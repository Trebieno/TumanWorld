using UnityEngine;

[CreateAssetMenu(fileName = "Turret Item", menuName = "Inventory/Items/New Turret Item")]
public class TurretItem : ItemScriptableObject
{
    public float damageAmount;
    public float healthAmount;

    private void Start()
    {
        ItemType = ItemType.Turret;
    }
}
