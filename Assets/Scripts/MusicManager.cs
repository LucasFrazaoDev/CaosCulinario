using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private const string PLAYER_MUSIC_VOLUME = "MusicVolume";

    public static MusicManager Instance { get; private set; }

    private AudioSource m_audioSource;
    private float m_volume = 0.3f;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
        Instance = this;

        m_volume = PlayerPrefs.GetFloat(PLAYER_MUSIC_VOLUME, 0.3f);
        m_audioSource.volume = m_volume;
    }

    public void ChangeVolume()
    {
        m_volume += 0.1f;
        if (m_volume > 1f)
        {
            m_volume = 0f;
        }

        m_audioSource.volume = m_volume;

        PlayerPrefs.SetFloat(PLAYER_MUSIC_VOLUME, m_volume);
    }

    public float GetVolume()
    {
        return m_volume;
    }
}
