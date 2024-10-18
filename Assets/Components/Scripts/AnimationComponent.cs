using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    private Animator animator;
    private string lastDirection = "Down";
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
            string animationType = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? "Run" : "Walk";
            PlayAnimation(animationType, movementComponent.Facing);
        }
        else
        {
            PlayAnimation("Idle", Vector3.zero);
        }
    }

    public void PlayAnimation(string action, Vector3 direction)
    {
        if (direction.x > 0) lastDirection = "Right";
        else if (direction.x < 0) lastDirection = "Left";
        else if (direction.z > 0) lastDirection = "Up";
        else if (direction.z < 0) lastDirection = "Down";

        animator.Play($"{action}_{lastDirection}_Animation");
    }
}
