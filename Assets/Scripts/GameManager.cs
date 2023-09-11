using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State m_state;
    private float m_waitingToStartTimer = 1f;
    private float m_countdownToStartTimer = 3f;
    private float m_gamePlayingTimer;
    private float m_gamePlayingTimerMax = 45f;
    private bool m_isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        m_state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (m_state)
        {
            case State.WaitingToStart:
                m_waitingToStartTimer -= Time.deltaTime;
                if(m_waitingToStartTimer < 0f)
                {
                    m_state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                m_countdownToStartTimer -= Time.deltaTime;
                if (m_countdownToStartTimer < 0f)
                {
                    m_state = State.GamePlaying;
                    m_gamePlayingTimer = m_gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                m_gamePlayingTimer -= Time.deltaTime;
                if (m_gamePlayingTimer < 0f)
                {
                    m_state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
            default:
                break;
        }
        Debug.Log(m_state);
    }

    public bool IsGamePlaying()
    {
        return m_state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return m_state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return m_countdownToStartTimer;
    }

    public bool IsGameOver()
    {
        return m_state == State.GameOver;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (m_gamePlayingTimer / m_gamePlayingTimerMax);
    }

    public void TogglePauseGame()
    {
        m_isGamePaused = !m_isGamePaused;

        if (m_isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
