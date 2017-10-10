using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManagement : MonoBehaviour 
{
	public static GUIManagement Instance;
	public List<GameObject> NamePlates;

	void OnEnable()
	{
		Instance = this;
		NamePlates = new List<GameObject>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0; i < NamePlates.Count; i++)
		{
			NamePlates[i].transform.LookAt(Camera.main.transform);
			Vector3 eulerAngles = NamePlates[i].transform.localEulerAngles;
			float rotY = eulerAngles.y + 180f;
			NamePlates[i].transform.localEulerAngles = new Vector3(-eulerAngles.x, rotY, -eulerAngles.z);
		}	
	}
}
