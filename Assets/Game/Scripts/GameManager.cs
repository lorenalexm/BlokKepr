using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	//------------------------------------------------
	#region Start method

	private void Start() {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		for(int i = 0; i < players.Length; i++) {
			players[i - 1].GetComponent<PlayerController>().PlayerNumber = i;
		}
	}

	#endregion
}
