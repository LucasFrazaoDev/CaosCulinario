using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button m_playButton;
    [SerializeField] private Button m_quitButton;

    private void Awake()
    {
        m_playButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.GameScene); });
        m_playButton.onClick.AddListener(() => { Application.Quit(); });
    
        Time.timeScale = 1f;
    }
}
