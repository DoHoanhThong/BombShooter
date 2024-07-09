using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    public float speed;
    [SerializeField] Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        speed = LevelManager.instance._speedofPipe;
        
        StartCoroutine(DeactiveAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.instance.isEnd)
            return;
        if (!LevelManager.instance.isStart)
            return;
        if(speed!= LevelManager.instance._speedofPipe)
        {
            speed= LevelManager.instance._speedofPipe;
            Debug.LogError("Increase: " + speed);
        }
        Move();
    }
    private void Move()
    {
        this.transform.position += Vector3.left * speed * Time.deltaTime;
    }
    IEnumerator DeactiveAfterTime()
    {
        Vector2 screen= new Vector2 (Screen.width, Screen.height);
        Vector2 newScreen= Camera.main.ScreenToWorldPoint (screen);
        while(this.transform.position.x >= - newScreen.x)
        {
            yield return new WaitForSeconds(1);
        }
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }
}
