using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float moveSpeed = 2f; // Movement speed of the enemy
    public float aggroRange = 5f; // Range to start chasing player

    // Reference to the animation manager script we wrote before
    private CommonSoldier commonSoldier; 
    private Vector2 lastMoveDirection; // To track the last direction

    private void Start()
    {
        // Get reference to the CommonSoldier script controlling the animations
        commonSoldier = GetComponent<CommonSoldier>(); 
    }

    private void Update()
    {
        // Calculate distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // If player is within aggro range, chase them
        if (distanceToPlayer < aggroRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // Play the appropriate run animation based on movement direction
            if (direction.x < 0)
                commonSoldier.PlayAnimation("Run", "Left");
            else if (direction.x > 0)
                commonSoldier.PlayAnimation("Run", "Right");
            else if (direction.y > 0)
                commonSoldier.PlayAnimation("Run", "Top");
            else if (direction.y < 0)
                commonSoldier.PlayAnimation("Run", "Bot");

            // Save last move direction for idle animations later
            lastMoveDirection = direction;
        }
        else
        {
            // Player is out of range, play idle animation based on the last movement direction
            PlayIdleAnimation();
        }
    }

    private void PlayIdleAnimation()
    {
        // Play the idle animation based on the last movement direction
        if (lastMoveDirection.x < 0)
            commonSoldier.PlayAnimation("Idle", "Left");
        else if (lastMoveDirection.x > 0)
            commonSoldier.PlayAnimation("Idle", "Right");
        else if (lastMoveDirection.y > 0)
            commonSoldier.PlayAnimation("Idle", "Top");
        else
            commonSoldier.PlayAnimation("Idle", "Bot");
    }
}
