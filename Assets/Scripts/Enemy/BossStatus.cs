using UnityEngine;
using System.Collections;

public class BossStatus : StatusScript {

	void Update()
	{
		base.Update ();
		if (base.isDead)
			GameManagerController.Instance.SetVictory ();
	}


}
