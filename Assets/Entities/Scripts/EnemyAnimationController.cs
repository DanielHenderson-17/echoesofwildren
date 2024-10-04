using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    // Define sprite arrays for different animations in each direction
    public Sprite[] walkLeftSprites;
    public Sprite[] walkRightSprites;
    public Sprite[] walkUpSprites;
    public Sprite[] walkDownSprites;

    public Sprite[] runLeftSprites;
    public Sprite[] runRightSprites;
    public Sprite[] runUpSprites;
    public Sprite[] runDownSprites;

    private float animationSpeed = 0.1f;
    private int currentFrame = 0;
    private float timer;

    private void Update()
    {
        // This handles looping through frames and switching them
        timer += Time.deltaTime;
        if (timer >= animationSpeed)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % walkLeftSprites.Length;  // You can adjust for other animations
        }
    }

    // Set the direction for walking animations
    public void WalkLeft()
    {
        spriteRenderer.sprite = walkLeftSprites[currentFrame];
    }

    public void WalkRight()
    {
        spriteRenderer.sprite = walkRightSprites[currentFrame];
    }

    public void WalkUp()
    {
        spriteRenderer.sprite = walkUpSprites[currentFrame];
    }

    public void WalkDown()
    {
        spriteRenderer.sprite = walkDownSprites[currentFrame];
    }

    // Set the direction for running animations
    public void RunLeft()
    {
        spriteRenderer.sprite = runLeftSprites[currentFrame];
    }

    public void RunRight()
    {
        spriteRenderer.sprite = runRightSprites[currentFrame];
    }

    public void RunUp()
    {
        spriteRenderer.sprite = runUpSprites[currentFrame];
    }

    public void RunDown()
    {
        spriteRenderer.sprite = runDownSprites[currentFrame];
    }

    public void Idle()
    {
        spriteRenderer.sprite = walkDownSprites[0];  // You can set your idle sprite here
    }
}
