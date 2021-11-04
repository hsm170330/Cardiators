using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] int value = 0;
    public GameObject CardBack = null;
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

    IEnumerator DeleteCard(float pauseDuration)
    {
        Debug.Log("Removing Card...");
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Card Removed");
        gameObject.SetActive(false);
    }
}
