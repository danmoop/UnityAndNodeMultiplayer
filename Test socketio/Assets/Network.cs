using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;

public class Network : MonoBehaviour {

	static SocketIOComponent socket;
	public GameObject playerPrefab;
	private Dictionary<string, GameObject> players;

	void Start(){
		socket = GetComponent<SocketIOComponent>();
		socket.On("open", onConnected);
		socket.On("spawn", onSpawned);
		socket.On("setPos", onSetPos);
		socket.On("close", onDisconnect);

		players = new Dictionary<string, GameObject>();
	}

	void onConnected(SocketIOEvent e){
		Debug.Log("Connected");
	}

	void onDisconnect(SocketIOEvent e){
		Debug.Log(e);
	}

	void onSpawned(SocketIOEvent e){
		playerPrefab = (GameObject) Instantiate(playerPrefab,
			new Vector3(e.data["x"].n,
				e.data["y"].n,0),Quaternion.Euler(0,0,0));
		AddPlayer(e.data["id"].str, playerPrefab);
		Debug.Log(e.data["id"].str);
	}

	void onSetPos(SocketIOEvent e){
		players[e.data["id"].str].transform.position =
		new Vector3(e.data["x"].n,
			e.data["y"].n,0);
	}

	public void AddPlayer(string id, GameObject player){
		players.Add(id, player);
	}

	void Update(){

		if(Input.GetKey(KeyCode.W)){
			socket.Emit("moveUpRequest");
		}

		if(Input.GetKey(KeyCode.S)){
			socket.Emit("moveDownRequest");
		}

		if(Input.GetKey(KeyCode.A)){
			socket.Emit("moveLeftRequest");
		}

		if(Input.GetKey(KeyCode.D)){
			socket.Emit("moveRightRequest");
		}

		if(Input.GetKeyDown(KeyCode.C)){
			Debug.Log(players);
		}

		if(Input.GetKeyDown(KeyCode.E)){
			socket.Emit("clearChat");
		}

	}
	
}
