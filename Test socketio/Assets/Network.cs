using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;

public class Network : MonoBehaviour {

	static SocketIOComponent socket;
	public GameObject playerPrefab;

	void Start(){
		socket = GetComponent<SocketIOComponent>();
		socket.On("open", onConnected);
		socket.On("spawn", onSpawn);
		socket.On("setPos", onSetPos);
	}


	void onConnected(SocketIOEvent e){
		Debug.Log("Connected");
	}

	void onSpawn(SocketIOEvent e){
		playerPrefab = (GameObject) Instantiate(playerPrefab,
			new Vector3(e.data["x"].n,
				e.data["y"].n,0),Quaternion.Euler(0,0,0));
	}

	void onSetPos(SocketIOEvent e){
		playerPrefab.transform.position =
		new Vector3(e.data["x"].n,
			e.data["y"].n,0);
	}

	void onDisconnect(SocketIOEvent e){
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
			socket.Emit("showDebug");
		}

		if(Input.GetKeyDown(KeyCode.E)){
			socket.Emit("clearChat");
		}

	}
	
}
