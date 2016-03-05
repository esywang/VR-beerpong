using UnityEngine;
using System.Collections;

public class destroyCup : MonoBehaviour
{
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Torus")
		{
			Destroy(col.gameObject);
		}
	}
}