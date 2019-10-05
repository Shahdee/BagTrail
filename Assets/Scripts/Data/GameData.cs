using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to get data from json 

[System.Serializable]
public class GameData
{
#region Parsing

    [System.Serializable]
    public class LevelBag{
        public int bagID;
        public int questionID;
        public string code;
    }

    [System.Serializable]
    public class BagQuestion{
        public int questionID;
        public string hint;
    }

    public List<int> levelSequence; 
    public List<LevelBag> bags;
    public List<BagQuestion> questions;

    // public List<PatternData> patternSequence;
    public void Print(){
        string toprint = "";

        for (int i=0; i<levelSequence.Count; i++)
            Debug.Log(levelSequence[i]);

        for (int i=0; i<bags.Count; i++){
            toprint = bags[i].bagID + " / " + bags[i].questionID + " / " + bags[i].code;
            Debug.Log(toprint);
        }

        for (int i=0; i<questions.Count; i++){
            toprint = questions[i].questionID + " / " + questions[i].hint;
            Debug.Log(toprint);
        }
    }
#endregion

    Level m_Level;
    List<Bag> m_UniqueBags;
    List<Question> m_UniqueQuestions;

    // maps data retrieved from JSON to internal classes 
    public void Map(){

        Bag bag = null;
        Question question = null;

        m_UniqueBags = new List<Bag>();

        for (int j=0; j<bags.Count; j++){
            bag = new Bag(bags[j].bagID, bags[j].questionID, bags[j].code);
            m_UniqueBags.Add(bag);
        }

        m_UniqueQuestions = new List<Question>();

        for (int j=0; j<questions.Count; j++){
            question = new Question(questions[j].questionID, questions[j].hint);
            m_UniqueQuestions.Add(question);
        }

#region Level
        var levelBags = new Dictionary<Bag, Question>();
        for (int i=0; i<levelSequence.Count; i++){
            bag = GetBag(levelSequence[i]);
            if (bag != null){
                question = GetQuestion(bag.m_QuestionId);
                if (question != null){
                    levelBags.Add(bag, question);
                }
            }
        }

        m_Level = new Level(levelBags, levelSequence);
#endregion
    }

    public List<Bag> GetUniqueBags(){
        return m_UniqueBags;
    }

    Bag GetBag(int bagId){
        for (int j=0; j<m_UniqueBags.Count; j++){
            if (m_UniqueBags[j].m_BagId == bagId)
                return m_UniqueBags[j];
        }
        return null;
    }

    public List<Question> GetUniqueQuestions(){
        return m_UniqueQuestions;
    }

    Question GetQuestion(int questionID){
        for (int j=0; j<m_UniqueQuestions.Count; j++){
            if (m_UniqueQuestions[j].m_QuestionId == questionID)
                return m_UniqueQuestions[j];
        }
        return null;
    }

    public Level GetLevel(){
        return m_Level;
    }
}

// Example 

// [System.Serializable]
// public class PlayerInfo
// {
//     public List<ActData> data;
//     public int status;
// }

// [System.Serializable]
// public class ActData
// {
//     public int id;
//     public string layoutLabel;
//     public int hasCustomProb;
// }


// {"data":[{"id":141,"layoutLabel":"Sameer","hasCustomProb":1},
// {"id":214,"layoutLabel":"abc","hasCustomProb":0}],"status":200}


// "patternSequence": [
// 		{"patternID": 0, "enemyID": [0,0,0,0]},
// 		{"patternID": 1, "enemyID": [1,1,1,1]}
// ]