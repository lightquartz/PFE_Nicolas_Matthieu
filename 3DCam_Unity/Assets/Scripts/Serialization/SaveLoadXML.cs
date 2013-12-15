using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;


namespace CAM3D
{
	public class Save
	{
		public static void rooms(string path)
		{
			StreamWriter wr = new StreamWriter(path+"/list_rooms.xml");
			XmlSerializer xmlSerial = new XmlSerializer(typeof(List<Room>));
			xmlSerial.Serialize(wr, Building.list_rooms);
			wr.Close();
		}

		/*
		public static void furniture(string path)
		{
			StreamWriter wr = new StreamWriter(path+"/list_furniture.xml");
			XmlSerializer xmlSerial = new XmlSerializer(typeof(List<Furniture>));
			xmlSerial.Serialize(wr, Building.list_furniture);
			wr.Close();
		}*/
	}

	public class Load
	{
		public static void rooms(string path)
		{
			if (File.Exists(path+"/list_rooms.xml"))
			{
				StreamReader wr = new StreamReader(path+"/list_rooms.xml");
				XmlSerializer xs = new XmlSerializer(typeof(List<Room>));
				Building.list_rooms = (List<Room>)xs.Deserialize(wr);
				wr.Close();
			}
			else UnityEngine.Debug.Log("File list_rooms.xml not found.");
		}
	}
}
