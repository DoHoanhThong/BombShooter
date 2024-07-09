using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PipeBoard : MonoBehaviour
{
    public static System.Action OnPipeClicked;
    [SerializeField]
    private PipeLevelSetting currentLevelSetting;
    [SerializeField]
    private GridLayoutGroup gridLayout;
    [SerializeField]
    private PipeCell cellPrefab;

    [SerializeField]
    private PipeData[] pipesData;
    [SerializeField]
    private float duration = 2;

    private List<PipeCell> pipeCells = new List<PipeCell>();

    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();       
        OnPipeClicked += OnCheckComplete;       
    }
    private void OnDestroy()
    {
        OnPipeClicked -= OnCheckComplete;
    }
    public void Init(PipeLevelSetting levelSetting)
    {
        this.currentLevelSetting = levelSetting;
        gridLayout.constraintCount = levelSetting.columns;
        for (int i = 0; i < levelSetting.rows * levelSetting.columns; i++)
        {
            var cell = Instantiate(cellPrefab, gridLayout.transform);
            if (levelSetting.cellNumbers[i] != 0)
            {
                cell.Initialize(GetData(levelSetting.cellNumbers[i].ToString()), levelSetting.cellRotations[i]);
            }
            cell.gameObject.SetActive(true);
            pipeCells.Add(cell);
        }
    }
    private PipeData GetData(string pipeID)
    {
        for (int i = 0; i < pipesData.Length; i++)
        {
            if (pipesData[i].id == pipeID)
            {
                return pipesData[i]; 
            }
        }
        return null;
    }
    private void OnCheckComplete()
    {
        if (CheckComplete())
        {
            Debug.Log("Level Complete!!");
            PipeConnectController.OnCompleteLevel?.Invoke();
        }
    }
    public bool CheckComplete()
    {
        for (int i = 0; i < pipeCells.Count; i++)
        {
            if (!pipeCells[i].CheckCorrect())
                return false;
        }
        return true;
    }
    public void NextLevel(PipeLevelSetting nextLevelSetting)
    {
        rect.DOAnchorPosY(1000, duration).OnComplete(() => 
        { 
            foreach (var item in pipeCells)
            {
                Destroy(item.gameObject);
                
            }
            pipeCells.Clear();
            //Init next level
            Init(nextLevelSetting);
            rect.DOAnchorPosY(-25, duration).SetDelay(1);
        });
        
    }
}
