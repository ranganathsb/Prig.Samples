using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace UntestableLibrary
{
    class ULTableStatus
    {
        internal bool IsOpened = false;
        internal int RowsCount = 0;
    }

    public class ULColumn
    {
        public ULColumn(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    public class ULColumns : IEnumerable
    {
        ULTableStatus m_status;
        List<ULColumn> m_columns = new List<ULColumn>();

        internal ULColumns(ULTableStatus status)
        {
            m_status = status;
        }

        public void Add(ULColumn column)
        {
            ValidateState(m_status);
            m_columns.Add(column);
        }

        public void Remove(ULColumn column)
        {
            ValidateState(m_status);
            m_columns.Remove(column);
        }

        public IEnumerator GetEnumerator()
        {
            return m_columns.GetEnumerator();
        }

        static void ValidateState(ULTableStatus status)
        {
            if (!status.IsOpened)
                throw new InvalidOperationException("The column can not be modified because owner table has not been opened.");

            if (0 < status.RowsCount)
                throw new ArgumentException("The column can not be modified because some rows already exist.");
        }
    }

    public class ULTable
    {
        ULTableStatus m_status = new ULTableStatus();

        public ULTable(string tableName)
        {
            TableName = tableName;
            Columns = new ULColumns(m_status);
        }

        public string TableName { get; private set; }
        public ULColumns Columns { get; private set; }

        public void Open(string connectionString)
        {
            Thread.Sleep(5000); // simulate connecting DB and filling this schema

            m_status.IsOpened = true;
        }
    }
}
