using System.Collections;
using AriumFramework;
using AriumFramework.Exceptions;
using AriumFramework.Plugins.UnityCore.Interactions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class NavigationTests
    {
        private Arium _arium;
        private static readonly int IsOpen = Animator.StringToHash("isOpen");

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _arium = new Arium();
            SceneManager.LoadScene("Main");
        }

        [UnityTest]
        public IEnumerator ShouldMoveToPosition()
        {
            Vector3 position = new Vector3(3f, 0.1f, 6f);
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                pointerPressRaycast = new RaycastResult
                {
                    worldPosition = position
                }
            };

            _arium.PerformAction(new UnityPointerClick(eventData), "Ground");

            yield return new WaitForSeconds(4);

            float distance = Vector3.Distance(position, _arium.GetComponent<Transform>("Player").position);
            
            Assert.IsTrue(distance < 0.02f);
        }

        [UnityTest]
        public IEnumerator ShouldDestroyCollectableOnCollection()
        {
            const string collectable = "Collectable";
            Transform player = _arium.GetComponent<Transform>("Player");
            Transform collectableTransform = _arium.GetComponent<Transform>(collectable);
            
            Vector3 position = Vector3.Lerp(player.position, collectableTransform.position, 0.5f);
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                pointerPressRaycast = new RaycastResult {worldPosition = position}
            };

            _arium.PerformAction(new UnityPointerClick(eventData), "Ground");

            yield return new WaitForSeconds(2);

            player.LookAt(collectableTransform);
            
            yield return new WaitForSeconds(0.5f);
            
            Assert.IsTrue(_arium.GetComponent<Animator>("Reticle").GetBool(IsOpen));
            _arium.PerformAction(new UnityPointerClick(), collectable);

            yield return new WaitForSeconds(2);

            Assert.Throws<GameObjectNotFoundException>(() => _arium.FindGameObject(collectable));
        }
    }
}