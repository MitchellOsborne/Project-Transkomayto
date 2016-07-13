using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
	public Transform Target;
	public float LerpValue;
	public float MaxDistMultiplier;
	public Vector2 MaxDistance;
	Rigidbody rb;
	bool MoveCam = true;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (MoveCam) {
			
			Vector3 dist = Target.position - transform.position;

			rb.velocity = Vector3.Lerp (Vector3.zero, dist, LerpValue);
			rb.velocity += (Target.GetComponentInParent<Agent> ().mov / 1.5f);
			
		}
	}

}
