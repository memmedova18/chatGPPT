using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;

public class ChatGPTAssistant : EditorWindow
{
    private string _promptText = "";
    private string _responseText = "";

    private void OnGUI()
    {
        GUILayout.Label("How I can help you?", EditorStyles.boldLabel);
        _promptText = EditorGUILayout.TextField("How I can help you?", _promptText);


        GUILayout.Label("Response", EditorStyles.boldLabel);
        _responseText = EditorGUILayout.TextArea(_responseText,
            GUILayout.MaxHeight(150));


        if (GUILayout.Button("ASK GPT!", GUILayout.Height(60)))
        {
            UnityWebRequest www = UnityWebRequest.Get("https://explain-like-im-five-api.onrender.com/api/explain?explain=" + _promptText);
            www.SendWebRequest();
            while (!www.isDone)
            {
                Debug.Log("Loading...");
            }

            if (www.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                _responseText = www.downloadHandler.text;
                Debug.Log(_responseText);
            }
        }
    }

    [MenuItem("GPT Assistant/GPT Assistant")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<ChatGPTAssistant>("GPT Assistant");
    }

    [MenuItem("GPT Assistant/About")]
    public static void ShowAbout()
    {
        EditorUtility.DisplayDialog("About GPT Assistant",
            "GPT Assistant is a tool that helps you to create your game faster and easier. It's a tool that uses AI to help you with your game development.",
            "OK");
    }
}