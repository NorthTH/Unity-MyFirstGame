using UnityEngine;
using System.Collections;

public class EnemyActionScript : MonoBehaviour {

	//アニメーションするためのコンポーネントを入れる
	private Animator myAnimator;

	public bool IsDamaged;

	// Use this for initialization
	void Start () {
		//アニメータコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		IsDamaged = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		DamageStart ();
		DamageEnd ();
	}

	void OnTriggerEnter(Collider other) {
		Damage ();
	}

	public void Damage()
	{
		this.myAnimator.SetBool ("IsDamage", true);
	}

	//
	private void DamageStart(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Damage")) {
			IsDamaged = true;
			this.myAnimator.SetBool ("IsDamage", false);
		}
	}

	//攻撃終了
	private void DamageEnd(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Damage"))
		{
			//Debug.Log (this.myAnimator.GetCurrentAnimatorStateInfo (0).normalizedTime);
			if(this.myAnimator.GetCurrentAnimatorStateInfo (0).normalizedTime >= 0.90f)
			{
				IsDamaged = false;
			}
		}
	}
}
