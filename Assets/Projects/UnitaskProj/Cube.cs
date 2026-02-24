using UnityEngine;

namespace Projects.UnitaskProj
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] protected MeshRenderer _mesh;
        
        public float Progress
        {
            get => _mesh.material.GetFloat("_FireProgress");
            set => _mesh.material.SetFloat("_FireProgress", value);
        }
    }
}