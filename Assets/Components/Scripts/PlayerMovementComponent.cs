using Unity.Entities;

public struct PlayerMovementComponent : IComponentData
{
    public float moveSpeed;
    public Unity.Mathematics.float3 moveDirection;
}
