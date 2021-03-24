using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
	public WeatherManager wm;
	public string currentWeather;
	private bool rain;
	private bool cloudy;
	private bool cloudy2;
	private bool sunny;
	private bool night;

	public GameObject rainObject;
	public GameObject cloudyObject;
	public GameObject cloudyObject2;
	public GameObject sunnyObject;
	public GameObject nightObject;

	void Update()
	{
		currentWeather = WeatherManager.icon;

		if (currentWeather == "rain" || currentWeather == "snow" || currentWeather == "sleet")
		{
			SpawnRain();
		}
		else if (currentWeather == "cloudy" || currentWeather == "partly-cloudy-day" || currentWeather == "fog")
		{
			SpawnCloudy();
		}
		else if (currentWeather == "clear-day" || currentWeather == "wind")
		{
			SpawnSunny();
		}
		else if (currentWeather == "clear-night" || currentWeather == "partly-cloudy-night")
		{
			SpawnNight();
		}
		else
		{
			None();
		}
	}

	void SpawnRain()
	{
		rain = true;
		rainObject.SetActive(true);
		if (cloudy)
		{
			StartCoroutine(DisableCloudy());
		}
		else if (sunny)
		{
			StartCoroutine(DisableSunny());
		}
		else if (night)
		{
			StartCoroutine(DisableNight());
		}
	}

	void SpawnCloudy()
	{
		cloudy = true;
		cloudy2 = true;
		cloudyObject.SetActive(true);
		cloudyObject2.SetActive(true);
		if (rain)
		{
			StartCoroutine(DisableRain());
		}
		else if (sunny)
		{
			StartCoroutine(DisableSunny());
		}
		else if (night)
		{
			StartCoroutine(DisableNight());
		}
	}

	void SpawnSunny()
	{
		sunny = true;
		sunnyObject.SetActive(true);
		if (cloudy)
		{
			StartCoroutine(DisableCloudy());
		}
		else if (rain)
		{
			StartCoroutine(DisableRain());
		}
		else if (night)
		{
			StartCoroutine(DisableNight());
		}
	}

	void SpawnNight()
	{
		night = true;
		nightObject.SetActive(true);
		if (cloudy)
		{
			StartCoroutine(DisableCloudy());
		}
		else if (rain)
		{
			StartCoroutine(DisableRain());
		}
		else if (sunny)
		{
			StartCoroutine(DisableSunny());
		}
	}

	void None()
	{
		if (rain)
		{
			StartCoroutine(DisableRain());
		}
		else if (cloudy)
		{
			StartCoroutine(DisableCloudy());
		}
		else if (sunny)
		{
			StartCoroutine(DisableSunny());
		}
		else if (night)
		{
			StartCoroutine(DisableNight());
		}
	}

	IEnumerator DisableRain()
	{
		rain = false;
		rainObject.GetComponent<ParticleSystem>().Stop();
		rainObject.GetComponent<Animator>().Play("rain_exit");
		yield return new WaitForSeconds(5);
		rainObject.SetActive(false);
	}

	IEnumerator DisableCloudy()
	{
		cloudy = false;
		cloudyObject.GetComponent<Animator>().Play("cloudy_exit");
		yield return new WaitForSeconds(5);
		cloudyObject.SetActive(false);
	}

	IEnumerator DisableSunny()
	{
		sunny = false;
		sunnyObject.GetComponent<Animator>().Play("sunny_exit");
		yield return new WaitForSeconds(5);
		sunnyObject.SetActive(false);
	}

	IEnumerator DisableNight()
	{
		night = false;
		nightObject.GetComponent<Animator>().Play("night_exit");
		yield return new WaitForSeconds(5);
		nightObject.SetActive(false);
	}
}