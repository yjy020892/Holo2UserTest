using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class SocketClient : MonoBehaviour
{
	#region private members 	
	private TcpClient socketConnection;
	private Thread clientReceiveThread;

	// ----------------------------------------------------------------------------------

	private TcpClient socketConnection1;
	private Thread clientReceiveThread1;

	// ----------------------------------------------------------------------------------

	private TcpClient socketConnection2;
	private Thread clientReceiveThread2;

	// ----------------------------------------------------------------------------------

	private TcpClient socketConnection3;
	private Thread clientReceiveThread3;
	#endregion
	void Start()
	{
		ConnectToTcpServer();
		ConnectToTcpServer1();
		ConnectToTcpServer2();
		ConnectToTcpServer3();
	}

	/// <summary> 	
	/// 소켓 연결 설정
	/// </summary> 	
	private void ConnectToTcpServer()
	{
		try
		{
			clientReceiveThread = new Thread(new ThreadStart(ListenForData));
			clientReceiveThread.IsBackground = true;
			clientReceiveThread.Start();
		}
		catch (Exception e)
		{
			Debug.Log("On client connect exception " + e);
		}
	}

	/// <summary> 	
	/// Runs in background clientReceiveThread; 들어오는 데이터 수신	
	/// </summary>     
	private void ListenForData()
	{
		try
		{
			socketConnection = new TcpClient(RootManager.comIP, 8052);
			Byte[] bytes = new Byte[1024];
			while (true)
			{
				using (NetworkStream stream = socketConnection.GetStream())
				{
					int length;
					// 들어오는 스트림을 바이트 배열로 읽기
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						var incommingData = new byte[length];
						Array.Copy(bytes, 0, incommingData, 0, length);
						// 바이트 배열을 문자열 메시지로 변환
						string serverMessage = Encoding.ASCII.GetString(incommingData);
						Debug.Log("server message received as: " + serverMessage);
					}
				}
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}

	/// <summary> 	
	/// 소켓 연결을 사용하여 서버에 메시지 보내기
	/// </summary> 	
	public void SendMessageNet(string msg)
	{
		if (socketConnection == null)
		{
			return;
		}
		try
		{		
			NetworkStream stream = socketConnection.GetStream();
			if (stream.CanWrite)
			{
				//string clientMessage = "This is a message from one of your clients.";
				// 문자열 메시지를 바이트 배열로 변환.
				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(msg);

				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);

				//Debug.Log("Client sent his message - " + msg);
				//Debug.Log("Client sent his message - should be received by server");
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}

	// ---------------------------------------------------------------------------------------------

	private void ConnectToTcpServer1()
	{
		try
		{
			clientReceiveThread1 = new Thread(new ThreadStart(ListenForData1));
			clientReceiveThread1.IsBackground = true;
			clientReceiveThread1.Start();
		}
		catch (Exception e)
		{
			Debug.Log("On client connect exception " + e);
		}
	}

	private void ListenForData1()
	{
		try
		{
			socketConnection1 = new TcpClient(RootManager.comIP, 8055);
			Byte[] bytes = new Byte[1024];
			while (true)
			{
				using (NetworkStream stream = socketConnection1.GetStream())
				{
					int length;
					// 들어오는 스트림을 바이트 배열로 읽기
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						var incommingData = new byte[length];
						Array.Copy(bytes, 0, incommingData, 0, length);
						// 바이트 배열을 문자열 메시지로 변환
						string serverMessage = Encoding.ASCII.GetString(incommingData);
						Debug.Log("server message received as: " + serverMessage);
					}
				}
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}

	public void SendMessageNet1(string msg)
	{
		if (socketConnection1 == null)
		{
			return;
		}
		try
		{
			NetworkStream stream = socketConnection1.GetStream();
			if (stream.CanWrite)
			{
				//string clientMessage = "This is a message from one of your clients.";
				// 문자열 메시지를 바이트 배열로 변환.

				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(msg);

				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);

				//Debug.Log("Client sent his message - " + msg);
				//Debug.Log("Client sent his message - should be received by server");
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}

	// ---------------------------------------------------------------------------------------------

	private void ConnectToTcpServer2()
	{
		try
		{
			clientReceiveThread2 = new Thread(new ThreadStart(ListenForData2));
			clientReceiveThread2.IsBackground = true;
			clientReceiveThread2.Start();
		}
		catch (Exception e)
		{
			Debug.Log("On client connect exception " + e);
		}
	}

	private void ListenForData2()
	{
		try
		{
			socketConnection2 = new TcpClient(RootManager.comIP, 8053);
			Byte[] bytes = new Byte[1024];
			while (true)
			{
				using (NetworkStream stream = socketConnection2.GetStream())
				{
					int length;
					// 들어오는 스트림을 바이트 배열로 읽기
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						var incommingData = new byte[length];
						Array.Copy(bytes, 0, incommingData, 0, length);
						// 바이트 배열을 문자열 메시지로 변환
						string serverMessage = Encoding.ASCII.GetString(incommingData);
						Debug.Log("server message received as: " + serverMessage);
					}
				}
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}

	public void SendMessageNet2(string msg)
	{
		if (socketConnection2 == null)
		{
			return;
		}
		try
		{
			NetworkStream stream = socketConnection2.GetStream();
			if (stream.CanWrite)
			{
				//string clientMessage = "This is a message from one of your clients.";
				// 문자열 메시지를 바이트 배열로 변환.

				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(msg);

				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);

				//Debug.Log("Client sent his message - " + msg);
				//Debug.Log("Client sent his message - should be received by server");
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}

	// ---------------------------------------------------------------------------------------------

	private void ConnectToTcpServer3()
	{
		try
		{
			clientReceiveThread3 = new Thread(new ThreadStart(ListenForData3));
			clientReceiveThread3.IsBackground = true;
			clientReceiveThread3.Start();
		}
		catch (Exception e)
		{
			Debug.Log("On client connect exception " + e);
		}
	}

	private void ListenForData3()
	{
		try
		{
			socketConnection3 = new TcpClient(RootManager.comIP, 8054);
			Byte[] bytes = new Byte[1024];
			while (true)
			{
				using (NetworkStream stream = socketConnection3.GetStream())
				{
					int length;
					// 들어오는 스트림을 바이트 배열로 읽기
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						var incommingData = new byte[length];
						Array.Copy(bytes, 0, incommingData, 0, length);
						// 바이트 배열을 문자열 메시지로 변환
						string serverMessage = Encoding.ASCII.GetString(incommingData);
						Debug.Log("server message received as: " + serverMessage);
					}
				}
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}

	public void SendMessageNet3(string msg)
	{
		if (socketConnection3 == null)
		{
			return;
		}
		try
		{
			NetworkStream stream = socketConnection3.GetStream();
			if (stream.CanWrite)
			{
				//string clientMessage = "This is a message from one of your clients.";
				// 문자열 메시지를 바이트 배열로 변환.

				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(msg);

				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);

				//Debug.Log("Client sent his message - " + msg);
				//Debug.Log("Client sent his message - should be received by server");
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}
}
