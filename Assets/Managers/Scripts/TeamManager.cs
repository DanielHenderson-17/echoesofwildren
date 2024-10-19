using UnityEngine;
using System.Collections.Generic;

public class TeamManager : MonoBehaviour
{
    public List<GameObject> characterPrefabs;
    private GameObject activeCharacterInstance;
    private int activeIndex;

    public PlayerManager playerManager;
    private CharacterData characterData;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        characterData = playerManager.GetCharacterData();
    }

    private void Start()
    {
        int savedCharacterId = characterData.activeCharacterId;
        activeIndex = characterData.team.IndexOf(savedCharacterId);

        if (activeIndex == -1)
        {
            activeIndex = 0;
        }

        SwitchCharacter(characterData.team[activeIndex], true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCharacter(characterData.team[0], false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCharacter(characterData.team[1], false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCharacter(characterData.team[2], false);
        }
    }

    private void SwitchCharacter(int characterId, bool isInitialLoad)
    {
        GameObject prefabToInstantiate = FindPrefabById(characterId);

        if (prefabToInstantiate == null)
        {
            Debug.LogError($"No prefab found for character ID: {characterId}");
            return;
        }

        Vector3 spawnPosition = Vector3.zero;

        if (isInitialLoad && characterData.playerPosition != null)
        {
            spawnPosition = new Vector3(
                characterData.playerPosition.x,
                characterData.playerPosition.y,
                characterData.playerPosition.z
            );
        }
        else if (activeCharacterInstance != null)
        {
            spawnPosition = activeCharacterInstance.transform.position;
            Destroy(activeCharacterInstance);
        }

        activeCharacterInstance = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
        playerManager.SetActiveCharacter(activeCharacterInstance);

        activeIndex = System.Array.IndexOf(characterData.team.ToArray(), characterId);
        SaveCharacterState(characterId, spawnPosition);
    }

    private GameObject FindPrefabById(int characterId)
    {
        Character character = characterData.characters.Find(c => c.id == characterId);

        if (character != null)
        {
            foreach (var prefab in characterPrefabs)
            {
                if (prefab.name == character.name)
                {
                    return prefab;
                }
            }
            Debug.LogWarning($"Prefab for character {character.name} not found in the prefab list!");
        }
        else
        {
            Debug.LogError($"Character with ID {characterId} not found in characterData.");
        }

        return null;
    }

    private void SaveCharacterState(int characterId, Vector3 position)
    {
        characterData.activeCharacterId = characterId;

        if (characterData.playerPosition == null)
        {
            characterData.playerPosition = new PlayerPosition();
        }

        characterData.playerPosition.x = position.x;
        characterData.playerPosition.y = position.y;
        characterData.playerPosition.z = position.z;

        playerManager.SavePlayerPosition();
    }
}
