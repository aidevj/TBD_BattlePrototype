using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour {

	Mesh hexMesh;
	List<Vector3> vertices;
	List<int> triangles;

	void Awake () {
		GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
		hexMesh.name = "Hex Mesh";
		vertices = new List<Vector3>();
		triangles = new List<int>();
	}

	/// <summary>
	/// Triangulate the specified cells. This can be invoked at any time, even when cells have already been triangulated earlier. 
	/// This will clear the old data then loop through all the cells and triangulate them individually.
	/// </summary>
	/// <param name="cells">Cells.</param>
	public void Triangulate (HexCell[] cells) {
		hexMesh.Clear();
		vertices.Clear();
		triangles.Clear();
		for (int i = 0; i < cells.Length; i++) {
			Triangulate(cells[i]);
		}
		hexMesh.vertices = vertices.ToArray();
		hexMesh.triangles = triangles.ToArray();
		hexMesh.RecalculateNormals();
	}

	/// <summary>
	/// Triangulate a single cell.
	/// </summary>
	void Triangulate (HexCell cell) {
		Vector3 center = cell.transform.localPosition;
		// loop through all six triangles
		for (int i = 0; i < 6; i++) {
			AddTriangle (
				center,
				center + HexMetrics.corners [i],
				center + HexMetrics.corners [i + 1]
			);
		}
	}

	/// <summary>
	/// Adds triangles conviniently, given three vertex positions, adds them order.
	/// The index of the first vertex is equal to the length of the vertex list before adding the new vertices to it.
	/// </summary>
	/// <param name="v1">Vertex location 1.</param>
	/// <param name="v2">Vertex location 2.</param>
	/// <param name="v3">Vertex location 3.</param>
	void AddTriangle (Vector3 v1, Vector3 v2, Vector3 v3) {
		int vertexIndex = vertices.Count;
		vertices.Add(v1);
		vertices.Add(v2);
		vertices.Add(v3);
		triangles.Add(vertexIndex);
		triangles.Add(vertexIndex + 1);
		triangles.Add(vertexIndex + 2);
	}
}
