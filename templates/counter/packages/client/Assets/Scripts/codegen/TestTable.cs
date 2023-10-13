/* Autogenerated file. Manual edits will not be saved.*/

#nullable enable
using System;
using mud;
using UniRx;
using Property = System.Collections.Generic.Dictionary<string, object>;

namespace mudworld
{
    public class TestTable : IMudTable
    {
        public class TestTableUpdate : RecordUpdate
        {
            public string? key;
            public string? Previouskey;
            public bool? value;
            public bool? Previousvalue;
        }

        public readonly static string ID = "Test";
        public static RxTable Table
        {
            get { return NetworkManager.Instance.ds.store[ID]; }
        }

        public override string GetTableId()
        {
            return ID;
        }

        public string? key;
        public bool? value;

        public override Type TableType()
        {
            return typeof(TestTable);
        }

        public override Type TableUpdateType()
        {
            return typeof(TestTableUpdate);
        }

        public override bool Equals(object? obj)
        {
            TestTable other = (TestTable)obj;

            if (other == null)
            {
                return false;
            }
            if (key != other.key)
            {
                return false;
            }
            if (value != other.value)
            {
                return false;
            }
            return true;
        }

        public override void SetValues(params object[] functionParameters)
        {
            key = (string)functionParameters[0];

            value = (bool)functionParameters[1];
        }

        public static IObservable<RecordUpdate> GetUpdates<T>()
            where T : IMudTable, new()
        {
            IMudTable mudTable = (IMudTable)Activator.CreateInstance(typeof(T));

            return NetworkManager.Instance.sync.onUpdate
                .Where(update => update.Table.Name == ID)
                .Select(recordUpdate =>
                {
                    return mudTable.RecordUpdateToTyped(recordUpdate);
                });
        }

        public override RecordUpdate RecordUpdateToTyped(RecordUpdate recordUpdate)
        {
            var currentValue = recordUpdate.CurrentValue as Property;
            var previousValue = recordUpdate.PreviousValue as Property;

            return new TestTableUpdate
            {
                Table = recordUpdate.Table,
                CurrentValue = recordUpdate.CurrentValue,
                PreviousValue = recordUpdate.PreviousValue,
                CurrentRecordKey = recordUpdate.CurrentRecordKey,
                PreviousRecordKey = recordUpdate.PreviousRecordKey,
                Type = recordUpdate.Type,
                key = (string)(currentValue?["key"] ?? null),
                value = (bool)(currentValue?["value"] ?? null),
                Previouskey = (string)(previousValue?["key"] ?? null),
                Previousvalue = (bool)(previousValue?["value"] ?? null),
            };
        }

        public override void RecordToTable(RxRecord record)
        {
            var table = record.RawValue;

            var keyValue = (string)table["key"];
            key = keyValue;
            var valueValue = (bool)table["value"];
            value = valueValue;
        }
    }
}
