using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Setup123 : CardiatorState
{
    public GameObject CardArea;
    public Card Card1, Card2, Card3;
    List<Card> cards = new List<Card>();
    bool _activated = false;

    public override void Enter()
    {
        Debug.Log("Entering 123 Setup");

        // put the cards in
        cards.Add(Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity));
        cards.Add(Instantiate(Card2, new Vector3(0, 0, 0), Quaternion.identity));
        cards.Add(Instantiate(Card3, new Vector3(0, 0, 0), Quaternion.identity));

        //Randomize the cards
        System.Random random = new System.Random();
        cards = cards.OrderBy(x => random.Next()).ToList();

        //transform the cards into the card area
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.SetParent(CardArea.transform, true);
        }

        // CANT change state while still in Enter()/Exit() transition!
        // DONT put ChangeState<> here.
        _activated = false;
    }

    public override void Tick()
    {
        if (_activated == false)
        {
            _activated = true;
            StateMachine.ChangeState<PlayerTurn123State>();
        }
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("Exiting 123 Setup");
    }

    public List<Card> GetCards()
    {
        return cards;
    }
}
