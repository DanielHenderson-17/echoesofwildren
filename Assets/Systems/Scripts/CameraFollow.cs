using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;  // You can set a predefined offset for top-down view in the Inspector
    public float smoothSpeed = 0.25f;
    public float driftFactor = 2f;

    private Vector3 currentVelocity;

    void Start()
    {
        if (target != null)
        {
            offset = this.transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        offset = this.transform.position - target.position;

        offset = new Vector3(0, Mathf.Abs(offset.y), -5); 
    }
}
