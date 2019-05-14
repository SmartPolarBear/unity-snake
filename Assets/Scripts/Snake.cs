using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    private static long intialLen = 1;
    public static long length = intialLen;

    Vector2 direction = Vector2.right;
    List<Transform> tail = new List<Transform>();

    bool ate = true;

    public GameObject tailPrefab;

    // Start is called before the first frame update
    void Start()
    {
        length = intialLen;
        InvokeRepeating(nameof(this.Move), 0.3f, 0.3f);

        var rootObjcs = gameObject.scene.GetRootGameObjects();
        var layout = (from c in rootObjcs where c.name.Contains("Layout") select c).First();
        var layoutObjcs = layout.GetComponentsInChildren<Canvas>();
        var ctrlPad = (from c in layoutObjcs where c.name.Contains("ControlPad") select c).First();

        var ctrlBtns = ctrlPad.GetComponentsInChildren<Button>();

        var lBtn = (Button)(from b in ctrlBtns where b.name.Contains("Left") select b).First();
        lBtn.onClick.AddListener(() =>
        {
            direction = Vector2.left;
        });

        var rBtn = (Button)(from b in ctrlBtns where b.name.Contains("Right") select b).First();
        rBtn.onClick.AddListener(() =>
        {
            direction = Vector2.right;
        });

        var uBtn = (Button)(from b in ctrlBtns where b.name.Contains("Up") select b).First();
        uBtn.onClick.AddListener(() =>
        {
            direction = Vector2.up;
        });

        var dBtn = (Button)(from b in ctrlBtns where b.name.Contains("Down") select b).First();
        dBtn.onClick.AddListener(() =>
        {
            direction = Vector2.down;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }

    }

    private void Move()
    {
        if (GameController.isPaused) return;

        Vector2 currentPos = transform.position;
        transform.Translate(direction);

        if (ate)
        {
            var newPart = (GameObject)Instantiate(tailPrefab, currentPos, Quaternion.identity);
            tail.Insert(0, newPart.transform);
            length++;
            ate = false;
        }
        else if (tail.Count != 0)
        {
            tail.Last().position = currentPos;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        //CAN BE IMPROVE WITH TAG PROPERTY
        if (collision.name.StartsWith("Food"))
        {
            ate = true;
            Destroy(collision.gameObject);
            SpawnFood.EatOne();
        }
        else if(collision.tag.Contains("Border"))
        {
            GameController.FailGame();
            //TODO:YOU LOSE SCREEN
        }
    }
}
