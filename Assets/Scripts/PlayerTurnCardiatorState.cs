using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Security.Cryptography;
using System;

public class PlayerTurnCardiatorState : CardiatorState
{
    [SerializeField] Text _playerTurnTextUI = null;

    int hover = 100;

    public GameObject controller = null;
    List<GameObject> cards = null;

    public override void Enter()
    {
        Debug.Log("Player Turn: ...Entering");
        _playerTurnTextUI.gameObject.SetActive(true);
        cards = controller.GetComponent<SetupCardiatorState>().GetCards();
        _playerTurnTextUI.text = "Player Turn";
        // hook into events
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        StateMachine.Input.PressedLeft += OnPressedLeft;
        StateMachine.Input.PressedRight += OnPressedRight;
        
    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);
        // unhook from events
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        Debug.Log("Player Turn: Exiting...");
    }

    void OnPressedConfirm()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].GetComponent<Card>().isHovered)
            {
                cards[i].GetComponent<Card>().OnClick();
                cards.RemoveAt(i);

                //controller.GetComponent<SetupCardiatorState>().SetCards(cards);
                StateMachine.ChangeState<EnemyTurnCardiatorState>();
            }
        }
    }

    void OnPressedLeft()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<Card>().UnHover();
        }

        if (hover == 100)
        {
            hover = cards.Count;
            cards[hover].GetComponent<Card>().Hover();
        }
        else
        {
            hover--;
            if (hover < 0)
            {
                hover = cards.Count;
            }
            cards[hover].GetComponent<Card>().Hover();
        }
    }

    void OnPressedRight()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<Card>().UnHover();
        }

        if (hover == 100)
        {
            hover = 0;
            cards[hover].GetComponent<Card>().Hover();
        }
        else
        {
            hover++;
            if (hover >= cards.Count)
            {
                hover = 0;
            }
            cards[hover].GetComponent<Card>().Hover();
        }
    }
}
