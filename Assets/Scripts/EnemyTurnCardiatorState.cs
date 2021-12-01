using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnemyTurnCardiatorState : CardiatorState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    public GameObject controller = null;
    List<Card> cards = null;

    [SerializeField] float _pauseDuration = 1.5f;

    public override void Enter()
    {
        Debug.Log("Enemy Turn: ...Enter");
        TurnText.aText.gameObject.SetActive(true);

        EnemyTurnBegan?.Invoke();
        cards = controller.GetComponent<SetupCardiatorState>().GetCards();

        StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
    }

    public override void Exit()
    {
        Debug.Log("Enemy Turn: Exit...");
        TurnText.aText.gameObject.SetActive(false);
    }

    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy thinking...");
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Enemy performs action");
        System.Random random = new System.Random();
        int num = random.Next(0, cards.Count);
        Card tempCard = cards[num];
        cards.RemoveAt(num);
        tempCard.OnClick();
        Health.PlayerHealth -= tempCard.value;

        yield return new WaitForSeconds(pauseDuration);

        EnemyTurnEnded?.Invoke();
        // turn over. Go back to Player.
        if (Health.PlayerHealth <= 0)
        {
            Health.PlayerHealth = 0;
            TurnText.pText.gameObject.SetActive(true);
            TurnText.pText.text = "You Lose";

            StateMachine.ChangeState<GameOverState>();
        }
        else
        {
            StateMachine.ChangeState<PlayerTurnCardiatorState>();
        }
    }
}
