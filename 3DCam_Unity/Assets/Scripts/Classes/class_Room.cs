using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CAM3D
{
	[Serializable]
	public class Room
	{
		public int idRoom{get;set;}
		public List<Furniture> list_furniture = new List<Furniture>();
		public int incr_nb_furnitures{get;set;}

		public Room()
		{
			this.idRoom = 0;
			this.incr_nb_furnitures = 0;
			this.list_furniture = new List<Furniture>();
		}

		public void addFurniture(Furniture new_furniture)
		{
			new_furniture.id_Furniture = incr_nb_furnitures;
			incr_nb_furnitures++;
			list_furniture.Add(new_furniture);
		}

		public bool removeFurniture(Furniture furniture_to_remove)
		{
			return list_furniture.Remove(furniture_to_remove);
		}

		public bool removeFurniture(int id_furniture_to_remove)
		{
			foreach(Furniture furniture_iterator in list_furniture)
			{
				if(furniture_iterator.id_Furniture == id_furniture_to_remove)
				{
					list_furniture.Remove(furniture_iterator);
					return true;
				}
			}
			UnityEngine.Debug.Log("No furniture with the id " + id_furniture_to_remove + " was found in the room number " + this.idRoom + " .\n");
			return false;
		}
	}
}
