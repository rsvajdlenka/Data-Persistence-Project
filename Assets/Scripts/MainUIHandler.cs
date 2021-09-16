using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIHandler : MonoBehaviour
{
    [SerializeField] private InputField NameTextEditor;
    [SerializeField] private Text BestScoreText;

    public void StartNew()
    {
        if (this.NameTextEditor != null)
        {
            GameManager.Instance.Name = this.NameTextEditor.text;
        }
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        GameManager.Instance.SaveData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void Start()
    {
        this.BestScoreText.text = GameManager.Instance.GetBestScoreText();
    }
}
