using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTurnCardiatorState : CardiatorState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    public GameObject controller = null;
    List<GameObject> cards = null;

    [SerializeField] float _pauseDuration = 1.5f;

    public override void Enter()
    {
        Debug.Log("Enemy Tuen: ...Enter");
        EnemyTurnBegan?.Invoke();
        cards = controller.GetComponent<SetupCardiatorState>().GetCards();
        StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
    }

    public override void Exit()
    {
        Debug.Log("Enemy Tuen: Exit...");
    }

    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy thinking...");
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Enemy performs action");
        System.Random random = new System.Random();
        int num = random.Next(1, cards.Count);
        cards[num].GetComponent<Card>().OnClick();

        EnemyTurnEnded?.Invoke();
        // turn over. Go back to Player.
        StateMachine.ChangeState<PlayerTurnCardiatorState>();
    }
}
