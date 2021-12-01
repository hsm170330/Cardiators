using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Security.Cryptography;
using System;

public class PlayerTurnCardiatorState : CardiatorState
{
    bool kbused = false;
    int hover = 100;

    public GameObject controller = null;
    List<Card> cards = null;
    public Card tempCard;

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
    [SerializeField] AudioClip AIDamage = null;

    public override void Enter()
    {
        Debug.Log("Player Turn: ...Entering");
        TurnText.pText.gameObject.SetActive(true);

        cards = controller.GetComponent<SetupCardiatorState>().GetCards();

        TurnText.pText.gameObject.SetActive(true);

        // hook into events
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        StateMachine.Input.PressedLeft += OnPressedLeft;
        StateMachine.Input.PressedRight += OnPressedRight;
        StateMachine.Input.MouseMoved += OnMouseMove;
        
    }

    public override void Exit()
    {
        TurnText.pText.gameObject.SetActive(false);
        Debug.Log("Player Turn: Exiting...");
    }

    public void OnPressedConfirm()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].isHovered)
            {
                // unhook from events
                StateMachine.Input.PressedConfirm -= OnPressedConfirm;
                StateMachine.Input.PressedLeft -= OnPressedLeft;
                StateMachine.Input.PressedRight -= OnPressedRight;
                StateMachine.Input.MouseMoved -= OnMouseMove;

                tempCard = cards[i];
                cards.RemoveAt(i);
                tempCard.OnClick();
                PlayAudio(tempCard.value);
                StartCoroutine(PlayAIDamageAudio(_pauseDuration));
            }
        }
    }

    void OnPressedLeft()
    {
        kbused = true;
        UnhoverAll();

        if (hover == 100)
        {
            hover = cards.Count-1;
            cards[hover].Hover();
        }
        else
        {
            hover--;
            if (hover < 0)
            {
                hover = cards.Count-1;
            }
            cards[hover].Hover();
        }
    }

    void OnPressedRight()
    {
        Debug.Log("Hover: " + hover);
        kbused = true;
        UnhoverAll();

        if (hover == 100)
        {
            hover = 0;
            cards[hover].Hover();
        }
        else
        {
            hover++;
            if (hover >= cards.Count)
            {
                hover = 0;
            }
            cards[hover].Hover();
        }
        Debug.Log("Hover: " + hover);
    }
    void OnMouseMove()
    {
        if (kbused)
        {
            UnhoverAll();
            kbused = false;
        }
        
    }
    public void UnhoverAll()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].UnHover();
        }
    }

    IEnumerator PlayAIDamageAudio(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration);
        AudioManager.PlayClip2D(AIDamage, 1);
        Health.AIHealth -= tempCard.value;
        if (Health.AIHealth <= 0)
        {
            Health.AIHealth = 0;
            StateMachine.ChangeState<GameOverState>();
        }
        else
        {
            StartCoroutine(ChangeToEnemyTurn(1.5f));
        }
    }
    IEnumerator ChangeToEnemyTurn(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration);
        StateMachine.ChangeState<EnemyTurnCardiatorState>();
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
