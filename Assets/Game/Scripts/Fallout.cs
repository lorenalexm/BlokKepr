﻿using UnityEngine;
using System.Collections;

public class Fallout : MonoBehaviour {
	//------------------------------------------------
	#region OnTriggerEnter2D method

	private void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.CompareTag("Blok") == true) {
			SimplePool.Despawn(col.gameObject);
		}

		if(col.gameObject.CompareTag("Player") == true) {
			Messenger<GameObject>.Broadcast("OnPlayerDeath", col.gameObject);
		}
	}

	#endregion
}
