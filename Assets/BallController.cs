using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BallController : MonoBehaviour {

	//スコア変数
	private int YourScore = 0;
	private GameObject scoreText;
	private int SmallStarScore = -1;
	private int LargeStarScore = -5;
	private int SmallCloudScore = 5;
	private int LargeCloudScore = 50;

	private float visiblePosZ = -6.5f;
	private GameObject gameoverText;

	// Use this for initialization
	void Start (){
		this.scoreText = GameObject.Find ("ScoreText");
		this.gameoverText = GameObject.Find ("GameOverText");
	}
	
	// Update is called once per frame
	void Update (){
		if (this.transform.position.z < this.visiblePosZ) {
			this.gameoverText.GetComponent<Text> ().text = "Game Over";
		}

		this.scoreText.GetComponent<Text> ().text = "your score:" + YourScore.ToString();
	}

	void OnCollisionEnter(Collision collision){

		if (collision.gameObject.tag == "SmallStarTag") {
			YourScore += SmallStarScore;
		} else if (collision.gameObject.tag == "LargeStarTag") {
			YourScore += LargeStarScore;
		} else if (collision.gameObject.tag == "SmallCloudTag") {
			YourScore += SmallCloudScore;
		}else if(collision.gameObject.tag == "LargeCloudTag") {
			YourScore += LargeCloudScore;
		}
	}

}
