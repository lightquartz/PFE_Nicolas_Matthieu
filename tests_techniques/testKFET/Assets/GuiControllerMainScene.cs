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
	public Transform blabla;
	public GameObject schtroumpf;
	private bool etatToggle = false;
	public GUISkin monskin;

	void OnGUI()
	{GUI.skin = monskin;
		if(GUI.Button(new Rect(0,0,200,200),"GUI Test"))
		{
			process = new Process();
			process.StartInfo.FileName = Application.dataPath + "/VS_solutionBidon.exe";


			process.StartInfo.Arguments = "300;400";
			process.Start();
		}
		etatToggle = GUI.Toggle(new Rect(200,200,200,200),etatToggle,"yooo");


	}
	// Use this for initialization
	void Start () 
	{
		schtroumpf = GameObject.FindGameObjectWithTag("Respawn");

	}
	
	// Update is called once per frame
	void Update () 
	{
		schtroumpf.transform.Translate(new Vector3(0.1f,0,0));
	}


}
