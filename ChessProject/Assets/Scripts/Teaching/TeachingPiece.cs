using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeachingPiece : MonoBehaviour {

    public string description;
    public string descriptionTest;
    public List<Vector2Int> startWhiteLocations;
    public List<Vector2Int> startBlackLocations;
    public List<Vector2Int> startWhileLocationsTest;
    public List<Vector2Int> startBlackLocationsTest;
    public GameObject whitePiecePrefab;
    public GameObject blackPiecePrefab;

    public TextMeshProUGUI text;

    private void Start()
    {
        
    }

    public void MakeLesson() {
        Debug.Log("piece make lesson");
        text.text = description;

        TeachingManager.instance.small.gameObject.SetActive(false);
        TeachingManager.instance.SetupForPiece(whitePiecePrefab, blackPiecePrefab, startWhiteLocations, startBlackLocations, this);
        

        Animate();
    }

    public virtual void Animate()
    {
        Debug.Log("piece animate");
    }

    public virtual void Clear()
    {

    }

    public virtual void MakeTest()
    {
        text.text = descriptionTest;

        if (startWhileLocationsTest.Count == 0)
        {
            startWhileLocationsTest.Add(startWhiteLocations[Random.Range(0, startWhiteLocations.Count)]);
        }
        TeachingManager.instance.SetupForPiece(whitePiecePrefab, blackPiecePrefab, startWhileLocationsTest, startBlackLocationsTest, this);
    }

    public virtual bool CheckTest(GameObject tile) {
        if (TeachingManager.instance.currentPiece != null)
        {
            List<Vector2Int> locations = whitePiecePrefab.GetComponent<Piece>().MoveLocations(startWhileLocationsTest[0]);
            return locations.Contains(Geometry.GridFromPoint(tile.transform.position));
        } else
        {
            return false;
        }
    }

}
