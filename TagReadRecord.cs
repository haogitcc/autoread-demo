using System;
using ThingMagic;

namespace ReceiveAutonomousReadingDemodotNet
{
    public class TagReadRecord
    {
        protected TagReadData RawRead = null;
        UInt32 serialNo = 0;

        public TagReadRecord(TagReadData newData)
        {
            lock (new Object())
            {
                RawRead = newData;
            }
        }

        public UInt32 SerialNumber { 
            get { return serialNo; } 
            set { serialNo = value; }
        }

        public string EPC
        {
            get { return RawRead.EpcString; }
        }

        public string TID
        {
            get { return null; }
        }

        public string RESERVED
        {
            get { return null; }
        }

        public string USER
        {
            get { return null; }
        }

        public int Antenna
        {
            get { return RawRead.Antenna; }
        }

        public int Frequency
        {
            get { return RawRead.Frequency; }
        }

        public int RSSI
        {
            get { return RawRead.Rssi; }
        }

        public int Phase
        {
            get { return RawRead.Phase; }
        }

        public int ReadCount
        {
            get { return RawRead.ReadCount; }
        }

        public DateTime TimeStamp
        {
            get {
                //return DateTime.Now.ToLocalTime();
                TimeSpan difftime = (DateTime.Now.ToUniversalTime() - RawRead.Time.ToUniversalTime());
                //double a1111 = difftime.TotalSeconds;
                if (difftime.TotalHours > 24)
                    return DateTime.Now.ToLocalTime();
                else
                    return RawRead.Time.ToLocalTime();
            }
        }

        public string GPIO
        {
            get
            {
                string gpi = "IN:", gpo = "OUT:";
                if (RawRead.GPIO != null)
                {
                    foreach (GpioPin item in RawRead.GPIO)
                    {
                        gpi += " " + item.Id + "-" + (item.Output ? "H" : "L");
                        gpo += " " + item.Id + "-" + (item.High ? "H" : "L");
                    }
                }
                return (gpi + "\r" + gpo);
            }
        }

        public string Data
        {
            get { return ByteFormat.ToHex(RawRead.Data, "", " "); }
        }

        public string Protocol
        {
            get { return RawRead.Tag.Protocol.ToString(); }
        }

        public void Update(TagReadData mergeData)
        {
            //Console.WriteLine("*** Update " + mergeData.EpcString);
            mergeData.ReadCount += ReadCount;
            TimeSpan timediff = mergeData.Time.ToUniversalTime() - this.TimeStamp.ToUniversalTime();
            // Update only the read counts and not overwriting the tag
            // read data of the existing tag in tag database when we 
            // receive tags in incorrect order.
            if (0 <= timediff.TotalMilliseconds)
            {
                RawRead = mergeData;
            }
            else
            {
                RawRead.ReadCount = mergeData.ReadCount;
            }
        }

        public override string ToString()
        {
            return String.Join("", new string[] {
                 "", "No.", SerialNumber.ToString(),
                " ", "EPC:", EPC,
                " ", "ant:", Antenna.ToString(),
                " ", "rssi:", RSSI.ToString(),
                " ", "count:", ReadCount.ToString(),
                " ", "time:", TimeStamp.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK"),
            });
        }
    }
}