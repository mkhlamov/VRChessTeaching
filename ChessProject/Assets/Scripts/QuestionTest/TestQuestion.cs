using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestQuestion {

    public string question;
    public List<TestAnswer> answers;
    public TestAnswer chosenAnswer;

    public bool UserAnsweredCorrectly()
    {
        return chosenAnswer.isRight;
    }

}

[System.Serializable]
public class TestAnswer {
    //public TestQuestion question;
    public string text;
    public bool isRight;
}
