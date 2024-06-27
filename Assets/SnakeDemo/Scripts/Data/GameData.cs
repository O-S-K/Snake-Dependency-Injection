using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{ 
    public static int Score { get; private set; }
 

    public static void AddScore(int score)
    {
        Score += score;
    }

    public static void ResetScore()
    {
        Score = 0;
    }
}
