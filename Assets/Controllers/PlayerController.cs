using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
public class PlayerController : MonoBehaviour
{
    private MovementComponent movementComponent;
    public float runSpeedModifier = 2f; // Additional speed when running

    private void Awake()
    {
        movementComponent = GetComponent<MovementComponent>();
    }

    private void Update()
    {
        Vector3 inputDirection = GetInputDirection();

        if (inputDirection != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                movementComponent.SetSpeedModifier(runSpeedModifier); // Apply run speed modifier
            }
            else
            {
                movementComponent.SetSpeedModifier(1f); // Reset to normal speed
            }

            movementComponent.SetDirection(inputDirection);
        }
        else
        {
            movementComponent.SetDirection(Vector3.zero);
        }
    }

    private Vector3 GetInputDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        return new Vector3(horizontal, 0f, vertical).normalized;
    }
}
