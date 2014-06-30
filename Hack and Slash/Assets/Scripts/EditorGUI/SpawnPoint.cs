using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawCube(transform.position, new Vector3(2,2,2));
	}
}
