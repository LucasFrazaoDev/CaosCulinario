using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image m_barImage;
    [SerializeField] private GameObject m_hasProgressGameObject;

    private IHasProgress m_hasProgress;
    private void Start()
    {
        m_hasProgress = m_hasProgressGameObject.GetComponent<IHasProgress>();
        if (m_hasProgress == null)
            Debug.LogError("Gameobject " + m_hasProgress + " dont have interface!");

        m_hasProgress.OnProgressChanged += M_HasProgress_OnProgressChanged;
        m_barImage.fillAmount = 0f;

        Hide();
    }

    private void M_HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        m_barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
            Hide();
        else
            Show();
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
