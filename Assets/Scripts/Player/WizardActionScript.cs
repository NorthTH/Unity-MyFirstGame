using UnityEngine;
using System.Collections;

public class WizardActionScript : MonoBehaviour {

	//アニメーションするためのコンポーネントを入れる
	private Animator myAnimator;

	private PlayerMoveScript PlayerMove;

	private float Range = 7.0f;

	private StatusScript Status;

	public bool IsAttacking;

	public bool IsCharge;

	public GameObject Fireblot;

	public Transform FireblotSpawn;

	public bool AutoAtk;

	private float timer;

	public float atkSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		//アニメータコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		this.Status = GetComponent<StatusScript> ();

		this.PlayerMove = GetComponent<PlayerMoveScript> ();

		IsAttacking = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		AttackStart ();
		AttackEnd ();
		if (AutoAtk)
			AutoAttack ();
	}

	//攻撃実行
	public void Attack(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")
			|| this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
			this.myAnimator.SetBool ("IsAttack", true);
			this.PlayerMove.moveable = false;

			GameObject nearestObject = SearchObject.GetClosetTagObject (this.transform, "Enemy", Range);
			if (nearestObject != null) {
				this.transform.LookAt (nearestObject.transform);
			}
		}
	}

	private void CallFireblot()
	{
		GameObject FireblotObject = (GameObject) Instantiate (Fireblot, FireblotSpawn.position, FireblotSpawn.rotation);
		FireblotObject.GetComponent<DamageScript> ().BelongTo = this.gameObject;
		FireblotObject.GetComponent<DamageScript> ().Damage = Status.Attack;
	}

	//攻撃中確認
	public bool isAttacking(){
		return IsAttacking;
	}

	//攻撃スタート
	private void AttackStart(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			IsAttacking = true;
			if (this.myAnimator.GetBool ("IsAttack")) 
				Invoke("CallFireblot", 0.4f);
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
				this.PlayerMove.moveable = true;
				IsAttacking = false;
			}
		}
	}

	private void AutoAttack()
	{
		GameObject Enemy = GetComponent<PlayerMoveScript> ().Enemy;
		if (Enemy != null && SearchObject.CheckObjectinRange (this.transform, Enemy.transform, 5.0f)) {
			timer += Time.deltaTime;
			if (timer > atkSpeed) {
				timer = 0;
				this.transform.LookAt (Enemy.transform);
				Attack ();
			}
		}
	}
}
