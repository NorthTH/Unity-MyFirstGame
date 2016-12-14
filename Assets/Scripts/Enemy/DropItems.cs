using UnityEngine;
using System.Collections;

public class DropItems : MonoBehaviour {

	public GameObject[] items;
	public float droprate = 10.0f;

	public void DropItem()
	{
		if (items.Length > 0) {
			int index = Random.Range (0, items.Length);
			Instantiate (items [index], this.transform.position, Quaternion.identity);
		}
	}
}
