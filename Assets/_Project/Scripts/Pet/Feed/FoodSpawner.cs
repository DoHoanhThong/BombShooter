using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    private RectTransform _spawnArea;
    [SerializeField]
    private PetFood[] _foodToSpawn;
    [SerializeField]
    private CleaningPaper cleaningPaperPrefab;
    [SerializeField]
    private float _spacing = 200f;
    [SerializeField]
    private float scaleDuration = .5f;
    [SerializeField]
    private float delaySpawnPaper = 3;
    private List<PetFood> foods = new List<PetFood>();


    public void SpawnFood()
    {
        float startY = 0 + (_foodToSpawn.Length / 2) * _spacing;

        for (int i = 0; i < _foodToSpawn.Length; i++)
        {
            Vector2 spawnPosition = new Vector2(0, startY - i * _spacing);

            PetFood spawnedFood = Instantiate(_foodToSpawn[i], _spawnArea.transform);
            spawnedFood.transform.localScale = Vector3.zero;
            spawnedFood.GetComponent<RectTransform>().anchoredPosition = spawnPosition;
            spawnedFood.gameObject.SetActive(true);
            foods.Add(spawnedFood);
            spawnedFood.transform.DOScale(1, scaleDuration).SetEase(Ease.OutBack).SetDelay(i * scaleDuration);
        }
    }
    public void SpawnPaper()
    {
        StartCoroutine(DelaySpawnPaper(delaySpawnPaper));
        
    }
    public void DisableFood()
    {
        foreach (var food in foods)
        {
            Destroy(food.gameObject);
        }
        foods.Clear();
    }
    IEnumerator DelaySpawnPaper(float delay)
    {
        yield return new WaitForSeconds(delay);
        CleaningPaper paper = Instantiate(cleaningPaperPrefab, _spawnArea.transform);
        paper.gameObject.SetActive(true);
        foods.Add(paper);
    }
}
