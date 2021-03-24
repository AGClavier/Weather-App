using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using System;

public class Shanghai : MonoBehaviour
{
    public string api;

    public TextMeshProUGUI location;
    public TextMeshProUGUI temperature;
    public TextMeshProUGUI humidity;
    public TextMeshProUGUI condition;
    public TextMeshProUGUI windspeed;
    public TextMeshProUGUI precipProbability;
    public TextMeshProUGUI precipIntensity;

    void Start()
    {
        StartCoroutine(Update());
    }

    IEnumerator Update()
    {
        api = "https://api.darksky.net/forecast/3049bfd4cdad104ba6f8789e3a448aeb/39.9042,116.4074?units=auto";
        StartAPI();
        yield break;
    }

    private void StartAPI()
    {
        StartCoroutine(FetchWeatherDataFromApi());
    }

    private IEnumerator FetchWeatherDataFromApi()
    {
        string url = api;
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
            humidity.text = response["hourly"]["data"][0]["humidity"] + "% Humidity";
            condition.text = response["hourly"]["icon"];
            windspeed.text = "Windspeed is " + response["hourly"]["data"][0]["windSpeed"] + " mp/h";
            precipProbability.text = response["hourly"]["data"][0]["precipProbability"] + "% chance of rain";
            precipIntensity.text = response["hourly"]["data"][0]["precipIntensity"] + " levels of rain";
        }
    }
}