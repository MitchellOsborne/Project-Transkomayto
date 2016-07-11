using UnityEngine;
using System.Collections.Generic;

public class RoomGenerator : MonoBehaviour {
	public Vector2 RoomSize = new Vector2(32,32);
	public GameObject[,] TileArr;
	public Vector2 TileSize = new Vector2(1,1);
	public bool isEditorRoom;
	public GameObject BaseTile;
	List<GameObject> TileList = new List<GameObject>();
	// Use this for initialization
	void Start () {
		if (isEditorRoom) {
			TileArr = new GameObject[(int)RoomSize.x,(int)RoomSize.y];
			for (int x = 0; x < RoomSize.x; ++x) {
				for (int y = 0; y < RoomSize.y; ++y) {
					TileArr [x,y] = BaseTile;
				}
			}
		}
		GenerateRoom ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ClearTileList()
	{
		foreach (GameObject obj in TileList) {
			Destroy (obj);
		}
		TileList.Clear ();
	}

	public void ExportRoom(string RoomName)
	{
		while (gameObject.transform.childCount > 1) {
			Destroy (gameObject.transform.GetChild (gameObject.transform.childCount - 1));
		}
		isEditorRoom = false;
		UnityEditor.PrefabUtility.CreatePrefab ("Assets/Prefabs/Rooms/" + RoomName + ".prefab", gameObject);
		isEditorRoom = true;
		GenerateRoom ();
	}

	public void GenerateRoom()
	{
		ClearTileList ();
		for (int x = 0; x < RoomSize.x; ++x) {
			for (int y = 0; y < RoomSize.y; ++y) {
				GameObject obj = (GameObject)GameObject.Instantiate (TileArr[x,y], new Vector3 (x * TileSize.x, y * TileSize.y, 0), Quaternion.identity);
				obj.transform.SetParent (gameObject.transform);
				TileList.Add(obj);
			}
		}
	}
}
