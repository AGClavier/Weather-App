using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;

public class WeatherManager : MonoBehaviour
{
    public string api = "https://api.darksky.net/forecast/3049bfd4cdad104ba6f8789e3a448aeb/";

    public TextMeshProUGUI statusText;
    public TextMeshProUGUI location;
    public TextMeshProUGUI temperature;
    public TextMeshProUGUI condition;
    public TextMeshProUGUI precipProbability;

    public static string icon;

    private LocationInfo lastLocation;

    void Start()
    {
        StartCoroutine(FetchLocationData());
    }

    private IEnumerator FetchLocationData()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            statusText.text = "Location Timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            statusText.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            lastLocation = Input.location.lastData;
            UpdateWeatherData();
        }
        Input.location.Stop();
    }

    private void UpdateWeatherData()
    {
        StartCoroutine(FetchWeatherDataFromApi(lastLocation.latitude.ToString(), lastLocation.longitude.ToString()));
    }

    private IEnumerator FetchWeatherDataFromApi(string latitude, string longitude)
    {
        string url = api + latitude + "," + longitude + "?units=auto";
        UnityWebRequest fetchWeatherRequest = UnityWebRequest.Get(url);
        yield return fetchWeatherRequest.SendWebRequest();

        if (fetchWeatherRequest.isNetworkError)
        {
            //Check and print error
            Debug.Log("Error: " + fetchWeatherRequest.error);
        }
        else if (fetchWeatherRequest.isHttpError)
        {
            Debug.Log("Server Not Responding");
        }
        else
        {
            Debug.Log(fetchWeatherRequest.downloadHandler.text);
            var response = JSON.Parse(fetchWeatherRequest.downloadHandler.text);

            location.text = response["timezone"];
            temperature.text = response["currently"]["temperature"] + "°C";
            condition.text = response["currently"]["icon"];
            precipProbability.text = response["currently"]["precipProbability"] + "% chance of rain";
            icon = response["currently"]["icon"];
        }
    }
}