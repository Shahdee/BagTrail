using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : WinViewBase
{
    public CommonButton m_BtnStart;
    public Text m_TxtName;

    protected override void InInit(){

        m_BtnStart.m_OnBtnClickClbck += StartClick;
        
    }

    protected override WinControllerBase CreateController(){

        var controller = new WinMenuController(m_MainLogic, this);
        return controller;
    }

    void StartClick(GUIButtonBase btn){
        (m_Controller as WinMenuController).SendStart();
    }
}
