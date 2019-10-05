using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    Dictionary<Bag, Question> m_Bags;
    List<int> m_BagOrder;

    public Level(Dictionary<Bag, Question> bags, List<int> bagOrder){
        m_Bags = bags;
        m_BagOrder = bagOrder;
    }

    public int GetBagsCount(){
        return m_BagOrder.Count;
    }

    public int GetBagId(int bagIndex){
        if (bagIndex >= m_BagOrder.Count)
            return -1;
        else
            return m_BagOrder[bagIndex];
    }

    public Bag GetBag(int bagId){
        var iter = m_Bags.GetEnumerator();
        while(iter.MoveNext()){
            if (iter.Current.Key.m_BagId == bagId)
                return iter.Current.Key;
        }
        return null;
    }

    public Question GetQuestion(int questionId){
        var iter = m_Bags.GetEnumerator();
        while(iter.MoveNext()){
            if (iter.Current.Value.m_QuestionId == questionId)
                return iter.Current.Value;
        }
        return null;
    }
}
