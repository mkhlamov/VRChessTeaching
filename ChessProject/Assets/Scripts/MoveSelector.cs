﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour {

    public GameObject moveLocationPrefab;
    public GameObject tileHighlightPrefab;
    public GameObject attackLocationPrefab;

    private GameObject tileHighlight;
    private GameObject movingPiece;
    private List<Vector2Int> moveLocations;
    private List<GameObject> locationHighlights;

    // Use this for initialization
    void Start () {
        this.enabled = false;
        tileHighlight = Instantiate(tileHighlightPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)),
            Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point;
            Vector2Int gridPoint = Geometry.GridFromPoint(point);

            tileHighlight.SetActive(true);
            tileHighlight.transform.position = Geometry.PointFromGrid(gridPoint);
            if (Input.GetMouseButtonDown(0))
            {
                // Опорная точка 2: проверка допустимой позиции хода
                if (!moveLocations.Contains(gridPoint))
                {
                    return;
                }
                if (GameManager.instance.PieceAtGrid(gridPoint) == null)
                {
                    GameManager.instance.Move(movingPiece, gridPoint);
                }
                // Опорная точка 3: здесь позже будет код взятия вражеской фигуры
                else
                {
                    GameManager.instance.CapturePieceAt(gridPoint);
                    GameManager.instance.Move(movingPiece, gridPoint);
                }
                ExitState();
            }
        }
        else
        {
            tileHighlight.SetActive(false);
        }
    }

    public void EnterState(GameObject piece)
    {
        movingPiece = piece;
        this.enabled = true;

        moveLocations = GameManager.instance.MovesForPiece(movingPiece);
        locationHighlights = new List<GameObject>();

        foreach (Vector2Int loc in moveLocations)
        {
            GameObject highlight;
            if (GameManager.instance.PieceAtGrid(loc))
            {
                highlight = Instantiate(attackLocationPrefab, Geometry.PointFromGrid(loc),
                    Quaternion.identity, gameObject.transform);
            }
            else
            {
                highlight = Instantiate(moveLocationPrefab, Geometry.PointFromGrid(loc),
                    Quaternion.identity, gameObject.transform);
            }
            locationHighlights.Add(highlight);
        }
    }

    private void ExitState()
    {
        this.enabled = false;
        tileHighlight.SetActive(false);
        GameManager.instance.DeselectPiece(movingPiece);
        movingPiece = null;
        TileSelector selector = GetComponent<TileSelector>();
        GameManager.instance.NextPlayer();
        selector.EnterState();

        foreach (GameObject highlight in locationHighlights)
        {
            Destroy(highlight);
        }
    }


}
