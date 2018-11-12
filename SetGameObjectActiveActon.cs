using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionVisualScripting.BasicActions
{
    public class SetGameObjectActiveActon : BaseAction
    {
        [RequiredReference, SerializeField] private GameObject _gameObject = null;
        [SerializeField] private bool _status = false;

        protected override void Function()
        {
            _gameObject.SetActive(_status);
        }
    }
}