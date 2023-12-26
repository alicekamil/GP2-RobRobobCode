using SpaceGame;
using TMPro;
using UnityEngine;

public class WeaponReadyText : MonoBehaviour
{
    private void Update()
    {
        _text.enabled = _railgunManager._readyToFire;
    }

    [SerializeField] private RailgunManager _railgunManager;
    [SerializeField] private TMP_Text _text;
}