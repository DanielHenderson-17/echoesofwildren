using UnityEngine;
using System;

public class MovementComponent : MonoBehaviour
{
    public float baseSpeed;
    private float speedModifier = 1f;
    private Transform body;

    private Vector3? direction = null;
    private Vector3? goal = null;

    public bool IsMoving { get; private set; }
    public Vector3 Facing { get; private set; }

    public event Action GoalReached;

    private void Awake()
    {
        body = this.transform;
    }

    private void Update()
    {
        if (direction.HasValue && direction.Value != Vector3.zero)
        {
            MoveInDirection();
        }
        else if (goal.HasValue)
        {
            MoveTowardsGoal();
        }
        else
        {
            IsMoving = false;
        }
    }

    private void MoveInDirection()
    {
        Vector3 move = baseSpeed * speedModifier * Time.deltaTime * direction.Value;
        body.position += move;
        Facing = direction.Value;
        IsMoving = true;
    }

    private void MoveTowardsGoal()
    {
        if (Vector3.Distance(body.position, goal.Value) < 0.1f)
        {
            body.position = goal.Value;
            GoalReached?.Invoke();
            IsMoving = false;
        }
        else
        {
            Vector3 move = Vector3.MoveTowards(body.position, goal.Value, baseSpeed * speedModifier * Time.deltaTime);
            body.position = move;
            Facing = (goal.Value - body.position).normalized;
            IsMoving = true;
        }
    }

    public void SetGoal(Vector3 newGoal)
    {
        direction = null;
        goal = newGoal;
    }

    public void SetDirection(Vector3 newDirection)
    {
        goal = null;
        direction = newDirection.normalized;
        IsMoving = direction != Vector3.zero;
    }

    public void SetSpeedModifier(float modifier)
    {
        speedModifier = modifier;
    }

    public void SetFacingDirection(Vector3 newFacing)
{
    Facing = newFacing;
}
}
