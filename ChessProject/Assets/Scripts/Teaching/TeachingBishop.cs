using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachingBishop : TeachingPiece {

    private void Start()
    {
        description = "Начальная позиция: c1 и f1 у белых, c8 и f8 у черных." +
            "Слон может перемещаться на любое количество полей вдоль диагонали." +
            "В начальной позиции у каждого игрока один слон располагается на белом поле, другой - на черном." +
            "Они называются соответственно белопольным и чернопольным.";

        startWhileLocations.Add(new Vector2Int(2, 0));
        startWhileLocations.Add(new Vector2Int(3, 4));

        startBlackLocations.Add(new Vector2Int(5, 5));
    }


    public new void MakeLesson()
    {
        Debug.Log("pawn bishop lesson");
        base.MakeLesson();
    }
}
