using UnityEngine;
using System.Collections;

public class AutoCreateGreenSlime : AutoCreatePrefabs {

	private MonsterManagerScript monsterManager;

	// Use this for initialization
	void Start () {
		monsterManager = Globals.monsterManager.GetComponent<MonsterManagerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManagerController.Instance.chackGameOver ()) {
			if(!monsterManager.isFullGreenSlime())
				CreatePrefabs ();
		}
	}

	protected override void CreatePrefabs()
	{
		timer += Time.deltaTime;

		if (Prefabs.Length > 0 && timer > time) {
			int prefabIndex = UnityEngine.Random.Range (0, Prefabs.Length - 1);
			Instantiate (Prefabs [prefabIndex], SpawnPoint.position, SpawnPoint.rotation);
			timer = 0;
			monsterManager.addGreenSlime ();
		}
	}
}
