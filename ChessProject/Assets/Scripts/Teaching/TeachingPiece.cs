using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeachingPiece : MonoBehaviour {

    public string description;
    public List<Vector2Int> startWhileLocations;
    public List<Vector2Int> startBlackLocations;
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
}
