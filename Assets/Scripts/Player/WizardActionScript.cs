using UnityEngine;
using System.Collections;

public class WizardActionScript : MonoBehaviour {

	//アニメーションするためのコンポーネントを入れる
	private Animator myAnimator;

	private PlayerMoveScript PlayerMove;

	private float Range = 7.0f;

	public bool IsAttacking;

	public GameObject Fireblot;

	public Transform FireblotSpawn;

	// Use this for initialization
	void Start () {
		//アニメータコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		this.PlayerMove = GetComponent<PlayerMoveScript> ();

		IsAttacking = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		AttackStart ();
		AttackEnd ();
	}

	//攻撃実行
	public void Attack(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")
			|| this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
			this.myAnimator.SetBool ("IsAttack", true);
			this.PlayerMove.moveable = false;

			if (findNearEnemy () != null) {
				this.transform.LookAt (findNearEnemy ().transform);
			}
		}
	}

	private void CallFireblot()
	{
		Instantiate (Fireblot, FireblotSpawn.position, FireblotSpawn.rotation);
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

	private GameObject findNearEnemy(){
		GameObject nearestEnemy = null;
		float minDis = Range;
		GameObject[] enemys = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject enemy in enemys) {
			float dis = Vector3.Distance (transform.position, enemy.transform.position);
			//if( dis<=Range && dis<minDis)/ && enemy.GetComponent<Enemy>().hp>0 )
			if (dis <= Range && dis < minDis) {
				minDis = dis;
				nearestEnemy = enemy;
			}
		}
		return nearestEnemy;
	}
}
