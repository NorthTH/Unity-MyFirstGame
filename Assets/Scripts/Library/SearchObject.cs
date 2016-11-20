using UnityEngine;
using System.Collections;

public class SearchObject : MonoBehaviour {

	// Use this for initialization
	public static GameObject GetClosestObject(GameObject First,GameObject[] Targets,float SearchRange)
	{
		GameObject tMin = null;
		float minDist = SearchRange;
		Vector3 currentPos = First.transform.position;
		foreach (GameObject t in Targets) {
			float dist = Vector3.Distance (t.transform.position, currentPos);
			if (dist < minDist) {
				tMin = t;
				minDist = dist;
			}
		}
		return tMin;
	}

	public static bool CheckObjectinRange(Transform First,Transform Second, float SearchRange)
	{
		float distance = Vector3.Distance(Second.position, First.position);
		if (distance < SearchRange)
			return true;
		else
			return false;
	}

	public static GameObject GetClosetTagObject(Transform transform, string tag, float Range ){
		GameObject nearestEnemy = null;
		float minDis = Range;
		GameObject[] Objects = GameObject.FindGameObjectsWithTag (tag);
		foreach (GameObject enemy in Objects) {
			float dis = Vector3.Distance (transform.position, enemy.transform.position);
			//if( dis<=Range && dis<minDis)/ && enemy.GetComponent<Enemy>().hp>0 )
			if (dis <= Range && dis < minDis) {
				minDis = dis;
				nearestEnemy = enemy;
			}
		}
		return nearestEnemy;
	}
}
