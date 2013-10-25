using UnityEngine;
using System.Collections;

public class Highlighter : MonoBehaviour 
{
	Color defaultColor;
	
	void Start () 
	{
		defaultColor = gameObject.renderer.material.GetColor("_Color");
	}
	
	
	void OnMouseDown()
	{
		this.gameObject.renderer.material.SetColor("_Color",new Color(1f,1f,0.4f));
		//this.gameObject.renderer.material.SetColor("_Color",new Color((defaultColor.r-0.1f)%1,(defaultColor.g-0.1f)%1,(defaultColor.b-0.1f)%1));
	}
	
	void OnMouseUp()
	{
		this.gameObject.renderer.material.SetColor("_Color",defaultColor);
	}
	
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
