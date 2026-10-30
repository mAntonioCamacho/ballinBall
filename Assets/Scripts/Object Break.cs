using UnityEngine;

public class ObjectBreak : MonoBehaviour
{
    [SerializeField] private GameObject debrisPrefab;
    private BallFormManager formMan;

    void OnCollisionEnter2D(Collision2D collision)
    {
        formMan = collision.gameObject.GetComponent<BallFormManager>();
        if(formMan.currentScriptObjBallForm.canBreakObjects)
        {
            GameObject debris = Instantiate(debrisPrefab, transform.position, Quaternion.identity);
            
            Destroy(debris, 1f);
            Destroy(this.gameObject);
        }
    }
}
