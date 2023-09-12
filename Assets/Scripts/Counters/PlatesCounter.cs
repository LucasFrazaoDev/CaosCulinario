using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO m_plateKitchenObjectSO;
    private float m_spawnTimer;
    private float m_spawnPlateTimerMax = 4f;
    private int m_platesSpawnedAmout;
    private int m_platesSpawnedAmoutMax = 4;

    private void Update()
    {
        m_spawnTimer += Time.deltaTime;
        if(m_spawnTimer > m_spawnPlateTimerMax)
        {
            m_spawnTimer = 0f;

            if(GameManager.Instance.IsGamePlaying() && m_platesSpawnedAmout < m_platesSpawnedAmoutMax)
            {
                m_platesSpawnedAmout++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            // Player dont have nothing
            if(m_platesSpawnedAmout > 0)
            {
                // There is at least one plate here
                m_platesSpawnedAmout--;

                KitchenObject.SpawnKitchenObject(m_plateKitchenObjectSO, player);
            
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
