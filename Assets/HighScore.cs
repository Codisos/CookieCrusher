using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HighScore
{
    public int[] _score;

    public HighScore (int[] score)
    {
        _score = score;
    }

}
