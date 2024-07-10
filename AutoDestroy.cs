using UnityEngine;

public class AutoDestroy : MonoBehaviour {
	
	void Update () 
	{
		Destroy(this.gameObject,1.3f);        
    }
}
