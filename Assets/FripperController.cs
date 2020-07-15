using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour {
	//HingeJointコンポーネントを入れる
	private HingeJoint myHingeJoint;

	//初期の傾き
	private float defaultAngle = 20;
	//弾いた時の傾き
	private float flickAngle = -20;

	//スクリーンの左右のタッチ状態の変数
	int countTouchesR = 0;
	int countTouchesL = 0;
	//スクリーンの幅
	float halfScreenWidth;

	// Use this for initialization
	void Start () {
		//HingeJointコンポーネント取得
		this.myHingeJoint = GetComponent<HingeJoint>();

		//フリッパーの傾きを設定
		SetAngle(this.defaultAngle);

		halfScreenWidth = Screen.width / 2.0f;
	}
		
	// Update is called once per frame
	void Update () {

		if (Application.platform == RuntimePlatform.WindowsEditor) {
			//左矢印キーを押した時左フリッパーを動かす
			if (Input.GetKeyDown (KeyCode.LeftArrow) && tag == "LeftFripperTag") {
				SetAngle (this.flickAngle);
			}
			//右矢印キーを押した時右フリッパーを動かす
			if (Input.GetKeyDown (KeyCode.RightArrow) && tag == "RightFripperTag") {
				SetAngle (this.flickAngle);
			}

			//矢印キー離された時フリッパーを元に戻す
			if (Input.GetKeyUp (KeyCode.LeftArrow) && tag == "LeftFripperTag") {
				SetAngle (this.defaultAngle);
			}
			if (Input.GetKeyUp (KeyCode.RightArrow) && tag == "RightFripperTag") {
				SetAngle (this.defaultAngle);
			}
		} else if (Application.platform == RuntimePlatform.IPhonePlayer) {
			foreach (Touch touch in Input.touches) {
				//タッチ開始の場合
				if (touch.phase == TouchPhase.Began) {
					if (touch.position.x < halfScreenWidth) {
						countTouchesL += 1;
					} else {
						countTouchesR += 1;
					}
				} else if (touch.phase == TouchPhase.Moved) {
					//タッチ移動の場合
					if (touch.position.x < halfScreenWidth && touch.position.x + touch.deltaPosition.x >= halfScreenWidth) {
						countTouchesL += 1;
						countTouchesR -= 1;
					} else if (touch.position.x >= halfScreenWidth && touch.position.x + touch.deltaPosition.x < halfScreenWidth) {
						countTouchesL -= 1;
						countTouchesR += 1;
					}
				} else if (touch.phase == TouchPhase.Ended) {
					//タッチ終了の場合
					if (touch.position.x < halfScreenWidth) {
						countTouchesL -= 1;
					} else {
						countTouchesR -= 1;
					}
				}
			}
			//スクリーンの左側がタッチされているとき
			if (countTouchesL > 0 && tag == "LeftFripperTag") {
				SetAngle (this.flickAngle);
			}
			//スクリーンの右側がタッチされているとき
			if (countTouchesR > 0 && tag == "RightFripperTag") {
				SetAngle (this.flickAngle);
			}
			//スクリーンの左側がタッチされていないとき
			if (countTouchesL == 0 && tag == "LeftFripperTag") {
				SetAngle (this.defaultAngle);
			}
			//スクリーンの左側がタッチされていないとき
			if (countTouchesR == 0 && tag == "RightFripperTag") {
				SetAngle (this.defaultAngle);
			}
		}
	}

	//フリッパーの傾きを設定
	public void SetAngle (float angle){
		JointSpring jointSpr = this.myHingeJoint.spring;
		jointSpr.targetPosition = angle;
		this.myHingeJoint.spring = jointSpr;
	}
}