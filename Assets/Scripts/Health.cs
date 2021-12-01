using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Text PlayerHealthText = null;
    public Text AIHealthText = null;
    public int PlayerMaxHealth = 20;
    public int AIMaxHealth = 20;
    public static int PlayerHealth;
    public static int AIHealth;

    private void Start()
    {
        PlayerHealth = 20;
        AIHealth = 20;
    }
    private void Update()
    {
        PlayerHealthText.text = "Player Health: " + PlayerHealth + "/" + PlayerMaxHealth;
        AIHealthText.text = "AI Health: " + AIHealth + "/" + AIMaxHealth;
    }
}
