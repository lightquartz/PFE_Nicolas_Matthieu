using UnityEngine;

using System.Collections;


public class cursor : MonoBehaviour 
{
	public Texture2D cursor_FPSView;
	private Camera cam_fps;
	Transform current_transform_obj = null;

	// Use this for initialization
	void Start () 
	{
		Screen.showCursor = false;
		Screen.lockCursor = true;
		cam_fps = GameObject.FindGameObjectWithTag("MainCamera").camera;
		if(cam_fps == null) UnityEngine.Debug.Log("Camera not found :/");
	}
	
	// Update is called once per frame
	void Update () 
	{
		Ray cursor_ray = cam_fps.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2));
		RaycastHit hit;
		if(Physics.Raycast(cursor_ray, out hit, Mathf.Infinity))
		{
			current_transform_obj = hit.transform;
		}
		else
			current_transform_obj = null;
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Screen.width/2 -cursor_FPSView.width/2 , Screen.height/2 -cursor_FPSView.height/2,56,56),cursor_FPSView);
		if(current_transform_obj!=null)
			GUI.Label(new Rect(0,0,100,20),current_transform_obj.name);
	}
	void OnMouseDown()
	{

	}
}
