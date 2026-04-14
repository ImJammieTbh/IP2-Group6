using UnityEngine;

public class BirdEncyclopediaAndPhotoAlbum : MonoBehaviour
{
    // determines which book you're reading
    public bool isBirdBook = false; // bird encyclopdia by default
    public bool isPhotoAlbum = false;

    public GameObject birdBook;
    public GameObject photoAlbum;

    public GameObject page; // would make as a list

    public HideUi hideUI;

    void Update()
    {
        if (hideUI.isBirdBookOpen == true) //esnures can't switch unless the books are open
        {
            // switch to photo album
            if (Input.GetKeyDown(KeyCode.C) && isPhotoAlbum == false && isBirdBook == true)
            {
                birdBook.SetActive(false);
                photoAlbum.SetActive(true);

                isBirdBook = false;
                isPhotoAlbum = true;
            }

            // switch to bird book
            else if (Input.GetKeyDown(KeyCode.C) && isPhotoAlbum == true && isBirdBook == false)
            {
                birdBook.SetActive(true);
                photoAlbum.SetActive(false);

                isBirdBook = true;
                isPhotoAlbum = false;
            }
            else if (Input.GetKeyDown(KeyCode.C)) // in case there is a glitch and both bools are both false or true, set as bird book
            {
                birdBook.SetActive(true);
                photoAlbum.SetActive(false);

                isBirdBook = true;
                isPhotoAlbum = false;
            }
        }
    }
}
