using System;
using System.Collections;
using System.Collections.Generic;
using ThingMagic;

namespace ReceiveAutonomousReadingDemodotNet
{
    public class TagDatabase
    {
        public Dictionary<string, TagReadRecord> EpcIndex = new Dictionary<string, TagReadRecord>();
        private List<TagReadRecord> _tagList = new List<TagReadRecord>();

        static long UniqueTagCounts = 0;
        static long TotalTagCounts = 0;
       

        public long UniqueTagCount
        {
            get { return UniqueTagCounts; }
        }
        public long TotalTagCount
        {
            get { return TotalTagCounts; }
        }

        public List<TagReadRecord> TagList {
            get { return _tagList; }
        }

        public void Add(TagReadData addData)
        {

            lock (new Object())
            {
                string key = null;

                key = addData.EpcString; //if only keying on EPCID

                UniqueTagCounts = 0;
                TotalTagCounts = 0;

                if (!EpcIndex.ContainsKey(key))
                {
                    TagReadRecord value = new TagReadRecord(addData);
                    value.SerialNumber = (uint)EpcIndex.Count + 1;

                    EpcIndex.Add(key, value);
                    _tagList.Add(value);
                    //Call this method to calculate total tag reads and unique tag read counts 
                    UpdateTagCountTextBox(EpcIndex);
                    Console.WriteLine("Add {0}", value);
                }
                else
                {
                    EpcIndex[key].Update(addData); //ToDo update temperature
                    UpdateTagCountTextBox(EpcIndex);
                }
            }
        }

        private void UpdateTagCountTextBox(Dictionary<string, TagReadRecord> epcIndex)
        {
            UniqueTagCounts += EpcIndex.Count;
            TagReadRecord[] dataRecord = new TagReadRecord[EpcIndex.Count];
            EpcIndex.Values.CopyTo(dataRecord, 0);
            TotalTagCounts = 0;
            for (int i = 0; i < dataRecord.Length; i++)
            {
                TotalTagCounts += dataRecord[i].ReadCount;
            }
        }

        public void Clear()
        {
            EpcIndex.Clear();
            UniqueTagCounts = 0;
            TotalTagCounts = 0;
            _tagList.Clear();
        }
    }
}