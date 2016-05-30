using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
    [SerializeField]
    private GameObject muzzle;
    [Header("Table")]
    [SerializeField]
    private Transform tableHolder;
    [SerializeField]
    private Vector3 tablePosition;
    [SerializeField]
    private Vector3 tableRotation;
    [Header("Hand")]
    [SerializeField]
    private Transform handHolder;
    [SerializeField]
    private Vector3 handPosition;
    [SerializeField]
    private Vector3 handRotation;
    


    public void Equip()
    {
        transform.parent = handHolder;
        transform.localPosition = handPosition;
        transform.localRotation= Quaternion.Euler(handRotation);
        GetComponentInParent<GunShooting>().EquipGun(muzzle);
    }

    public void PutDown()
    {
        GetComponentInParent<GunShooting>().PutDownGun();
        transform.parent = tableHolder;
        transform.localPosition= tablePosition;
        transform.localRotation = Quaternion.Euler(tableRotation);
    }
}
