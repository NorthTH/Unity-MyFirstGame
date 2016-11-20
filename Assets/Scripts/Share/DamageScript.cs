using UnityEngine;
using System.Collections;

public class DamageScript : MonoBehaviour {

	public int Damage;

	public int OffSet;

	public float lifetime;

	public GameObject BelongTo;

	public bool EnemyHarm;

	public bool PlayerHarm;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (EnemyHarm && other.tag == ("Enemy")) {
			other.gameObject.GetComponent<StatusScript> ().Damage ();
			other.gameObject.GetComponent<StatusScript> ().GetDamage (Random.Range (Damage - OffSet, Damage + OffSet));
			if (BelongTo != null)
				other.gameObject.GetComponent<EnemyMovement> ().changeTargetPlayer (BelongTo);
		}
		if (PlayerHarm && other.tag == ("Player")) {
			other.gameObject.GetComponent<StatusScript> ().Damage ();
			other.gameObject.GetComponent<StatusScript> ().GetDamage (Random.Range (Damage - OffSet, Damage + OffSet));
		}
	}
}
