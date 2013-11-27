using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;
using System.ComponentModel;

public class GuiControllerMainScene : MonoBehaviour 
{
	public Process process;
	private bool etatToggle = false;
	public GUISkin monskin;
	private float xDep = 150f;
	private float yDep = 150f;

	void OnGUI()
	{
		GUI.skin = monskin;
		if(Input.GetMouseButton(4))
		{
			MouseLook scriptMouse =  GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();
			UnityEngine.Debug.Log(scriptMouse);
			xDep = Screen.width/2-150;
			yDep = Screen.height/2;
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
	// Use this for initialization
	void Start () 
	{


	}
	
	// Update is called once per frame
	void Update () 
	{

	}


}
