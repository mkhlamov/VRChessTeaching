using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TeachingPawn : TeachingPiece
{

    private void Start()
    {
        description = "Начальная позиция: 8 пешек на 2 и 7 ряду соответственно. \n" +
            "Пешка может двигаться только на одну клетку вперед по вертикали. \n" +
            "Исключение для начальной позиции, например, белая пешка на втором ряду и черная пешка на 7 ряду может пойти сразу на 2 поля вперед.\n" +
            "Пешки перемещаются только по вертикали, но взятия совершают по диагонали, влево или вправо.";

        descriptionTest = "Переместите пешку на любую возможную начальную позицию";

        startWhileLocations.Add(new Vector2Int(1, 1));
        startWhileLocations.Add(new Vector2Int(4, 4));

        startBlackLocations.Add(new Vector2Int(5, 5));

        startWhileLocationsTest.Add(new Vector2Int(0, 0));
    }


    public new void MakeLesson()
    {
        Debug.Log("pawn make lesson");
        base.MakeLesson();
    }

    public override void Animate()
    {
        Debug.Log("pawn animate");

        TeachingManager.instance.small.gameObject.SetActive(true);
        Sequence mySequence = DOTween.Sequence();

        GameObject pawnStart = TeachingManager.instance.white.pieces[0];
        GameObject pawnMove = TeachingManager.instance.white.pieces[1];
        GameObject pawnBlack = TeachingManager.instance.black.pieces[0];

        Vector3[] pathA = new Vector3[2];
        pathA[0] = pawnStart.transform.position;
        pathA[1] = new Vector3(pawnStart.transform.position.x, pawnStart.transform.position.y, pawnStart.transform.position.z + 1);

        Vector3[] pathB = new Vector3[2];
        pathB[0] = pawnStart.transform.position;
        pathB[1] = new Vector3(pawnStart.transform.position.x, pawnStart.transform.position.y, pawnStart.transform.position.z + 2);


        Vector3[] pathC = new Vector3[2];
        pathC[0] = pawnMove.transform.position;
        pathC[1] = new Vector3(pawnMove.transform.position.x, pawnMove.transform.position.y, pawnMove.transform.position.z + 1);

        Vector3[] pathD = new Vector3[2];
        pathD[0] = pawnMove.transform.position;
        pathD[1] = new Vector3(pawnMove.transform.position.x + 1, pawnMove.transform.position.y, pawnMove.transform.position.z + 1);

        Vector3[] pathE = new Vector3[2];
        pathE[0] = pawnBlack.transform.position;
        pathE[1] = new Vector3(pawnBlack.transform.position.x + 4, pawnMove.transform.position.y, pawnMove.transform.position.z);

        mySequence.Append(pawnStart.transform.DOPath(pathA, 1.5f, resolution: 5))
            .PrependInterval(1)
            .Append(pawnStart.transform.DOPath(pathB, 3.0f, resolution: 5))
            .PrependInterval(1)
            .Append(pawnMove.transform.DOPath(pathC, 1.5f, resolution: 5))
            .PrependInterval(1)
            .Append(pawnMove.transform.DOPath(pathD, 3.0f, resolution: 5))
            .Append(pawnBlack.transform.DOPath(pathE, 3.0f, resolution: 5))
            .SetLoops(-1);
    }

    public override void MakeTest()
    {
        base.MakeTest();
    }

    public override bool CheckTest(GameObject tile)
    {
        Vector2Int gridPoint = Geometry.GridFromPoint(tile.transform.position);
        return gridPoint.y == 1;
    }
}
