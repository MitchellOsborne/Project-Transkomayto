using UnityEngine;
using System.Collections;

public class LedgeGrabScript : MonoBehaviour {

	Agent owner;

	// Use this for initialization
	void Start () {
		owner = GetComponentInParent<Agent> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ledge") {
			if (other.transform.position.x > transform.position.x && Input.GetAxis ("Horizontal") > 0.5) {
				owner.GrabLedge (other.transform);
			} else if (other.transform.position.x < transform.position.x && Input.GetAxis ("Horizontal") > -0.5) {
				owner.GrabLedge (other.transform);
			}
		}
	}
}
