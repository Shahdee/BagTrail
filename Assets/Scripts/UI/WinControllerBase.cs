using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// concrete view instantiate concrete controller 

public class WinControllerBase
{
    protected MainLogic m_MainLogic;
    protected WinViewBase m_View;

    public WinControllerBase(){
        
    }

    public WinControllerBase(MainLogic lg, WinViewBase view){
        m_MainLogic = lg;
        m_View = view;
    }

    
   
}
