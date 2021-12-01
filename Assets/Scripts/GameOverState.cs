using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : CardiatorState
{
    public override void Enter()
    {
        StartCoroutine(GameOverText());
    }

    IEnumerator GameOverText()
    {
        yield return new WaitForSeconds(1.5f);
        if (Health.AIHealth == 0)
        {
            TurnText.pText.gameObject.SetActive(true);
            TurnText.pText.text = "You Win";
        }
        else
        {
            TurnText.pText.gameObject.SetActive(true);
            TurnText.pText.text = "You Lose";
        }
    }
}
