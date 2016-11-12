using UnityEngine;
using System.Collections;

public class KnightActionScript : MonoBehaviour {

	//アニメーションするためのコンポーネントを入れる
	private Animator myAnimator;

	private float Range = 5.0f;

	public GameObject Slash;

	public Transform SlashSpawn;

	// Use this for initialization
	void Start () {
		//アニメータコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		SetAttackEnd ();
		AttackEnd ();
	}

	//攻撃実行
	public void Attack(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")
		    || this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
			this.myAnimator.SetBool ("IsAttack", true);
			GetComponent<PlayerMoveScript> ().moveable = false;

			if (findNearEnemy () != null) {
				this.transform.LookAt (findNearEnemy ().transform);
			}

			Instantiate (Slash, SlashSpawn.position, SlashSpawn.rotation);
		}
	}

	//攻撃フラクリセット
	private void SetAttackEnd(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			this.myAnimator.SetBool ("IsAttack", false);
		}
	}

	//攻撃終了確認
	public bool isAttackEnd(){
		return (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack") &&
		this.myAnimator.GetCurrentAnimatorStateInfo (0).normalizedTime >= 1.0f) ? true : false;
	}

	//攻撃終了時
	private void AttackEnd(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack"))
		{
			//Debug.Log (this.myAnimator.GetCurrentAnimatorStateInfo (0).normalizedTime);
			if(this.myAnimator.GetCurrentAnimatorStateInfo (0).normalizedTime >= 0.95f)
			{
				GetComponent<PlayerMoveScript> ().moveable = true;
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
