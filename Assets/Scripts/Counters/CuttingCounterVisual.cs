using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter m_cuttingCounter;

    private Animator m_animator;
    private const string CUT = "Cut";

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_cuttingCounter.OnCut += M_cuttingCounter_OnCut;
    }

    private void M_cuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        m_animator.SetTrigger(CUT);
    }
}
