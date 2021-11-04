using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCardiatorState : CardiatorState
{
    [SerializeField] int _startingCardNumber = 9;
    [SerializeField] int _numberOfPlayers = 2;

    bool _activated = false;
    public GameObject CardArea;
    public GameObject Card1, Card2, Card3, Card4, Card5, Card6, Card7, Card8, Card9;

    public override void Enter()
    {
        Debug.Log("Setup: ...Entering");
        Debug.Log("Creating " + _numberOfPlayers + " players.");
        Debug.Log("Laying out " + _startingCardNumber + " cards.");

        // instantiate the cards onto the play area
        GameObject FirstCard = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
        FirstCard.transform.SetParent(CardArea.transform, false);

        GameObject SecondCard = Instantiate(Card2, new Vector3(0, 0, 0), Quaternion.identity);
        SecondCard.transform.SetParent(CardArea.transform, false);

        GameObject ThirdCard = Instantiate(Card3, new Vector3(0, 0, 0), Quaternion.identity);
        ThirdCard.transform.SetParent(CardArea.transform, false);

        GameObject FourthCard = Instantiate(Card4, new Vector3(0, 0, 0), Quaternion.identity);
        FourthCard.transform.SetParent(CardArea.transform, false);

        GameObject FifthCard = Instantiate(Card5, new Vector3(0, 0, 0), Quaternion.identity);
        FifthCard.transform.SetParent(CardArea.transform, false);

        GameObject SixthCard = Instantiate(Card6, new Vector3(0, 0, 0), Quaternion.identity);
        SixthCard.transform.SetParent(CardArea.transform, false);

        GameObject SeventhCard = Instantiate(Card7, new Vector3(0, 0, 0), Quaternion.identity);
        SeventhCard.transform.SetParent(CardArea.transform, false);

        GameObject EighthCard = Instantiate(Card8, new Vector3(0, 0, 0), Quaternion.identity);
        EighthCard.transform.SetParent(CardArea.transform, false);

        GameObject NinthCard = Instantiate(Card9, new Vector3(0, 0, 0), Quaternion.identity);
        NinthCard.transform.SetParent(CardArea.transform, false);

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
}
