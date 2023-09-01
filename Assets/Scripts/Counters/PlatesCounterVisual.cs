using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter m_platesCounter;
    [SerializeField] private Transform m_counterTopPoint;
    [SerializeField] private Transform m_plateVisualPrefab;

    private List<GameObject> m_plateVisualGameObjectList;

    private void Awake()
    {
        m_plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        m_platesCounter.OnPlateSpawned += M_platesCounter_OnPlateSpawned;
        m_platesCounter.OnPlateRemoved += M_platesCounter_OnPlateRemoved;
    }

    private void M_platesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = m_plateVisualGameObjectList[m_plateVisualGameObjectList.Count - 1];
        m_plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void M_platesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(m_plateVisualPrefab, m_counterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * m_plateVisualGameObjectList.Count, 0);

        m_plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
