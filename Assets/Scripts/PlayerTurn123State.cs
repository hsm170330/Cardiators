using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn123State : CardiatorState
{
    bool kbused = false;
    int hover = 100;

    public GameObject controller = null;
    public GameObject Values = null;
    public static Card tempCard = null;
    List<Card> cards = null;

    public override void Enter()
    {
        Debug.Log("Player Turn 123");
        TurnText.pText.gameObject.SetActive(true);

        cards = controller.GetComponent<Setup123>().GetCards();

        //hook into events
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        StateMachine.Input.PressedLeft += OnPressedLeft;
        StateMachine.Input.PressedRight += OnPressedRight;
        StateMachine.Input.MouseMoved += OnMouseMove;
    }

    public override void Exit()
    {
        //unhook from events
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        StateMachine.Input.PressedLeft -= OnPressedLeft;
        StateMachine.Input.PressedRight -= OnPressedRight;
        StateMachine.Input.MouseMoved -= OnMouseMove;

        TurnText.pText.gameObject.SetActive(false);
    }

    public void OnPressedConfirm()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].isHovered)
            {
                tempCard = cards[i];
                Values123.PlayerValue = tempCard.value;
                cards.RemoveAt(i);
                tempCard.Display();
                

                //controller.GetComponent<SetupCardiatorState>().SetCards(cards);
                StateMachine.ChangeState<EnemyTurn123State>();

            }
        }
    }

    void OnPressedLeft()
    {
        kbused = true;
        UnhoverAll();

        if (hover == 100)
        {
            hover = cards.Count - 1;
            cards[hover].Hover();
        }
        else
        {
            hover--;
            if (hover < 0)
            {
                hover = cards.Count - 1;
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
