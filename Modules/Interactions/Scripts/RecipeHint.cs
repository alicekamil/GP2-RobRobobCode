using System;
using SpaceGame;
using UnityEngine;
using UnityEngine.UI;

public class RecipeHint : MonoBehaviour
{
    public void SetRecipe(TaskData data, int idx)
    {
        if (data == null)
        {
            Recipes[idx].Parent.SetActive(false);
        }
        else
        {
            Recipes[idx].ModifierImage.sprite = data.ModifierIcon;
            Recipes[idx].ResultImage.sprite = data.ResultIcon;
            Recipes[idx].Parent.SetActive(true);
        }
    }

    [Serializable]
    public struct RecipeItem
    {
        public GameObject Parent;
        public Image ModifierImage;
        public Image ResultImage;
    }

    public RecipeItem[] Recipes;
}