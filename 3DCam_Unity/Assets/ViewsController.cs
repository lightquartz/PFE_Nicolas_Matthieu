using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;
using System.ComponentModel;

public class ViewsController : MonoBehaviour 
{
	public Process process;
	private bool etatToggle = false;
	public GUISkin monskin;
	private float xDep;
	private float yDep;
	private float buttonChangingView_width_height = 150f;

	// Use this for initialization
	void Start () 
	{
		xDep = Screen.width/2  - 0.75f*GameObject.Find("_GUI").GetComponent<cursor>().cursor_width;
		xDep -= 1.5f*buttonChangingView_width_height;
		yDep = Screen.height/2 - 0.75f*GameObject.Find("_GUI").GetComponent<cursor>().cursor_height;
		yDep -= 0.5f*buttonChangingView_width_height;
		UnityEngine.Debug.Log("xyDep : " + xDep + " / " +yDep);
		//TODO : optimiser l'accès à ces variables
	}

	void OnGUI()
	{
		GUI.skin = monskin;
		if(Input.GetMouseButton(4))
		{
			//MouseLook scriptMouse =  GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();
			//UnityEngine.Debug.Log(scriptMouse);
			if(GUI.Button(new Rect(xDep,yDep,150,150),"Mode 1"))
			{
				
			}
			if(GUI.Button(new Rect(xDep + 150,yDep,150,150),""))
			{
				
			}
			if(GUI.Button(new Rect(xDep+300,yDep,150,150),"Mode 2"))
			{
				
			}
			if(GUI.Button(new Rect(xDep+150,yDep-150,150,150),"Mode 3"))
			{
				
			}
			if(GUI.Button(new Rect(xDep+150,yDep+150,150,150),"Mode 4"))
			{
				
			}
			//etatToggle = GUI.Toggle(new Rect(200,200,200,200),etatToggle,"yooo");
		}
		
		
	}

	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	
}
