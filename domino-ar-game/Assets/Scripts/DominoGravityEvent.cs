using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominoGravityEvent : MonoBehaviour {

	MeshRenderer meshRen;
	Rigidbody rBody;

	void Start () {
		meshRen = gameObject.GetComponent<MeshRenderer> ();
		rBody = gameObject.GetComponent<Rigidbody> ();
	}
	
	void Update () {
		if (meshRen.enabled) {
			if(!rBody.useGravity) {
				rBody.useGravity = true;
			}
		} else {
			if (rBody.useGravity) {
				rBody.useGravity = false;
			}
		}
	}
}
