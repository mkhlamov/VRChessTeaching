using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Start, FigureChosen, Moved };

public class InteractiveTest : MonoBehaviour {

    //child objects are questions
    //there are tests for some figures
    public List<TeachingPiece> questions;
    public LayerMask layerMask;
    public Transform playerHead;
    public GameObject nextQuestionBt;

    [SerializeField]
    private State state = State.Start;
    private GameObject selectedPiece = null;
    private int currentQuestion;
    private int currentRightAnswers;

    private void Start()
    {
        state = State.Start;
        foreach (Transform t in gameObject.transform)
        {
            TeachingPiece tp = t.GetComponent<TeachingPiece>();
            if (tp != null)
            {
                questions.Add(tp);
            }
        }
        questions.Shuffle();
        currentQuestion = 0;
        questions[currentQuestion].MakeTest();
    }

    private void Update()
    {
        Ray ray = new Ray(playerHead.position, playerHead.forward);

        RaycastHit hit;
        if (state == State.FigureChosen)
        {
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.gameObject.CompareTag("ChessBoard"))
                {
                    Vector3 point = hit.point;
                    selectedPiece.transform.position = new Vector3(point.x, 0f, point.z);
                }
            }
        }
    }

    private void MoveFigure(GameObject tile)
    {
        Vector2Int gridPoint = Geometry.GridFromPoint(tile.transform.position);
        selectedPiece = TeachingManager.instance.PieceAtGrid(gridPoint);
        if (selectedPiece != null)
        {
            state = State.FigureChosen;
        }

    }

    private void CheckAnswer(GameObject tile)
    {
        bool checkRes = TeachingManager.instance.currentPiece.CheckTest(tile);
        TeachingManager.instance.small.enabled = true;
        TeachingManager.instance.small.text = checkRes ? "Верный ответ" : "Неверный ответ";
        nextQuestionBt.SetActive(true);
        state = State.Moved;
    }

    private void ShowNextQuestion()
    {
        TeachingManager.instance.small.enabled = false;
        nextQuestionBt.SetActive(false);
        questions[currentQuestion].MakeTest();
        state = State.Start;
    }

    public void NextState(GameObject tile)
    {
        if (state == State.Start)
        {
            MoveFigure(tile);
        }
        else if (state == State.FigureChosen)
        {
            CheckAnswer(tile);
        }
        else if (state == State.Moved) {
            if (tile.GetComponent<InteractiveTile>() == null)
            {
                ShowNextQuestion();
            }
        }
    }
}
