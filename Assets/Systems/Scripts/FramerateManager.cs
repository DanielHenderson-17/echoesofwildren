using UnityEngine;

public class FramerateManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Set the target frame rate to 30 FPS
        Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
