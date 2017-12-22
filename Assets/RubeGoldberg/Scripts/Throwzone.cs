using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwzone : MonoBehaviour {
	[HideInInspector]
	public static bool isOut;
	private Renderer areaRenderer;
	private Material areaMaterial;
	// Use this for initialization
	void Start () {
		areaRenderer = GetComponent<Renderer>();
		areaMaterial = areaRenderer.material;
	}

	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Platfrom")
		{
			areaMaterial.color = Color.green;
			Debug.Log("Has enter zone");
		}
		isOut = false;

	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "platform")
		{
			areaMaterial.color = Color.red;
			Debug.Log ("Has left zone");
		}
		isOut = true;
	}
}