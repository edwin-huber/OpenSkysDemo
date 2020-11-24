using Newtonsoft.Json.Linq;
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
        public float? longitude; //  float	WGS-84 longitude in decimal degrees. Can be null.
        public float latitude; //  float	WGS-84 latitude in decimal degrees. Can be null.
        public float? baro_altitude; //  float	Barometric altitude in meters. Can be null.
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
        // maybe need to give some thought to what the null values should represent, 
        // and if we want to propagate these nulls... 
        // currently quick and dirty setting strings to empty, bool to false and numbers to 0
        public static State ConvertJArray(Newtonsoft.Json.Linq.JArray array)
        {
            if(array.Count < 17)
            {
                throw new Exception("unexpected number of elements in Json Array retrieved from service");
            }
            State result = new State();
            result.icao24 = CheckJArrayValue(array[0]) ? array.Value<string>(0) : "";
            result.callsign = CheckJArrayValue(array[1]) ? array.Value<string>(1) : "";
            result.origin_country = CheckJArrayValue(array[2]) ? array.Value<string>(2) : "";
            result.time_position = CheckJArrayValue(array[3]) ? array.Value<int>(3) : 0;
            result.last_contact = CheckJArrayValue(array[4]) ? array.Value<int>(4) : 0;
            result.longitude = CheckJArrayValue(array[5]) ? array.Value<float>(5) : 0;
            result.latitude = CheckJArrayValue(array[6]) ? array.Value<float>(6) : 0;
            result.baro_altitude = CheckJArrayValue(array[7]) ? array.Value<float>(7) : 0;
            result.on_ground = CheckJArrayValue(array[8]) ? array.Value<bool>(8) : false;
            result.velocity = CheckJArrayValue(array[9]) ? array.Value<float>(9) : 0;
            result.true_track = CheckJArrayValue(array[10]) ? array.Value<float>(10) : 0;
            result.vertical_rate = CheckJArrayValue(array[11]) ? array.Value<float>(11) : 0;
            result.sensors = CheckJArrayValue(array[12]) ? array.Value<int[]>(12) : new int [0];
            result.geo_altitude = CheckJArrayValue(array[13]) ? array.Value<float>(13) : 0;
            result.squawk = CheckJArrayValue(array[14]) ? array.Value<string>(14) : "";
            result.spi = CheckJArrayValue(array[15]) ? array.Value<bool>(15) : false;
            result.position_source = CheckJArrayValue(array[16]) ? array.Value<int>(16) : 0;
            return result;
        }

        private static bool CheckJArrayValue(JToken token)
        {
            if(token.Type == JTokenType.Null)
            {
                return false;
            }
            return true;
        }
    }
}