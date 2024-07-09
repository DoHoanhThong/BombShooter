using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlockPuzzle
{
    public class ShapeStorage : MonoBehaviour
    {
        public static System.Action<Shape> OnLockShape;
        public List<ShapeData> shapeData;
        public List<Shape> shapeList;

        private int shapeCount;
        private void Start()
        {
            OnLockShape += CheckListShape;
            SpawnRandomWave();
        }
        private void OnDestroy()
        {
            OnLockShape -= CheckListShape;
        }
        private void SpawnRandomWave()
        {
            foreach (Shape shape in shapeList)
            {
                int shapeIndex = Random.Range(0, shapeData.Count);
                shape.CreateShape(shapeData[shapeIndex]);
                shape.gameObject.SetActive(true);
            }
            shapeCount = 3;
        }

        public void CheckListShape(Shape shape)
        {
            shapeCount--;
            if (shapeCount == 0)
            {
                SpawnRandomWave();
                return;
            }

            if (CheckGameOver() == true)
            {
                Debug.Log("Game Over!!!");
                GameController.OnOutOfPlace?.Invoke();
            }
            //check gameover

        }
        public void SpawnReviveWave()
        {
            for (int i = 0; i < shapeList.Count; i++)
            {
                int shapeIndex;
                do
                {
                    shapeIndex = Random.Range(0, shapeData.Count);
                } while (!GridController.Instance.CanPlace(shapeData[shapeIndex]));
                shapeList[i].CreateShape(shapeData[shapeIndex]);
                shapeList[i].gameObject.SetActive(true);
            }
            shapeCount = 3;
        }
        //public void RequestNewWave()
        //{
        //    foreach (Shape shape in shapeList)
        //    {
        //        shape.ClearShape();
        //    }
        //    SpawnRandomWave();
        //}
        public bool CheckGameOver()
        {
            for (int i = 0; i < shapeList.Count; i++)
            {
                if (shapeList[i].gameObject.activeSelf)
                    if (GridController.Instance.CanPlace(shapeList[i].CurrentShapeData))
                    {
                        return false;
                    }
            }
            return true;
        }
        public bool CheckCanPlace(ShapeData shapeData)
        {
            return GridController.Instance.CanPlace(shapeData);
        }

    }

}
