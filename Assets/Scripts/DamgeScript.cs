using UnityEngine;
using System.Collections;

public class DamgeScript : MonoBehaviour {

	public int damage;

	public float lifetime;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		//if (other.tag == ("Enemy")) {
			other.gameObject.GetComponent<StatusScript> ().Damage ();
			other.gameObject.GetComponent<StatusScript> ().GetDamage (damage);
		//}
	}
}
