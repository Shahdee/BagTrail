using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

	// Bag 
	// 	animated 
	// 	win/lose condition 

public class UIBag : UIObject
{
    public RectTransform m_RTrsBag;
	public GameObject m_UnlockResult;
	public Text m_UnlockResultMessage;

	public enum BagState{
		Locked,
		Unlocked,
		Blocked // when we were unable to unlock it on time -> it's blocked forever 
	}

	BagState m_PrevBagState;
	BagState m_BagState;
	static string c_MessYes = "Yeah! ^_^";
	static string c_MessNo = "Nope.. -.-";
	static string c_MessFail = "Wrong pin.. >_>";

	public void SetBagState(BagState state){

		m_BagState = state;

		ShowFail(false);

		switch(m_BagState){
			case BagState.Locked:
				m_UnlockResult.SetActive(false);
			break;

			case BagState.Unlocked:
				m_UnlockResult.SetActive(true);
				m_UnlockResultMessage.text = c_MessYes;
			break;

			case BagState.Blocked:
				m_UnlockResult.SetActive(true);
				m_UnlockResultMessage.text = c_MessNo;
			break;
		}
	}

	bool m_AnimateFail = false;

	static float c_FailTime = 1.5f;
	float m_CurrFailTime = 0;

	public void ShowFail(bool start){
		m_AnimateFail = start;

		m_UnlockResult.SetActive(start);
		if (start){
			m_CurrFailTime = c_FailTime;
			m_UnlockResultMessage.text = c_MessFail;
		}
	}

	public void UpdateMe(float deltaTime){

		if (m_AnimateFail){

            m_CurrFailTime -= deltaTime;

            if (m_CurrFailTime < 0){

                ShowFail(false);
            }
        }
	}
}
