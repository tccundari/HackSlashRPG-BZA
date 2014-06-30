/// <summary>
/// GameTime.cs
/// Tiago Cundari
/// 
/// Esta classe é responsável por monitorar o tempo no jogo. Ela também vai rotacionar o 
/// sol e a lua no céu de acordo com a hora no jogo.
/// Esta classe também vai trocar as skyboxs de acordo com o o ciclo de dia e noite 
/// </summary>
using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	
	public enum TimeOfDay
	{
		Idle,
		SunRise,
		SunSet		
	}
	
	public Transform[] sun;								//um array para guardar todos os nossos sois
	public float dayCicleInMinutes = 1;					//quantos minutos em tempo real equivale a um dia no jogo	
	
	public float sunRise;								//a hora do dia que começa o nascer do sol
	public float sunSet;								//a hora do dia que começa o por do sol
	public float skyboxBlendModifier;					//a velocidade da troca de texturas da skybox
	
	public Color ambLightMax;
	public Color ambLightMin;
	
	public float morningLight;
	public float nightLight;
	private bool _isMorning;
	
	private Sun[] _sunScript;							//um array para guardar todos os Sun.cs script de cada sol
	private float _degreeRotation;						//quantidade de graus que estamos rotacionando para cada unidade de tempo
	private float _timeOfDay;							//monitora a passagem do tempo através do dia
	private float _dayCicleInSeconds;					//o número de segundos em tempo real no jogo
	
	private const float SECOND = 1;						//constante para 1 segundo
	private const float MINUTE = 60 * SECOND;			//constante para a quantidade de segundos em um minuto
	private const float HOUR = 60 * MINUTE;				//constante para a quantidade de segundos em uma hora
	private const float DAY = 24 * HOUR;				//constante para a quantidade de segundos em um dia
	private const float DEGREES_PER_SECOND = 360/DAY;	//constante para a quantidade de graus que temos que rotacionar por segundo para formar 360 
	
	private TimeOfDay _tod;
	private float _noonTime;							//este é o tempo do dia para a tarde	
	private float _morningLength;
	private float _eveingLength;
	
	// Use this for initialization
	void Start () {
		_isMorning = false;
		_tod = TimeOfDay.Idle;
		
		_dayCicleInSeconds = dayCicleInMinutes * MINUTE;
		
		RenderSettings.skybox.SetFloat("_Blend", 0);
		
		_sunScript = new Sun[sun.Length];
		
		for(int cnt = 0; cnt < sun.Length; cnt++)
		{
			Sun temp = sun[cnt].GetComponent<Sun>();
			
			if(temp == null)
			{
				Debug.LogWarning("Sun script not find. Adding it.");
				sun[cnt].gameObject.AddComponent<Sun>();
			}
			
			_sunScript[cnt] = temp;
		}
		
		//_timeOfDay = 0;
		_timeOfDay = sunRise;
		_degreeRotation = DEGREES_PER_SECOND * DAY / (_dayCicleInSeconds);
		
		sunRise *= _dayCicleInSeconds;
		sunSet *= _dayCicleInSeconds;
		_noonTime = _dayCicleInSeconds / 2;
		
		_morningLength = _noonTime - sunRise;		//o tamanho da manhã em segundos
		_eveingLength = sunSet - _noonTime;			//o tamanho da tarde em segundos
		
		morningLight *= _dayCicleInSeconds;
		nightLight *= _dayCicleInSeconds;
		
		SetupLighting();
	}
	
	// Update is called once per frame
	void Update () {
		_timeOfDay += Time.deltaTime;
		
		if(_timeOfDay > _dayCicleInSeconds)
			_timeOfDay -= _dayCicleInSeconds;
		
		if(!_isMorning && _timeOfDay > morningLight && _timeOfDay < nightLight)
		{
			_isMorning = true;
			Messenger<bool>.Broadcast("Morning Light Time", true);
		}
		else if (_isMorning && _timeOfDay > nightLight)
		{
			_isMorning = false;
			Messenger<bool>.Broadcast("Morning Light Time", false);
		}
		
		for(int cnt = 0; cnt < sun.Length; cnt ++)
		{
			sun[cnt].Rotate(new Vector3(_degreeRotation,0,0) * Time.deltaTime);
		}
		
		
		if(_timeOfDay > sunRise && _timeOfDay < _noonTime)
		{
			AdjustLinghting(true);
		}
		else if(_timeOfDay > _noonTime && _timeOfDay < sunSet)
		{
			AdjustLinghting(false);
		}
		
		if(_timeOfDay > sunRise && _timeOfDay < sunSet && RenderSettings.skybox.GetFloat("_Blend") < 1)
		{
			_tod = GameTime.TimeOfDay.SunRise;
			BlendSkybox();
		}		
		else if (_timeOfDay > sunSet && RenderSettings.skybox.GetFloat("_Blend") > 0)		
		{
			_tod = GameTime.TimeOfDay.SunSet;
			BlendSkybox();
		}
		else
		{
			_tod = GameTime.TimeOfDay.Idle;
		}
	}
	
	private void BlendSkybox()
	{
		float temp = 0;
		
		switch(_tod)
		{
		case TimeOfDay.SunRise:
			temp = (_timeOfDay - sunRise) / _dayCicleInSeconds * skyboxBlendModifier;
			break;
		case TimeOfDay.SunSet:
			temp = (_timeOfDay - sunSet) / _dayCicleInSeconds * skyboxBlendModifier;
			temp = 1 - temp;
			break;
		}
		
		RenderSettings.skybox.SetFloat("_Blend", temp);
	}
	
	private void SetupLighting()
	{
		RenderSettings.ambientLight = ambLightMin;
		
		for(int cnt = 0; cnt < _sunScript.Length; cnt++)
		{
			if(_sunScript[cnt].giveLight)
			{
				sun[cnt].GetComponent<Light>().intensity = _sunScript[cnt].minLightBrightness;
			}
		}
	}
	
	private void AdjustLinghting(bool brighten)
	{
		float pos = 0;
		
		if(brighten)
		{
			pos = (_timeOfDay - sunRise) / _morningLength;	//recebe a posição do sol no ceu de manha
			
			
		}
		else
		{
			pos = (sunSet - _timeOfDay) / _eveingLength;	//recebe a posição do sol no ceu de manha			
		}
		
		RenderSettings.ambientLight = new Color(ambLightMin.r + ambLightMax.r * pos, 
												ambLightMin.g + ambLightMax.g * pos, 
												ambLightMin.b + ambLightMax.b * pos);
		
		for(int cnt = 0; cnt < _sunScript.Length; cnt ++)
		{
			if(_sunScript[cnt].giveLight)
			{
				_sunScript[cnt].GetComponent<Light>().intensity = _sunScript[cnt].maxLightBrightness * pos;
			}
		}
	}
}
