using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {
	public SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device device;
	public float throwForce = 1.5f;


	//Teleporter
	private LineRenderer laser;
	public GameObject teleportAimerObject;
	public Vector3 teleportLocation;
	public GameObject player;
	public LayerMask laserMask;
	public float yNudgeAmount = 1f; //specific to teleportAimerObject height

	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		laser = GetComponentInChildren<LineRenderer>();


	}

	// Update is called once per frame
	void Update () {
		device = SteamVR_Controller.Input((int)trackedObj.index);

		if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
		{
			laser.gameObject.SetActive(true);
			teleportAimerObject.SetActive(true);

			laser.SetPosition(0, gameObject.transform.position);
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit,5, laserMask))
			{
				teleportLocation = hit.point;
				laser.SetPosition(1, teleportLocation);
				//aimer position
				teleportAimerObject.transform.position = new Vector3(teleportLocation.x, teleportLocation.y + yNudgeAmount, teleportLocation.z);
			}
			else
			{
				teleportLocation = transform.position + transform.forward *5;
				RaycastHit groundRay;
				if(Physics.Raycast(teleportLocation, -Vector3.up, out groundRay, 17, laserMask))
				{
					teleportLocation = new Vector3 (transform.forward.x *5 + transform.position.x, groundRay.point.y, transform.forward.z *5 + transform.position.z);

				}
				laser.SetPosition(1, transform.forward *5 + transform.position);
				//aimer position
				teleportAimerObject.transform.position = teleportLocation + new Vector3(0, yNudgeAmount, 0);

			}

		}
		if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
		{
			laser.gameObject.SetActive(false);
			teleportAimerObject.SetActive(false);
			player.transform.position = teleportLocation;
		}

	}

	//Rube Goldberg Object Grabing Case
	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.CompareTag("Structure"))
		{
			if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
			{
				ThrowObject(col);    
			}
			else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
			{
				DropObject(col);
			}
		}
	}
	void DropObject(Collider coli)
	{
		coli.transform.SetParent(gameObject.transform);
		coli.GetComponent<Rigidbody>().isKinematic = true;
		device.TriggerHapticPulse(2000);
		Debug.Log("You are touching down the trigger on an object");
	}
	void ThrowObject(Collider coli)
	{
		coli.transform.SetParent(null);
		Rigidbody rigidBody = coli.GetComponent<Rigidbody>();
		rigidBody.isKinematic = false;
		rigidBody.velocity = device.velocity * throwForce;
		rigidBody.angularVelocity = device.angularVelocity;
		Debug.Log("You have released the trigger");
	}
}
