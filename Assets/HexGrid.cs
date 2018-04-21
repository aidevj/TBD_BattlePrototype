using UnityEngine;
using UnityEngine.Collections;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

	public int width = 6;
	public int height = 6;

	public HexCell cellPrefab;
	public Text cellLabelPrefab;

	Canvas gridCanvas;
	HexMesh hexMesh;

	HexCell[] cells;

	void Awake () {
		cells = new HexCell[height * width];

		gridCanvas = GetComponentInChildren<Canvas> ();
		hexMesh = GetComponentInChildren<HexMesh> ();

		for (int z = 0, i = 0; z < height; z++) {
			for (int x = 0; x < width; x++) {
				CreateCell(x, z, i++);
			}
		}
	}

	void Start(){
		// after the grid has awoken
		hexMesh.Triangulate (cells);
	}

	/// <summary>
	/// Creates cells at the given positions.
	/// </summary>
	/// <param name="x">The x position.</param>
	/// <param name="z">The z position.</param>
	/// <param name="i">Index of the cell out of all.</param>
	void CreateCell (int x, int z, int i) {
		Vector3 position;
		// space them out according to radius and offset
		position.x = ( x + z * 0.5f  - z / 2 ) * (HexMetrics.innerRadius * 2f); //position.x = x * 10f; // <-- non shifted
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f); //position.z = z * 10f;

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;

		// instantiate the labels and show cell coordinates
		Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition =
			new Vector2(position.x, position.z);
		label.text = x.ToString() + "\n" + z.ToString();
	}

}

// reference: http://catlikecoding.com/unity/tutorials/hex-map/part-1/