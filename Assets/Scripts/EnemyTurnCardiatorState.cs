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

    [SerializeField] AudioClip Clip01 = null;
    [SerializeField] AudioClip Clip02 = null;
    [SerializeField] AudioClip Clip05 = null;
    [SerializeField] AudioClip Clip06 = null;
    [SerializeField] AudioClip Clip07 = null;
    [SerializeField] AudioClip Clip08 = null;
    [SerializeField] AudioClip Clip09 = null;
    [SerializeField] AudioClip Clip10 = null;
    [SerializeField] AudioClip Clip12 = null;
    [SerializeField] AudioClip PlayerDamage = null;

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
        PlayAudio(tempCard.value);

        yield return new WaitForSeconds(pauseDuration);
        AudioManager.PlayClip2D(PlayerDamage, 1);
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

    public void PlayAudio(int value)
    {
        switch (value)
        {
            case 1:
                AudioManager.PlayClip2D(Clip01, 1);
                break;
            case 2:
                AudioManager.PlayClip2D(Clip02, 1);
                break;
            case 5:
                AudioManager.PlayClip2D(Clip05, 1);
                break;
            case 6:
                AudioManager.PlayClip2D(Clip06, 1);
                break;
            case 7:
                AudioManager.PlayClip2D(Clip07, 1);
                break;
            case 8:
                AudioManager.PlayClip2D(Clip08, 1);
                break;
            case 9:
                AudioManager.PlayClip2D(Clip09, 1);
                break;
            case 10:
                AudioManager.PlayClip2D(Clip10, 1);
                break;
            case 12:
                AudioManager.PlayClip2D(Clip12, 1);
                break;
        }

    }
}
