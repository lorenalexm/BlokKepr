using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	//------------------------------------------------
	#region Fields

	[SerializeField]
	private GameObject spawnerFor = null;
	[SerializeField]
	private GameObject spawnPoint = null;
	[SerializeField]
	private GameObject blokPrefab = null;
	[SerializeField]
	private float spawnInterval = 2.0f;

	private float lastSpawn = 0.0f;

	#endregion


	//------------------------------------------------
	#region OnTriggerEnter2D method
	
	private void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject == this.spawnerFor) {
			if(this.spawnPoint != null && this.blokPrefab != null && (Time.time > (this.lastSpawn + this.spawnInterval))) {
				SimplePool.Spawn(this.blokPrefab, this.spawnPoint.transform.position, Quaternion.Euler(0, 0, Random.Range(-45.0f, 45.0f)));
				this.lastSpawn = Time.time;
			}
		}
	}
	
	#endregion
}
