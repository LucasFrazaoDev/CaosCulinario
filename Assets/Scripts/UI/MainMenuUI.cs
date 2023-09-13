using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button m_playButton;
    [SerializeField] private Button m_quitButton;

    private void Awake()
    {
        m_playButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.GameScene); });
        m_quitButton.onClick.AddListener(() => 
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        });
    
        Time.timeScale = 1f;
    }
}
