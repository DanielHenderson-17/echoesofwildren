using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
public class PlayerController : MonoBehaviour
{
    private MovementComponent movementComponent;
    private AnimationComponent animationComponent;
    public float runSpeedModifier = 2f;

    private void Awake()
    {
        movementComponent = GetComponent<MovementComponent>();
    }

    private void Update()
    {
        if (movementComponent == null) return;

        Vector3 inputDirection = GetInputDirection();

        if (inputDirection != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                movementComponent.SetSpeedModifier(runSpeedModifier);
            }
            else
            {
                movementComponent.SetSpeedModifier(1f);
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
    public void SetActiveComponents(MovementComponent newMovementComponent, AnimationComponent newAnimationComponent)
    {
        movementComponent = newMovementComponent;
        animationComponent = newAnimationComponent;
    }

    public MovementComponent GetMovementComponent()
    {
        return movementComponent;
    }

    public bool IsMoving()
    {
        return movementComponent.IsMoving;
    }
}


