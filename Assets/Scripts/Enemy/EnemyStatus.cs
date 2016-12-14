using UnityEngine;
using System.Collections;

public class EnemyStatus : StatusScript {

	public GameObject potion;
	public float DropPotion = 10.0f;

	protected override void Update () {
		if (HP <= 0 && !isDead) {
			Die ();
		}
		base.DamageStart ();
		base.DamageEnd ();
	}

	protected override void Die(){
		isDead = true;
		myAnimator.SetTrigger ("IsDead");

		Instantiate (potion);

		Destroy (gameObject, DestroyTime);
	}
}
