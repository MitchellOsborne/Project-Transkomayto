using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExportBtnScript : MonoBehaviour {
	string ExportName;
	public InputField inputField;
	public RoomGenerator roomObj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ExportRoom()
	{
		ExportName = inputField.text;

		roomObj.ExportRoom (ExportName);
	}
}
