using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class DataController
{
    public string www;
    public Dictionary<string,string> character = new Dictionary<string, string>();
}


public class WebManager : MonoBehaviour
{
    public Data characters;
    private readonly string _initUrl =
        "https://raw.githubusercontent.com/OldBernhardt/Langrisser/main/config/init.json";

    private Configuration _configuration;
    

    [SerializeField] private TMP_Text messageText;
    [SerializeField] private GUIController _guiController;

    

    void Start()
    {
        StartCoroutine(GetConfiguration());
    }

    IEnumerator GetConfiguration()
    {
        UnityWebRequest www = UnityWebRequest.Get(_initUrl);
        yield return www.SendWebRequest();

        if (www.isHttpError || www.isNetworkError)
        {
            messageText.text = "Error";
        }
        else
        {
            _configuration = JsonUtility.FromJson<Configuration>(www.downloadHandler.text);
            
            UnityWebRequest www2 = UnityWebRequest.Get(_configuration.characterWWW);
            yield return www2.SendWebRequest();
            if (www2.isHttpError || www2.isNetworkError)
            {
                messageText.text = "Error";
            }
            else
            {
                characters= JsonUtility.FromJson<Data>(www2.downloadHandler.text);
                _guiController.InstantiateContentLists(characters);
                _guiController.canDisplayMenu = true;
                messageText.text = "Right Click Me";

            }

            
            
        }
        
    }


    
}
