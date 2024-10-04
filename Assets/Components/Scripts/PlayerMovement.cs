using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 moveDirection;
    private Animator animator;
    private string jsonFilePath;
    private string lastDirection;
    // Store the last direction the character faced

    // Reference to the DataManager to save and load player position
    private DataManager dataManager;
    public CharacterData characterData;
    [System.Serializable]
    public class Character
    {
        public int id;
        public string name;
        public string element;
        public string animalia;
        public string weapon;
        public int starLevel;
        public BaseStats baseStats;
    }

    [System.Serializable]
    public class BaseStats
    {
        public int baseHp;
        public int baseAD;
        public int baseAP;
        public int baseSpecial;
    }

    [System.Serializable]
    public class CharacterData
    {
        public List<Character> characters;
        public List<int> team;
        public int activeCharacterId;
        public PlayerPosition playerPosition;
    }
    [System.Serializable]
    public class PlayerPosition
    {
        public float x;
        public float y;
        public float z;
    }

    void Start()
    {

        jsonFilePath = Path.Combine(Application.persistentDataPath, "characters.json");
        LoadCharacterData();
        SetPlayerPosition();
        // Get the Animator component
        animator = GetComponent<Animator>();

        // Find and reference the DataManager in the scene
        dataManager = FindObjectOfType<DataManager>();

        // Load and set the player's position when the game starts
        if (dataManager != null)
        {
            dataManager.save.AddListener(savePlayer);

            // Debug.Log($"Player starting at saved position: {savedPosition}");
        }
    }

    void Update()
    {
        // Get movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Normalize movement to avoid diagonal speed boost
        moveDirection = new Vector3(moveX, 0f, moveY).normalized;

        // Move the player
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Play walking animations based on input
        if (moveX > 0)
        {
            animator.Play("Walk_Right_Animation");
            lastDirection = "Right"; // Store the last direction
        }
        else if (moveX < 0)
        {
            animator.Play("Walk_Left_Animation");
            lastDirection = "Left";
        }
        else if (moveY > 0)
        {
            animator.Play("Walk_Up_Animation");
            lastDirection = "Up";
        }
        else if (moveY < 0)
        {
            animator.Play("Walk_Down_Animation");
            lastDirection = "Down";
        }
        else
        {
            // Play idle animation based on last direction
            if (lastDirection == "Right")
            {
                animator.Play("Idle_Right_Animation");
            }
            else if (lastDirection == "Left")
            {
                animator.Play("Idle_Left_Animation");
            }
            else if (lastDirection == "Up")
            {
                animator.Play("Idle_Up_Animation");
            }
            else if (lastDirection == "Down")
            {
                animator.Play("Idle_Down_Animation");
            }
        }


    }
    private void SetPlayerPosition()
    {
        if (characterData.playerPosition != null)
        {
            // Assuming your player GameObject is tagged as "Player"
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                Vector3 position = new Vector3(characterData.playerPosition.x, characterData.playerPosition.y, characterData.playerPosition.z);
                player.transform.position = position;

                // Debug.Log($"Player position set to: {position}");
            }
            else
            {
                Debug.LogError("Player GameObject not found!");
            }
        }
    }


    void savePlayer()
    {
        Debug.Log("Invoked");
        // Save the player's position when moving
        if (moveDirection != Vector3.zero && dataManager != null)
        {
            SavePlayerPosition(transform.position);
        }
    }
    // Save player position to JSON when changing scenes or saving manually
    public void SavePlayerPosition(Vector3 position)
    {
        if (characterData.playerPosition == null)
        {
            characterData.playerPosition = new PlayerPosition();
        }

        // Update the playerPosition with current position
        characterData.playerPosition.x = position.x;
        characterData.playerPosition.y = position.y;
        characterData.playerPosition.z = position.z;

        // Save the updated JSON file
        SaveCharacterData();
    }

    // Save character data (including player position) back to the JSON file
    private void SaveCharacterData()
    {
        string json = JsonUtility.ToJson(characterData, true);
        File.WriteAllText(jsonFilePath, json);

        // Debug.Log($"Character data (including position) saved to {jsonFilePath}");
    }
    private void LoadCharacterData()
    {
        if (File.Exists(jsonFilePath))
        {

            Debug.Log($"Loading character data from persistent file: {jsonFilePath}");
            string savedJson = File.ReadAllText(jsonFilePath);
            characterData = JsonUtility.FromJson<CharacterData>(savedJson);
        }
        else
        {

            Debug.Log("Loading default character data from Resources.");
            TextAsset jsonFile = Resources.Load<TextAsset>("characters");
            if (jsonFile != null)
            {
                characterData = JsonUtility.FromJson<CharacterData>(jsonFile.text);
            }
            else
            {
                Debug.LogError("JSON file not found in Resources folder!");
            }
        }
    }
}
