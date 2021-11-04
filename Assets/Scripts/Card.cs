using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : CardiatorState
{
    [SerializeField] public int value = 0;
    public GameObject CardBack = null;
    public GameObject Glow = null;
    public bool isHovered = false;
    [SerializeField] float _pauseDuration = 1.5f;
    public void OnClick()
    {
        Debug.Log("Value: " + value);
        if (CardBack != null)
        {
            CardBack.SetActive(true);
        }
        StartCoroutine(DeleteCard(_pauseDuration));
    }

    public void Hover()
    {
        Debug.Log("Card value " + value + "hovered");
        if (Glow != null)
        {
            Glow.SetActive(true);
            isHovered = true;
        }
    }
    public void UnHover()
    {
        if (Glow != null)
        {
            Glow.SetActive(false);
            isHovered = false;
        }
    }

    IEnumerator DeleteCard(float pauseDuration)
    {
        Debug.Log("Removing Card...");
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Card Removed");
        gameObject.SetActive(false);
    }
}
