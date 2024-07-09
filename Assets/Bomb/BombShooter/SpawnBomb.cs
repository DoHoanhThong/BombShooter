using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static BombCTL;

public class SpawnBomb : MonoBehaviour
{
    Coroutine a;
    public static SpawnBomb instance;
    private static SpawnBomb _instance => instance;
    [SerializeField] List<GameObject> BombList = new List<GameObject>();
    [SerializeField] int _turn;
    [SerializeField] int _countBomb;
    [SerializeField] Transform _leftPos, _rightPos;
    [SerializeField] GameObject _listBomb;
    Vector2 _screen;
    int _totalBomb;
    void Start()
    {
        BombShooterController.End += Lose;
        instance = this;
        _totalBomb = 0;
        _screen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        _turn = 1;
        _countBomb = 0;
        a=StartCoroutine(StartSpawn());
    }
    private void OnDestroy()
    {
        BombShooterController.End -= Lose;
        if (a != null)
        {
            StopCoroutine(a);
            a = null;
        }
        //Des();
    }
    IEnumerator StartSpawn()
    {
        while (true)
        {
            if (BombShooterController.Instance.isEnd)
                break;
            while (!BombShooterController.Instance.isStart)
            {
                yield return new WaitForNextFrameUnit();
            }
            while (BombShooterController.Instance.bom_exist)
            {
                yield return new WaitForNextFrameUnit();
            }
            while (BombShooterController.Instance.isExistRocket)
            {
                yield return new WaitForNextFrameUnit();
            }
            GameObject g = null;
            int a = Random.Range(0, 100);
            HpBomb hp_tmp = new HpBomb();
            switch (_turn)
            {
                case 1:
                    g = ObjectPooling.instance.GetObject(BombList[0]);
                    _countBomb += 1;
                    _totalBomb += 1;
                    break;
                case 2:

                    g = ObjectPooling.instance.GetObject((a < 50) ? BombList[2] : BombList[3]);
                    _countBomb += 1;
                    _totalBomb += 1;
                    break;
                case 3:
                    g = ObjectPooling.instance.GetObject(BombList[4]);
                    _countBomb += 1;
                    _totalBomb += 1;
                    break;
                default:
                    break;
            }
            BombShooterController.Instance.HPbegin = 20 * _totalBomb + PlayerPrefs.GetInt("Player_dmg") * (_totalBomb - 1);
            BombShooterController.Instance.bom_exist = true;
            g.transform.GetChild(1).GetComponent<HpBomb>().HP = BombShooterController.Instance.HPbegin;
            g.transform.position = new Vector3((a < 50) ? -_screen.x - 4 : _screen.x + 4, _leftPos.position.y, 1);
            g.transform.rotation = Quaternion.identity;
            g.transform.SetParent(_listBomb.transform);
            g.transform.localScale = Vector3.one;
            yield return new WaitForNextFrameUnit();
            g.SetActive(true);

            if (_countBomb == 3)
            {
                _countBomb = 0;
                _turn += 1;
                if (_turn >= 4)
                {
                    _turn = 1;
                }
            }
            yield return new WaitForNextFrameUnit();
        }
    }
    public void SpawnX2(TypeBom a, Vector3 pos)
    {
        if (a == TypeBom.circle || a == TypeBom.grenade)
            return;
        GameObject g1 = null;
        GameObject g2 = null;
        if (a == TypeBom.bigbomb || a == TypeBom.biggrenade)
        {
            int b = Random.Range(0, 100);
            g1 = ObjectPooling.instance.GetObject((b < 50) ? BombList[0] : BombList[1]);
            b = Random.Range(0, 100);
            g2 = ObjectPooling.instance.GetObject((b < 50) ? BombList[0] : BombList[1]);
        }
        else if (a == TypeBom.rocket)
        {
            int b = Random.Range(0, 100);
            g2 = ObjectPooling.instance.GetObject((b < 50) ? BombList[2] : BombList[3]);
        }
        //setup
        g2.transform.GetChild(1).GetComponent<HpBomb>().HP = BombShooterController.Instance.HPbegin / 2;
        g2.transform.GetComponent<BombCTL>().x2 = true;
        g2.transform.GetComponent<BombCTL>().SetJumpLeft(true);
        g2.transform.position = pos;
        g2.transform.SetParent(_listBomb.transform);
        g2.transform.rotation = Quaternion.identity;
        g2.transform.localScale = Vector3.one;
        g2.SetActive(true);
        if (g1 == null)
            return;
        g1.transform.GetComponent<BombCTL>().x2 = true;
        g1.transform.GetChild(1).GetComponent<HpBomb>().HP = BombShooterController.Instance.HPbegin / 2;
        g1.transform.GetComponent<BombCTL>().SetJumpLeft(false);
        g1.transform.position = pos + new Vector3(0.1f, 0, 0);
        g1.transform.SetParent(_listBomb.transform);
        g1.transform.rotation = Quaternion.identity;
        g1.transform.localScale = Vector3.one;
        g1.SetActive(true);
    }
    void Lose()
    {
        Rigidbody2D tmp = null;
        for (int i = 0; i < _listBomb.transform.childCount; i++){
            tmp = _listBomb.transform.GetChild(0).GetComponent<Rigidbody2D>();
            tmp.velocity = Vector2.zero;
            tmp.gravityScale = 0;
        }
    }
    
}
