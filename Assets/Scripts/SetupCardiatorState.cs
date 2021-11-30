using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;
using System;

public class SetupCardiatorState : CardiatorState
{
    [SerializeField] int _startingCardNumber = 9;
    [SerializeField] int _numberOfPlayers = 2;

    bool _activated = false;
    public GameObject CardArea;
    public Card Card1, Card2, Card3, Card4, Card5, Card6, Card7, Card8, Card9;
    List<Card> cards = new List<Card>();


    public override void Enter()
    {
        Debug.Log("Setup: ...Entering");
        Debug.Log("Creating " + _numberOfPlayers + " players.");
        Debug.Log("Laying out " + _startingCardNumber + " cards.");

        // instantiate the cards onto the play area
        cards.Add(Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity));
        cards.Add(Instantiate(Card2, new Vector3(0, 0, 0), Quaternion.identity));
        cards.Add(Instantiate(Card3, new Vector3(0, 0, 0), Quaternion.identity));
        cards.Add(Instantiate(Card4, new Vector3(0, 0, 0), Quaternion.identity));
        cards.Add(Instantiate(Card5, new Vector3(0, 0, 0), Quaternion.identity));
        cards.Add(Instantiate(Card6, new Vector3(0, 0, 0), Quaternion.identity));
        cards.Add(Instantiate(Card7, new Vector3(0, 0, 0), Quaternion.identity));
        cards.Add(Instantiate(Card8, new Vector3(0, 0, 0), Quaternion.identity));
        cards.Add(Instantiate(Card9, new Vector3(0, 0, 0), Quaternion.identity));

        //Randomize the cards
        System.Random random = new System.Random();
        cards = cards.OrderBy(x => random.Next()).ToList();

        //Transform the cards into place
        for(int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.SetParent(CardArea.transform, true);
        }

        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
        _activated = false;
    }

    public override void Tick()
    {
        // admittedly hacky for demo. You would usually have delays or Input.
        if (_activated == false)
        {
            _activated = true;
            if (Values123.PlayerValue > Values123.AIValue)
            {
                StateMachine.ChangeState<PlayerTurnCardiatorState>();
            }
            else
            {
                StateMachine.ChangeState<EnemyTurnCardiatorState>();
            }
            
        }
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("Setup: Exiting...");
    }

    public List<Card> GetCards()
    {
        return cards;
    }

    public void SetCards(List<Card> newCards)
    {
        cards = newCards;
    }
}
