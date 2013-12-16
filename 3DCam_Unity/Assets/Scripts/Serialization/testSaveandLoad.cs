using UnityEngine;
using System.Collections;

namespace CAM3D
{	
	public class testSaveandLoad : MonoBehaviour 
	{
		void Awake ()
		{
			Load.rooms("Assets/XML_Data");
		}
		// Use this for initialization
		void Start () 
		{

			Room r = new Room();
			//r.idRoom = 40;
			Furniture f = new Furniture();
			f.serial_number = "1ff32112";
			r.addFurniture(f);
			Building.addRoom(r);
			Save.rooms("Assets/XML_Data");
			Debug.Log("pouet");
		}
		
		// Update is called once per frame
		void Update () 
		{
		
		}
	}
}