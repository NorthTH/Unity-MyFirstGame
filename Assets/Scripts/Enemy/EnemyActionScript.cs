using UnityEngine;
using System.Collections;

public class EnemyActionScript : MonoBehaviour {

	//アニメーションするためのコンポーネントを入れる
	protected Animator myAnimator;
	protected EnemyMovement EnemyMove;
	protected GameObject player;                         
	protected StatusScript playerHealth;                  // Reference to the player's health.
	protected StatusScript enemyHealth;                    // Reference to this enemy's health.
	protected bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
	protected float timer;

	protected StatusScript Status;

	protected AudioSource audio;

	public GameObject Atk;

	public Transform AttackSpawn;

	public bool IsAttacking;

	public float timeBetweenAttacks = 0.5f;

	public float StartAtkTime = 0.0f;

	public float SearchRange;

	// Use this for initialization
	void Awake () {
		enemyHealth = GetComponent<StatusScript>();
		myAnimator = GetComponent <Animator> ();
		EnemyMove = GetComponent<EnemyMovement> ();
		this.Status = GetComponent<StatusScript> ();
		audio = GetComponent<AudioSource> ();
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

	protected virtual void findPlayer()
	{
		player = GetComponent<EnemyMovement>().Player;
		playerHealth = (player != null) ? player.GetComponent<StatusScript> () : null;
	}

	protected virtual void Attack ()
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
					this.transform.LookAt (player.transform);
				}
			}
		}
	}

	//攻撃スタート
	protected virtual void AttackStart(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			IsAttacking = true;
			if (this.myAnimator.GetBool ("IsAttack"))
				Invoke("CallAttack", StartAtkTime);
			this.myAnimator.SetBool ("IsAttack", false);
		}
	}

	//攻撃終了
	protected virtual void AttackEnd(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack"))
		{
			//Debug.Log (this.myAnimator.GetCurrentAnimatorStateInfo (0).normalizedTime);
			if(this.myAnimator.GetCurrentAnimatorStateInfo (0).normalizedTime >= 0.90f)
			{
				IsAttacking = false;
			}
		}
	}

	protected virtual void CallAttack()
	{
		GameObject AtkObject = (GameObject) Instantiate (Atk, AttackSpawn.position, AttackSpawn.rotation);
		AtkObject.transform.GetComponent<DamageScript> ().Damage = Status.Attack;
		audio.Play ();
	}
}
