using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInteractive : MonoBehaviour {

    public GameObject tileHighlightPrefab;
    public GameObject canvas;

    private GameObject tileHighlight;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector2Int gridPoint = Geometry.GridPoint(i, j);
                Vector3 point = Geometry.PointFromGrid(gridPoint);
                tileHighlight = Instantiate(tileHighlightPrefab, point, canvas.transform.rotation /*Quaternion.Euler(90, 0 , 0)*/, canvas.transform);
            }
        }
        //canvas.SetActive(false);
    }

    public void ShowCanvas()
    {
        canvas.SetActive(true);
    }
}
