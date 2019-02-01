using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachingManager : MonoBehaviour {

    public static TeachingManager instance;

    public Board board;

    public GameObject whiteKing;
    public GameObject whiteQueen;
    public GameObject whiteBishop;
    public GameObject whiteKnight;
    public GameObject whiteRook;
    public GameObject whitePawn;

    public GameObject blackKing;
    public GameObject blackQueen;
    public GameObject blackBishop;
    public GameObject blackKnight;
    public GameObject blackRook;
    public GameObject blackPawn;

    public TeachingPiece currentPiece;

    
    public Player white;
    public Player black;

    public TMPro.TextMeshProUGUI small;

    private GameObject[,] pieces;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pieces = new GameObject[8, 8];

        Debug.Log("teaching manager start pieces: " + pieces);

        white = new Player("white", true);
        black = new Player("black", false);

    }


    public void AddPiece(GameObject prefab, Player player, int col, int row)
    {
        GameObject pieceObject = board.AddPiece(prefab, col, row);
        player.pieces.Add(pieceObject);
        pieces[col, row] = pieceObject;
    }

    public void ClearBoard()
    {
        if (currentPiece)
        {
            currentPiece.Clear();
        }
        
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (pieces[i, j] != null) {
                    Destroy(pieces[i, j]);
                }
            }
        }

        white.pieces.Clear();
        black.pieces.Clear();
    }

    public void SetupForPiece(GameObject whitePrefab, GameObject blackPrefab, List<Vector2Int> startWhiteLoc, List<Vector2Int> startBlackLoc, TeachingPiece curPiece)
    {
        ClearBoard();

        foreach (Vector2Int p in startWhiteLoc)
        {
            AddPiece(whitePrefab, white, p.x, p.y);
        }

        foreach (Vector2Int p in startBlackLoc)
        {
            AddPiece(blackPrefab, black, p.x, p.y);
        }

        currentPiece = curPiece;
    }

    public bool CheckStartPosition(Piece p) {
        bool res = true;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (pieces[i, j] && pieces[i, j].GetComponent<Piece>().type == p.type)
                {
                    res = res && (p.possibleStartLocations.Contains(new Vector2Int(i, j)));
                }
            }
        }

        return res;
    }

    public GameObject PieceAtGrid(Vector2Int gridPoint)
    {
        if (gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0)
        {
            return null;
        }
        return pieces[gridPoint.x, gridPoint.y];
    }
}
