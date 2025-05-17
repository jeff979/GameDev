using UnityEngine;

public class SwordHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            //Destroy(other.gameObject); 

            EnemyDeathEffect deathEffect = other.GetComponent<EnemyDeathEffect>();

            if (deathEffect != null)
            {
                if(!deathEffect.hasScored)
                {
                    deathEffect.hasScored = true;
                    Score.Instance.AddKill();
                }

                deathEffect.TriggerDeathEffect();
                //FindObjectOfType<EnemySpawner>().EnemyDied();

                other.GetComponent<Collider>().enabled = false;

                FollowTest follow = other.GetComponent<FollowTest>();
                if (follow != null) follow.enabled = false;

                CharacterController controller = other.GetComponent<CharacterController>();
                if (controller != null) controller.enabled = false;
            }
        }
    }
}