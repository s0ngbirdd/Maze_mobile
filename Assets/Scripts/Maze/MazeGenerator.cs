using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class MazeGenerator : MonoBehaviour
{
    // Serialize
    [SerializeField] private MazeNode _nodePrefab;
    [SerializeField] private Vector2Int _mazeSize;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _finishPrefab;
    [SerializeField] private NavMeshSurface _navMeshSurface;

    // Private
    private List<MazeNode> nodes = new List<MazeNode>();

    private void Start()
    {
        GenerateMazeInstant(_mazeSize);

        // for debug
        //StartCoroutine(GenerateMaze(_mazeSize));

        _navMeshSurface.BuildNavMesh();
    }

    private void GenerateMazeInstant(Vector2Int size)
    {
        // create nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePosition = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f)); // center around 0, 0 instead of bottom left corner
                MazeNode newNode = Instantiate(_nodePrefab, nodePosition, Quaternion.identity, transform);
                nodes.Add(newNode);
            }
        }

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        // choose starting node
        //currentPath.Add(nodes[Random.Range(0, nodes.Count)]); // starting node random position
        currentPath.Add(nodes[0]);

        nodes[0].RemoveDeadZone();
        nodes[nodes.Count - 1].RemoveDeadZone();
        Instantiate(_playerPrefab, new Vector3(nodes[0].transform.position.x, _playerPrefab.transform.position.y, nodes[0].transform.position.z), Quaternion.identity);
        Instantiate(_finishPrefab, new Vector3(nodes[nodes.Count - 1].transform.position.x, _finishPrefab.transform.position.y, nodes[nodes.Count - 1].transform.position.z), Quaternion.identity);

        while (completedNodes.Count < nodes.Count)
        {
            // check nodes next to the current node
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if (currentNodeX < size.x - 1)
            {
                // check node to the right of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) && !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }

            if (currentNodeX > 0)
            {
                // check node to the left of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) && !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }

            if (currentNodeY < size.y - 1)
            {
                // check node above the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) && !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }

            if (currentNodeY > 0)
            {
                // check node below the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) && !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            // choose next node
            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }

                currentPath.Add(chosenNode);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);

                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }
    }

    public void InstantiatePlayer()
    {
        Instantiate(_playerPrefab, new Vector3(nodes[0].transform.position.x, _playerPrefab.transform.position.y, nodes[0].transform.position.z), Quaternion.identity);
    }

    // for debug
    private IEnumerator GenerateMaze(Vector2Int size)
    {
        // create nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePosition = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f)); // center around 0, 0 instead of bottom left corner
                MazeNode newNode = Instantiate(_nodePrefab, nodePosition, Quaternion.identity, transform);
                nodes.Add(newNode);

                yield return null;
            }
        }

        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        // choose starting node
        //currentPath.Add(nodes[Random.Range(0, nodes.Count)]); // starting node random position
        currentPath.Add(nodes[0]);
        currentPath[0].SetState(NodeState.CURRENT);

        Instantiate(_playerPrefab, nodes[0].transform.position, Quaternion.identity);
        Instantiate(_finishPrefab, nodes[nodes.Count - 1].transform.position, Quaternion.identity);

        while (completedNodes.Count < nodes.Count)
        {
            // check nodes next to the current node
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if (currentNodeX < size.x - 1)
            {
                // check node to the right of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) && !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }

            if (currentNodeX > 0)
            {
                // check node to the left of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) && !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }

            if (currentNodeY < size.y - 1)
            {
                // check node above the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) && !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }

            if (currentNodeY > 0)
            {
                // check node below the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) && !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            // choose next node
            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }

                currentPath.Add(chosenNode);
                chosenNode.SetState(NodeState.CURRENT);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);

                currentPath[currentPath.Count - 1].SetState(NodeState.COMPLETED);
                currentPath.RemoveAt(currentPath.Count - 1);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}
