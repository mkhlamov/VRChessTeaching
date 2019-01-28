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
        }

        rightAnswersCount = 0;
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

            answer.SetActive(true);
            StartCoroutine(SetColliderSize(collider, image));

            //collider.size = new Vector3(image.rect.width, image.rect.height, 0.01f);
        }
    }

    IEnumerator SetColliderSize(BoxCollider c, RectTransform t)
    {
        //размеры изображений рассчитываются после того, как все объекты answer добавлены,
        // поэтому надо ждать конца кадра, чтобы у коллайдеров были правильные размеры.
        yield return new WaitForEndOfFrame();

        c.size = new Vector3(t.rect.width, t.rect.height, 0.01f);
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
        if (ta.isRight)
        {
            rightAnswersCount += 1;
        }

        if (currentQuestion < questions.Count - 1)
        {
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

        resultText.text = "Результаты: " + rightAnswersCount.ToString() + " из " + questions.Count + " ответов." + "\n";

        float rightAnswerPercentage = (float)rightAnswersCount / (float)questions.Count;
        if (rightAnswerPercentage < 0.5)
        {
            resultText.text = resultText.text + "Слабовательно. Давай еще раз.";
            startTest.text = "Начать заново";
        } else if (rightAnswerPercentage < 0.8)
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
