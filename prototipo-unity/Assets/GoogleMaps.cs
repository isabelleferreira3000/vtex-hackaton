using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GoogleMaps : MonoBehaviour
{

    // public Renderer renderer;
    string key = ""; //put your own API key here.
    
    public RawImage image;
    string url;

    public float lat = -23.202569f;
    public float lon = -45.884370f;

    LocationInfo li;

    public int zoom = 14;
    public int mapWidth = 640;
    public int mapHeight = 640;

    public enum mapType { roadmap, satellite, hybrid, terrain }
    public mapType mapSelected;
    public int scale;

    IEnumerator GetCoordinates()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            print("Serviços de localização não ativos");
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            print("Trying to connect");
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();

        lat = Input.location.lastData.latitude;
        lon = Input.location.lastData.longitude;
    }

    // string url = "http://maps.googleapis.com/maps/api/staticmap?center=Brooklyn+Bridge," +
    //                     "New+York,NY&zoom=13&size=600x300&maptype=roadmap" +
    //                     "&markers=color:blue%7Clabel:S%7C40.702147,-74.015794" +
    //                     "&markers=color:green%7Clabel:G%7C40.711614,-74.012318" +
    //                     "&markers=color:red%7Ccolor:red%7Clabel:C%7C40.718217,-73.998284" +
    //                     "&sensor=false";
    // Get map

    IEnumerator Start()
    {
        if (key == "") {
            Debug.LogWarning("There's no API key inserted");
            return;
        }
        
        List<string> cores = new List<string>();
        cores.Add("blue");
        cores.Add("red");
        cores.Add("green");

        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat.ToString().Replace(',', '.') + "," + lon.ToString().Replace(',', '.') +
             "&zoom=" + zoom.ToString() + "&size=" + mapWidth.ToString() + "x" + mapHeight.ToString()// + "&scale=" + scale
             + "&maptype=" + mapSelected;
        foreach (Seller s in Database.sellers)
        {
            url += "&markers=color:" + cores[Random.Range(0, cores.Count)] + "%7Clabel:"+s.name.ToCharArray()[0].ToString().ToUpper()+"%7C" +
            s.xCord.ToString().Replace(',', '.') +
            ',' +
            s.yCord.ToString().Replace(',', '.'); print(s.name);
        }

        url += "&sensor=false" +
            "&key=" + key;
        print(url);

        WWW www = new WWW(url);

        yield return www;

        image.texture = www.texture;
        image.SetNativeSize();
    }
}
/* http://maps.googleapis.com/maps/api/staticmap?center=-23.202569,-45.884370&zoom=20&size=600x300&maptype=roadmap&markers=color:blue%7Clabel:S%7C40.702147,-74.015794&markers=color:green%7Clabel:G%7C40.711614,-74.012318&markers=color:red%7Ccolor:red%7Clabel:C%7C40.718217,-73.998284&sensor=false&key=AIzaSyAPYUtU6TgGebSbWKmndo-bSbkDaQPMB0w */

// https://maps.googleapis.com/maps/api/staticmap?center=-23.20257,-45.88437&zoom=17&size=600x480&maptype=roadmap&markers=color:blue%7Clabel:S%7C-23.202569,-45.88437&markers=color:blue%7Clabel:S%7C-20.202569,-46.88437&markers=col/or:green%7Clabel:S%7C-24.202569,-46.88437&markers=color:blue%7Clabel:S%7C-23,-50&markers=color:blue%7Clabel:S%7C-27,-50&sensor=false&key=AIzaSyAPYUtU6TgGebSbWKmndo-bSbkDaQPMB0w

//&markers=color:blue%7Clabel:S%7C40.702147,-74.015794&markers=color:green%7Clabel:G%7C40.711614,-74.012318&markers=color:red%7Ccolor:red%7Clabel:C%7C40.718217,-73.998284&
