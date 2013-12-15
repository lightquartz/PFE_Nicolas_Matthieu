using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CAM3D
{
	[Serializable]
	public static class Building //batiment
	{
		public static List<Room> list_rooms = new List<Room>();
		public static List<Furniture> list_furniture = new List<Furniture>();
		public static int nb_floors;
		public static string name_structure;
		public static int incr_nb_rooms;

		public static void addRoom(Room new_room)
		{
			new_room.idRoom = incr_nb_rooms;
			incr_nb_rooms++;
			list_rooms.Add(new_room);
			return;
		}

		public static bool removeRoom(Room room_to_remove)
		{
			return list_rooms.Remove(room_to_remove);
		}

		public static bool removeRoom(int id_room_to_remove)
		{
			foreach(Room room_iterator in list_rooms)
			{
				if (room_iterator.idRoom == id_room_to_remove)
				{
					list_rooms.Remove(room_iterator);
					return true;
				}
			}
			UnityEngine.Debug.Log("No room with the id " + id_room_to_remove + " was found in this building.\n");
			return false;
		}
	}
}
