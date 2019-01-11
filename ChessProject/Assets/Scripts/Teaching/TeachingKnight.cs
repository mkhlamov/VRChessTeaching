using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeachingKnight : TeachingPiece {

    public GameObject tilePrefab;
    public GameObject board;

    //private GameObject tileHighlight;
    private List<GameObject> highlighted;

    private void Start()
    {
        highlighted = new List<GameObject>();
        description = "Начальная позиция: b1 и g1 у белых, b8 и g8 у черных." +
            "Конь перемешается на 1 клетку вперед и одну по диагонали и только на поле противоположного цвета полю на котором он находится в начале хода.";

        startWhileLocations.Add(new Vector2Int(3, 3));

        //bot left
        startBlackLocations.Add(new Vector2Int(2, 1));
    }


    public new void MakeLesson()
    {
        Debug.Log("rook lesson");
        base.MakeLesson();
    }

    private void HighlightSquare(Vector3 p)
    {
        //Vector2Int gridPoint = Geometry.GridPoint(x, y);
        Vector3 point = p; // Geometry.PointFromGrid(gridPoint);
        GameObject tileHighlight = Instantiate(tilePrefab, point, Quaternion.identity, board.transform);
        tileHighlight.transform.localScale /= board.transform.localScale.x;
        Vector3 pos = tileHighlight.transform.localPosition;
        pos.z = 0.0013f;
        tileHighlight.transform.localPosition = pos;
        highlighted.Add(tileHighlight);
    }

    public override void Animate()
    {
        Debug.Log("rook animate");

        TeachingManager.instance.small.gameObject.SetActive(true);
        Sequence mySequence = DOTween.Sequence();

        GameObject knightMove = TeachingManager.instance.white.pieces[0];
        GameObject knightBlack = TeachingManager.instance.black.pieces[0];
        //up
        HighlightSquare(new Vector3(knightMove.transform.position.x - 1, knightMove.transform.position.y, knightMove.transform.position.z + 2));
        HighlightSquare(new Vector3(knightMove.transform.position.x + 1, knightMove.transform.position.y, knightMove.transform.position.z + 2));
        //right
        HighlightSquare(new Vector3(knightMove.transform.position.x + 2, knightMove.transform.position.y, knightMove.transform.position.z + 1));
        HighlightSquare(new Vector3(knightMove.transform.position.x + 2, knightMove.transform.position.y, knightMove.transform.position.z - 1));
        //bot
        HighlightSquare(new Vector3(knightMove.transform.position.x - 1, knightMove.transform.position.y, knightMove.transform.position.z - 2));
        HighlightSquare(new Vector3(knightMove.transform.position.x + 1, knightMove.transform.position.y, knightMove.transform.position.z - 2));
        //left
        HighlightSquare(new Vector3(knightMove.transform.position.x - 2, knightMove.transform.position.y, knightMove.transform.position.z + 1));
        HighlightSquare(new Vector3(knightMove.transform.position.x - 2, knightMove.transform.position.y, knightMove.transform.position.z - 1));

        Vector2Int[] points = new Vector2Int[] { new Vector2Int(-2, -1), new Vector2Int(-2, 1), new Vector2Int(-1, 2), new Vector2Int(1, 2),
                                                new Vector2Int(2, 1), new Vector2Int(2, -1), new Vector2Int(1, -2)};

        Debug.Log(points.Length);
        Vector3[] pathA = new Vector3[points.Length * 2 + 1];
        Vector3 kpos = knightMove.transform.position;

        for (int i = 0; i < points.Length; i++)
        {
            pathA[i * 2] = kpos;
            pathA[i * 2 + 1] = new Vector3(kpos.x + points[i].x, kpos.y, kpos.z + points[i].y);
        }
        pathA[pathA.Length - 1] = knightMove.transform.position;

        Vector3[] pathB = new Vector3[2];
        pathB[0] = knightMove.transform.position;
        pathB[1] = knightBlack.transform.position;

        Vector3[] pathC = new Vector3[2];
        pathC[0] = knightBlack.transform.position;
        pathC[1] = new Vector3(knightBlack.transform.position.x - 3, knightBlack.transform.position.y, knightBlack.transform.position.z);

        mySequence.Append(knightMove.transform.DOPath(pathA, (points.Length * 2) + 1, resolution: 5, pathType: PathType.CatmullRom))
            .PrependInterval(1f)
            .Append(knightMove.transform.DOPath(pathB, 2f, resolution: 5))
            .PrependInterval(1f)
            .Append(knightBlack.transform.DOPath(pathC, 2f, resolution: 5))
            .PrependInterval(1f)
            .SetLoops(-1);
    }

    public override void Clear()
    {
        foreach (GameObject o in highlighted)
        {
            Destroy(o);
        }
        highlighted.Clear();
    }
}
