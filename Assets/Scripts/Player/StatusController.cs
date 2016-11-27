using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusController : MonoBehaviour {

	public GameObject Player;

	public GameObject HPBar;
	public GameObject HPText;

	public GameObject MPBar;
	public GameObject MPText;

	public GameObject Image;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		updateStatus ();
	}

	void updateStatus()
	{
		if (Player != null) {
			int hp = Player.GetComponent<StatusScript> ().HP;
			int maxHp = Player.GetComponent<StatusScript> ().MaxHP;
			int mp = Player.GetComponent<StatusScript> ().MP;
			int maxMp = Player.GetComponent<StatusScript> ().MaxMP;

			Image iHP = HPBar.GetComponent<Image> ();
			iHP.fillAmount = (float)hp / (float)maxHp;
			Text tHP = HPText.GetComponent<Text> ();
			tHP.text = "HP : " + hp + "/" + maxHp;

			Image iMP = MPBar.GetComponent<Image> ();
			iMP.fillAmount = (float)mp / (float)maxMp;
			Text tMP = MPText.GetComponent<Text> ();
			tMP.text = "MP : " + mp + "/" + maxMp;
		} else
			Destroy (Image.gameObject);
	}
}
