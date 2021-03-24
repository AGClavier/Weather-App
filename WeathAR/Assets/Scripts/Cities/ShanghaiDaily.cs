using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using System;

public class ShanghaiDaily : MonoBehaviour
{
    public string api;

    public TextMeshProUGUI humidityDaily;
    public TextMeshProUGUI conditionDaily;
    public TextMeshProUGUI windspeedDaily;
    public TextMeshProUGUI precipProbabilityDaily;
    public TextMeshProUGUI precipIntensityDaily;

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

            humidityDaily.text = response["daily"]["data"][0]["humidity"] + "% Humidity";
            conditionDaily.text = response["daily"]["icon"];
            windspeedDaily.text = "Windspeed is " + response["daily"]["data"][0]["windSpeed"] + " mp/h";
            precipProbabilityDaily.text = response["daily"]["data"][0]["precipProbability"] + "% chance of rain";
            precipIntensityDaily.text = response["daily"]["data"][0]["precipIntensity"] + " levels of rain";
        }
    }
}