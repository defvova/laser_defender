using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Text healthDisplay;
    Player player;

    private void Start()
    {
        healthDisplay = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        healthDisplay.text = player.GetHealth().ToString();
    }
}
