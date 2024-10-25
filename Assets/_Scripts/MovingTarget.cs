using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public GameObject Spawner;
    public float StartSize = 10;
    public float Speed = 2;
    public float TimeChasingPosMin = 1;
    public float TimeChasingPosMax = 3;
    private float _timePassed = 0;
    private float _timeChasingPos;
    private Vector3 _newPos;
    private Vector3 _pos1;
    private Vector3 _pos2;


    private void Start()
    {
        SetGameMode();
        Vector3 sizeVector = new Vector3(StartSize, StartSize, StartSize);
        transform.localScale = sizeVector;
        float machin = transform.localScale.x * (1.15f / 8);
        float x = Spawner.transform.localScale.x / 2 - machin;
        float y = Spawner.transform.localScale.y / 2 - machin;
        float z = Spawner.transform.localScale.z / 2;
        transform.position = Spawner.transform.position + new Vector3(Random.Range(-x, x), Random.Range(-y, y), Random.Range(-z, z));
        _timeChasingPos = Random.Range(TimeChasingPosMin, TimeChasingPosMax);
        _pos1 = new Vector3(-Spawner.transform.localScale.x / 2, transform.position.y, transform.position.z);
        _pos2 = new Vector3(Spawner.transform.localScale.x / 2, transform.position.y, transform.position.z);
        if (Random.Range(0, 1) == 1)
            _newPos = _pos1;
        else _newPos = _pos2;
    }

    void Update()
    {
        MoveUpdate();
        _timePassed += Time.deltaTime;
        if (_timePassed > _timeChasingPos)
        {
            ChangeNewPos();
            _timeChasingPos = Random.Range(TimeChasingPosMin, TimeChasingPosMax);
            _timePassed = 0f;
        }
    }
    private void MoveUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _newPos, Speed * Time.deltaTime);
    }

    private void ChangeNewPos()
    {
        if (_newPos == _pos1)
            _newPos = _pos2;
        else
            _newPos = _pos1;
    }

    private void SetGameMode()
    {

        switch (PlayerPrefs.GetString("mode"))
        {
            case "easy":
                SetEasyMode();
                break;
            case "medium":
                SetMediumMode();
                break;
            case "hard":
                SetHardMode();
                break;
            case "training":
                SetTrainingMode();
                break;
            default:
                PlayerPrefs.SetString("mode", "training");
                SetTrainingMode();
                break;
        }
    }

    private void SetTrainingMode()
    {
        StartSize = 10;
        Speed = 0;
    }

    private void SetEasyMode()
    {
        StartSize = 10;
        Speed = 2;
        TimeChasingPosMin = 1;
        TimeChasingPosMax = 3;
    }

    private void SetMediumMode()
    {
        StartSize = 8;
        Speed = 3;
        TimeChasingPosMin = 0.5f;
        TimeChasingPosMax = 2;
    }

    private void SetHardMode()
    {
        StartSize = 6;
        Speed = 4;
        TimeChasingPosMin = 0.3f;
        TimeChasingPosMax = 1.5f;
    }
}
