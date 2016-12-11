using UnityEngine;
using System.Collections;

public class RotateWithSpawnPoint : MonoBehaviour {

	public GameObject belongTo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = belongTo.GetComponent<DragonActionScript>().AttackSpawn.position;
		this.transform.rotation = belongTo.GetComponent<DragonActionScript>().AttackSpawn.rotation;
	}
}
