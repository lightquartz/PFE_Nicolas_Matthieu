using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuiTest : MonoBehaviour 
{
	public List<float> listBidon;

	void OnGUI()
	{
		int cpt = 0;
		foreach(float fl in listBidon)
		{
			GUI.Label(new Rect(20,cpt*20,300,20),fl.ToString());
			cpt++;
		}

		if (GUI.Button(new Rect(10,10,300,300),"coucou !!"))
		{

		}
	}
	// Use this for initialization
	void Start () 
	{
		listBidon.Add(1.0f);listBidon.Add(198.0f);
		listBidon.Add(2.0f);listBidon.Add(11.0f);
		listBidon.Add(56.0f);listBidon.Add(0.10f);
		listBidon.Add(77.0f);listBidon.Add(1.220f);
		listBidon.Add(15.0f);listBidon.Add(12.0f);
		listBidon.Add(16.0f);listBidon.Add(65.0f);
		listBidon.Add(18.0f);listBidon.Add(45.0f);
		listBidon.Add(21.0f);listBidon.Add(1.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
