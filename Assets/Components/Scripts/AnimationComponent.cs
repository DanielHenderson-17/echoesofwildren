using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    private Animator animator;
    private string lastDirection = "Down";  // Set a default direction
    private MovementComponent movementComponent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movementComponent = GetComponent<MovementComponent>();
    }

    private void Update()
    {
        if (movementComponent.IsMoving)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                PlayRunningAnimation(movementComponent.Facing);
            }
            else
            {
                PlayWalkingAnimation(movementComponent.Facing);
            }
        }
        else
        {
            PlayIdleAnimation();
        }
    }

    private void PlayWalkingAnimation(Vector3 direction)
    {
        if (direction.x > 0)
        {
            animator.Play("Walk_Right_Animation");
            lastDirection = "Right";
        }
        else if (direction.x < 0)
        {
            animator.Play("Walk_Left_Animation");
            lastDirection = "Left";
        }
        else if (direction.z > 0)
        {
            animator.Play("Walk_Up_Animation");
            lastDirection = "Up";
        }
        else if (direction.z < 0)
        {
            animator.Play("Walk_Down_Animation");
            lastDirection = "Down";
        }
    }

    private void PlayRunningAnimation(Vector3 direction)
    {
        if (direction.x > 0)
        {
            animator.Play("Run_Right_Animation");
            lastDirection = "Right";
        }
        else if (direction.x < 0)
        {
            animator.Play("Run_Left_Animation");
            lastDirection = "Left";
        }
        else if (direction.z > 0)
        {
            animator.Play("Run_Up_Animation");
            lastDirection = "Up";
        }
        else if (direction.z < 0)
        {
            animator.Play("Run_Down_Animation");
            lastDirection = "Down";
        }
    }

    private void PlayIdleAnimation()
    {
        switch (lastDirection)
        {
            case "Right":
                animator.Play("Idle_Right_Animation");
                break;
            case "Left":
                animator.Play("Idle_Left_Animation");
                break;
            case "Up":
                animator.Play("Idle_Up_Animation");
                break;
            case "Down":
                animator.Play("Idle_Down_Animation");
                break;
        }
    }
}
