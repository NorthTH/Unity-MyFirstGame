using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

	//アニメーションするためのコンポーネントを入れる
	private Animator myAnimator;
	//プレイヤーを移動させるコンポーネントを入れる
	private Rigidbody myRigidbody;
	//前進するための力
	public float MaxSpeed = 4.0f;
	//前進するための力
	public float Speed = 0.0f;
	//プレイヤー移動許可フラク
	public bool moveable;

	public bool AIMode;

	public GameObject FollowPlayer;               // Reference to the player's position.
	StatusScript TargetPlayerHP;      // Reference to the player's health.
	StatusScript ThisPlayerHP;      
	public float Range;

	public GameObject Enemy;
	StatusScript enemyHP;        // Reference to this enemy's health.
	public float SearchRange;
	public float StopRange;

	NavMeshAgent nav;               // Reference to the nav mesh agent.

	void Awake ()
	{
		nav = GetComponent <NavMeshAgent> ();
		ThisPlayerHP = GetComponent <StatusScript> ();
	}

	// Use this for initialization
	void Start () {
		//プレイヤー移動可能
		moveable = true;

		//アニメータコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		//Rigidbodyコンポーネントを取得
		this.myRigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		idleIni ();
		//プレイヤー移可能を確認
		if (moveable) {
			if (!AIMode)
				walk ();
			else {
				findEnemy ();
				if (Enemy == null || !CheckPlayerInRange ())
					moveToPlayer ();
				else
					moveToEnemy ();
			}
		}
	}

	private void idleIni()
	{
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")
		    || this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Walk"))
			moveable = true;
	}

	private void walk()
	{
		Speed = (Speed < MaxSpeed) ? Speed : MaxSpeed;
		//歩くアニメーションを開始
		this.myAnimator.SetFloat ("Speed", (this.Speed <= 0) ? 0 : this.Speed);
		//プレイヤーに前方向の力を加える
		Vector3 movement = transform.forward  * Speed * Time.deltaTime;

		myRigidbody.MovePosition (myRigidbody.position + movement);

		this.myAnimator.speed = (Speed == 0) ? 1 : this.Speed / this.MaxSpeed;
	}

	public void turn(float angle)
	{
		if (moveable) {
			transform.rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
		}
	}

	void moveToPlayer()
	{
		if (FollowPlayer != null && ThisPlayerHP.HP >0) {
			TargetPlayerHP = FollowPlayer.GetComponent <StatusScript> ();
			if (!SearchObject.CheckObjectinRange (this.transform, FollowPlayer.transform, Range)) {
				nav.enabled = true;
				myAnimator.SetFloat ("Speed", 1);
				nav.SetDestination (FollowPlayer.transform.position);
			} else {
				myAnimator.SetFloat ("Speed", 0);
				nav.enabled = false;
			}
		} else {
			myAnimator.SetFloat ("Speed", 0);
			nav.enabled = false;
		}
	}

	private bool CheckPlayerInRange()
	{
		return SearchObject.CheckObjectinRange (this.transform, FollowPlayer.transform, Range);
	}

	void moveToEnemy()
	{
		enemyHP = Enemy.GetComponent <StatusScript> ();
		if (!SearchObject.CheckObjectinRange (this.transform, Enemy.transform, StopRange) && ThisPlayerHP.HP > 0 && enemyHP.HP > 0) {
			nav.enabled = true;
			myAnimator.SetFloat ("Speed", 1);
			nav.SetDestination (Enemy.transform.position);
		} else {
			myAnimator.SetFloat ("Speed", 0);
			nav.enabled = false;
		}
	}

	void findEnemy()
	{
		if (Enemy == null) {
			Enemy = SearchObject.GetClosestObject (FollowPlayer, GameObject.FindGameObjectsWithTag ("Enemy"), SearchRange);
		} else {
			Enemy = (SearchObject.CheckObjectinRange (FollowPlayer.transform, Enemy.transform, SearchRange)) ? Enemy : null;
		}
	}
}
