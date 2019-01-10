using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachingRook : TeachingPiece {

    private void Start()
    {
        description = "Начальная позиция: a1 и h1 у белых, a8 и h8 у черных." +
            "Ладья может перемещаться на любое количество полей вдоль горизонтали или вертикали.";

        startWhileLocations.Add(new Vector2Int(0, 0));
        startWhileLocations.Add(new Vector2Int(4, 4));

        startBlackLocations.Add(new Vector2Int(6, 4));
    }


    public new void MakeLesson()
    {
        Debug.Log("pawn bishop lesson");
        base.MakeLesson();
    }


}
