using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {
	CharacterController cc;
	public float Gravity;
	public float CreepThreshold, WalkThreshold, RunThreshold;
	public float CreepSpeed, WalkSpeed, RunSpeed;
	public float JumpHeight;
	public float MaxFallSpeed;
	public Vector3 mov;
	float JumpVel;
	public int JumpCount;
	//public float JumpSpeed;
	public bool canJump;
	bool isLeft = false;
	int remainingJumps;

	public BoxCollider ledgeCollider;

	public bool isGrappled = false;
	//public bool isGrounded = false;

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
		remainingJumps = JumpCount;
		JumpVel = 0;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		mov = new Vector3 (0, 0, 0);
		if (Mathf.Abs (Input.GetAxis ("Horizontal")) >= RunThreshold) {
			mov.x = RunSpeed;
		} else if ((Mathf.Abs (Input.GetAxis ("Horizontal")) >= WalkThreshold)) {
			mov.x = WalkSpeed;
		} else if ((Mathf.Abs (Input.GetAxis ("Horizontal")) >= CreepThreshold)) {
			mov.x = CreepSpeed;
		}
		if (Input.GetAxis ("Horizontal") < 0) {
			isLeft = false;
			mov.x *= -1;
		} else if(Input.GetAxis("Horizontal") > 0){
			isLeft = true;
		}

		Vector3 scale = transform.lossyScale;
		if (isLeft) {
			scale.x = 1;
		} else {
			scale.x = -1;
		}

		transform.localScale = scale;

		if (Input.GetAxis ("Jump") > 0) 
		{
			if (Input.GetAxis ("Vertical") < -0.4f && isGrappled) {
				JumpVel = -5;
				isGrappled = false;
				remainingJumps -= 1;
				canJump = false;
			}else if (canJump) {
				isGrappled = false;
				if (remainingJumps > 0 || cc.isGrounded) {
					remainingJumps -= 1;
					JumpVel = JumpHeight;
					canJump = false;
				}
			}
		} else {
			if (JumpVel > 0) {
				JumpVel *= 0.5f;
			}
			canJump = true;
		}
		JumpVel -= Gravity * Time.fixedDeltaTime;
		JumpVel = Mathf.Clamp (JumpVel, -MaxFallSpeed, MaxFallSpeed);
		mov.y = JumpVel;
		if (isGrappled) {
			mov = Vector3.zero;
		}

		cc.Move (mov * Time.fixedDeltaTime);
	}


	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.normal.y > 0.4f) {
			JumpVel = 0;
			remainingJumps = JumpCount;
		}
		if (hit.normal.y <= -0.8f) {
				if (JumpVel > 0) {
					JumpVel *= 0.5f;
				}
		}

	}

	public void GrabLedge(Transform ledge, bool a_isLeft)
	{
		Vector3 LockPos = ledge.position - ledgeCollider.center;
		if (a_isLeft) {
			LockPos.x = ledge.position.x+ ledgeCollider.center.x;
		}
		cc.transform.position = LockPos;
		isGrappled = true;
		remainingJumps = JumpCount;
	}



}
