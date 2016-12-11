using UnityEngine;
using System.Collections;

public class MonsterManagerScript : MonoBehaviour {

	public int MaxGreenSlime = 20;

	public int MaxBlueSlime = 10;

	public int MaxBlueGoblin = 5;

	public int NowGreenSlime;

	public int NowBlueSlime;

	public int NowBlueGoblin;

	void Start () {
		NowGreenSlime = 0;
		NowBlueSlime = 0;
		NowBlueGoblin = 0;
	}
		
	public void addGreenSlime ()
	{
		++NowGreenSlime;
	}

	public void subGreenSlime ()
	{
		--NowGreenSlime;
	}

	public bool isFullGreenSlime ()
	{
		return (NowGreenSlime >= MaxGreenSlime) ? true : false;	
	}
}
