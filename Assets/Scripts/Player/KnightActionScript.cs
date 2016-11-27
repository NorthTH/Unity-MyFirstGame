using UnityEngine;
using System.Collections;

public class KnightActionScript : MonoBehaviour {

	//アニメーションするためのコンポーネントを入れる
	private Animator myAnimator;

	private PlayerMoveScript PlayerMove;

	private float Range = 5.0f;

	private StatusScript Status;

	public bool IsAttacking;

	public GameObject Slash;

	public Transform SlashSpawn;

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

		timer = 0;
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

			GameObject nearestObject = SearchObject.GetClosetTagObject (this.transform, "Enemy", Range);
			if (nearestObject != null) {
				Vector3 targetPostition = new Vector3( nearestObject.transform.position.x, 
					this.transform.position.y, 
					nearestObject.transform.position.z ) ;
				this.transform.LookAt (targetPostition);
			}
		}
	}

	private void CallSlash()
	{
		GameObject SlashObject = (GameObject) Instantiate (Slash, SlashSpawn.position, SlashSpawn.rotation);
		SlashObject.GetComponent<DamageScript> ().BelongTo = this.gameObject;
		SlashObject.GetComponent<DamageScript> ().Damage = Status.Attack;
	}

	//攻撃中確認
	public bool isAttacking(){
		return IsAttacking;
	}

	//攻撃スタート
	private void AttackStart(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			IsAttacking = true;
			this.PlayerMove.moveable = false;
			if (this.myAnimator.GetBool ("IsAttack"))
				Invoke("CallSlash", 0.0f);
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
		if (Enemy != null && SearchObject.CheckObjectinRange (this.transform, Enemy.transform, 1.5f)) {
			timer += Time.deltaTime;
			if (timer > atkSpeed) {
				timer = 0;
				//this.transform.LookAt (Enemy.transform);
				Attack ();
			}
		}
	}
}
