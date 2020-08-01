using System.Collections;
using Managers;
using UnityEngine;

namespace Animations
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private float collectTime = 3f;
        
        private Transform _player;
        private const float CollectingDistance = 0.1f;
        
        private void Start()
        {
            _player = GameManager.Instance.GetPlayer();
        }

        public void Collect()
        {
            Collider collider = GetComponent<Collider>();
            if (collider) collider.enabled = false;

            StartCoroutine(Collect(collectTime));
        }

        private IEnumerator Collect(float delay)
        {
            float time = 0;

            while (time < delay && Vector3.Distance(transform.position, _player.position) > CollectingDistance)
            {
                transform.position = Vector3.Lerp(transform.position, _player.position, time / delay);
                yield return null;
                time += Time.deltaTime;
            }
            
            Destroy(gameObject);
        }
    }
}