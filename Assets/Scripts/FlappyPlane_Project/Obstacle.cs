using UnityEngine;

namespace FlappyPlane_Project
{
    public class Obstacle : MonoBehaviour
    {
        private GameManager gameManager;

        public float highPosY = 1f;
        public float lowPosY = -1f;

        public float holeSizeMin = 1f;
        public float holeSizeMax = 3f;

        public Transform topObject;
        public Transform bottomObject;

        public float widthPadding = 4f;

        private void Start()
        {
            gameManager = GameManager.Instance;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Player>(out var player))
            {
                gameManager.AddScore(1);
            }
        }

        public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
        {
            float holeSize = Random.Range(holeSizeMin, holeSizeMax);
            float halfHoleSize = holeSize / 2f;
            topObject.localPosition = new Vector3(0, halfHoleSize);
            bottomObject.localPosition = new Vector3(0, -halfHoleSize);

            Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
            placePosition.y = Random.Range(lowPosY, highPosY);

            transform.position = placePosition;

            return placePosition;
        }
    }
}