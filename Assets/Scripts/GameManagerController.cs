using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerController : SingletonMonoBehaviour<GameManagerController> {
	public GameObject Camera;
	public GameObject Controller;
	public int PlayerNo = 1;
	public GameObject[] Player;

	public GameObject[] PlayerHP;

	private bool gameover = false;

	private bool IsVictory = false;

	private int maxPlayer = 0;

	enum Players {knight =1, wizard=2}; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (checkPlayer ()) {
			if (IsVictory) {
				Victory ();
			}
			setPlayerNo ();
			Camera.GetComponent<ThirdPersonCamera> ().setToPlayer (PlayerNo);
			Controller.GetComponent<PlayerContorller> ().setToPlayer (PlayerNo);
		} 
		else{
			GameOver ();
		}
	}

	bool checkPlayer()
	{
		if (PlayerHP [0].GetComponent<Image> ().fillAmount > 0 || PlayerHP [1].GetComponent<Image> ().fillAmount > 0) {
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

	public void SetVictory()
	{
		IsVictory = true;
	}

	private void Victory()
	{
		var enemys = GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject enemy in enemys) {
			Destroy (enemy.gameObject);
		}

		GameObject.Find("GameOverText").GetComponent<Text>().text = "You Are Win!!";
	}
		
	private void GameOver()
	{
		gameover = true;

		GameObject.Find("GameOverText").GetComponent<Text>().text = "Game Over";
	}

	public void changePlayer()
	{
		PlayerNo = (PlayerNo == 1) ? 2 : 1;
	}

	public void changeToPlayer1()
	{
		PlayerNo = 1;
	}

	public void changeToPlayer2()
	{
		PlayerNo = 2;
	}

	public int  GetPlayerNo()
	{
		return PlayerNo;
	}

	public bool chackGameOver()
	{
		return gameover;
	}
}
