using UnityEngine;
using System.Collections;

public class CameraLerp : MonoBehaviour {
	public GameObject Target;
	Rigidbody rb;
	public float LerpValue;
	public bool LerpToPlayer;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
