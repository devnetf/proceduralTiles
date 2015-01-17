using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class GenerateMap : MonoBehaviour {

	public GameObject TilePrefab;
	public int maxNumberTile;
	public int tilesToRemove;

	private Graph map;
	private List<Node> unvisited_vertices;
	private System.Random rand = new System.Random();
	// Use this for initialization
	void Start () {

		Node firstNode = new Node (new Vector2 (0, 0));
		map = new Graph (firstNode, null);
		unvisited_vertices = new List<Node>();
		unvisited_vertices.Add(firstNode);

		while(map.vertices.Count < maxNumberTile)
		{
			Node curr = unvisited_vertices[0];

			Node up = new Node(new Vector2(curr.point.x + 1, curr.point.y));
			Node down = new Node(new Vector2(curr.point.x-1, curr.point.y));
			Node leftup = new Node(new Vector2(curr.point.x + 0.5f, curr.point.y + 0.75f));
			Node rightup = new Node(new Vector2(curr.point.x + 0.5f, curr.point.y - 0.75f));
			Node leftdown = new Node(new Vector2(curr.point.x - 0.5f, curr.point.y + 0.75f));
			Node rightdown = new Node(new Vector2(curr.point.x - 0.5f, curr.point.y - 0.75f));

			unvisited_vertices.RemoveAt(0);

			insertNode(curr, up);
			insertNode(curr, down);
			insertNode(curr, leftup);
			insertNode(curr, rightup);
			insertNode(curr, leftdown);
			insertNode(curr, rightdown);
		}

		int nodeRemoved = 0;
		int index = 0, count = 0;

		while(nodeRemoved < tilesToRemove)
		{
			if(count > 1000)
			{
				//avoid infinite loop
				break;
			}

			index = rand.Next(map.vertices.Count);

			Node curr = map.vertices[index];
			bool remove = true;

			foreach(Node neighboor in curr.neighbours)
			{
				if(neighboor.neighbours.Count <= 1)
				{
					remove = false;
					break;
				}
			}

			if(remove)
			{
				foreach(Node neighboor in curr.neighbours)
				{
					neighboor.neighbours.Remove(curr);
				}

				map.vertices.Remove(curr);
				nodeRemoved++;
			}

			count++;
		}

		foreach(Node n in map.vertices)
		{
			Instantiate(TilePrefab, new Vector3(n.point.x, 0, n.point.y), TilePrefab.transform.rotation);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void insertNode(Node curr, Node n)
	{
		if(map.addNodeUnique(n))
		{
			unvisited_vertices.Add(n);
			curr.addNeighbour(n);
		}
		else
		{
			Node tmpNode = map.GetNode(n.point.x, n.point.y);
			curr.addNeighbour(tmpNode);
		}
	}

}
