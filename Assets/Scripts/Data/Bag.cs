using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// entity for bag 

// question id 
    // code 
    // bag id 
    // random item id - TODO future 

public class Bag
{
    public int m_BagId;
    public int m_QuestionId;
    public string m_Code;
    
    public Bag(int bid, int qid, string code){
        m_BagId = bid;
        m_QuestionId = qid;
        m_Code = code;
    }

    
}
