using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO m_recipeListSO;
    private List<RecipeSO> m_waitingRecipeSOList;
    private float m_spawnRecipeTimer;
    private float m_spawnRecipeTimerMax = 4f;
    private int m_waitingRecipeMax = 4;

    private void Awake()
    {
        Instance = this;
        m_waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        m_spawnRecipeTimer -= Time.deltaTime;
        if(m_spawnRecipeTimer <= 0f)
        {
            m_spawnRecipeTimer = m_spawnRecipeTimerMax;

            if(m_waitingRecipeSOList.Count < m_waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = m_recipeListSO.recipeSOList[Random.Range(0, m_recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                m_waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < m_waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = m_waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // Has the same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    // Cycling through all ingredients in the recipe
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        // Cycling through all ingredients in the plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            // ingredient matches
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        // This recipe ingredient wasn't found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }
                if(plateContentsMatchesRecipe)
                {
                    //`Player delivered the correct recipe!
                    Debug.Log("Player delivered the correct recipe!");
                    m_waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        // No matches found, wrong recipe
        Debug.Log("Wrong recipe!!");
    }
}
