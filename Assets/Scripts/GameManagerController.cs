using UnityEngine;
using System.Collections;

public class GameManagerController : MonoBehaviour {
	public GameObject Camera;
	public GameObject Controller;
	public int PlayerNo = 1;
	public GameObject[] Player;

	private int maxPlayer = 0;

	enum Players {knight =1, wizard=2}; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (checkPlayer ()) {
			setPlayerNo ();
			Camera.GetComponent<ThirdPersonCamera> ().setToPlayer (PlayerNo);
			Controller.GetComponent<PlayerContorller> ().setToPlayer (PlayerNo);
		} 
		else {
			
		}
	}

	bool checkPlayer()
	{
		if (GameObject.FindGameObjectsWithTag ("Player") != null) {
			return true;
		}
		else
			return false;
	}

	void setPlayerNo()
	{
		switch (PlayerNo) {
		case (int)Players.knight:
			if (Player [0] != null) {
				Player [0].GetComponent<PlayerMoveScript> ().AIMode = false;
				Player [0].GetComponent<KnightActionScript> ().AutoAtk = false;
			}
			if (Player [1] != null) {
				Player [1].GetComponent<PlayerMoveScript> ().AIMode = true;
				Player [1].GetComponent<WizardActionScript> ().AutoAtk = true;
			}
			break;
		case (int)Players.wizard:
			if (Player [0] != null) {
				Player [0].GetComponent<PlayerMoveScript> ().AIMode = true;
				Player [0].GetComponent<KnightActionScript> ().AutoAtk = true;
			}
			if (Player [1] != null) {
				Player [1].GetComponent<PlayerMoveScript> ().AIMode = false;
				Player [1].GetComponent<WizardActionScript> ().AutoAtk = false;
			}
			break;
		}
	}
}
