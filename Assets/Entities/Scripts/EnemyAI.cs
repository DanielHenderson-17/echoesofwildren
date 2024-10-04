using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // For scene management
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float aggroRange = 10f; // Distance at which the enemy will start chasing
    public float maxChaseDistance = 30f; // Maximum distance from the starting point the enemy can chase the player
    public Animator animator; // Reference to the Animator
    public DataManager dataManager; // Reference to the DataManager (Make sure it's set in the inspector or via script)

    private NavMeshAgent agent;
    private Vector3 originalPosition; // Store the original position
    private bool isReturningToStart = false; // Track whether the enemy is returning to its start position
    private bool isChasingPlayer = false; // Track if the enemy is currently chasing the player
    private bool isWaitingToReturn = false; // To check if the enemy is waiting before returning

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // Prevent the agent from rotating the GameObject
        agent.speed = 3f; // Set the enemy speed to 3f
        agent.stoppingDistance = 0.5f; // Ensure the agent stops properly at the target

        // Store the enemy's starting position
        originalPosition = transform.position;

        // Find the DataManager in the scene if not set in the Inspector
        if (dataManager == null)
        {
            dataManager = FindObjectOfType<DataManager>();
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        float distanceFromStart = Vector3.Distance(transform.position, originalPosition);

        // If the player is within aggro range and the enemy hasn't exceeded the max chase distance
        if (distanceToPlayer <= aggroRange && distanceFromStart <= maxChaseDistance)
        {
            isChasingPlayer = true;
            isReturningToStart = false;
            isWaitingToReturn = false;
            agent.SetDestination(player.position);

            HandleAnimations();
        }
        else
        {
            // If the enemy is too far from the starting point or the player has escaped
            if (isChasingPlayer && (distanceFromStart > maxChaseDistance || distanceToPlayer > aggroRange))
            {
                isChasingPlayer = false;
                if (!isWaitingToReturn) // Start waiting before returning to original position
                {
                    StartCoroutine(WaitBeforeReturning()); // Start 2-second wait
                }
            }

            if (isReturningToStart)
            {
                // Command enemy to return to original position
                agent.SetDestination(originalPosition);

                // Apply the walking animation while returning
                HandleAnimations();

                // Check if the enemy has reached its original position
                if (Vector3.Distance(transform.position, originalPosition) < agent.stoppingDistance)
                {
                    isReturningToStart = false;
                    agent.ResetPath(); // Stop moving
                    HandleIdleState(); // Set idle animations
                }
            }
            else if (!isWaitingToReturn)
            {
                HandleIdleState(); // Idle state during waiting period
            }
        }
    }

    // Coroutine to wait before returning to the original position
    IEnumerator WaitBeforeReturning()
    {
        isWaitingToReturn = true;
        HandleIdleState(); // Play idle animation
        yield return new WaitForSeconds(2); // Wait for 2 seconds
        isWaitingToReturn = false;
        isReturningToStart = true; // Start returning after the wait
    }

    // Handle walking animation when moving towards the player or returning to original position
    private void HandleAnimations()
    {
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        float horizontalMovement = localVelocity.x;
        float verticalMovement = localVelocity.z;

        animator.SetFloat("Horizontal", horizontalMovement);
        animator.SetFloat("Vertical", verticalMovement);

        if (Mathf.Abs(horizontalMovement) > 0.1f || Mathf.Abs(verticalMovement) > 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    // Handle idle state animations when not chasing
    private void HandleIdleState()
    {
        agent.ResetPath(); // Ensure the agent stops moving
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
        animator.SetBool("isWalking", false);
    }

    // Collision detection: Transition to the fight scene when the player and enemy collide
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player is tagged as "Player"
        {
            // Save the player's current position before loading the scene
            // if (dataManager != null)
            // {
            //     dataManager.SavePlayerPosition(player.position);
            //     Debug.Log($"Player position saved before scene transition: {player.position}");
            // }

            // Load the fight scene
            SceneManager.LoadScene("FightScene");
        }
    }
}
