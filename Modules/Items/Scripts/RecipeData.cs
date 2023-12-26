using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    [CreateAssetMenu(fileName = "NewRecipe", menuName = "Data/RecipeData")]
    public class RecipeData : ScriptableObject
    {
        public IReadOnlyList<string> RequiredItems => _requiredItems;
        public string Result => _result;

        public bool CanCraft(List<string> items, bool sequence)
        {
            if (items.Count != _requiredItems.Count)
                return false;

            return OverlapsItems(items, sequence);
        }

        public bool OverlapsItems(List<string> items, bool sequence)
        {
            if (sequence)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (_requiredItems[i] != items[i])
                        return false;
                }
            }
            else
            {
                var unusedItems = new List<string>(_requiredItems);
                for (int i = 0; i < items.Count; i++)
                {
                    int itemIdx = unusedItems.FindIndex(x => x == items[i]);
                    if (itemIdx == -1)
                    {
                        return false;
                    }

                    unusedItems.RemoveAt(itemIdx);
                }
            }

            return true;
        }

        [SerializeField] private List<string> _requiredItems;
        [SerializeField] private string _result;
    }
}