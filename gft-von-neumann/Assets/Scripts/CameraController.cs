using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	public static CameraController Instance;
	public float PanSpeed = 5;
	public float ZoomedInAngle = 45;
	public float ZoomedOutAngle = 90;
	public float MinZoom = 20;
	public float MaxZoom = 200;
	public bool InverseZoom = false;

	Transform _rotationObject;
	Transform _zoomObject;
	float _zoomLevel = 0;
	float _rotationSpeed = 30f;

	void Awake()
	{
		Instance = this;
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
		ChangeZoom();
		ChangePosition();
		ChangeRotation();
	}

	public void MoveTo(Vector3 position)
	{
		this.transform.position = position;
	}
	
	public void ResetCamera()
	{
		this.transform.position = new Vector3(0, 0, 0);
		_zoomLevel = 0;
		_rotationObject.transform.rotation = Quaternion.Euler(ZoomedInAngle, 0, 0);
		_zoomObject.transform.localPosition = new Vector3(0, 0, MinZoom);
	}

	void ChangeZoom()
	{
		if (Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			if (InverseZoom)
			{
				_zoomLevel = Mathf.Clamp01(_zoomLevel + Input.GetAxis("Mouse ScrollWheel"));
			}
			else
			{
				_zoomLevel = Mathf.Clamp01(_zoomLevel - Input.GetAxis("Mouse ScrollWheel"));
			}
			float zoom = Mathf.Lerp(-MinZoom, -MaxZoom, _zoomLevel);
			_zoomObject.transform.localPosition = new Vector3(0, 0, zoom);

			float zoomAngle = Mathf.Lerp(ZoomedInAngle, ZoomedOutAngle, _zoomLevel);
			_rotationObject.transform.localRotation = Quaternion.Euler(zoomAngle, 0, 0);
		}
	}

	void ChangePosition()
	{
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			float movementFactor = Mathf.Lerp(MinZoom, MaxZoom, _zoomLevel);
			float distance = PanSpeed * Time.deltaTime;
			var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
			float dampeningFactor = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));

			transform.Translate(distance * dampeningFactor * movementFactor * direction);

			ClampCameraPan();
		}
	}

	void ChangeRotation()
	{
		if (Input.GetAxis("RightJoystickX") != 0 || Input.GetAxis("RightJoystickY") != 0)
		{
			float angleZ = Input.GetAxis("RightJoystickY") * _rotationSpeed * Time.deltaTime;
			float angleX = Input.GetAxis("RightJoystickX") * _rotationSpeed * Time.deltaTime;

			//_rotationObject.transform.localRotation = Quaternion.Euler(angleX, 0, angleZ);
			transform.Rotate(angleX, 0, angleZ);
		}
	}

	void ClampCameraPan()
	{
		var position = this.transform.position;

		if (Galaxy.Instance.GalaxyView)
		{
			position.x = Mathf.Clamp(transform.position.x, -Galaxy.Instance.maximumRadius, Galaxy.Instance.maximumRadius);
			position.z = Mathf.Clamp(transform.position.z, -Galaxy.Instance.maximumRadius, Galaxy.Instance.maximumRadius);
		}
		else
		{
			float solarSystemRadius = 60f;
			position.x = Mathf.Clamp(transform.position.x, -solarSystemRadius, solarSystemRadius);
			position.z = Mathf.Clamp(transform.position.z, -solarSystemRadius, solarSystemRadius);
		}

		this.transform.position = position;
	}
}
