using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static int Turn;
    public static ChessColor TurnColor;
    void Start()
    {
        Turn = 0;
        TurnColor = ChessColor.Blue;
    }
    
    public static void changeTurn()
    {
        if(TurnColor == ChessColor.Blue)
        {
            TurnColor = ChessColor.Red;
            Turn += 1;
            Debug.Log(Turn);
        }else if(TurnColor == ChessColor.Red)
        {
            TurnColor = ChessColor.Blue;
            Turn += 1;
            Debug.Log(Turn);
        }
        
    }
}
