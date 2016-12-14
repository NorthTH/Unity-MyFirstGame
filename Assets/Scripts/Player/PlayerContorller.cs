using UnityEngine;
using System.Collections;

public class PlayerContorller : MonoBehaviour {

	private GameObject Player;
	private int PlayerNumber = 1;

	private Vector2 startPos;
	private Vector2 direction;
	private Vector2 endPos;

	private float clickTime;
	private float distance;

	private float offset = 90.0f;

	// Use this for initialization
	void Start () {
		setToPlayer (PlayerNumber);
		clickTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Player != null) {
			if (Input.GetMouseButtonDown (0))
				BeginClick ();
			if (Input.GetMouseButtonUp (0))
				EndClick ();
			if (Input.GetMouseButton (0))
				TrackingClick ();
		}
	}

	public void setToPlayer(int No){
		switch (No) {
		case 1:
			this.Player = GameObject.Find ("Knight");
			PlayerNumber = 1;
			break;
		case 2:
			this.Player = GameObject.Find ("Wizard");
			PlayerNumber = 2;
			break;
		default:
			break;
		}
	}

	private void BeginClick(){
		startPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		//Debug.Log ("startPos :" + startPos.x + " " + startPos.y);
	}

	private void EndClick(){
		endPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Player.GetComponent<PlayerMoveScript> ().Speed = 0.0f;

		if (clickTime <= 0.5f) {
		//ドラックがしないこと確認し攻撃行う。
		//if (distance <= 2.0f) {
			switch (PlayerNumber) {
			case 1:
				Player.GetComponent<KnightActionScript> ().Attack ();
				break;
			case 2:
				Player.GetComponent<WizardActionScript> ().Attack ();
				break;
			}
		}
		//値リセット
		clickTime = 0.0f;
		distance = 0.0f;
	}

	private void TrackingClick(){
		clickTime += Time.deltaTime;
		Vector2 currentPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

		distance = Vector2.Distance (currentPos, startPos) / 50.0f;
		if (distance >= 1.5f) {
			Player.GetComponent<PlayerMoveScript> ().Speed = distance;
			direction = currentPos - startPos;
			float sign = (currentPos.y < startPos.y) ? -1.0f : 1.0f;
			float angle = (Vector2.Angle (Vector2.left, direction) * sign) - offset + Camera.main.transform.eulerAngles.y;
			angle = (angle > -180.0f) ? angle : angle + 360.0f;
			//Player.transform.rotation = Quaternion.Euler(Player.transform.rotation.x, angle, Player.transform.rotation.z);
			Player.GetComponent<PlayerMoveScript> ().turn(angle);
		}
	}
}
