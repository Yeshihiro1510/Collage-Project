using UnityEngine;

public class Attraction : MonoBehaviour
{
    [SerializeField] private float _radius;

    private Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private void Update()
    {
        var toAttract = Physics2D.OverlapCircleAll(MousePosition, _radius);
        foreach (var a in toAttract)
        {
            if (a.TryGetComponent(out DragAndDrop c))
            {
                c.Rigidbody.AddForce(MousePosition - c.Position);
            }
        }
    }
}