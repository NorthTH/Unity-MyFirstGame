using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	private Animator myAnimator;

	private bool moveable;

	GameObject player;               // Reference to the player's position.
	StatusScript playerHP;      // Reference to the player's health.
	StatusScript enemyHP;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.

	public float SearchRange = Mathf.Infinity;

	public float StopRange = 1;

	public GameObject Player
	{
		get{return player;}
	}

	void Awake ()
	{
		myAnimator = GetComponent<Animator> ();
		enemyHP = GetComponent <StatusScript> ();
		nav = GetComponent <NavMeshAgent> ();
		moveable = true;
	}


	void Update ()
	{
		if (!GameManagerController.Instance.chackGameOver ()) {
			findPlayer ();
			SetMoveable ();
			if (moveable) {
				moveToPlayer ();
			} else {
				nav.enabled = false;
			}
		} else {
			nav.enabled = false;
		}
	}

	void SetMoveable()
	{
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")
		    || this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
			moveable = true;
		} else {
			moveable = false;
		}
	}

	void moveToPlayer()
	{
		if (player != null && !SearchObject.CheckObjectinRange(this.transform,Player.transform,StopRange)) {
			playerHP = player.GetComponent <StatusScript> ();
			if (enemyHP.HP > 0 && playerHP.HP > 0) {
				nav.enabled = true;
				myAnimator.SetFloat ("Speed", 1);
				nav.SetDestination (player.transform.position);
			} else {
				myAnimator.SetFloat ("Speed", 0);
				nav.enabled = false;
			}
		} else {
			myAnimator.SetFloat ("Speed", 0);
			nav.enabled = false;
		}
	}

	void findPlayer()
	{
		if (player == null) {
			player = SearchObject.GetClosestObject (this.gameObject, GameObject.FindGameObjectsWithTag ("Player"), SearchRange);
		} else {
			player = (SearchObject.CheckObjectinRange (this.transform, player.transform, SearchRange)) ? player : null;
		}
	}

	public void changeTargetPlayer (GameObject targetPlayer)
	{
		this.player = targetPlayer;
	}
}
