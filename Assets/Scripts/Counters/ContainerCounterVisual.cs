using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter m_containerCounter;

    private Animator m_animator;
    private const string OPEN_CLOSE = "OpenClose";

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_containerCounter.OnPlayerGrabbedObject += M_containerCounter_OnPlayerGrabbedObject;
    }

    private void M_containerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        m_animator.SetTrigger(OPEN_CLOSE);
    }
}
