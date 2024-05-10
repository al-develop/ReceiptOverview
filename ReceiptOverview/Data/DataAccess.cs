using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using ReceiptOverview.Models;

namespace ReceiptOverview.Data;

public class DataAccess
{
    private readonly string connectionString;
    private SqliteConnection connection;

    public DataAccess(string dbPath)
    {
        this.connectionString = $"Data Source={dbPath};";
        connection = new SqliteConnection(connectionString);
    }

    // CRUD Positions
    public List<Position> GetPositions()
    {
        List<Position> resultSet = new();
        using (connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using (SqliteCommand selectCommand = new(SqlQueries.GetPositions(), connection))
            {
                SqliteDataReader reader = selectCommand.ExecuteReader();
                if (!reader.HasRows)
                    return new List<Position>();

                while (reader.Read())
                {
                    Position current = new();
                    current.Id = (Int32)reader[ColumnNames.ID];
                    current.Date = DateTime.Parse(reader[ColumnNames.DATE].ToString()!);
                    current.Total = (decimal)reader[ColumnNames.TOTAL];

                    resultSet.Add(current);
                }
            }

            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        return resultSet;
    }

    public int NewPosition(Position newPosition)
    {
        int newPositionId = 0;
        using (connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using (SqliteCommand insertCommand = new(SqlQueries.NewPosition(), connection))
            {
                SqliteParameter paramDate = ParameterSelector.GetParameter(ColumnNames.DATE, newPosition.Date);
                SqliteParameter paramTotal = ParameterSelector.GetParameter(ColumnNames.TOTAL, newPosition.Total);

                insertCommand.Parameters.Add(paramDate);
                insertCommand.Parameters.Add(paramTotal);

                insertCommand.ExecuteNonQuery();
            }

            newPositionId = GetNextPositionId();

            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        return newPositionId;
    }

    public int UpdatePosition(Position position)
    {
        using (connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using (SqliteCommand updateCommand = new(SqlQueries.UpdatePosition(), connection))
            {
                SqliteParameter paramId = ParameterSelector.GetParameter(ColumnNames.ID, position.Id);
                SqliteParameter paramDate = ParameterSelector.GetParameter(ColumnNames.DATE, position.Date);
                SqliteParameter paramTotal = ParameterSelector.GetParameter(ColumnNames.TOTAL, position.Total);

                updateCommand.Parameters.Add(paramId);
                updateCommand.Parameters.Add(paramDate);
                updateCommand.Parameters.Add(paramTotal);

                updateCommand.ExecuteNonQuery();
            }

            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        return position.Id;
    }

    public void DeletePosition(Position position)
    {
        using (connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            
            using (SqliteCommand deleteCommand = new(SqlQueries.DeletePosition(), connection))
            {
                SqliteParameter paramId = ParameterSelector.GetParameter(ColumnNames.ID, position.Id);
                deleteCommand.Parameters.Add(paramId);
                deleteCommand.ExecuteNonQuery();
            }

            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
    }


    // CRUD Entries
    private List<Entry> GetAllEntries(int positionId = -1)
    {
        List<Entry> resultSet = new();
        using (connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using (SqliteCommand selectCommand = new(SqlQueries.GetEntriesForPosition(), connection))
            {
                selectCommand.Parameters.Add(ParameterSelector.GetParameter(ColumnNames.POS_ID, positionId));

                SqliteDataReader reader = selectCommand.ExecuteReader();
                if (!reader.HasRows)
                    return new List<Entry>();

                while (reader.Read())
                {
                    Entry current = new();
                    current.Id = (Int32)reader[ColumnNames.ID];
                    current.PositionId = (Int32)reader[ColumnNames.POS_ID];
                    current.Item = reader[ColumnNames.ITEM].ToString()!;
                    current.Category = reader[ColumnNames.CATEGORY].ToString()!;
                    current.Price = (decimal)reader[ColumnNames.PRICE];

                    resultSet.Add(current);
                }
            }

            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        return resultSet;
    }

    public List<Entry> GetEntries(int positionId)
    {
        return GetAllEntries(positionId);
    }

    public int NewEntry(Entry newEntry)
    {
        int newEntryId = 0;
        using (connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using (SqliteCommand insertCommand = new(SqlQueries.NewEntry(), connection))
            {
                SqliteParameter paramPositionId = ParameterSelector.GetParameter(ColumnNames.POS_ID, newEntry.PositionId);
                SqliteParameter paramItem = ParameterSelector.GetParameter(ColumnNames.ITEM, newEntry.Item);
                SqliteParameter paramCategory = ParameterSelector.GetParameter(ColumnNames.CATEGORY, newEntry.Category);
                SqliteParameter paramPrice = ParameterSelector.GetParameter(ColumnNames.PRICE, newEntry.Price);

                insertCommand.Parameters.Add(paramPositionId);
                insertCommand.Parameters.Add(paramItem);
                insertCommand.Parameters.Add(paramCategory);
                insertCommand.Parameters.Add(paramPrice);

                insertCommand.ExecuteNonQuery();
            }

            newEntryId = GetNextEntryId();

            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        return newEntryId;
    }

    public int UpdateEntry(Entry entry)
    {
        using (connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using (SqliteCommand updateCommand = new(SqlQueries.UpdateEntry(), connection))
            {
                // if an entry is assigned to a position, it should not be possible, to change this assignment anymore
                // so no parameter for PositionId
                SqliteParameter paramId = ParameterSelector.GetParameter(ColumnNames.ID, entry.Id);
                SqliteParameter paramItem = ParameterSelector.GetParameter(ColumnNames.ITEM, entry.Item);
                SqliteParameter paramCategory = ParameterSelector.GetParameter(ColumnNames.CATEGORY, entry.Category);
                SqliteParameter paramPrice = ParameterSelector.GetParameter(ColumnNames.PRICE, entry.Price);

                updateCommand.Parameters.Add(paramId);
                updateCommand.Parameters.Add(paramItem);
                updateCommand.Parameters.Add(paramCategory);
                updateCommand.Parameters.Add(paramPrice);

                updateCommand.ExecuteNonQuery();
            }

            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        return entry.Id;
    }

    public void DeleteEntry(Entry entry)
    {
        using (connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            using (SqliteCommand deleteCommand = new(SqlQueries.DeleteEntry(), connection))
            {
                SqliteParameter paramId = ParameterSelector.GetParameter(ColumnNames.ID, entry.Id);
                deleteCommand.Parameters.Add(paramId);
                deleteCommand.ExecuteNonQuery();
            }

            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
    }


    // Helper
    public bool CheckDbConnection()
    {
        bool success;
        try
        {
            using (connection)
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                // Attempt to read the ID of the first position.
                // if the database has no tables, an exception occurs
                // if the table exists, but has no entries, 0 will be returned
                using (SqliteCommand selectCommand = new(SqlQueries.GetFirstPositionId(), connection))
                {
                    SqliteDataReader reader = selectCommand.ExecuteReader();
                    reader.Read();
                }

                success = true;
            }
        }
        catch (Exception ex)
        {
            success = false;
        }
        finally
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        return success;
    }

    private int GetNextPositionId()
    {
        int nextPostitionId = 0;
        using (connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            using (SqliteCommand selectCommand = new(SqlQueries.GetLatestPositionId(), connection))
            {
                SqliteDataReader reader = selectCommand.ExecuteReader();
                if (!reader.HasRows)
                    return nextPostitionId;

                while (reader.Read())
                {
                    nextPostitionId = (Int32)reader[ColumnNames.ID];
                }
            }

            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        return nextPostitionId++;
    }

    private int GetNextEntryId()
    {
        int nextEntryId = 0;
        using (connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            
            using (SqliteCommand selectCommand = new(SqlQueries.GetLatestEntryId(), connection))
            {
                SqliteDataReader reader = selectCommand.ExecuteReader();
                if (!reader.HasRows)
                    return nextEntryId;

                while (reader.Read())
                {
                    nextEntryId = (Int32)reader[ColumnNames.ID];
                }
            }

            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }

        return nextEntryId++;
    }
}
