using System.Collections;
using UnityEngine;

public class CardFlipAnimation : MonoBehaviour
{
    [SerializeField]
    private Sprite frontSprite; // Sprite mặt úp
    [SerializeField]
    private Sprite backSprite; // Sprite mặt ngửa

    public float flipDuration = 1f; // Thời gian hoàn thành lật thẻ
    [SerializeField]
    private float scaleMultiplier = 0.5f; // Hệ số với kích thước gốc

    private SpriteRenderer spriteRenderer;

    private bool isFlipping = false;
    [SerializeField]
    private float _startScale = 0.3f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isFlipping)
        {
            StartCoroutine(FlipCard());
        }
    }

    private IEnumerator FlipCard()
    {
        isFlipping = true;

        // Lật thẻ từ mặt úp sang mặt ngửa
        for (float t = 0f; t <= flipDuration / 2f; t += Time.deltaTime)
        {
            float normalizedTime = Mathf.Clamp01(t / (flipDuration / 2f));
            float rotationAngle = Mathf.Lerp(0f, 90f, normalizedTime);
            float scale = Mathf.Lerp(_startScale, scaleMultiplier, normalizedTime);

            transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            transform.localScale = new Vector3(scale, scale, 1f);

            yield return null;
        }

        // Thay đổi sprite thành mặt ngửa
        spriteRenderer.sprite = backSprite;

        // Lật thẻ từ mặt ngửa sang mặt úp
        for (float t = 0f; t <= flipDuration / 2f; t += Time.deltaTime)
        {
            float normalizedTime = Mathf.Clamp01(t / (flipDuration / 2f));
            float rotationAngle = Mathf.Lerp(90f, 180f, normalizedTime);
            float scale = Mathf.Lerp(scaleMultiplier, _startScale, normalizedTime);

            transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            transform.localScale = new Vector3(scale, scale, 1f);

            yield return null;
        }

        // Đảm bảo góc quay cuối cùng là 0
        //transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one  * _startScale;

        isFlipping = false;
    }
}
