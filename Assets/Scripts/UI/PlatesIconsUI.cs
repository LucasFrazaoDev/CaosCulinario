using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject m_plateKitchenObject;
    [SerializeField] private Transform m_iconTemplate;

    private void Awake()
    {
        m_iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        m_plateKitchenObject.OnIngredientAdded += M_plateKitchenObject_OnIngredientAdded;
    }

    private void M_plateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == m_iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in m_plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(m_iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlatesIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
