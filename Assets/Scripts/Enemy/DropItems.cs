using UnityEngine;
using System.Collections;

public class DropItems : MonoBehaviour {

	public GameObject[] items;
	public float[] droprate;

	public void DropItem()
	{
		if (items.Length > 0 && items.Length == droprate.Length) {
			float rate = Random.Range (0, 100);
			int index = -1;
			for (int i = 0; i < droprate.Length; i++) {
				if (rate < droprate [i])
					index = i;
				else
					rate -= droprate [i];
			}
			if (index >= 0)
				Instantiate (items [index], this.transform.position, Quaternion.identity);
		}
	}
}
