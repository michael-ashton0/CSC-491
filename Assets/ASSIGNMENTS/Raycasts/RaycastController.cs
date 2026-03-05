using System;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    private Camera rayCamera;
    public float maxDistance = 10f;
    public LayerMask pickupLayer;

    public bool HasHit { get; private set; }
    public RaycastHit HitInfo { get; private set; }
    public Vector3 HitPoint { get; private set; }

    void Awake()
    {
        if (rayCamera == null) rayCamera = Camera.main;
    }

    public void TickRaycast()
    {
        Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);

        HasHit = Physics.Raycast(
            ray,
            out RaycastHit hit,
            maxDistance,
            pickupLayer,
            QueryTriggerInteraction.Ignore
        );

        if (HasHit)
        {
            HitInfo = hit;
            HitPoint = hit.point;
        }
        else
        {
            HitInfo = default;
            HitPoint = ray.GetPoint(maxDistance);
        }

        Debug.DrawRay(
            ray.origin,
            ray.direction * (HasHit ? HitInfo.distance : maxDistance),
            HasHit ? Color.green : Color.red
        );
    }

    public bool IsGameObjectOnPickupLayer(GameObject go)
    {
        return (pickupLayer.value & (1 << go.layer)) != 0;
    }
}