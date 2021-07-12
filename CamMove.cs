
using UnityEngine;

public class CamMove : MonoBehaviour
{
    #region --- helper ---
    private enum enumMouseButton
    {
        left = 0,
        right = 1,
        middle = 2,
    }
    #endregion

    public float scrollspeed = 8.0f;
    public float turnspeed = 120.0f;
    public Transform rotatecenter = null;

    private void Update()
    {
        //mouse wheel = zoom camera
        if (Input.mouseScrollDelta.y != 0f)
        {
            this.transform.Translate(Vector3.forward * Input.mouseScrollDelta.y * scrollspeed * Time.deltaTime);
        }

        //middle mouse button & mouse move = rotate around
        if (Input.GetMouseButton((int)enumMouseButton.middle) == true)
        {
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");

            this.transform.RotateAround(rotatecenter.position, Vector3.down, turnspeed * mx * Time.deltaTime);
            this.transform.RotateAround(rotatecenter.position, Vector3.right, turnspeed * my * Time.deltaTime);

            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, 0.0f);
        }
    }
}
