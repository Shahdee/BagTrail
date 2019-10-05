using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgress : UIObject
{
    public Image m_Back;
    public Image m_Front;
    public RectTransform m_RTrsFront;

    float m_FrontWidth;

    public void Init(){
        m_FrontWidth = m_RTrsFront.sizeDelta.x;
    }

    Vector2 m_VecTempSize;

    public void SetProgress(float value){
        m_VecTempSize = m_RTrsFront.sizeDelta;
        m_VecTempSize.x = m_FrontWidth * Mathf.Clamp01(value);
        m_RTrsFront.sizeDelta = m_VecTempSize;
    }
}
