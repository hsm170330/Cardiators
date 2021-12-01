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
        // unhook from events
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        StateMachine.Input.PressedLeft -= OnPressedLeft;
        StateMachine.Input.PressedRight -= OnPressedRight;
        StateMachine.Input.MouseMoved -= OnMouseMove;
        Debug.Log("Player Turn: Exiting...");
    }

    public void OnPressedConfirm()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].isHovered)
            {
                Card tempCard = cards[i];
                cards.RemoveAt(i);
                tempCard.OnClick();
                Health.AIHealth -= tempCard.value;

                //controller.GetComponent<SetupCardiatorState>().SetCards(cards);
                if (Health.AIHealth <= 0)
                {
                    Health.AIHealth = 0;
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
}
