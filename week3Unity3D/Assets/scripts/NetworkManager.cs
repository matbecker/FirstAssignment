using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string typeName = "NetworkExample";
	private const string gameName = "HumberGame";
	[SerializeField] GameObject playerPrefab;
	private HostData[] hostList;


	private void StartServer()
	{
		Network.InitializeServer(4, 25500, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}
	private void SpawnPoint()
	{
		Network.Instantiate(playerPrefab, new Vector3(0f,5f,0f), Quaternion.identity, 0);
	}
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
		{
			hostList = MasterServer.PollHostList();
		}
	}
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	void OnServerInitialized()
	{
		Debug.Log("Server Initialized");
	}
	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(20f,20f,100f,50f), "Start Server"))
			{
				StartServer();
			}
			if (GUI.Button (new Rect(20f,80f,100f,50f), "Refresh Hosts"))
			{
				RefreshHostList();

				if (hostList != null)
				{
					for (int i = 0; i < hostList.Length; i++)
					{
						if (GUI.Button(new Rect(20f, 130f + (50 * i), 100f,50f), hostList[i].gameName))
						{
							JoinServer(hostList[i]);
						}
					}
				}
			}
		}

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
