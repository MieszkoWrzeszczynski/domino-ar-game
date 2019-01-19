using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N1S_AddDomino : MonoBehaviour {

	public Camera cam;
	public GameObject dominoObject;
	public GameObject dominoObjectList;
	public GameObject dominoBase;
	public GameObject plane;

	private GameObject priDominoBase;
	private N2S_AddMultiDomino n2s_amd;
	private List<GameObject> dominoList = new List<GameObject>();

	void Start () {
		
		// Add domino base to plane
		Quaternion rotation = Quaternion.LookRotation(Vector3.down);
		priDominoBase = Instantiate (dominoBase, new Vector3(0f, 0.01f, 0f), rotation) as GameObject;
		priDominoBase.transform.parent = plane.transform;
		priDominoBase.SetActive (false);
		
		// Get and set N2S_AddMultiDomino class
		n2s_amd = gameObject.GetComponent<N2S_AddMultiDomino> ();

		// Add all domino to list
		if(dominoObjectList != null){
			foreach(Transform child in dominoObjectList.transform){
				dominoList.Add (child.gameObject);
			}
		}
	}

	void Update () {
		if (cam !=null && n2s_amd.isFirstPointSet()) {
			RaycastHit hit;
			Ray ray = new Ray (cam.transform.position, cam.transform.forward);

			if (Physics.Raycast (ray, out hit)) {

				if (hit.transform.name == plane.transform.name) {
					
					// Show domino base
					if (!priDominoBase.activeSelf) {
						priDominoBase.SetActive (true);
					}

					// Set domino base position and rotation form target hit point and camera
					priDominoBase.transform.position = new Vector3 (hit.point.x, 0.01f, hit.point.z);
					priDominoBase.transform.eulerAngles = new Vector3 (90, cam.transform.eulerAngles.y, 0);
				} else {
					
					// Hide domino base
					if (priDominoBase.activeSelf) {
						priDominoBase.SetActive (false);
					}

				}
			}
		} else {
			
			// Hide domino base
			if (priDominoBase.activeSelf) {
				priDominoBase.SetActive (false);
			}
		}
	}

	public void addDomino(){
		RaycastHit hit;
		Ray ray = new Ray(cam.transform.position, cam.transform.forward);

		if (Physics.Raycast (ray, out hit)) {

			if (hit.transform.name == plane.transform.name) {

				setRandomDominoFromList ();

				// Set domino position and rotation form target hit point and camera
				Vector3 position = new Vector3 (hit.point.x, dominoObject.transform.localScale.y, hit.point.z);
				Quaternion rotation = Quaternion.AngleAxis (cam.transform.eulerAngles.y, Vector3.up);
				
				// Create new domino
				GameObject newDO = Instantiate (dominoObject, position, rotation) as GameObject;
				newDO.transform.parent = plane.transform.parent;
			}
		}
	}

	private void setRandomDominoFromList(){
		if (dominoObjectList != null) {
			
			// Get random list item
			int r = Random.Range (0, dominoList.Count - 1);
			dominoObject =  dominoList [r];
		}
	}
}
