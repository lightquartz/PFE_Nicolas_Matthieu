using UnityEngine;

using System.Collections;


public class cursor : MonoBehaviour 
{
	public Texture2D cursor_FPSView; //Gui texture for the cursor
	internal float cursor_width = 56f;
	internal float cursor_height = 56f;
	public float interaction_range = 100f;

	private Camera cam_fps; //camera of the "player"
	Transform current_transform_obj = null; //the transform of the current object "viewed" by the player


	// Use this for initialization
	void Start () 
	{
		Screen.showCursor = false; //hides the cursor
		Screen.lockCursor = true; //locks the cursor
		cam_fps = GameObject.FindGameObjectWithTag("MainCamera").camera;
		if(cam_fps == null) UnityEngine.Debug.Log("Camera not found :/");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//throwing a ray in front of the camera.
		Ray cursor_ray = cam_fps.ScreenPointToRay(new Vector3(Screen.width/2 - cursor_width/2,Screen.height/2 + cursor_height/2));
		RaycastHit hit;
		//we keep the gameobject viewed by the player (the closest of the player, and in a certain range.
		if(Physics.Raycast(cursor_ray, out hit, interaction_range))
		{
			current_transform_obj = hit.transform;
		}
		else
			current_transform_obj = null;

		if (Input.GetMouseButton(4) && Screen.lockCursor)
		{
			Screen.lockCursor = false;
			Screen.showCursor = true;
		}
		else if (Input.GetMouseButtonUp(4) && !Screen.lockCursor)
		{
			Screen.lockCursor = true;
			Screen.showCursor = false;
		}
		if(!Screen.lockCursor && Input.GetMouseButtonDown(0) && !Input.GetMouseButton(4))
		{
			Screen.lockCursor = true;
			Screen.showCursor = false;
		}
		if(Screen.lockCursor && Input.GetKeyDown(KeyCode.Escape))
		{
			Screen.lockCursor = false;
			Screen.showCursor = true;
		}

	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Screen.width/2 -cursor_FPSView.width/2 , Screen.height/2 -cursor_FPSView.height/2,cursor_width,cursor_height),cursor_FPSView);
		if(current_transform_obj!=null)
			GUI.Label(new Rect(0,0,100,20),current_transform_obj.name);
	}
	void OnMouseDown()
	{

	}
}
