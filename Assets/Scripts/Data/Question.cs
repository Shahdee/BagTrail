using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question 
{
    public int m_QuestionId;
    public string m_QuestionHint;

    public Question(int qid, string hint){
        m_QuestionId = qid;
        m_QuestionHint = hint;
    }
}
