using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    public List<TestQuestion> questions;

    public GameObject questionsUI;
    public Text questionText;
    public GameObject answers;
    public GameObject resultsUI;
    public Text resultText;
    public Text startTest;
    public GameObject visualTest;

    public GameObject answerPrefab;
    public int currentQuestion;
    public int rightAnswersCount;

    public void StartTest()
    {
        questionsUI.SetActive(true);
        resultsUI.SetActive(false);
        visualTest.SetActive(false);

        questions.Shuffle();
        foreach (TestQuestion q in questions)
        {
            q.answers.Shuffle();
            Debug.Log(q.question);
        }

        currentQuestion = 0;
        showQuestion(currentQuestion);
    }

    public void showQuestion(int questionNum)
    {
        ClearQuestion();
        TestQuestion tq = questions[questionNum];
        questionText.text = tq.question;
        foreach (TestAnswer a in tq.answers)
        {
            GameObject answer = Instantiate(answerPrefab, answers.transform);
            TestAnswerUI aUI = answer.GetComponent<TestAnswerUI>();
            aUI.isRight = a.isRight;
            answer.GetComponentInChildren<Text>().text = a.text;
            
            BoxCollider collider = answer.GetComponent<BoxCollider>();
            RectTransform image = answer.transform.GetChild(0).transform.GetComponent<RectTransform>();

            Debug.Log("collider size: " + answer.transform.GetComponent<RectTransform>().rect.width + " " +
                                (answer.transform.GetComponent<RectTransform>().rect.height).ToString());
            Debug.Log("image size: " + image.rect.width + " " + image.rect.height);


            collider.size = new Vector3(answer.transform.GetComponent<RectTransform>().rect.width - image.rect.width,
                                        answer.transform.GetComponent<RectTransform>().rect.height - image.rect.height, 
                                        0.01f);

            answer.SetActive(true);
        }
    }

    public void ClearQuestion()
    {
        questionText.text = "";
        for (int i = 1; i < answers.transform.childCount; i++)
        {
            Destroy(answers.transform.GetChild(i).gameObject);
        }
    }

    public void NextQuestion(TestAnswerUI ta)
    {
        if (currentQuestion < questions.Count - 1)
        {
            if (ta.isRight)
            {
                rightAnswersCount += 1;
            }
            currentQuestion++;
            showQuestion(currentQuestion);
        } else
        {
            TestEnd();
        }
    }

    public void TestEnd()
    {
        questionsUI.SetActive(false);
        resultsUI.SetActive(true);

        resultText.text = "Результаты: " + rightAnswersCount.ToString() + "правильных ответов из " + questions.Count + "\n";

        if (rightAnswersCount / questions.Count < 0.5)
        {
            resultText.text = resultText.text + "Слабовательно. Давай еще раз.";
            startTest.text = "Начать заново";
        } else if (rightAnswersCount / questions.Count < 0.8)
        {
            resultText.text = resultText.text + "Неплохо, но надо лучше.";
            startTest.text = "Начать заново";
        } else
        {
            resultText.text = resultText.text + "Сойдет. Можете перейти к визуальному тесту.";
            visualTest.SetActive(true);
        }

    }
}

static class Shuffler
{
    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
