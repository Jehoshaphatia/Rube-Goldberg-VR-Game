using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMenuManager : MonoBehaviour {
	#region Properties (Game Manager)
	private GameManager gameManager;
	#endregion

	#region Properties (Controller Input Manager)
	public ControllerInputManager controllerInputManager;
	private SteamVR_Controller.Device controller;
	#endregion

	#region Properties (Swipe)
	private float swipeSum;
	private float touchLast;
	private float touchCurrent;
	private float distance;
	private bool hasSwipedLeft;
	private bool hasSwipedRight;
	#endregion

	#region Properties (Objects)
	public List<GameObject> objectList; // handled automatically at start
	public List<GameObject> objectPrefabList; // set manually in inspector and MUST match order of scene menu objects
	#endregion

	#region Properties (Object Displays)
	public bool isEnabled;
	public GameObject objectDisplay;

	public Text objectTitle;
	public int currentObject = 0;

	public Text objectQuantity;
	public int currentQuantity = 0;
	#endregion

	#region Properties (Tutorial)
	public bool isPlayingTutorial;
	public bool isUsingMenu;
	public bool hasSpawnedObject;
	#endregion

	#region Initialization
	void Start () {
		//gameManager = FindObjectOfType<GameManager> ();

		// Generate the list of objects
		foreach (Transform child in transform) {
			objectList.Add (child.gameObject);
		}

		// Setup the object title and quantity displays
		/*UpdateTitleDisplay ();
		UpdateQuantityDisplay ();*/
	}

	private void InitializeController () {
		// Controller Input Manager
		if (controllerInputManager == null) {
			controllerInputManager = GetComponent<ControllerInputManager> ();
		}

		// Controller
		if (controller == null && controllerInputManager != null) {
			controller = controllerInputManager.device;
		}
	}
	#endregion

	#region Frame Update
	void Update () {
		
	}

	#endregion

	#region Swipe
	public void SwipeLeft () {
		objectList [currentObject].SetActive (false);
		currentObject--;
		if (currentObject < 0) {
			currentObject = objectList.Count - 1;
		}
		objectList [currentObject].SetActive (true);
		/*UpdateTitleDisplay ();
		UpdateQuantityDisplay ();
		TutorialSelectPlank ();*/
	}

	public void SwipeRight () {
		objectList [currentObject].SetActive (false);
		currentObject++;
		if (currentObject > objectList.Count - 1) {
			currentObject = 0;
		}
		objectList [currentObject].SetActive (true);
		/*UpdateTitleDisplay ();
		UpdateQuantityDisplay ();
		TutorialSelectPlank ();*/
	}
	#endregion

	#region Spawn Object
	public void SpawnCurrentObject () {
		Instantiate(objectPrefabList[currentObject], 
			objectList[currentObject].transform.position, 
			objectList[currentObject].transform.rotation);
	}
	#endregion
}
