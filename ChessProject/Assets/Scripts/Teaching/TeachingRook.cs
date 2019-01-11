using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeachingRook : TeachingPiece {

    private void Start()
    {
        description = "Начальная позиция: a1 и h1 у белых, a8 и h8 у черных." +
            "Ладья может перемещаться на любое количество полей вдоль горизонтали или вертикали.";

        startWhileLocations.Add(new Vector2Int(0, 0));
        startWhileLocations.Add(new Vector2Int(2, 4));

        startBlackLocations.Add(new Vector2Int(6, 4));
    }


    public new void MakeLesson()
    {
        Debug.Log("rook lesson");
        base.MakeLesson();
    }

    public override void Animate()
    {
        Debug.Log("rook animate");

        TeachingManager.instance.small.gameObject.SetActive(true);
        Sequence mySequence = DOTween.Sequence();

        GameObject rookStart = TeachingManager.instance.white.pieces[0];
        GameObject rookMove = TeachingManager.instance.white.pieces[1];
        GameObject rookBlack = TeachingManager.instance.black.pieces[0];

        Vector3[] pathA = new Vector3[4];
        pathA[0] = rookStart.transform.position;
        pathA[1] = new Vector3(rookStart.transform.position.x, rookStart.transform.position.y, rookStart.transform.position.z + 5);
        pathA[2] = rookStart.transform.position;
        pathA[3] = new Vector3(rookStart.transform.position.x + 4, rookStart.transform.position.y, rookStart.transform.position.z);

        Vector3[] pathB = new Vector3[2];
        pathB[0] = rookMove.transform.position;
        pathB[1] = rookBlack.transform.position;

        Vector3[] pathC = new Vector3[2];
        pathC[0] = rookBlack.transform.position;
        pathC[1] = new Vector3(rookBlack.transform.position.x + 2, rookBlack.transform.position.y, rookBlack.transform.position.z);

        mySequence.Append(rookStart.transform.DOPath(pathA, 7.5f, resolution: 5))
            .PrependInterval(1f)
            .Append(rookMove.transform.DOPath(pathB, 2f, resolution: 5))
            .PrependInterval(1f)
            .Append(rookBlack.transform.DOPath(pathC, 2f, resolution: 5))
            .PrependInterval(1f)
            .SetLoops(-1);
    }

}
