using UnityEngine;

public class HideUi : MonoBehaviour
{
    //hidden state
    public GameObject closedWantedList;
    public GameObject closedPhotoAlbum;
    public GameObject closedBirdBook; //Bird encyclopedia and photo album

    //open
    public GameObject openWantedList;
    public GameObject openPhotoAlbum;
    public GameObject openBirdBook;

    //states
    public bool isWantedOpen = false;
    public bool isBirdBookOpen = false;

    public BirdEncyclopediaAndPhotoAlbum birdAndPhotos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedWantedList.SetActive(true);
        closedPhotoAlbum.SetActive(true);
        closedBirdBook.SetActive(true);

        openWantedList.SetActive(false);
        openPhotoAlbum.SetActive(false);
        openBirdBook.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // WANTED LIST
        if (Input.GetKeyDown(KeyCode.R) && isWantedOpen == false)
        {
            //if other UI open
            openPhotoAlbum.SetActive(false);
            openBirdBook.SetActive(false);
            closedPhotoAlbum.SetActive(true);
            closedBirdBook.SetActive(true);
            isBirdBookOpen = false;
            birdAndPhotos.isBirdBook = false;
            birdAndPhotos.isPhotoAlbum = false;

            openWantedList.SetActive(true);
            closedWantedList.SetActive(false);
            isWantedOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.R) && isWantedOpen == true)
        {
            openWantedList.SetActive(false);
            closedWantedList.SetActive(true);
            isWantedOpen = false;
        }

        // BIRD ENCYCLOPEDIA AND PHOTO ALBUM 
        if (Input.GetKeyDown(KeyCode.X) && isBirdBookOpen == false)
        {
            //if other UI open
            openWantedList.SetActive(false);
            closedWantedList.SetActive(true);
            isWantedOpen = false;

            //openPhotoAlbum.SetActive(true);
            openBirdBook.SetActive(true);
            closedPhotoAlbum.SetActive(false);
            closedBirdBook.SetActive(false);
            isBirdBookOpen = true;
            birdAndPhotos.isBirdBook = true;
        }
        else if (Input.GetKeyDown(KeyCode.X) && isBirdBookOpen == true)
        {
            openPhotoAlbum.SetActive(false);
            openBirdBook.SetActive(false);
            closedPhotoAlbum.SetActive(true);
            closedBirdBook.SetActive(true);

            isBirdBookOpen = false;
            birdAndPhotos.isBirdBook = false;
            birdAndPhotos.isPhotoAlbum = false;
        }
    }
}
