using UnityEngine;
using System.Collections;

public class destroyCup : MonoBehaviour
{
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Sphere")
		{
			Destroy(this.gameObject);
		}
	}
}