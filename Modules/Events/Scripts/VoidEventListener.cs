using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceGame
{
    public class VoidEventListener : MonoBehaviour
    {
        public UnityEvent RaisedEvent;

        private void Awake()
        {
            _channel.EventRaised += () =>
            {
                print("Event raised!");
                RaisedEvent?.Invoke();
            };
        }

        [SerializeField]
        private VoidEventChannel _channel;
    }
}