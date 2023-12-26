using UnityEngine;

namespace SpaceGame
{
   public class OxygenManager : MonoBehaviour
   {
      private void Awake()
      {
         _oxygenLevel = 100f;
      }

      private void Update()
      {
         if (GameManager.Instance.IsGameplayPaused)
            return;
         int multiplier = RepairManager.Instance.TotalDamaged;
         if (multiplier != 0)
         {
            _oxygenLevel -= multiplier * _oxygenDecaySpeed * Time.deltaTime;
            if (_oxygenLevel <= 0)
            {
               _outOfOxygenEvent.RaiseEvent();
            }
         }
         else
         {
            _oxygenLevel += _oxygenRegainSpeed * Time.deltaTime;
         }

         _oxygenLevel = Mathf.Clamp(_oxygenLevel, 0, 100);
         _oxygenBar.Progress = _oxygenLevel / 100f;
         
         ShakeBar(Mathf.InverseLerp(30, 5, _oxygenLevel));
      }

      private void ShakeBar(float strength)
      {
         float offset = Mathf.Sin(Time.time * 35f) * strength;
         _oxygenBar.transform.localRotation = Quaternion.Euler(0, 0, offset);
      }
      
      [SerializeField]
      private float _oxygenRegainSpeed = 1f;
      [SerializeField]
      private float _oxygenDecaySpeed = 1f;
      [SerializeField]
      private ProgressBar _oxygenBar;
      [SerializeField]
      private VoidEventChannel _outOfOxygenEvent;
      private float _oxygenLevel;
   }
}