using UnityEngine;
using System.Collections;
using Holoville;
using Holoville.HOTween;


public class MoveCamera : MonoBehaviour {
	
	public float vitesseDep;
	// Use this for initialization
	void Start () 
	{
		vitesseDep = 0.1f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			this.transform.Translate(vitesseDep*Vector3.forward);
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			this.transform.Translate(vitesseDep*Vector3.back);
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			this.transform.Translate(vitesseDep*Vector3.left);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			this.transform.Translate(vitesseDep*Vector3.right);
		}
		//Debug.Log("mousepos = "+Input.mousePosition);
		//iTween.LookTo(this.camera.gameObject,Input.mousePosition,1f);
		//iTween.LookUpdate(this.gameObject,Input.mousePosition+new Vector3(0,90,this.camera.transform.position.z),1f);
		
	}
}
