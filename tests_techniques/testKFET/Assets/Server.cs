using UnityEngine;
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
//using EV3D.BLL;
/*
public class RemotingTest : MonoBehaviour
{
	[STAThread]
	// Update is called once per frame
	protected void OnGUI()
	{
		
	}

	static void Main(string[] args)
	{
		TcpChannel canal = new TcpChannel(8900);
		ChannelServices.RegisterChannel(canal);
	}
	protected void Update()
	{
		Debug.Log("Update");
		TcpChannel chan = new TcpChannel();
		try 
		{
			// Create a channel for communicating w/ the remote object
			// Notice no port is specified on the client
			
			ChannelServices.RegisterChannel(chan, false);
			Debug.Log("Canal Registrado");
			// Create an instance of the remote object
			
			SampleObject obj = (SampleObject) Activator.GetObject(
				typeof(SampleObject),
				"tcp://localhost:8090/GetCount" );
			Debug.Log("Objeto Activado");
			// Use the object
			if( obj == null )
			{
				Debug.Log("Remoting Error: unable to locate server");
			}
			else
			{
				Debug.Log("Contador Remoto: "+  obj.GetCount());
			}
		}
		catch(Exception ex)
		{
			Debug.Log("Error de Remoting : " + ex.ToString());
		}
		finally
		{
			ChannelServices.UnregisterChannel(chan);
		}
	}
}*/