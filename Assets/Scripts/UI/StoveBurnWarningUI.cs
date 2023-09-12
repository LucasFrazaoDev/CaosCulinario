using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter m_stoveCounter;

    private void Start()
    {
        m_stoveCounter.OnProgressChanged += M_stoveCounter_OnProgressChanged;

        Hide();
    }

    private void M_stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = m_stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        if (show)
            Show();
        else
            Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
