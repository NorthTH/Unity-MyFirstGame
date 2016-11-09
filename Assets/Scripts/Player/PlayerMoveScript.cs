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
	//転回するための力
	public float TurnAngle  = 0.0f;

	// Use this for initialization
	void Start () {
		//アニメータコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		//Rigidbodyコンポーネントを取得
		this.myRigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			walk ();
			turn ();
		}
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

	public void turn()
	{
		//		float turn = TurnSpeed * Time.deltaTime;
		//
		//		Quaternion turnRotation = Quaternion.Euler (0f, TurnSpeed, 0f);
		//		myRigidbody.MoveRotation (myRigidbody.rotation * turnRotation);
		transform.rotation = Quaternion.Euler(transform.rotation.x, TurnAngle, transform.rotation.z);
		//transform.Rotate (transform.rotation.x, TurnSpeed, transform.rotation.z);
	}
}
