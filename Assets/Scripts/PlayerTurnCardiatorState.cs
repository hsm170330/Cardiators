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
    

    [SerializeField] Text enemyHealth = null;
    public int enemymaxhealth = 20;
    public int enemycurrenthealth = 20;

    int hover = 100;

    public GameObject controller = null;
    List<Card> cards = null;

    public override void Enter()
    {
        Debug.Log("Player Turn: ...Entering");
        _playerTurnTextUI.gameObject.SetActive(true);

        cards = controller.GetComponent<SetupCardiatorState>().GetCards();

        _playerTurnTextUI.text = "Player Turn";
        
        enemyHealth.text = "Enemy Health: " + enemycurrenthealth + "/" + enemymaxhealth;

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
        StateMachine.Input.PressedLeft -= OnPressedLeft;
        StateMachine.Input.PressedRight -= OnPressedRight;
        Debug.Log("Player Turn: Exiting...");
    }

    void OnPressedConfirm()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].isHovered)
            {
                Card tempCard = cards[i];
                cards.RemoveAt(i);
                tempCard.OnClick();
                enemycurrenthealth -= tempCard.value;

                //controller.GetComponent<SetupCardiatorState>().SetCards(cards);
                if (enemycurrenthealth <= 0)
                {
                    enemycurrenthealth = 0;
                    StateMachine.ChangeState<GameOverState>();
                }
                else
                {
                    StateMachine.ChangeState<EnemyTurnCardiatorState>();
                }
                
            }
        }
    }

    void OnPressedLeft()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].UnHover();
        }

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
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].UnHover();
        }

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
}
