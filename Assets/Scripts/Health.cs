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

    bool reducingPlayerHealth = false;
    bool reducingAIHealth = false;

    public Slider PlayerSlider;
    public Slider AISlider;

    public Gradient gradient;
    public Image PlayerFill, AIFill;

    float _pauseDuration = 0.04f;

    private void Start()
    {
        PlayerHealth = 20;
        AIHealth = 20;

        PlayerSlider.maxValue = PlayerMaxHealth;
        AISlider.maxValue = AIMaxHealth;

        PlayerFill.color = gradient.Evaluate(PlayerSlider.normalizedValue);
        AIFill.color = gradient.Evaluate(AISlider.normalizedValue);
    }
    private void Update()
    {
        PlayerHealthText.text = "Player Health: " + PlayerHealth + "/" + PlayerMaxHealth;
        AIHealthText.text = "AI Health: " + AIHealth + "/" + AIMaxHealth;

        //update health slider
        while (PlayerSlider.value != PlayerHealth && !reducingPlayerHealth)
        {
            reducingPlayerHealth = true;
            StartCoroutine(ReducePlayerHealth(_pauseDuration));
        }

        while (AISlider.value != AIHealth && !reducingAIHealth)
        {
            reducingAIHealth = true;
            StartCoroutine(ReduceAIHealth(_pauseDuration));
        }
    }

    IEnumerator ReducePlayerHealth(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration);
        PlayerSlider.value--;
        PlayerFill.color = gradient.Evaluate(PlayerSlider.normalizedValue);
        reducingPlayerHealth = false;
    }
    IEnumerator ReduceAIHealth(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration);
        AISlider.value--;
        AIFill.color = gradient.Evaluate(AISlider.normalizedValue);
        reducingAIHealth = false;
    }
}
