using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnText : MonoBehaviour
{
    public Text PlayerTurnText = null;
    public Text AITurnText = null;
    public static Text pText;
    public static Text aText;
    private void Start()
    {
        pText = PlayerTurnText;
        aText = AITurnText;
    }
}
