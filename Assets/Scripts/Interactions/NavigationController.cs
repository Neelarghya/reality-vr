using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Interactions
{
    public class NavigationController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private NavMeshAgent playerNavMeshAgent;
        [SerializeField] private Transform marker;
        [SerializeField] private float markerHideDelay = 2f;

        private const float SpeedMultiplier = 0.75f;
        private Coroutine _displayMarkerCoroutine;

        public void OnPointerClick(PointerEventData eventData)
        {
            Vector3 position = eventData.pointerPressRaycast.worldPosition;
            playerNavMeshAgent.SetDestination(position);

            if (_displayMarkerCoroutine != null) StopCoroutine(_displayMarkerCoroutine);
            _displayMarkerCoroutine = StartCoroutine(DisplayMarker(position, markerHideDelay));
        }

        private IEnumerator DisplayMarker(Vector3 position, float delay)
        {
            marker.position = position;
            marker.gameObject.SetActive(true);
            float time = 0;
            while (time < delay)
            {
                marker.position += SpeedMultiplier * Time.deltaTime * Vector3.down;
                yield return null;
                time += Time.deltaTime;
            }

            marker.gameObject.SetActive(false);
        }
    }
}