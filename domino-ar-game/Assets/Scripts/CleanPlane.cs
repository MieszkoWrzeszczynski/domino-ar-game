using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanPlane : MonoBehaviour {

	public GameObject plane;

	public void clean(){
		foreach(Transform child in plane.transform.parent){
			if (child != plane.transform) {
				GameObject.Destroy (child.gameObject);
			}
		}
	}
}
