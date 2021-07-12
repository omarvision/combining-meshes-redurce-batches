using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    #region --- helper ---
    private enum enumMouseButton
    {
        left = 0,
        right = 1,
        middle = 2,
    }
    #endregion

    public GameObject prefabShape = null;
    public Camera cam = null;

    private void Update()
    {
        //mouse left button = add block
        if (Input.GetMouseButtonDown((int)enumMouseButton.left) == true)
        {
            // 1. ray(line) from camera into scene
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f) == true)
            {
                // 2. ray hits mesh side (use normal, get position on side where hit)
                Vector3 newPos = hit.point + (hit.normal * 0.5f);

                // 3. round position to middle of 1 unit space
                newPos.x = (float)System.Math.Round(newPos.x, System.MidpointRounding.AwayFromZero);
                newPos.y = (float)System.Math.Round(newPos.y, System.MidpointRounding.AwayFromZero);
                newPos.z = (float)System.Math.Round(newPos.z, System.MidpointRounding.AwayFromZero);

                // 4. instantiate a new shape, make child
                GameObject shape = (GameObject)Instantiate(prefabShape, newPos, Quaternion.identity);
                shape.transform.parent = this.transform; 

                // 5. combine mesh and children into one (to keep Batches count low)
                Combine(shape);
            }
        }
    }

    private void Combine(GameObject shape)
    {
        // 1. destroy existing meshcollider
        Destroy(this.gameObject.GetComponent<MeshCollider>());

        // 2. mesh and child meshes into array
        MeshFilter[] meshfilters = this.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshfilters.Length];
        int i = 0;
        while (i < meshfilters.Length)
        {
            combine[i].mesh = meshfilters[i].sharedMesh;
            combine[i].transform = meshfilters[i].transform.localToWorldMatrix;
            meshfilters[i].gameObject.SetActive(false);
            i++;
        }

        // 3. combine meshes in array, into one mesh
        MeshFilter meshfilter = this.GetComponent<MeshFilter>();
        meshfilter.mesh = new Mesh();
        meshfilter.mesh.CombineMeshes(combine, true);
        meshfilter.mesh.RecalculateBounds();
        meshfilter.mesh.RecalculateNormals();
        meshfilter.mesh.Optimize();

        // 4. remake the meshcollider
        this.gameObject.AddComponent<MeshCollider>();
        this.transform.gameObject.SetActive(true);

        // 5. cleanup
        Destroy(shape);
    }

}
