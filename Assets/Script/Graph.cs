using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Node Data Structure for building Graphs
public class Node
{
	public Vector2 point;
	public List<Node> neighbours;
	
	public Node()
	{
		this.point = new Vector2();
		neighbours = new List<Node>();
	}
	
	public Node(Vector2 pt)
	{
		this.point = pt;
		neighbours = new List<Node>();
	}
	
	public void addNeighbour(Node node)
	{
		if(this.neighbours.Where(n => n.point.x == node.point.x && n.point.y == node.point.y).Count() == 0)
		{
			this.neighbours.Add(node);
		}
	}
}

//The Graph Class used to store the maze as a grid
public class Graph
{
	
	public List<Node> vertices;
	public Node startNode; 
	public Node Goal;
	
	public Graph()
	{
		this.vertices = new List<Node>();
	}
	
	public Graph(Node firstNode, Node endNode)
	{
		this.vertices = new List<Node>();
		this.startNode = firstNode;
		this.Goal = endNode;
		this.vertices.Add(firstNode);
		
	}

	public bool addNodeUnique(Node n)
	{
		if (!this.Contains(n.point.x, n.point.y)) 
		{
			this.vertices.Add(n);
			return true;
		}

		return false;
	}
	
	public bool Contains(float x, float y)
	{
		return this.vertices.Where(node => (node.point.x == x && node.point.y == y)).Count() > 0;
	}
	
	public Node GetNode(float x, float y)
	{
		return this.vertices.Where(node => (node.point.x == x && node.point.y == y)).FirstOrDefault();
	}
	
	public bool isGoal(Node n)
	{
		return n.point.x == this.Goal.point.x && n.point.y == this.Goal.point.y;
	}
	
	public bool isStart(Node n)
	{
		return n.point.x == this.startNode.point.x && n.point.y == this.startNode.point.y;
	}
}
