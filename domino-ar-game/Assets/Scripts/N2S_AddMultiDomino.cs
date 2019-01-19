using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class N2S_AddMultiDomino : MonoBehaviour {
	//P2
	public Camera cam;
	public GameObject dominoObject;
	public GameObject dominoObjectList;
	public GameObject plane;
	public GameObject startPoint;
	public GameObject finishPoint;
	public GameObject dominoBase;

	private bool firstPoint = true;
	private Vector3 startPointAxis;
	private Vector3 finishPointAxis;
	private float step = 1.5f;

	private List<GameObject> priListDominoBase = new List<GameObject>();
	private List<GameObject> dominoList = new List<GameObject>();

	void Start () {
		
		// Add all domino to list
		if(dominoObjectList != null){
			foreach(Transform child in dominoObjectList.transform){
				dominoList.Add (child.gameObject);
			}
		}
	}

	void Update () {
		if (cam != null && !firstPoint) {
			RaycastHit hit;
			Ray ray = new Ray (cam.transform.position, cam.transform.forward);

			if (Physics.Raycast (ray, out hit)) {

				if (hit.transform.name == plane.transform.name) {
					
					// Set finish position of the target
					Vector3 positionEnd = new Vector3 (hit.point.x, 0.01f, hit.point.z);

					if (startPointAxis != positionEnd) {

						// Control list size
						int size = (int)(Vector3.Distance (startPointAxis, hit.point) / step) + 1;
						while (priListDominoBase.Count != size) {
							if (priListDominoBase.Count < size) {
								Quaternion rotation = Quaternion.LookRotation (Vector3.down);
								GameObject priDominoBase = Instantiate (dominoBase, new Vector3 (0f, 0.01f, 0f), rotation) as GameObject;
								priDominoBase.transform.parent = plane.transform;

								priListDominoBase.Add (priDominoBase);
							} else {
								GameObject.Destroy (priListDominoBase [0]);
								priListDominoBase.RemoveAt (0);
							}
						}

						// Set domino base position and rotation 
						Vector3 position = startPointAxis;
						Vector3 eAngle = new Vector3 (90, getPointAngle (positionEnd), 0);
						foreach (GameObject go in priListDominoBase) {
							go.transform.position = position;
							go.transform.eulerAngles = eAngle;
							position = Vector3.MoveTowards (position, positionEnd, step);
						}
					}
				}
			}
		} else {

			// Remove all domino Base
			while(priListDominoBase.Count != 0){
				GameObject.Destroy (priListDominoBase [0]);
				priListDominoBase.RemoveAt (0);
			}
		}
	}

	public void addPoint(){
		if (firstPoint) {
			
			// Create and set start point
			addPointAs (startPoint);
		} else {
			
			// Create and set finish point
			if( addPointAs (finishPoint) ){
				
				// Set domino position
				Vector3 position = startPointAxis;

				// Set distance of start and finish point
				float dist = Vector3.Distance(startPointAxis, finishPointAxis);

				// Set domino rotation
				Quaternion rotation = Quaternion.AngleAxis (getPointAngle(finishPointAxis), Vector3.up);

				for(int i = 0; i < dist / step; i++){
					setRandomDominoFromList ();
					
					position.y = dominoObject.transform.localScale.y;

					// Create new domino
					GameObject newDO = Instantiate (dominoObject, position, rotation) as GameObject;
					newDO.transform.parent = plane.transform.parent;

					// set next domino position
					position = Vector3.MoveTowards(position, finishPointAxis, step);
				}
			}
		}
	}

	public bool isFirstPointSet(){
		return this.firstPoint;
	}

	private bool addPointAs(GameObject pointObject){
		RaycastHit hit;
		Ray ray = new Ray(cam.transform.position, cam.transform.forward);

		if (Physics.Raycast (ray, out hit)) {

			if (hit.transform.name == plane.transform.name) {

				// Set point position and rotation form target hit point
				Vector3 position = new Vector3 (hit.point.x, 0.01f, hit.point.z);
				Quaternion rotation = Quaternion.LookRotation(Vector3.down);

				// Create new point
				GameObject newP = Instantiate (pointObject, position, rotation) as GameObject;
				newP.transform.parent = plane.transform.parent;

				// Set start or finish point position
				if (firstPoint) {
					startPointAxis = position;
				} else {
					finishPointAxis = position;
				}

				firstPoint = !firstPoint;

				return true;
			}
		}

		return false;
	}

	private float getPointAngle(Vector3 finishPointAxis){
		Vector3 v1 = startPointAxis;
		Vector3 v2 = finishPointAxis;
		float xDiff = v2.x - v1.x;
		float yDiff = v2.z - v1.z;
		return - Mathf.Atan2(yDiff, xDiff) * (180 / Mathf.PI) + 90;
	}

	private void setRandomDominoFromList(){
		if (dominoObjectList != null) {
			
			// Get random list item
			int r = Random.Range (0, dominoList.Count - 1);
			dominoObject =  dominoList [r];
		}
	}
}
