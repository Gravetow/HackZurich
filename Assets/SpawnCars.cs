using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnCars : MonoBehaviour
{

    [Inject]
    public CarModel CarModel;

    public CarPath[] paths = {
    new CarPath(new Vector3( 20,0,50), new Vector3( 20,0,-50)),
    new CarPath(new Vector3(-20,0,50), new Vector3(-20,0,-50)),
    new CarPath(new Vector3(20,0,-50), new Vector3(20,0,50)),
    new CarPath(new Vector3(-20,0,-50), new Vector3(-20,0,50)),
    new CarPath(new Vector3(50,0,20), new Vector3(-50,0,20)),
    new CarPath(new Vector3(-50,0,20), new Vector3(50,0,20)),
    new CarPath(new Vector3(50,0,-20), new Vector3(-50,0,-20)),
    new CarPath(new Vector3(-50,0,-20), new Vector3(50,0,-20)),
    };

    private void Start()
    {
        SpawnRandomCar(2);
       
    }

    public int currentPath = 0;

    private void SpawnRandomCar(float delay)
    {
        int randomPathNumber = Random.Range(0, 8);
        currentPath = randomPathNumber;
        CarPath randomPath = paths[randomPathNumber];

        int randomCarNumber = Random.Range(0, 3);
        GameObject car = Instantiate(CarModel.Cars[randomCarNumber]);
        //car.transform.lo
        car.transform.localScale = Vector3.zero;

        switch (randomPathNumber)
        {
            case 0:
            case 1:
                car.transform.eulerAngles = new Vector3(0, 180, 0);
                break;
            case 2:
            case 3:
                break;
            case 4:
                car.transform.eulerAngles = new Vector3(0, 270, 0);
                break;
            case 5:
                car.transform.eulerAngles = new Vector3(0, 90, 0);
                break;
            case 6:
            case 7:
                car.transform.eulerAngles = new Vector3(0, 270, 0);
                break;
        }

        if(randomCarNumber == 2)
        {
            car.transform.eulerAngles = new Vector3(0, car.transform.eulerAngles.y + 180, 0);
        }

        car.transform.position = randomPath.start += Vector3.up * 2;
        float speed = Random.Range(2f, 5f);

        Sequence spawnCarSequence = DOTween.Sequence();
        // Add a movement tween at the beginning
        spawnCarSequence.Append(car.transform.DOMove(randomPath.end + Vector3.up * 2, speed).SetEase(Ease.InQuad));
        spawnCarSequence.Join(car.transform.DOScale(1f, 0.5f).SetEase(Ease.InQuad));
        spawnCarSequence.Join(car.transform.DOScale(0f, 0.5f).SetEase(Ease.OutQuad).SetDelay(speed));

        spawnCarSequence.PrependInterval(delay);
        spawnCarSequence.OnComplete(() => { 
            Destroy(car);
            SpawnRandomCar(Random.Range(1f,5f));
        });
    }


}

public class CarPath
{
    public CarPath(Vector3 start, Vector3 end)
    {
        this.start = start;
        this.end = end;
    }

    public Vector3 start;
    public Vector3 end;
}