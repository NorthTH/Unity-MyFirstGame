﻿using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

	private GameObject Player;
	private int PlayerNumber = 1;
	public float damping = 1;
	Vector3 offset;

	void Start() {
		this.Player = GameObject.FindGameObjectWithTag ("Player" + PlayerNumber);
		transform.position = new Vector3 (Player.transform.position.x, transform.position.y, transform.position.z);
		offset = Player.transform.position - transform.position;
	}

	void LateUpdate() {
		float currentAngle = transform.eulerAngles.y;
		float desiredAngle = Player.transform.eulerAngles.y;
		float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);

		Quaternion rotation = Quaternion.Euler(0, angle, 0);
		transform.position = Player.transform.position - (rotation * offset);

		transform.LookAt(Player.transform);
	}

	public void setToPlayer(int No){
		this.Player = GameObject.FindGameObjectWithTag ("Player" + No);
	}
}