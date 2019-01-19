using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class N3S_PushDomino : MonoBehaviour {

	public Camera cam;
	public Text text;

	private float hitForce = 100f;
	private int forceLvl = 2;

	public void push(){
		RaycastHit hit;
		Ray ray = new Ray(cam.transform.position, cam.transform.forward);

		if (Physics.Raycast (ray, out hit)) {

			if (hit.rigidbody != null) {
				
				// Set force to domino hit point to target hit point
				hit.rigidbody.AddForce (-hit.normal * hitForce);
			}
		}
	}

	public void changePushForce(){
		switch (forceLvl) {
		case 1:
			text.text = "1";
			hitForce = 100f;
			forceLvl++;

			break;
		case 2:
			text.text = "2";
			hitForce = 200f;
			forceLvl++;

			break;
		case 3:
			text.text = "3";
			hitForce = 300f;
			forceLvl = 1;

			break;
		}
	}
}
