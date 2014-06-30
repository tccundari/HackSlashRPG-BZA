using UnityEngine;
using System.Collections;

[AddComponentMenu("Day Nigh Cycle/Timed Lighting")]
public class TimedLighting : MonoBehaviour {
	public void OnEnable()
	{
		Messenger<bool>.AddListener("Morning Light Time", OnToggleLight);
	}
	
	public void OnDisable()
	{
		Messenger<bool>.RemoveListener("Morning Light Time", OnToggleLight);
	}
	
	private void OnToggleLight(bool b)
	{
		if(b)
		{	
			EnableController(false);
		}
		else
		{
			EnableController(true);
		}
	}
	
	private void EnableController(bool enable)
	{
		if(GetComponent<Light>() != null)
			GetComponent<Light>().enabled = enable;
		
		if(GetComponent<LensFlare>() != null)
			GetComponent<LensFlare>().enabled = enable;
		
		if(GetComponent<ParticleRenderer>() != null)
			GetComponent<ParticleRenderer>().enabled = enable;
		
		if(GetComponent<ParticleEmitter>() != null)
			GetComponent<ParticleEmitter>().enabled = enable;
	}
}
