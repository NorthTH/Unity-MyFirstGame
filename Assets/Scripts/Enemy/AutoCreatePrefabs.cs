using UnityEngine;
using System.Collections;

public class AutoCreatePrefabs : MonoBehaviour {

	public GameObject[] Prefabs;

	public Transform SpawnPoint; 

	public float time = 5;

	private float timer;

	// Use this for initialization
	void Start () {
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		CreatePrefabs ();
	}

	private void CreatePrefabs()
	{
		timer += Time.deltaTime;

		if (Prefabs.Length > 0 && timer > time) {
			int prefabIndex = UnityEngine.Random.Range (0, Prefabs.Length - 1);
			Instantiate (Prefabs [prefabIndex], SpawnPoint.position, SpawnPoint.rotation);
			timer = 0;
		}
	}
}
