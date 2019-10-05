using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

	// Lock 
	// 	pad with numbers 
	// 		can swipe to change number 
	// 		produce sound when swiped 
	// 	button try 

public class UILock : UIObject
{
    public GUIButtonBase m_BtnTry;
	public List<UIPad> m_Pads; 
	public InputField m_Input;

	public string GetPin(){
		return m_Input.text;
	}
}
