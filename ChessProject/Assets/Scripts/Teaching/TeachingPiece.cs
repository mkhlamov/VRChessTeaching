using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeachingPiece : MonoBehaviour {

    public string description;
    public string descriptionTest;
    public List<Vector2Int> startWhileLocations;
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
        TeachingManager.instance.SetupForPiece(whitePiecePrefab, blackPiecePrefab, startWhileLocations, startBlackLocations, this);
        

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

        TeachingManager.instance.SetupForPiece(whitePiecePrefab, blackPiecePrefab, startWhileLocationsTest, startBlackLocationsTest, this);
    }

    public virtual bool CheckTest(GameObject tile) {
        return false;
    }

}
