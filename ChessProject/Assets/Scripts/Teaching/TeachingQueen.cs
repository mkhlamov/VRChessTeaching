using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeachingQueen : TeachingPiece {

    private void Start()
    {
        description = "Начальная позиция: d1 у белых, d8 у черных." +
            "Ферзь может перемещаться на любое число полей по вертикали, горизонтали и диагонали.";

        startWhileLocations.Add(new Vector2Int(3, 3));

        //bot left
        startBlackLocations.Add(new Vector2Int(4, 4));
    }


    public new void MakeLesson()
    {
        Debug.Log("queen lesson");
        base.MakeLesson();
    }

    public override void Animate()
    {
        Debug.Log("queen animate");

        TeachingManager.instance.small.gameObject.SetActive(true);
        Sequence mySequence = DOTween.Sequence();

        GameObject queenMove = TeachingManager.instance.white.pieces[0];
        GameObject queenBlack = TeachingManager.instance.black.pieces[0];

        Vector3[] pathA = new Vector3[6];
        pathA[0] = queenMove.transform.position;
        pathA[1] = new Vector3(queenMove.transform.position.x, queenMove.transform.position.y, queenMove.transform.position.z - 3); // 3 0
        pathA[2] = new Vector3(queenMove.transform.position.x - 2, queenMove.transform.position.y, queenMove.transform.position.z - 3); // 1 0
        pathA[3] = new Vector3(queenMove.transform.position.x - 1, queenMove.transform.position.y, queenMove.transform.position.z - 2 ); // 2 1
        pathA[4] = new Vector3(queenMove.transform.position.x - 1, queenMove.transform.position.y, queenMove.transform.position.z + 1); // 2 4
        pathA[5] = queenBlack.transform.position;

        Vector3[] pathB = new Vector3[2];
        pathB[0] = queenBlack.transform.position;
        pathB[1] = new Vector3(queenBlack.transform.position.x + 5, queenBlack.transform.position.y, queenBlack.transform.position.z);

        mySequence.Append(queenMove.transform.DOPath(pathA, 14f, resolution: 5, pathType: PathType.Linear))
            .PrependInterval(1f)
            .Append(queenBlack.transform.DOPath(pathB, 2f, resolution: 5))
            .SetLoops(-1);
    }

}
