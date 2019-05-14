using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnFood : MonoBehaviour
{

    public GameObject foodPrefab;

    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    private static int foodCount = 0;
    private const int maxFoodCountInScene = 4;

    public static void EatOne()
    {
        if (foodCount >= 1)
        {
            foodCount--;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("Spawn", 3, 4);
    }

    private void Spawn()
    {
        if (GameController.isPaused) return;

        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);
        int y = (int)Random.Range(borderBottom.position.y, borderTop.position.y);

        if (foodCount < maxFoodCountInScene)
        {
            Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
            foodCount++;
        }
    }
}
