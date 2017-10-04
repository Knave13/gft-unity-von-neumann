using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour 
{
	public int vertexCount = 40;
	public float lineWidth = 0.2f;
	public float radius;
	public bool circleFillscreen;
	public Color CircleColor1 = Color.white;
	public Color CircleColor2 = Color.white;

	private LineRenderer lineRenderer;
	
	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
		//SetupCircle();
	}

	public void SetupCircle()
	{
		lineRenderer.widthMultiplier = lineWidth;

		if (circleFillscreen)
		{
			radius = Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMax, 0f)),
				Camera.main.ScreenToWorldPoint(new Vector3(0f, Camera.main.pixelRect.yMin, 0f))) * 0.5f - lineWidth;
		}

		float deltaTheta = (2f * Mathf.PI) / vertexCount;
		float theta = 0f;

		lineRenderer.positionCount = vertexCount;
		lineRenderer.loop = true;
		SetColor(CircleColor1, CircleColor2);
		for (int i = 0; i < lineRenderer.positionCount; i++)
		{
			Vector3 pos = new Vector3(radius * Mathf.Cos(theta), 0, radius * Mathf.Sin(theta));
			lineRenderer.SetPosition(i, pos);
			theta += deltaTheta;
		}
	}

	public void SetColor(Color c1, Color c2)
	{
		CircleColor1 = c1;
		CircleColor2 = c2;

		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(CircleColor1, CircleColor2);
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		float deltaTheta = (2f * Mathf.PI) / vertexCount;
		float theta = 0f;

		Vector3 oldPos = Vector3.zero;
		for (int i = 0; i < vertexCount + 1; i++)
		{
			Vector3 pos = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
			Gizmos.DrawLine(oldPos, transform.position + pos);
			oldPos = transform.position + pos;

			theta += deltaTheta;
		}
	}
#endif
}
