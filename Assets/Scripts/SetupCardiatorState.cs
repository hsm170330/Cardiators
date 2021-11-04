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
    public GameObject Card1, Card2, Card3, Card4, Card5, Card6, Card7, Card8, Card9;
    public GameObject[] Cards = new GameObject[9];
    List<GameObject> cards = new List<GameObject>();


    public override void Enter()
    {
        Debug.Log("Setup: ...Entering");
        Debug.Log("Creating " + _numberOfPlayers + " players.");
        Debug.Log("Laying out " + _startingCardNumber + " cards.");

        // instantiate the cards onto the play area
        //Cards[0] = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
        cards.Add(Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity));
        //Cards[0].transform.SetParent(CardArea.transform, false);

        //Cards[1] = Instantiate(Card2, new Vector3(0, 0, 0), Quaternion.identity);
        cards.Add(Instantiate(Card2, new Vector3(0, 0, 0), Quaternion.identity));
        //Cards[1].transform.SetParent(CardArea.transform, false);

        //Cards[2] = Instantiate(Card3, new Vector3(0, 0, 0), Quaternion.identity);
        cards.Add(Instantiate(Card3, new Vector3(0, 0, 0), Quaternion.identity));
        //Cards[2].transform.SetParent(CardArea.transform, false);

        //Cards[3] = Instantiate(Card4, new Vector3(0, 0, 0), Quaternion.identity);
        cards.Add(Instantiate(Card4, new Vector3(0, 0, 0), Quaternion.identity));
        //Cards[3].transform.SetParent(CardArea.transform, false);

        // Cards[4] = Instantiate(Card5, new Vector3(0, 0, 0), Quaternion.identity);
        cards.Add(Instantiate(Card5, new Vector3(0, 0, 0), Quaternion.identity));
        //Cards[4].transform.SetParent(CardArea.transform, false);

        //Cards[5] = Instantiate(Card6, new Vector3(0, 0, 0), Quaternion.identity);
        cards.Add(Instantiate(Card6, new Vector3(0, 0, 0), Quaternion.identity));
        //Cards[5].transform.SetParent(CardArea.transform, false);

        //Cards[6] = Instantiate(Card7, new Vector3(0, 0, 0), Quaternion.identity);
        cards.Add(Instantiate(Card7, new Vector3(0, 0, 0), Quaternion.identity));
        //Cards[6].transform.SetParent(CardArea.transform, false);

        //Cards[7] = Instantiate(Card8, new Vector3(0, 0, 0), Quaternion.identity);
        cards.Add(Instantiate(Card8, new Vector3(0, 0, 0), Quaternion.identity));
        //Cards[7].transform.SetParent(CardArea.transform, false);

        //Cards[8] = Instantiate(Card9, new Vector3(0, 0, 0), Quaternion.identity);
        cards.Add(Instantiate(Card9, new Vector3(0, 0, 0), Quaternion.identity));
        //Cards[8].transform.SetParent(CardArea.transform, false);

        //Randomize the cards
        System.Random random = new System.Random();
        //Cards = Cards.OrderBy(x => random.Next()).ToArray();
        cards = cards.OrderBy(x => random.Next()).ToList();

        //Transform the cards into place
        for(int i = 0; i < Cards.Length; i++)
        {
            //Cards[i].transform.SetParent(CardArea.transform, true);
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
            StateMachine.ChangeState<PlayerTurnCardiatorState>();
        }
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("Setup: Exiting...");
    }

    public List<GameObject> GetCards()
    {
        return cards;
    }

    public void SetCards(List<GameObject> newCards)
    {
        cards = newCards;
    }
}
