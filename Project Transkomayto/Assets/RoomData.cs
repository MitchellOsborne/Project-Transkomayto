using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class RoomData : MonoBehaviour {

	public Dropdown TileDD;

	public List<GameObject> TileList;

	// Use this for initialization
	void Start () {
		RefreshList ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void RefreshList()
	{
		TileDD.options.Clear ();
		foreach (GameObject obj in TileList) {
			TileDD.options.Add (new Dropdown.OptionData(obj.name));
		}
	}
}
