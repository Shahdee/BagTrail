using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIQuestion : UIObject
{
    public Image m_Icon;
    public Text m_Hint;

    public void SetHint(string hint){
        m_Hint.text = hint;
    }

    
    
}
