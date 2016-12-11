using UnityEngine;
using System.Collections;

public class DragonActionScript : EnemyActionScript {

	private GameObject flow_target;
	public float flow_speed = 0.005F;

	void Start () {
		flow_target = new GameObject ();
	}

	void Update ()
	{
		findPlayer ();
		Attack ();
	}

	void LateUpdate ()
	{
		AttackStart ();
		AttackEnd ();
	}

	protected override void findPlayer()
	{
		player = GetComponent<DragonMovement>().Player;
		if (flow_target == null)
			flow_target.transform.position = player.transform.position;
		if (player != null)
			flow_target.transform.position = Vector3.Lerp (flow_target.transform.position, player.transform.position, flow_speed);
		playerHealth = (player != null) ? player.GetComponent<StatusScript> () : null;

		this.transform.LookAt (flow_target.transform);
	}

	protected override void Attack ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;

		if (player != null && playerHealth != null) {
			// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
			if (timer >= timeBetweenAttacks && enemyHealth.HP > 0 && SearchObject.CheckObjectinRange (this.transform, player.transform, SearchRange)) {
				// Reset the timer.
				timer = 0f;

				// ... damage the player.
				if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")
					|| this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
					this.myAnimator.SetBool ("IsAttack", true);
				}
			}
		}
	}

	//攻撃スタート
	protected override void AttackStart(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			IsAttacking = true;
			if (this.myAnimator.GetBool ("IsAttack"))
				Invoke("CallAttack", StartAtkTime);
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

	protected override void CallAttack()
	{
		GameObject AtkObject = (GameObject) Instantiate (Atk, AttackSpawn.position, AttackSpawn.rotation);
		AtkObject.transform.GetComponent<RotateWithSpawnPoint> ().belongTo = this.gameObject;
		AtkObject.transform.GetComponent<DamageScript> ().Damage = Status.Attack;
	}
}
