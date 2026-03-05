using UnityEngine;

public class PickupInput : MonoBehaviour
{

    private RaycastController raycastController;
    private SimplePickupSystem pickupSystem;
    
    private float holdHeight = 1.0f;
    private bool isHolding = false;

    void Awake()
    {
        if (raycastController == null) raycastController = FindFirstObjectByType<RaycastController>();
        if (pickupSystem == null) pickupSystem = FindFirstObjectByType<SimplePickupSystem>();
    }

    void Update()
    {
        raycastController.TickRaycast();
        
        if (isHolding)
        {
            Vector3 holdPos = raycastController.HitPoint + Vector3.up * holdHeight;
            pickupSystem.UpdatePickupPosition(holdPos);
        }

        if (Input.GetKeyDown(KeyCode.E))
            TryPickup();

        if (Input.GetKeyDown(KeyCode.Q))
            Drop();
    }

    private void TryPickup()
    {
        if (isHolding) return;
        if (!raycastController.HasHit) return;

        GameObject hitObject = raycastController.HitInfo.collider.gameObject;

        pickupSystem.Pickup(hitObject);
        isHolding = true;
    }

    private void Drop()
    {
        if (!isHolding) return;

        pickupSystem.Drop();
        isHolding = false;
    }
}