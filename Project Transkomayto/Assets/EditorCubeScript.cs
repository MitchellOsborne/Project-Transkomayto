using UnityEngine;
using System.Collections;

public class EditorCubeScript : MonoBehaviour {
	public RoomData rd = null;
	public RoomGenerator rg = null;
	public Vector2 ArrIndex;

	bool isEditor = true;

	// Use this for initialization
	void Start () {
		try
		{
		rg = GameObject.FindGameObjectWithTag ("RoomGenEditor").GetComponent<RoomGenerator>();
		rd = GameObject.FindGameObjectWithTag ("DataHolderEditor").GetComponent<RoomData>();
		}catch {
			isEditor = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMouseOver()
	{
		if (isEditor) {
			if (Input.GetMouseButton (0)) {
				if (rg.TileArr [(int)transform.localPosition.x, (int)transform.localPosition.y] != rd.TileList [rd.TileDD.value]) {
					rg.TileArr [(int)transform.localPosition.x, (int)transform.localPosition.y] = rd.TileList [rd.TileDD.value];
					rg.GenerateRoom ();
				}
			}
		}
	}

	public void SetIndex(Vector2 a_Index)
	{
		ArrIndex = a_Index;
	}
}
