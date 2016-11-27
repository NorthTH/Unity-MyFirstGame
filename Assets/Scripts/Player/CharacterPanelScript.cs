using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CharacterPanelScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler  {

	private bool touched;
	private int pointerID;

	public int PlayerNo = 1;

	void Awake(){
		touched = false;
	}
	
	public void OnPointerDown (PointerEventData data){
		//Set our start point
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			if (GameManagerController.Instance.GetPlayerNo () != PlayerNo)
				GameManagerController.Instance.changePlayer ();
		}
	}

	public void OnPointerUp (PointerEventData data){
		//Reset Everything
		if (data.pointerId == pointerID) {
			touched = false;
		}
	}
}
