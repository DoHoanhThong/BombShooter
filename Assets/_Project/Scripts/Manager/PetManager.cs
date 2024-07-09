using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    [SerializeField]
    private int maxPetRequestPerTime = 2;
    [SerializeField]
    private float nextRequestTime = 15;
    [SerializeField]
    private PetController petPrefab;
    [SerializeField]
    private UnlockGiftbox giftboxPrefab;
    [SerializeField]
    private Transform petHolder;
    [SerializeField]
    private Bound bound;
    
    [SerializeField]
    private List<PetController> listPet = new List<PetController>();

    private List<PetController> requestingPets = new List<PetController>();
    private float counter = 0;

    private void Start()
    {
        Invoke("Initialize", .5f);
        //Initialize();
        counter = 0;
    }
    private void Update()
    {
        counter += Time.deltaTime;
        if (counter > nextRequestTime)
        {
            SendRequest();
            counter = 0;
        }
    }

    public void Initialize()
    {
        foreach(var data in DataContainer.Instance.runtimeDatas)
        {
            Vector2 spawnPos = new Vector2(Random.Range(bound.leftBound, bound.rightBound), Random.Range(bound.bottomBound, bound.topBound));
            SpawnPet(data, spawnPos);
        }
    }
    public PetController SpawnPet(RuntimeData data, Vector2 spawnPos)
    {
        var pet = Instantiate(petPrefab, petHolder);
        pet.transform.position = spawnPos;
        pet.Initialize(data);
        listPet.Add(pet);
        if (pet.isRequesting)
        {
            requestingPets.Add(pet);
        }
        return pet;
    }
    public void AddNewPet(string petID)
    {
        var data = new RuntimeData(DataContainer.Instance.GetData(petID));
        DataContainer.Instance.runtimeDatas.Add(data);
        SpawnPet(data, Vector2.zero);

    }
    public void SendRequest()
    {
        //if (requestingPets.Contains())
        if (requestingPets.Count < maxPetRequestPerTime)
        {
            var availablePets = listPet.Except(requestingPets).ToList();
            if (availablePets.Count > 0)
            {
                int randomIndex = Random.Range(0, availablePets.Count);
                var selectedPet = availablePets[randomIndex];
                if (!selectedPet.gameObject.activeSelf)
                    return;
                selectedPet.ShowRandomRequest();
                requestingPets.Add(selectedPet);
            }           
        }        
    }
    public void UnlockNewPet(string petID)
    {
        var data = new RuntimeData(DataContainer.Instance.GetData(petID));
        DataContainer.Instance.runtimeDatas.Add(data);
        var newPet = SpawnPet(data, new Vector2(Camera.main.transform.position.x, bound.bottomBound) );
        //newPet.OnUnlocked();
        var giftBox = Instantiate(giftboxPrefab, new Vector2(Camera.main.transform.position.x, bound.bottomBound), Quaternion.identity);
        giftBox.SetUnlockObj(newPet.gameObject);
    }

}
