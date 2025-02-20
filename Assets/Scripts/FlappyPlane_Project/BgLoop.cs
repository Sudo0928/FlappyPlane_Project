using UnityEngine;

namespace FlappyPlane_Project
{
    public class BgLoop : MonoBehaviour
    {
        public int numBgCount = 5;

        public int obstacleCount = 0;
        public Vector3 obstacleLastPosition = Vector3.zero;

        private void Start()
        {
            Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
            obstacleLastPosition = obstacles[0].transform.position;
            obstacleCount = obstacles.Length;

            for (int i = 0; i < obstacleCount; i++)
            {
                obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigged: " + collision.name);

            if (collision.CompareTag("BackGround"))
            {
                float widthOfBgObject = ((BoxCollider2D)collision).size.x;
                Vector3 pos = collision.transform.position;

                pos.x = widthOfBgObject * numBgCount;
                collision.transform.position = pos;
                return;
            }

            Obstacle obstacle = collision.GetComponent<Obstacle>();
            if (obstacle)
            {
                obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
            }
        }
    }
}