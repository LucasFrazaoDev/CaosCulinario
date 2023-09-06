using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_recipeNameText;
    [SerializeField] private Transform m_iconContainer;
    [SerializeField] private Transform m_iconTemplate;

    private void Awake()
    {
        m_iconTemplate.gameObject.SetActive(false);
    }

    public void setRecipeSO(RecipeSO recipeSO)
    {
        m_recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in m_iconContainer)
        {
            if (child == m_iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(m_iconTemplate, m_iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
