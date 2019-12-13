using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.Mathematics;
using Nezix;

/// Simple example showing how to use the Marching Cubes implementation

public class MCBexample : MonoBehaviour {

	MarchingCubesBurst mcb;

	void Start() {
		//Create fake density map
		int3 gridSize;
		gridSize.x = 20;
		gridSize.y = 25;
		gridSize.z = 17;

		int totalSize = gridSize.x * gridSize.y * gridSize.z;
		float dx = 0.05f;//Size of one voxel
		float[] densVal = new float[totalSize];
		Vector3 oriXInv = Vector3.zero; //Why this name ? Because you might need to invert the X axis

		int id = 0;
		for (int i = 0; i < gridSize.x; i++) {
			float x = -2.0f + i * 3 * dx;
			for (int j = 0; j < gridSize.y; j++) {
				float y =   -2.0f + j * 3 * dx;
				for (int k = 0; k < gridSize.z; k++) {
					float z = -2.0f + k * 3 * dx;
					densVal[id++] = (x * x * x * x - 5.0f * x * x + y * y * y * y - 5.0f * y * y + z * z * z * z - 5.0f * z * z + 11.8f) * 0.2f + 0.5f;
				}
			}
		}


		//Instantiate the MCB class

		mcb = new MarchingCubesBurst(densVal, gridSize, oriXInv, dx);

		//Compute an iso surface, this can be called several time without modifying mcb
		float isoValue = 1.5f;
		mcb.computeIsoSurface(isoValue);

		Vector3[] newVerts = mcb.getVertices();
		if (newVerts.Length == 0) {
			Debug.Log("Empty mesh");
			mcb.Clean();
		}

		//Invert x of each vertex
		for (int i = 0; i < newVerts.Length; i++) {
			newVerts[i].x *= -1;
		}
		int[] newTri = mcb.getTriangles();
		Color32[] newCols = new Color32[newVerts.Length];
		Color32 w = Color.white;
		for (int i = 0; i < newCols.Length; i++) {
			newCols[i] = w;
		}

		GameObject newMeshGo = new GameObject("testDX");

		Mesh newMesh = new Mesh();
		newMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
		newMesh.vertices = newVerts;
		newMesh.triangles = newTri;
		newMesh.colors32 = newCols;



		newMesh.RecalculateNormals();

		MeshFilter mf = newMeshGo.AddComponent<MeshFilter>();
		mf.mesh = newMesh;

		MeshRenderer mr = newMeshGo.AddComponent<MeshRenderer>();

		Material mat = new Material(Shader.Find("Standard"));
		mat.SetFloat("_Glossiness", 0.0f);
		mat.SetFloat("_Metallic", 0.0f);

		mr.material = mat;

		//When done => free data
		mcb.Clean();
	}

}