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
    public GameObject answerPrefab;
    public int currentQuestion;

    public void StartTest()
    {
        questionsUI.SetActive(true);
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
            TestAnswerUI aUI = answer.AddComponent<TestAnswerUI>();
            aUI.isRight = a.isRight;
            answer.GetComponentInChildren<Text>().text = a.text;
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

    public void NextQuestion()
    {
        if (currentQuestion < questions.Count - 1)
        {
            currentQuestion++;
            showQuestion(currentQuestion);
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
