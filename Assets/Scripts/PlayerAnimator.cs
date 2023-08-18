using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    [SerializeField] private Player m_player;
    private Animator p_animator;

    private void Awake()
    {
        p_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        p_animator.SetBool(IS_WALKING, m_player.IsWalking());
    }
}
