using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurningFlashingBarUI : MonoBehaviour
{
    private const string IS_FLASHING = "isFlashing";

    [SerializeField] private StoveCounter m_stoveCounter;

    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_stoveCounter.OnProgressChanged += M_stoveCounter_OnProgressChanged;

        m_animator.SetBool(IS_FLASHING, false);
    }

    private void M_stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = m_stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        m_animator.SetBool(IS_FLASHING, show);
    }
}
