using UnityEngine;
using System.Collections;

public class HPBottleScript : MonoBehaviour {

	public int Heal = 50;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			other.gameObject.GetComponent<StatusScript> ().HP += Heal;
			Destroy (this.gameObject);
		}
	}
}
