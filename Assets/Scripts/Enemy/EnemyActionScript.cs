using UnityEngine;
using System.Collections;

public class EnemyActionScript : MonoBehaviour {

	//アニメーションするためのコンポーネントを入れる
	private Animator myAnimator;
	private EnemyMovement EnemyMove;
	GameObject player;                         
	StatusScript playerHealth;                  // Reference to the player's health.
	StatusScript enemyHealth;                    // Reference to this enemy's health.
	bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
	float timer;

	private StatusScript Status;

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

	void findPlayer()
	{
		if (player == null) {
			player = SearchObject.GetClosestObject (this.gameObject, GameObject.FindGameObjectsWithTag ("Player"), SearchRange);
			playerHealth = (player != null) ? player.GetComponent<StatusScript> () : null;
		} else {
			player = (SearchObject.CheckObjectinRange (this.transform, player.transform, SearchRange)) ? player : null;
		}
	}

	void Attack ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;

		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if (timer >= timeBetweenAttacks && enemyHealth.HP > 0) {
			// Reset the timer.
			timer = 0f;

			// If the player has health to lose...
			if (playerHealth != null) {
				// ... damage the player.
				//playerHealth.GetDamage (GetComponent<StatusScript>().Attack);
				if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")
				    || this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
					this.myAnimator.SetBool ("IsAttack", true);
					this.EnemyMove.moveable = false;

					GameObject nearestObject = SearchObject.GetClosetTagObject (this.transform, "Player", SearchRange);
					if (nearestObject != null) {
						this.transform.LookAt (nearestObject.transform);
					}
				}
			}
		}
	}

	//攻撃スタート
	private void AttackStart(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			IsAttacking = true;
			if (this.myAnimator.GetBool ("IsAttack"))
				Invoke("CallAttack", StartAtkTime);
			this.myAnimator.SetBool ("IsAttack", false);
		}
	}

	//攻撃終了
	private void AttackEnd(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack"))
		{
			//Debug.Log (this.myAnimator.GetCurrentAnimatorStateInfo (0).normalizedTime);
			if(this.myAnimator.GetCurrentAnimatorStateInfo (0).normalizedTime >= 0.90f)
			{
				this.EnemyMove.moveable = true;
				IsAttacking = false;
			}
		}
	}

	private void CallAttack()
	{
		GameObject AtkObject = (GameObject) Instantiate (Atk, AttackSpawn.position, AttackSpawn.rotation);
		AtkObject.transform.GetComponent<DamageScript> ().Damage = Status.Attack;
	}
}
