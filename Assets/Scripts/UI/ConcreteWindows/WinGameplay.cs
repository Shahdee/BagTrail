using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO future - animate 2 bags 

public class WinGameplay : WinViewBase
{
    public UIBag m_CurrBag;
    public UIBag m_PrevBag;
    public UIQuestion m_Question;
    public UILock m_Lock;
    public UIProgress m_Progress;
    public Text m_UnlockedBags;

    protected override void InInit(){

        m_MainLogic.GetLevelLogic().AddWaitForBagStartIsUpListener(OnWaitBagStart);
        m_MainLogic.GetLevelLogic().AddBagStartUnlockListener(OnBagUnlockStart);
        m_MainLogic.GetLevelLogic().AddBagUnlockTimeIsUpListener(OnBagUnlockTimeIsUp);
        m_MainLogic.GetLevelLogic().AddBagUnlockListener(OnBagUnlocked);
        m_MainLogic.GetLevelLogic().AddBagUnlockFailListener(OnBagUnlockAttemptFailed);
        
        m_Progress.Init();
        m_Lock.m_BtnTry.m_OnBtnClickClbck += TryClick;
    }

    protected override WinControllerBase CreateController(){
        return new WinGameplayController(m_MainLogic, this);;
    }

    protected override void OnShow(){

        SetUnlockedBags();

        ResetUI();
    }

    void ResetUI(){
        m_Lock.Show(false);
        m_PrevBag.Show(false);
        m_Progress.Show(false);
        m_Question.Show(false);
    }

    void SetUnlockedBags(){
        m_UnlockedBags.text = m_MainLogic.GetLevelLogic().GetUnlockedBags().ToString();
    }

    protected override void OnHide(){

    }

    void TryClick(GUIButtonBase btn){

        string pin = m_Lock.GetPin();

        // Debug.Log("Try " + pin);

        (m_Controller as WinGameplayController).SendTryUnlock(pin);
    }

    // bag appears on screen
    void OnWaitBagStart(){

        // Debug.Log("OnWaitBagStart");
        
        m_CurrBag.Show(true);
        m_CurrBag.SetBagState(UIBag.BagState.Locked);
    }


    // you have limited time to try to unlock the bag 
    void OnBagUnlockStart(){

        // Debug.Log("OnBagUnlockStart");

        ShowQuestion();

        m_Progress.Show(true);
        m_Lock.Show(true);

        SetAttemptTimer(true);
    }

    void ShowQuestion(){
        var bag = m_MainLogic.GetLevelLogic().GetCurrentBag();
        var question = m_MainLogic.GetLevelLogic().GetCurrentQuestion();

        m_Question.Show(true);
        m_Question.SetHint(question.m_QuestionHint);
    }

    bool m_AttemptTimer = false;
    float m_CurrentAtemptTimer = 0;

    void SetAttemptTimer(bool start){
        m_AttemptTimer = start;
        if (start)
            m_CurrentAtemptTimer = LevelLogic.GetUnlockAttempTime();
    }

    static float c_MoveNextTime = 2f;
    float m_CurrMoveNextTime = 0;

    bool m_MoveNextTimer = false;

    void SetMoveNextTimer(){
        m_MoveNextTimer = true;
        m_CurrMoveNextTime = c_MoveNextTime;
    }

    // you lost!
    void OnBagUnlockTimeIsUp(){
        m_CurrBag.SetBagState(UIBag.BagState.Blocked);
        m_Lock.Show(false);

        SetAttemptTimer(false);
        SetMoveNextTimer();
    }

    // you won!
    void OnBagUnlocked(){

        m_CurrBag.SetBagState(UIBag.BagState.Unlocked);
        SetAttemptTimer(false);

        SetUnlockedBags();

        SetMoveNextTimer();
    }

    void OnBagUnlockAttemptFailed(){
        m_CurrBag.ShowFail(true);
    }

    public override void UpdateMe(float deltaTime){

        UpdateProgress(deltaTime);
    }

    void AskForNext(){
        ResetUI();

        (m_Controller as WinGameplayController).SendMoveNext();
    }

    void UpdateProgress(float deltaTime){

        if (m_AttemptTimer){

            m_CurrentAtemptTimer -= deltaTime;

            m_Progress.SetProgress(m_CurrentAtemptTimer/LevelLogic.GetUnlockAttempTime());

            if (m_CurrentAtemptTimer < 0){

                m_AttemptTimer = false;
            }
        }

        if (m_MoveNextTimer){

            m_CurrMoveNextTime -= deltaTime;

            if (m_CurrMoveNextTime < 0){

                m_MoveNextTimer = false;

                AskForNext();
            }
        }

        m_CurrBag.UpdateMe(deltaTime);
    }
}
