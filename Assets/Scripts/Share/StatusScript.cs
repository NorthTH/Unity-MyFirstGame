using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusScript : MonoBehaviour {

	public int MaxHP;

	public int MaxMP;

	public int Attack;

	public int HP;

	public int MP;

	public bool IsDamaged;



	private bool isDead;

	private Image HealthBar;

	private Canvas StatusCanvas;

	private Animator myAnimator;

	// Use this for initialization
	void Start () {
		this.HP = this.MaxHP;
		this.MP = this.MaxMP;
		this.isDead = false;
		this.myAnimator = GetComponent<Animator>();
		HealthBar = transform.FindChild ("StatusCanvas").FindChild ("HealthBG").FindChild ("Health").GetComponent<Image> ();
		StatusCanvas = transform.FindChild ("StatusCanvas").GetComponent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (HP <= 0 && !isDead) {
			Die ();
		}
		DamageStart ();
		DamageEnd ();
	}

	public void GetDamage(int damage)
	{
		HP -= damage;
		HP = (HP < 0) ? 0 : HP;
		Invoke ("showHP", 0.0f);
		HealthBar.fillAmount = (float)HP / (float)MaxHP;
		Invoke ("hideHP", 2.0f);
	}

	void showHP()
	{
		StatusCanvas.enabled = true;
	}

	void hideHP()
	{
		StatusCanvas.enabled = false;
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

	private void Die(){
		isDead = true;
		myAnimator.SetTrigger ("IsDead");
		//未使用
//		if (gameObject.tag == "Player")
//			this.GetComponent<PlayerMoveScript>().moveable = false;
//		if (gameObject.tag == "Enemy")
			Destroy (gameObject, 1.5f);
	}
}
