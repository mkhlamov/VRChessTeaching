using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeachingBishop : TeachingPiece {

    private void Start()
    {
        description = "Начальная позиция: c1 и f1 у белых, c8 и f8 у черных." +
            "Слон может перемещаться на любое количество полей вдоль диагонали." +
            "В начальной позиции у каждого игрока один слон располагается на белом поле, другой - на черном." +
            "Они называются соответственно белопольным и чернопольным.";

        startWhileLocations.Add(new Vector2Int(2, 0));
        startWhileLocations.Add(new Vector2Int(3, 4));

        startBlackLocations.Add(new Vector2Int(5, 6));
    }


    public new void MakeLesson()
    {
        Debug.Log("pawn bishop lesson");
        base.MakeLesson();
    }

    public override void Animate() {
        TeachingManager.instance.small.gameObject.SetActive(true);
        Sequence mySequence = DOTween.Sequence();

        GameObject bishopStart = TeachingManager.instance.white.pieces[0];
        GameObject bishopMove = TeachingManager.instance.white.pieces[1];
        GameObject bishopBlack = TeachingManager.instance.black.pieces[0];

        Vector3[] pathA = new Vector3[5];
        pathA[0] = bishopStart.transform.position;
        pathA[1] = new Vector3(bishopStart.transform.position.x + 3, bishopStart.transform.position.y, bishopStart.transform.position.z + 3);
        pathA[2] = new Vector3(bishopStart.transform.position.x + 1, bishopStart.transform.position.y, bishopStart.transform.position.z + 1);
        pathA[3] = new Vector3(bishopStart.transform.position.x - 2, bishopStart.transform.position.y, bishopStart.transform.position.z + 4);
        pathA[4] = new Vector3(bishopStart.transform.position.x + 1, bishopStart.transform.position.y, bishopStart.transform.position.z + 7);

        Vector3[] pathB = new Vector3[2];
        pathB[0] = bishopMove.transform.position;
        pathB[1] = new Vector3(bishopMove.transform.position.x + 2, bishopMove.transform.position.y, bishopMove.transform.position.z + 2);

        Vector3[] pathC = new Vector3[2];
        pathC[0] = bishopBlack.transform.position;
        pathC[1] = new Vector3(bishopBlack.transform.position.x + 3, bishopBlack.transform.position.y, bishopBlack.transform.position.z);


        mySequence.Append(bishopStart.transform.DOPath(pathA, 7.5f, resolution: 5))
            .PrependInterval(1f)
            .Append(bishopMove.transform.DOPath(pathB, 2f, resolution: 5))
            .PrependInterval(1f)
            .Append(bishopBlack.transform.DOPath(pathC, 2f, resolution: 5))
            .PrependInterval(1f)
            .SetLoops(-1);

    }
}
