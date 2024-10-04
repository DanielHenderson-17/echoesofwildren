using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    public GameObject grassPrefab;  // Drag your grass prefab here
    public int grassAmount = 100;   // Number of grass prefabs to spawn
    public Vector3 planeSize = new Vector3(10, 0, 10);  // Adjust to match your plane size
    public GameObject plane;  // Reference to the plane object

    void Start()
    {
        // Spawn grass patches
        for (int i = 0; i < grassAmount; i++)
        {
            SpawnGrass();
        }
    }

    void SpawnGrass()
    {
        // Randomize position within the bounds of the plane
        float xPos = Random.Range(-planeSize.x / 2, planeSize.x / 2);
        float zPos = Random.Range(-planeSize.z / 2, planeSize.z / 2);

        // Dynamically get the Y position from the plane's transform and add a slight offset
        float yPos = plane.transform.position.y + 0.01f;  // Add a small offset above the plane
        Vector3 spawnPosition = new Vector3(xPos, yPos, zPos);

        // Instantiate the grass prefab at the calculated position
        GameObject grassInstance = Instantiate(grassPrefab, spawnPosition + transform.position, Quaternion.identity);

        // Get the SpriteRenderer of the grass prefab and adjust the sorting order based on Y position
        SpriteRenderer grassRenderer = grassInstance.GetComponent<SpriteRenderer>();
        if (grassRenderer != null)
        {
            // Set sorting order based on Y position (lower Y is in front, higher Y is behind)
            grassRenderer.sortingOrder = Mathf.RoundToInt(yPos * -100);  // Adjust the multiplier as needed
        }
    }

}

