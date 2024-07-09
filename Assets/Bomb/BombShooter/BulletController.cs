using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Coroutine a;
    Rigidbody2D _rigi;
    [SerializeField] float _speed;
    [SerializeField] int _dmg;

    // Start is called before the first frame update
    void Start()
    {
        _dmg = PlayerPrefs.GetInt("Player_dmg");
        _rigi = this.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        a = StartCoroutine(Deactive());
    }
    private void OnDisable()
    {
        if (a != null)
        {
            StopCoroutine(a);
            a = null;
        }
    }

    void FixedUpdate()
    {
        _rigi.velocity = this.transform.up * _speed;
    }
    IEnumerator Deactive()
    {
        Vector2 screen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 pos = this.transform.position;
        while (pos.x >= -screen.x && pos.x <= screen.x && pos.y <= screen.y && pos.y >= -screen.y)
        {
            yield return new WaitForSeconds(1);
            pos = this.transform.position;
        }
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bom")
        {
            Vector2 collisionPoint = collision.ClosestPoint(transform.position);
            collision.transform.GetChild(1).GetComponent<HpBomb>().getDMG(_dmg, collisionPoint);
            
            this.gameObject.SetActive(false);
        }
    }
}
