using UnityEngine;
using System.Collections;

public class DragonActionScript : EnemyActionScript {

	void LateUpdate ()
	{
		AttackStart ();
		AttackEnd ();
	}

	//攻撃スタート
	protected override void AttackStart(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			IsAttacking = true;
//			if (this.myAnimator.GetBool ("IsAttack"))
//				Invoke("CallAttack", StartAtkTime);
			this.myAnimator.SetBool ("IsAttack", false);
		}
	}

	protected override void AttackEnd(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack"))
		{
			//Debug.Log (this.myAnimator.GetCurrentAnimatorStateInfo (0).normalizedTime);
			if(this.myAnimator.GetCurrentAnimatorStateInfo (0).normalizedTime >= 0.90f)
			{
				IsAttacking = false;
			}
		}
	}
}
