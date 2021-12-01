using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTurn123State : CardiatorState
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
        cards = controller.GetComponent<Setup123>().GetCards();

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
        Debug.Log(num);
        Card tempCard = cards[num];
        Values123.AIValue = tempCard.value;
        cards.RemoveAt(num);
        tempCard.Display();

        yield return new WaitForSeconds(pauseDuration);

        //remove the cards
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].gameObject.SetActive(false);
        }
        PlayerTurn123State.tempCard.gameObject.SetActive(false);
        tempCard.gameObject.SetActive(false);

        EnemyTurnEnded?.Invoke();
        // turn over. Go to cardiator setup
        StateMachine.ChangeState<SetupCardiatorState>();
    }
}
