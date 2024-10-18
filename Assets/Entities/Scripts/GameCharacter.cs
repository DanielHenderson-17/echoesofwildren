using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacter : MonoBehaviour
{
    public int id;
    public string characterName;
    public bool unlocked;
    public bool active;
    public bool selected;

    public void SetActive(bool value)
    {
        active = value;
        gameObject.SetActive(value); // Activate or deactivate the actual GameObject
    }

    public void SetSelected(bool value)
    {
        selected = value;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        return id == ((GameCharacter)obj).id;
    }
}
