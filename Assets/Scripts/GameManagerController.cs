using UnityEngine;
using System.Collections;

public class GameManagerController : MonoBehaviour {
	public GameObject Camera;
	public GameObject Controller;
	public int PlayerNo = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Camera.GetComponent<ThirdPersonCamera> ().setToPlayer (PlayerNo);
		Controller.GetComponent<PlayerContorller> ().setToPlayer (PlayerNo);
	}
}
