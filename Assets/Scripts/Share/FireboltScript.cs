using UnityEngine;
using System.Collections;

public class FireboltScript : MonoBehaviour {

	public float speed;

	public float lifetime;

	public GameObject FireboltExpo; 

	// Use this for initialization
	void Start () {
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;
		Destroy (gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == ("Enemy")) {
			
			GameObject FireblotExpoObject = (GameObject) Instantiate (FireboltExpo, transform.position, transform.rotation);

			Destroy (this.gameObject);
		}
	}
}
