using Google.XR.Cardboard;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Interactions
{
    public class GazeInteractionSource : MonoBehaviour
    {
        [SerializeField] private float intractableDistance = 10;

        [SerializeField] private UnityEvent onFocusIntractable;
        [SerializeField] private UnityEvent onLoseFocus;
        [SerializeField] private UnityEvent onClick;

        private GameObject _gazedObject;
        private PointerEventData _eventData;

        private void Start()
        {
            _eventData = new PointerEventData(EventSystem.current);
        }

        public void Update()
        {
            UpdateInteraction();
        }

        private void UpdateInteraction()
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hit, intractableDistance))
            {
                if (_gazedObject != hit.transform.gameObject)
                {
                    if (_gazedObject)
                    {
                        _gazedObject.GetComponent<IPointerExitHandler>()?.OnPointerExit(_eventData);

                        if (IsGazedObjectIntractable())
                            onLoseFocus?.Invoke();
                    }

                    _gazedObject = hit.transform.gameObject;
                    _gazedObject.GetComponent<IPointerEnterHandler>()?.OnPointerEnter(_eventData);

                    if (IsGazedObjectIntractable())
                        onFocusIntractable?.Invoke();
                }
            }
            else if (_gazedObject)
            {
                _gazedObject.GetComponent<IPointerExitHandler>()?.OnPointerExit(_eventData);
                _gazedObject = null;
                onLoseFocus?.Invoke();
            }

            if (_gazedObject != null && Api.IsTriggerPressed)
            {
                _gazedObject.GetComponent<IPointerClickHandler>()?.OnPointerClick(_eventData);

                if (IsGazedObjectIntractable())
                    onClick?.Invoke();
            }
        }

        private bool IsGazedObjectIntractable()
        {
            return _gazedObject.GetComponent<IEventSystemHandler>() != null;
        }
    }
}