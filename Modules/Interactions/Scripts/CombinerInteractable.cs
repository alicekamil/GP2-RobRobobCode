using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceGame
{
    /// <summary>
    /// Combines two items using the recipe system
    /// </summary>
    public class CombinerInteractable : Interactable
    {
        public void Craft()
        {
            _animator.Play("Craft");
            LeanTween.delayedCall(gameObject, 1f, () => _combineClip.Play());
            LeanTween.delayedCall(gameObject, 1f,
                () => ParticleManager.Instance.Spawn(ParticleType.Smack, _effectOrigin.position));
            _resultItemHolder.SetItem(_resultItemId);
            _items.Clear();
            _buttons.Disable();
        }

        public override bool CanInteract(Interactor interactor)
        {
            if (!base.CanInteract(interactor))
            {
                return false;
            }

            if (interactor.ItemHolder.ItemId == null && CanPickup())
            {
                return true;
            }

            if (CanAddItem(interactor.ItemHolder.ItemId) && !CanPickup())
            {
                return true;
            }

            return false;
        }

        protected override void OnInteractionFinished()
        {
            if (CanPickup())
            {
                _currentInteractor.ItemHolder.SetItem(_resultItemHolder.ItemId);
                _resultItemHolder.SetItem(null);
                _animator.Play("Idle");
                foreach (var itemHolder in _itemHolders)
                {
                    itemHolder.SetItem(null);
                }
            }
            else
            {
                AddItem(_currentInteractor.ItemHolder.ItemId);
                _dropClip.Play();
                _currentInteractor.ItemHolder.SetItem(null);
                if (GetResult() != null)
                {
                    _resultItemId = GetResult();
                    _buttons.Enable();
                }
            }

            base.OnInteractionFinished();
        }

        private void Awake()
        {
            _items = new List<string>();
        }
        
        private void Start()
        {
            EncounterManager.Instance.EncounterChanged += OnEncounterChanged;
        }

        private void OnEncounterChanged()
        {
            // Disable crafting if we can't get special ammo
            IsDisabled = !EncounterManager.CurrentEncounter.AllowSpecialAmmo;
        }

        private bool CanAddItem(string id)
        {
            if (id == null)
            {
                return false;
            }

            var newItems = _items.Append(id).ToList();
            foreach (var recipe in _recipes)
            {
                if (recipe.OverlapsItems(newItems, _combineInSequence))
                {
                    return true;
                }
            }

            return false;
        }

        private void AddItem(string id)
        {
            _items.Add(id);
            _itemHolders[_items.Count - 1].SetItem(id);
        }

        private string GetResult()
        {
            foreach (var recipe in _recipes)
            {
                if (recipe.CanCraft(_items, _combineInSequence))
                {
                    return recipe.Result;
                }
            }

            return null;
        }

        private bool CanPickup() => _resultItemHolder.ItemId != null;

        [SerializeField] private List<RecipeData> _recipes;
        [SerializeField] private ItemHolder _resultItemHolder;
        [SerializeField] private List<ItemHolder> _itemHolders;
        [SerializeField] private bool _combineInSequence;
        [SerializeField] private CoopButtonInteractable _buttons;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioClipSO _combineClip;
        [SerializeField] private AudioClipSO _dropClip;
        [SerializeField] private Transform _effectOrigin;

        private string _resultItemId;
        private List<string> _items;
    }
}