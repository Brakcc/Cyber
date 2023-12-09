using UnityEngine;

namespace Utilities.SoundManagement
{
    public class IfHitPlay : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D col;
        [SerializeField] private AudioClip soundeff;
        private AudioSource audiosource;
        private bool canPlay;
        private float playCursor;

        private void Start()
        {
            audiosource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        }

        private void Update()
        {
            /*if (PlayerHealth.instance.isDed && !canPlay)
        {
            canPlay = true;
        }*/
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && canPlay && Time.time > playCursor + 1)
            {
                playCursor = Time.time;
                AudioManager.ad.PlayClipAt(soundeff, transform.position);
                canPlay = false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y) + col.offset, col.size);
        }
    }
}
