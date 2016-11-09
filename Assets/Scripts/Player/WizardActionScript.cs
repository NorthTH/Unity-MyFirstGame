using UnityEngine;
using System.Collections;

public class WizardActionScript : MonoBehaviour {

	//アニメーションするためのコンポーネントを入れる
	private Animator myAnimator;

	// Use this for initialization
	void Start () {
		//アニメータコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		checkAttackEnd ();
	}

	public void Attack(){
		this.myAnimator.SetBool ("IsAttack", true);
	}

	public void checkAttackEnd(){
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			if (this.myAnimator.GetCurrentAnimatorStateInfo (0).normalizedTime < 1.0f) {
				this.myAnimator.SetBool ("IsAttack", false);
			}
		}
	}
}
