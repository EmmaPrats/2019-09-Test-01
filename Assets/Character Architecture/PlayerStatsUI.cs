using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    public Character character;

    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI attackDisplay;
    public TextMeshProUGUI defenseDisplay;

    void Update()
    {
        healthDisplay.text = (int)character.Health + " HP";
        attackDisplay.text = character.Strength + " atk";
        defenseDisplay.text = character.Defense + " def";
    }
}
