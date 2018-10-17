using RestSharp;
using SongsAPIConsultant.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongsAPIConsultant
{
    class Program
    {
        static void Main(string[] args)
        {

            var client = new RestClient("http://ws.audioscrobbler.com");
            string createText = "artist_id|song_name|mbid|duration|listeners|playcount" + Environment.NewLine;
            string path = @"D:/songs.txt";
            string csv = @"D:/songs.csv";

            using (var reader = new StreamReader(csv))
            {

                int count = 0;
            
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split('|');

                    string trackId = values[0];
                    string artist = values[1];
                    string song = values[2];

                    var request = new RestRequest("2.0/", Method.GET);
                    request.AddParameter("method", "track.getInfo"); // adds to POST or URL querystring based on Method
                    request.AddParameter("track", song);
                    request.AddParameter("artist", artist);
                    request.AddParameter("api_key", "afaaf45a9e6f1fbb6bfac4139e4fd8ae");
                    request.AddParameter("format", "json");

                    //or automatically deserialize result
                    // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
                    IRestResponse<Song> response2 = client.Execute<Song>(request);
                    Track track = response2.Data.Track;

                    if (track == null)
                    {
                        track = new Track();
                        track.Name = song;
                        track.Duration = "";
                        track.Playcount = "";
                        track.Mbid = "";
                        track.Listeners = "";
                    }

                    track.TrackId = trackId;
                    track.Artist = artist;

                    // Create a file to write to.
                    string attributes = track.GetAttributes();
                    createText += attributes + Environment.NewLine;

                    Console.WriteLine(++count + " - " + attributes);
                }
            }

            File.WriteAllText(path, createText);

            Console.ReadLine();

        }
    }
}
