using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

// TODO future - write to player profile that certain bag was unlocked 

public class LevelLogic: IUpdatable
{
    MainLogic m_MainLogic;

    UnityAction<int> m_OnLevelStartCallback;
    UnityAction m_OnBagStartUnlockCallback;
    UnityAction m_OnBagUnlockTimeIsUpCallback;
    UnityAction m_WaitForBagStartCallback;
    UnityAction m_BagUnlockCallback;
    UnityAction m_BagUnlockFailCallback;

    int m_CurrentLevel = 0;
    int m_CurrentBagIndex = 0;
    int m_UnlockedBags = 0;
    Bag m_CurrentBag = null; 
    Question m_CurrentQuestion = null;
    static float c_TimeBeforeUnlock = 0.5f;
    float m_CurrentBeforeUnlockTime = 0;
    bool m_WaitForBagTimer = false;
    static float c_TimeToAttemptUnlock = 20f;
    float m_CurrentTimeToAttemptUnlock = 0;
    bool m_UnlockTimer = false;

    public static float GetUnlockAttempTime(){
        return c_TimeToAttemptUnlock;
    }

    public static float GetBeforeUnlockTime(){
        return c_TimeBeforeUnlock;
    }

#region Callbacks
    public void AddLevelStartListener(UnityAction<int> listener){
        m_OnLevelStartCallback += listener;
    }

    public void RemoveLevelStartListener(UnityAction<int> listener){
        m_OnLevelStartCallback -= listener;
    }

    public void OnLevelStart(int level){
        if (m_OnLevelStartCallback != null)
            m_OnLevelStartCallback(level);
    }

     // Wait for bag start
    public void AddWaitForBagStartIsUpListener(UnityAction listener){
        m_WaitForBagStartCallback += listener;
    }

    public void RemoveWaitForBagStartListener(UnityAction listener){
        m_WaitForBagStartCallback -= listener;
    }

    public void OnWaitForBagStart(){
        if (m_WaitForBagStartCallback != null)
            m_WaitForBagStartCallback();
    }

    // Bag start unlock 
    public void AddBagStartUnlockListener(UnityAction listener){
        m_OnBagStartUnlockCallback += listener;
    }

    public void RemoveBagStartUnlockListener(UnityAction listener){
        m_OnBagStartUnlockCallback -= listener;
    }

    public void OnBagStartUnlockStart(){
        if (m_OnBagStartUnlockCallback != null)
            m_OnBagStartUnlockCallback();
    }

    // Bag unlock time is up
    public void AddBagUnlockTimeIsUpListener(UnityAction listener){
        m_OnBagUnlockTimeIsUpCallback += listener;
    }

    public void RemoveBagUnlockTimeIsUpListener(UnityAction listener){
        m_OnBagUnlockTimeIsUpCallback -= listener;
    }

    public void OnBagUnlockTimeIsUp(){
        if (m_OnBagUnlockTimeIsUpCallback != null)
            m_OnBagUnlockTimeIsUpCallback();
    }

     // Bag unlocked
    public void AddBagUnlockListener(UnityAction listener){
        m_BagUnlockCallback += listener;
    }

    public void RemoveBagUnlockListener(UnityAction listener){
        m_BagUnlockCallback -= listener;
    }

    public void OnBagUnlock(){
        if (m_BagUnlockCallback != null)
            m_BagUnlockCallback();
    }

    // m_BagUnlockFailCallback

    public void AddBagUnlockFailListener(UnityAction listener){
        m_BagUnlockFailCallback += listener;
    }

    public void RemoveBagUnlockFailListener(UnityAction listener){
        m_BagUnlockFailCallback -= listener;
    }

    public void OnBagUnlockFail(){
        if (m_BagUnlockFailCallback != null)
            m_BagUnlockFailCallback();
    }

#endregion

    public LevelLogic(MainLogic main){

        m_MainLogic = main;

    }

    public void StartLevel(int level){

        m_CurrentLevel = level;

        OnLevelStart(m_CurrentLevel);
        
        m_CurrentBagIndex = 0;
        SetCurrentData(m_CurrentBagIndex);
        StartNextBag(m_CurrentBagIndex);
    }

    public void TryUnlock(string code){
        if (CanUnlockBag(code)){
            SetUnlockBagTimer(false);

            m_UnlockedBags++;

            OnBagUnlock();
        }
        else
            OnBagUnlockFail();
    }

    public int GetUnlockedBags(){
        return m_UnlockedBags;
    }

    // TODO future - can be bag or level 
    public void MoveNext(){
        m_CurrentBagIndex ++;

        // if we are beyond bags, go back to 1st one 
        var data = m_MainLogic.GetDataLoader().GetData();
        var levelData = data.GetLevel();
        if (m_CurrentBagIndex >= levelData.GetBagsCount())
            m_CurrentBagIndex = 0;

        StartNextBag(m_CurrentBagIndex);
    }

    void StartNextBag(int bagIndex){
        SetCurrentData(bagIndex);
        SetWaitBagTimer();

        OnWaitForBagStart();
    }

    void SetCurrentData(int bagIndex){
        var data = m_MainLogic.GetDataLoader().GetData();
        var levelData = data.GetLevel();

        int bagId = levelData.GetBagId(bagIndex);
        m_CurrentBag = levelData.GetBag(bagId);
        m_CurrentQuestion = levelData.GetQuestion(m_CurrentBag.m_QuestionId);
    }

    void SetWaitBagTimer(){
        m_WaitForBagTimer = true;
        m_CurrentBeforeUnlockTime = c_TimeBeforeUnlock;
    }

    void SetUnlockBagTimer(bool startTimer){
        m_UnlockTimer = startTimer;
        
        if (startTimer)
            m_CurrentTimeToAttemptUnlock = c_TimeToAttemptUnlock;
    }

    public bool CanUnlockBag(string code){
        return (m_CurrentBag.m_Code.Equals(code));
    }

    public Bag GetCurrentBag(){
        return m_CurrentBag;
    }

    public Question GetCurrentQuestion(){
        return m_CurrentQuestion;
    }

    public void UpdateMe(float deltaTime){

        UpdateTimers(deltaTime);

    }

    void UpdateTimers(float deltaTime){

        if (m_WaitForBagTimer){
            m_CurrentBeforeUnlockTime -= deltaTime;
            if (m_CurrentBeforeUnlockTime < 0){

                m_WaitForBagTimer = false;

                OnBagStartUnlockStart();
                SetUnlockBagTimer(true);
            }
        }

        if (m_UnlockTimer){
            m_CurrentTimeToAttemptUnlock -= deltaTime;
            if (m_CurrentTimeToAttemptUnlock < 0){

                m_UnlockTimer = false;

                OnBagUnlockTimeIsUp();
            }
        }
    }
}
