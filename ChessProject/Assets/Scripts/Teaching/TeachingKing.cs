using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeachingKing : TeachingPiece {

    private void Start()
    {
        description = "Начальная позиция: e1 у белых, e8 у черных." +
            "Король может перемещаться на одно поле по вертикали, горизонтали и диагонали.";

        startWhiteLocations.Add(new Vector2Int(3, 3));

        //startBlackLocations.Add(new Vector2Int(4, 4));
    }


    public new void MakeLesson()
    {
        Debug.Log("king lesson");
        base.MakeLesson();
    }

    public override void Animate()
    {
        Debug.Log("king animate");

        TeachingManager.instance.small.gameObject.SetActive(true);
        Sequence mySequence = DOTween.Sequence();

        GameObject kingMove = TeachingManager.instance.white.pieces[0];
        //GameObject queenBlack = TeachingManager.instance.black.pieces[0];

        Vector3[] pathA = new Vector3[6];
        pathA[0] = kingMove.transform.position;
        pathA[1] = new Vector3(kingMove.transform.position.x, kingMove.transform.position.y, kingMove.transform.position.z - 1); // 3 2
        pathA[2] = new Vector3(kingMove.transform.position.x - 1, kingMove.transform.position.y, kingMove.transform.position.z - 1); // 2 2
        pathA[3] = new Vector3(kingMove.transform.position.x - 1, kingMove.transform.position.y, kingMove.transform.position.z - 2 ); // 2 1
        pathA[4] = new Vector3(kingMove.transform.position.x, kingMove.transform.position.y, kingMove.transform.position.z - 1); // 3 2
        pathA[5] = kingMove.transform.position;


        mySequence.Append(kingMove.transform.DOPath(pathA, 14f, resolution: 5, pathType: PathType.Linear))
            .SetLoops(-1);
    }

}
