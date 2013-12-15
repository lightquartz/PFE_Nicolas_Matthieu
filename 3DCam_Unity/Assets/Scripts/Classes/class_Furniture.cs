using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CAM3D
{
	[Serializable]
	public class Furniture
	{
		public int id_Furniture{get;set;}
		public string serial_number{get;set;}

		public Furniture()
		{
			this.id_Furniture = 0;
			this.serial_number = "";
		}
	}
}
