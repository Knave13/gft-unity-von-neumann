using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	public float PanSpeed = 100;
	Transform _rotationObject;
	Transform _zoomObject;

	void Awake()
	{
		_rotationObject = transform.GetChild(0);
		_zoomObject = _rotationObject.GetChild(0);
		ResetCamera();
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		ChangePosition();
	}

	public void ResetCamera()
	{
		this.transform.position = new Vector3(0, 0, 0);
	}

	void ChangePosition()
	{
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			float distance = PanSpeed * Time.deltaTime;
			var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
			float dampeningFactor = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));

			transform.Translate(distance * dampeningFactor * direction);
		}
	}
}
