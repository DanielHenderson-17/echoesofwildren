using UnityEngine;

public class CommonSoldier : MonoBehaviour
{
    // Define your animations directly
    public AnimationClip SkeletonWarrior_Run_Left;
    public AnimationClip SkeletonWarrior_Run_Right;
    public AnimationClip SkeletonWarrior_Run_Top;
    public AnimationClip SkeletonWarrior_Run_Bot;

    public AnimationClip SkeletonWarrior_Walk_Left;
    public AnimationClip SkeletonWarrior_Walk_Right;
    public AnimationClip SkeletonWarrior_Walk_Top;
    public AnimationClip SkeletonWarrior_Walk_Bot;

    public AnimationClip SkeletonWarrior_Idle_Left;
    public AnimationClip SkeletonWarrior_Idle_Right;
    public AnimationClip SkeletonWarrior_Idle_Top;
    public AnimationClip SkeletonWarrior_Idle_Bot;

    // Current animation
    private Animation currentAnimation;

    private void Start()
    {
        currentAnimation = gameObject.AddComponent<Animation>();

        // Add animation clips to the Animation component programmatically
        currentAnimation.AddClip(SkeletonWarrior_Run_Left, "Run_Left");
        currentAnimation.AddClip(SkeletonWarrior_Run_Right, "Run_Right");
        currentAnimation.AddClip(SkeletonWarrior_Run_Top, "Run_Top");
        currentAnimation.AddClip(SkeletonWarrior_Run_Bot, "Run_Bot");

        currentAnimation.AddClip(SkeletonWarrior_Walk_Left, "Walk_Left");
        currentAnimation.AddClip(SkeletonWarrior_Walk_Right, "Walk_Right");
        currentAnimation.AddClip(SkeletonWarrior_Walk_Top, "Walk_Top");
        currentAnimation.AddClip(SkeletonWarrior_Walk_Bot, "Walk_Bot");

        currentAnimation.AddClip(SkeletonWarrior_Idle_Left, "Idle_Left");
        currentAnimation.AddClip(SkeletonWarrior_Idle_Right, "Idle_Right");
        currentAnimation.AddClip(SkeletonWarrior_Idle_Top, "Idle_Top");
        currentAnimation.AddClip(SkeletonWarrior_Idle_Bot, "Idle_Bot");
    }

    // Method to play animation based on action and direction
    public void PlayAnimation(string action, string direction)
    {
        string clipName = $"{action}_{direction}";

        if (currentAnimation[clipName] != null)
        {
            currentAnimation.Play(clipName);
        }
        else
        {
            Debug.LogWarning($"No animation clip found for {clipName}");
        }
    }
}
