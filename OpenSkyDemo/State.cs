using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSkyDemo
{
    // [JsonArrayAttribute]
    public class State
    {
        public string icao24; // Unique ICAO 24-bit address of the transponder in hex string representation
        public string callsign; // Callsign of the vehicle (8 chars). Can be null if no callsign has been received.
        public string origin_country; // Country name inferred from the ICAO 24-bit address.
        public int time_position; //  int	Unix timestamp (seconds) for the last position update. Can be null if no position report was received by OpenSky within the past 15s.
        public int last_contact; //  int	Unix timestamp (seconds) for the last update in general. This field is updated for any new, valid message received from the transponder.
        public float longitude; //  float	WGS-84 longitude in decimal degrees. Can be null.
        public float latitude; //  float	WGS-84 latitude in decimal degrees. Can be null.
        public float baro_altitude; //  float	Barometric altitude in meters. Can be null.
        public bool on_ground; //  boolean	Boolean value which indicates if the position was retrieved from a surface position report.
        public float velocity; //  float	Velocity over ground in m/s. Can be null.
        public float true_track; //  float	True track in decimal degrees clockwise from north (north=0°). Can be null.
        public float vertical_rate; //  float	Vertical rate in m/s. A positive value indicates that the airplane is climbing, a negative value indicates that it descends. Can be null.
        public int[] sensors; //  int[]	IDs of the receivers which contributed to this state vector. Is null if no filtering for sensor was used in the request.
        public float geo_altitude; // float	Geometric altitude in meters. Can be null.
        public string squawk; // string	The transponder code aka Squawk. Can be null.
        public bool spi; //  boolean	Whether flight status indicates special purpose indicator.
        public int position_source; //  int	Origin of this state’s position: 0 = ADS-B, 1 = ASTERIX, 2 = MLAT*/


        // not sure I like this, as we are very schema sensitive, but there is no other way to get this.
        // ToDo: need to check the api definition for potential handling requirement of null values etc
        public static State ConvertJArray(Newtonsoft.Json.Linq.JArray array)
        {
            State result = new State();
            result.icao24 = array.Value<string>(0);
            result.callsign = array.Value<string>(1);
            result.origin_country = array.Value<string>(2);
            result.time_position = array.Value<int>(3);
            result.last_contact = array.Value<int>(4);
            result.longitude = array.Value<float>(5);
            result.latitude = array.Value<float>(6);
            result.baro_altitude = array.Value<float>(7);
            result.on_ground = array.Value<bool>(8);
            result.velocity = array.Value<float>(9);
            result.true_track = array.Value<float>(10);
            result.vertical_rate = array.Value<float>(11);
            result.sensors = array.Value<int[]>(12);
            result.geo_altitude = array.Value<float>(13);
            result.squawk = array.Value<string>(14);
            result.spi = array.Value<bool>(15);
            result.position_source = array.Value<int>(16);
            return result;
        }
    }
}