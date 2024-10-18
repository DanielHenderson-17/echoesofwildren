using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.25f;
    public float driftFactor = 2f;

    private Vector3 currentVelocity;
    private Vector3 oldTargetPosition;

    void Start()
    {
        oldTargetPosition = target.transform.position;
        offset = this.transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 targetDirection = (target.position - oldTargetPosition).normalized;
        Vector3 desiredPosition;

        if (Vector3.Distance(target.position, oldTargetPosition) > 0.01f)
        {
            desiredPosition = target.position + offset + targetDirection * driftFactor;
        }
        else
        {
            desiredPosition = target.position + offset;
        }

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);
        transform.position = smoothedPosition;
        oldTargetPosition = target.position;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        oldTargetPosition = target.position;
    }
}
