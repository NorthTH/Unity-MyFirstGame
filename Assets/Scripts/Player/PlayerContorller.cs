using UnityEngine;
using System.Collections;

public class PlayerContorller : MonoBehaviour {

	private GameObject Player;
	private int PlayerNumber = 1;

	private Vector2 startPos;
	private Vector2 direction;
	private Vector2 endPos;

	private float clickTime;

	private float offset = 90.0f;

	// Use this for initialization
	void Start () {
		this.Player = GameObject.FindGameObjectWithTag ("Player" + PlayerNumber);
		clickTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetMouseButtonDown(0) ) BeginClick();
		if( Input.GetMouseButtonUp(0) ) EndClick();
		if( Input.GetMouseButton(0) ) TrackingClick();
	}

	public void setToPlayer(int No){
		this.Player = GameObject.FindGameObjectWithTag ("Player" + No);
	}

	private void BeginClick(){
		startPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		//Debug.Log ("startPos :" + startPos.x + " " + startPos.y);
	}

	private void EndClick(){
		endPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Player.GetComponent<PlayerMoveScript> ().Speed = 0.0f;

		if (clickTime < 0.3f) {
			switch (this.Player.tag)
			{
			case "Player1":
				Player.GetComponent<KnightActionScript> ().Attack ();
				break;
			case "Player2":
				Player.GetComponent<WizardActionScript> ().Attack ();
				break;
			}
		}

		clickTime = 0.0f;
	}

	private void TrackingClick(){
		clickTime += Time.deltaTime;
		Vector2 currentPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

		float distance = Vector2.Distance (currentPos, startPos) / 50.0f;
		Player.GetComponent<PlayerMoveScript> ().Speed = distance;

		if (distance != 0) {
			direction = currentPos - startPos;
			float sign = (currentPos.y < startPos.y) ? -1.0f : 1.0f;
			float angle = (Vector2.Angle (Vector2.left, direction) * sign) - offset + Camera.main.transform.eulerAngles.y;
			angle = (angle > -180.0f) ? angle : angle + 360.0f;
			Player.GetComponent<PlayerMoveScript> ().TurnAngle = angle;
		}
	}
}
