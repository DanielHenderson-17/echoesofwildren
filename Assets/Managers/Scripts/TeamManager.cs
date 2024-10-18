using UnityEngine;
using System.Collections.Generic;

public class TeamManager : MonoBehaviour
{
    public List<GameObject> teamMembers;
    private int activeIndex = 0;

    public PlayerManager playerManager;


    private void Awake()
{
    playerManager = FindObjectOfType<PlayerManager>();
}


    private void Start()
    {
        for (int i = 0; i < teamMembers.Count; i++)
        {
            teamMembers[i].SetActive(i == activeIndex);
        }
        playerManager.SetActiveCharacter(teamMembers[activeIndex]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Pressed 1");
            SwitchActiveMember(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Pressed 2");
            SwitchActiveMember(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Pressed 3");
            SwitchActiveMember(2);
        }
    }


    private void SwitchActiveMember(int newIndex)
{
    if (newIndex >= 0 && newIndex < teamMembers.Count)
    {

        Vector3 currentPosition = teamMembers[activeIndex].transform.position;

        foreach (var member in teamMembers)
        {
            member.SetActive(false);
        }

        activeIndex = newIndex;


        teamMembers[activeIndex].transform.position = currentPosition;

        teamMembers[activeIndex].SetActive(true);
        playerManager.SetActiveCharacter(teamMembers[activeIndex]);
    }
    else
    {
        Debug.LogError($"Invalid team member index: {newIndex}. Count: {teamMembers.Count}");
    }
}

}
