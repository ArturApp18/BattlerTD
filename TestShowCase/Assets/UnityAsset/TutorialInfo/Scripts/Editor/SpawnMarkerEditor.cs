using CodeBase.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
	[CustomEditor(typeof(SpawnMarker))]
	public class SpawnMarkerEditor : UnityEditor.Editor
	{
		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
		public static void RenderCustomGizmo(SpawnMarker spawner, GizmoType gizmo)
		{
			Gizmos.color = Color.green;
			//	Gizmos.DrawSphere(spawner.transform.position, 0.5f);
			Quaternion rotation = spawner.transform.rotation * Quaternion.Euler(270f, 0f, 0f);
			Gizmos.DrawMesh(spawner.mobModelPrefab.GetComponent<SkinnedMeshRenderer>().sharedMesh, spawner.transform.position + spawner.modelOffset, rotation,
				spawner.mobModelPrefab.transform.lossyScale * 5);
		}
	}

}