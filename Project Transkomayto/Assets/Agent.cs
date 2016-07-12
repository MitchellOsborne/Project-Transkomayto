using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class Agent : MonoBehaviour {
	Rigidbody rb;
	public float CreepThreshold, WalkThreshold, RunThreshold;
	public float CreepSpeed, WalkSpeed, RunSpeed;
	public int JumpCount;
	public float JumpSpeed;
	public bool canJump;
	int remainingJumps;

	BoxCollider ledgeCollider;

	public bool isGrappled = false;
	public bool isGrounded = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		remainingJumps = JumpCount;
		ledgeCollider = GetComponents<BoxCollider> () [1];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		Vector3 mov = new Vector3 (0,rb.velocity.y,0);
		if (Mathf.Abs (Input.GetAxis ("Horizontal")) >= RunThreshold) {
			mov.x = RunSpeed;
		} else if ((Mathf.Abs (Input.GetAxis ("Horizontal")) >= WalkThreshold)) {
			mov.x = WalkSpeed;
		} else if ((Mathf.Abs (Input.GetAxis ("Horizontal")) >= CreepThreshold)) {
			mov.x = CreepSpeed;
		}
		if(Input.GetAxis("Horizontal") < 0)
		{
			mov.x *= -1;
		}


		if (Input.GetAxis ("Jump") > 0) {
			if (canJump) {
				isGrappled = false;
				mov.y = Jump ();
			}
			canJump = false;
		} else {
			canJump = true;
		}

		if (isGrappled) {
			rb.useGravity = false;
			mov = Vector3.zero;
		} else {
			rb.useGravity = true;
		}

		rb.velocity = (mov);
	}

	float Jump()
	{
		float Vel = rb.velocity.y;
		if (isGrounded || remainingJumps > 0) {
			isGrounded = false;
			remainingJumps -= 1;
			Vel = JumpSpeed;
		}
		return Vel;
	}

	void OnCollisionEnter(Collision hit)
	{
		for (int i = 0; i < hit.contacts.Length; ++i) {
			if (hit.contacts[i].normal.y > 0) {
				isGrounded = true;
				remainingJumps = JumpCount;
			}
		}
	}

	void GrabLedge(Transform ledge)
	{
		rb.MovePosition (ledge.position - ledgeCollider.center);
		isGrappled = true;
		isGrounded = true;
		remainingJumps = JumpCount;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ledge") {
			if (other.transform.position.x > transform.position.x && Input.GetAxis ("Horizontal") > 0.5) {
				GrabLedge (other.transform);
			} else if (other.transform.position.x < transform.position.x && Input.GetAxis ("Horizontal") > -0.5) {
				GrabLedge (other.transform);
			}
		}
	}

}
