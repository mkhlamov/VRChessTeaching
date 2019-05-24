using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private int rightAnswersCount;
    [SerializeField]
    private GameObject resultsUI;
    [SerializeField]
    private Text resultText;
    [SerializeField]
    private GameObject nextQuestionGO;
    [SerializeField]
    private GameObject startTestGO;
    [SerializeField]
    private Text startTestText;
    [SerializeField]
    private GameObject goToTeachingGO;
    [SerializeField]
    private GameObject taskTextGO;

    private void Start()
    {
        nextQuestionGO.SetActive(false);
        startTestGO.SetActive(true);
        goToTeachingGO.SetActive(false);
        resultsUI.SetActive(false);
        taskTextGO.SetActive(false);
    }

    public void StartTest() {
        state = State.Start;
        TeachingManager.instance.small.enabled = false;
        nextQuestionGO.SetActive(false);
        startTestGO.SetActive(false);
        goToTeachingGO.SetActive(false);
        resultsUI.SetActive(false);
        taskTextGO.SetActive(true);

        questions.Clear();
        foreach (Transform t in gameObject.transform)
        {
            TeachingPiece tp = t.GetComponent<TeachingPiece>();
            if (t.gameObject.activeSelf && tp != null)
            {
                questions.Add(tp);
            }
        }
        questions.Shuffle();
        currentQuestion = 0;
        rightAnswersCount = 0;
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
                    //selectedPiece.transform.position = new Vector3(point.x, 0f, point.z);
                    selectedPiece.transform.position = Geometry.PointFromGrid(Geometry.GridFromPoint(new Vector3(point.x, 0f, point.z)));
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
        rightAnswersCount += checkRes ? 1 : 0;
        TeachingManager.instance.small.enabled = true;
        TeachingManager.instance.small.text = checkRes ? "Верный ответ" : "Неверный ответ";
        nextQuestionBt.SetActive(true);
        currentQuestion++;
        state = State.Moved;
        nextQuestionGO.SetActive(true);
        if (currentQuestion == questions.Count)
        {
            TestEnd();
        }
    }

    private void ShowNextQuestion()
    {
        if (currentQuestion < questions.Count)
        {
            TeachingManager.instance.small.enabled = false;
            nextQuestionBt.SetActive(false);
            questions[currentQuestion].MakeTest();
            state = State.Start;
            nextQuestionGO.SetActive(false);
        }
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

    public void TestEnd()
    {
        nextQuestionBt.SetActive(false);
        resultsUI.SetActive(true);
        taskTextGO.SetActive(false);

        resultText.text = "Результаты: " + rightAnswersCount.ToString() + " из " + questions.Count + " ответов." + "\n";

        float rightAnswerPercentage = (float)rightAnswersCount / (float)questions.Count;
        if (rightAnswerPercentage < 0.5)
        {
            resultText.text = resultText.text + "Слабовательно. Давай еще раз.";
            startTestText.text = "Начать заново";
            startTestGO.SetActive(true);
        }
        else if (rightAnswerPercentage < 0.8)
        {
            resultText.text = resultText.text + "Неплохо, но надо лучше.";
            startTestText.text = "Начать заново";
            startTestGO.SetActive(true);
        }
        else
        {
            resultText.text = resultText.text + "Сойдет. Поздравляю, вы сдали тесты.";
            startTestGO.SetActive(true);
            startTestText.text = "Начать заново";
            goToTeachingGO.SetActive(true);
        }

    }
}
