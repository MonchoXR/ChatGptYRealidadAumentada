using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class chatgptHttp : MonoBehaviour
{
    private string openAIURL = "https://api.openai.com/v1/chat/completions";
    private string apiKey = "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"; // Reemplaza con tu API Key

    void Start()
    {
        StartCoroutine(SendRequestToOpenAI("Hola, ¿cómo estás?"));
    }

    IEnumerator SendRequestToOpenAI(string userInput)
    {
        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "user", content = userInput }
            }
        };

        string jsonBody = JsonUtility.ToJson(requestBody);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest webRequest = new UnityWebRequest(openAIURL, "POST"))
        {
            webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + apiKey);

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Response: " + webRequest.downloadHandler.text);
            }
        }
    }

}
