using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

	float minimum = 1.0f;
	float magSpeed = 5.0f;
	float magnification = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localScale = new Vector3 (this.minimum + Mathf.Sin (Time.time * this.magSpeed) * this.magnification, this.transform.localScale.y, this.minimum + Mathf.Sin (Time.time * this.magSpeed) * this.magnification);
	}
}
