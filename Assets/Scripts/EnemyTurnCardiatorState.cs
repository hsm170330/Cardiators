using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnemyTurnCardiatorState : CardiatorState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;
    [SerializeField] Text playerHealth = null;
    public int maxhealth = 20;
    public int currenthealth = 20;

    public GameObject controller = null;
    List<Card> cards = null;

    [SerializeField] float _pauseDuration = 1.5f;

    public override void Enter()
    {
        Debug.Log("Enemy Tuen: ...Enter");
        EnemyTurnBegan?.Invoke();
        cards = controller.GetComponent<SetupCardiatorState>().GetCards();

        playerHealth.gameObject.SetActive(true);
        playerHealth.text = "Player Health: " + currenthealth + "/" + maxhealth;

        StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
    }

    public override void Exit()
    {
        Debug.Log("Enemy Turn: Exit...");
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
        currenthealth -= tempCard.value;

        EnemyTurnEnded?.Invoke();
        // turn over. Go back to Player.
        if (currenthealth <= 0)
        {
            currenthealth = 0;
            StateMachine.ChangeState<GameOverState>();
        }
        else
        {
            StateMachine.ChangeState<PlayerTurnCardiatorState>();
        }
    }
}
