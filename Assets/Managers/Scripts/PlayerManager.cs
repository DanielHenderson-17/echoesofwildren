using UnityEngine;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class PlayerPosition
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class Character
{
    public int id;
    public string name;
    public bool unlocked;
}

[System.Serializable]
public class CharacterData
{
    public List<Character> characters;
    public List<int> team;
    public int activeCharacterId;
    public PlayerPosition playerPosition;
}

public class PlayerManager : MonoBehaviour
{
    public PlayerController playerController;
    private CameraFollow cameraFollow;
    private string jsonFilePath;
    private CharacterData characterData;
    private GameObject activeCharacter;

    private void Awake()
    {
        jsonFilePath = Path.Combine(Application.persistentDataPath, "characters.json");

        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        cameraFollow = FindObjectOfType<CameraFollow>();

        LoadCharacterData();
    }

    private void Start()
    {
        if (activeCharacter == null && characterData != null)
        {
            activeCharacter = FindCharacterById(characterData.activeCharacterId);
        }

        DataManager dataManager = FindObjectOfType<DataManager>();
        if (dataManager != null)
        {
            dataManager.save.AddListener(SavePlayerPosition);
        }

        if (activeCharacter != null)
        {
            SetPlayerPosition();
        }
        else
        {
            Debug.LogWarning("No active character found during Start()");
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit triggered. Saving player position.");
        SavePlayerPosition();
    }

    public void SetActiveCharacter(GameObject character)
    {
        Debug.Log("SetActiveCharacter called. Saving player position before switching.");
        SavePlayerPosition();

        activeCharacter = character;

        MovementComponent movementComponent = character.GetComponent<MovementComponent>();
        AnimationComponent animationComponent = character.GetComponent<AnimationComponent>();

        if (playerController != null)
        {
            Vector3 previousFacing = playerController.GetMovementComponent().Facing;
            bool wasMoving = playerController.IsMoving();

            movementComponent.SetFacingDirection(previousFacing);

            if (wasMoving)
            {
                movementComponent.SetDirection(previousFacing);
            }
            else
            {
                movementComponent.SetDirection(Vector3.zero);
            }

            animationComponent.PlayAnimation(wasMoving ? "Walk" : "Idle", previousFacing);
        }

        playerController.SetActiveComponents(movementComponent, animationComponent);

        if (cameraFollow != null)
        {
            cameraFollow.SetTarget(character.transform);
        }
    }

    public void SavePlayerPosition()
    {
        Debug.Log("SavePlayerPosition invoked.");

        if (characterData == null || characterData.playerPosition == null)
        {
            Debug.LogWarning("CharacterData or PlayerPosition is null. Initializing PlayerPosition.");
            characterData.playerPosition = new PlayerPosition();
        }

        if (activeCharacter != null)
        {
            Vector3 currentPos = activeCharacter.transform.position;
            Debug.Log($"Current Active Character Position: {currentPos.x}, {currentPos.y}, {currentPos.z}");

            characterData.playerPosition.x = currentPos.x;
            characterData.playerPosition.y = currentPos.y;
            characterData.playerPosition.z = currentPos.z;
        }
        else
        {
            Debug.LogWarning("Active character is null when trying to save the player position.");
        }

        characterData.activeCharacterId = characterData.team[0];

        SaveCharacterData();
    }

    private void SetPlayerPosition()
    {
        if (characterData != null && characterData.playerPosition != null)
        {
            if (activeCharacter != null)
            {
                Debug.Log($"Setting active character position to {characterData.playerPosition.x}, {characterData.playerPosition.y}, {characterData.playerPosition.z}.");
                activeCharacter.transform.position = new Vector3(
                    characterData.playerPosition.x,
                    characterData.playerPosition.y,
                    characterData.playerPosition.z
                );
            }
            else
            {
                Debug.LogWarning("No active character to set the position.");
            }
        }
    }

    private GameObject FindCharacterById(int characterId)
    {
        Character character = characterData.characters.Find(c => c.id == characterId);

        if (character != null)
        {
            GameObject characterObject = GameObject.Find(character.name);

            if (characterObject != null)
            {
                Debug.Log($"Found character: {character.name} for id: {characterId}");
                return characterObject;
            }
            else
            {
                Debug.LogWarning($"Character GameObject with name {character.name} not found in the scene!");
                return null;
            }
        }
        else
        {
            Debug.LogWarning($"Character with ID {characterId} not found in characterData.");
            return null;
        }
    }

    private void SaveCharacterData()
    {
        Debug.Log("Saving character data to JSON.");
        string json = JsonUtility.ToJson(characterData, true);
        File.WriteAllText(jsonFilePath, json);
    }

    private void LoadCharacterData()
    {
        if (File.Exists(jsonFilePath))
        {
            Debug.Log($"Loading character data from {jsonFilePath}.");
            string savedJson = File.ReadAllText(jsonFilePath);
            characterData = JsonUtility.FromJson<CharacterData>(savedJson);
        }
        else
        {
            Debug.LogError("Character data not found.");
        }
    }

    public CharacterData GetCharacterData()
{
    return characterData; // Assuming characterData is populated in PlayerManager
}

}
