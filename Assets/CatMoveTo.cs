using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMoveTo : MonoBehaviour {

	public Transform startMarker;
	public Vector3 endMarker;
	public float speed = 0.1F;
	private float startTime;
	private float journeyLength;
	private Animator catAnim;

	void Start() {
		journeyLength = 0;
		catAnim = GetComponent<Animator> ();
	}

	void Update() {
		if (journeyLength > 0) {
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			transform.position = Vector3.Lerp (startMarker.position, endMarker, fracJourney);

			if (fracJourney < 0.1) {
				var lookPos = endMarker - transform.position;
				lookPos.y = 0;
				var rotation = Quaternion.LookRotation (lookPos);
				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * 10f); 
			}
		}

		if (Vector3.Distance (startMarker.position, endMarker) < 0.1) {
			catAnim.SetBool ("IsRunning", false);
		}
	}

	public void StartMove(Vector3 endPosition) {
		catAnim.SetBool ("IsRunning", true);
		startMarker = this.transform;
		endMarker = endPosition;
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker.position, endMarker);
	}
		
}
