using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFactory : MonoBehaviour
{
    [SerializeField] private GameObject homePrefab, playerPrefab, enemyPrefab, grasPrefab;


    public void Start()
    {


    }

    public void CreateHome(Vector3 position, string name, float pieceScale, float cellScale)
    {
        this.Create(position, name, pieceScale, cellScale, homePrefab);
    }

    public void CreatePlayer(Vector3 position, string name, float pieceScale, float cellScale)
    {
        this.Create(position, name, pieceScale, cellScale, playerPrefab);
    }

    public void CreateEnemy(Vector3 position, string name, float pieceScale, float cellScale)
    {
        this.Create(position, name, pieceScale, cellScale, enemyPrefab);
    }

    public void CreateGras(Vector3 position, string name, float pieceScale, float cellScale)
    {
        this.Create(position, name, pieceScale, cellScale, grasPrefab);
    }

    private void Create(Vector3 position, string name, float pieceScale, float cellScale, GameObject prefab)
    {
        GameObject home = Instantiate(prefab, new Vector3(position.x, position.y + cellScale / 2, position.z), Quaternion.identity);
        home.transform.localScale = new Vector3(pieceScale, pieceScale, pieceScale);
        home.transform.parent = transform;
        home.name = name;
    }
}
