using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

	private GameObject Player;
	private int PlayerNumber = 1;
	public float damping = 1;
	Vector3 offset;

	void Start() {
		setToPlayer (PlayerNumber);
		if (Player != null) {
			transform.position = new Vector3 (Player.transform.position.x, transform.position.y, transform.position.z);
			offset = Player.transform.position - transform.position;
		}
	}

	void LateUpdate() {
		if (Player != null) {
			float currentAngle = transform.eulerAngles.y;
			float desiredAngle = Player.transform.eulerAngles.y;
			float angle = Mathf.LerpAngle (currentAngle, desiredAngle, Time.deltaTime * damping);

			Quaternion rotation = Quaternion.Euler (0, angle, 0);
			transform.position = Player.transform.position - (rotation * offset);

			transform.LookAt (Player.transform);
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
}
