using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using ReceiptOverview.Models;

namespace ReceiptOverview.Data;

public class DataAccess
{
    private SqliteConnection _connection;

    public DataAccess(string dbPath)
    {
        _connection = new SqliteConnection($"Data Source={dbPath};");
    }

    // CRUD Positions
    public List<Position> GetPositions()
    {
        List<Position> resultSet = new();
        using (_connection)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            using (SqliteCommand selectCommand = new(SqlQueries.GetPositions(), _connection))
            {
                SqliteDataReader reader = selectCommand.ExecuteReader();
                if (!reader.HasRows)
                    return new List<Position>();
                
                while (reader.Read())
                {
                    Position current = new();
                    current.Id = Convert.ToInt32(reader[ConstStrings.ID]);
                    current.Date = DateTime.Parse(reader[ConstStrings.DATE].ToString()!);
                    current.Total = Convert.ToDecimal(reader[ConstStrings.TOTAL]);

                    resultSet.Add(current);
                }
                reader.Close();
            }

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        return resultSet;
    }

    public int NewPosition(Position newPosition)
    {
        int newPositionId = 0;
        using (_connection)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            using (SqliteCommand insertCommand = new(SqlQueries.NewPosition(), _connection))
            {
                SqliteParameter paramDate = ParameterSelector.GetParameter(ConstStrings.DATE, newPosition.Date);
                SqliteParameter paramTotal = ParameterSelector.GetParameter(ConstStrings.TOTAL, newPosition.Total);

                insertCommand.Parameters.Add(paramDate);
                insertCommand.Parameters.Add(paramTotal);

                insertCommand.ExecuteNonQuery();
            }

            newPositionId = GetLatestPositionId();

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        return newPositionId;
    }

    public int UpdatePosition(Position position)
    {
        using (_connection)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            using (SqliteCommand updateCommand = new(SqlQueries.UpdatePosition(), _connection))
            {
                SqliteParameter paramId = ParameterSelector.GetParameter(ConstStrings.ID, position.Id);
                SqliteParameter paramDate = ParameterSelector.GetParameter(ConstStrings.DATE, position.Date);
                SqliteParameter paramTotal = ParameterSelector.GetParameter(ConstStrings.TOTAL, position.Total);

                updateCommand.Parameters.Add(paramId);
                updateCommand.Parameters.Add(paramDate);
                updateCommand.Parameters.Add(paramTotal);

                updateCommand.ExecuteNonQuery();
            }

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        return position.Id;
    }

    public void DeletePosition(Position position)
    {
        using (_connection)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            using (SqliteCommand deleteCommand = new(SqlQueries.DeletePosition(), _connection))
            {
                SqliteParameter paramId = ParameterSelector.GetParameter(ConstStrings.ID, position.Id);
                deleteCommand.Parameters.Add(paramId);
                deleteCommand.ExecuteNonQuery();
            }

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }
    }


    // CRUD Entries
    private List<Entry> GetAllEntries(int positionId = -1)
    {
        List<Entry> resultSet = new();
        using (_connection)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            using (SqliteCommand selectCommand = new(SqlQueries.GetEntriesForPosition(), _connection))
            {
                selectCommand.Parameters.Add(ParameterSelector.GetParameter(ConstStrings.POS_ID, positionId));

                SqliteDataReader reader = selectCommand.ExecuteReader();
                if (!reader.HasRows)
                    return new List<Entry>();

                while (reader.Read())
                {
                    Entry current = new();
                    current.Id = Convert.ToInt32(reader[ConstStrings.ID]);
                    current.PositionId = Convert.ToInt32(reader[ConstStrings.POS_ID]);
                    current.Item = reader[ConstStrings.ITEM].ToString()!;
                    current.Category = reader[ConstStrings.CATEGORY].ToString()!;
                    current.Price = Convert.ToDecimal(reader[ConstStrings.PRICE]);

                    resultSet.Add(current);
                }
                reader.Close();
            }

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
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
        using (_connection)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            using (SqliteCommand insertCommand = new(SqlQueries.NewEntry(), _connection))
            {
                SqliteParameter paramPositionId = ParameterSelector.GetParameter(ConstStrings.POS_ID, newEntry.PositionId);
                SqliteParameter paramItem = ParameterSelector.GetParameter(ConstStrings.ITEM, newEntry.Item);
                SqliteParameter paramCategory = ParameterSelector.GetParameter(ConstStrings.CATEGORY, newEntry.Category);
                SqliteParameter paramPrice = ParameterSelector.GetParameter(ConstStrings.PRICE, newEntry.Price);

                insertCommand.Parameters.Add(paramPositionId);
                insertCommand.Parameters.Add(paramItem);
                insertCommand.Parameters.Add(paramCategory);
                insertCommand.Parameters.Add(paramPrice);

                insertCommand.ExecuteNonQuery();
            }

            newEntryId = GetLatestEntryId();

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        return newEntryId;
    }

    public int UpdateEntry(Entry entry)
    {
        using (_connection)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            using (SqliteCommand updateCommand = new(SqlQueries.UpdateEntry(), _connection))
            {
                // if an entry is assigned to a position, it should not be possible, to change this assignment anymore
                // so no parameter for PositionId
                SqliteParameter paramId = ParameterSelector.GetParameter(ConstStrings.ID, entry.Id);
                SqliteParameter paramItem = ParameterSelector.GetParameter(ConstStrings.ITEM, entry.Item);
                SqliteParameter paramCategory = ParameterSelector.GetParameter(ConstStrings.CATEGORY, entry.Category);
                SqliteParameter paramPrice = ParameterSelector.GetParameter(ConstStrings.PRICE, entry.Price);

                updateCommand.Parameters.Add(paramId);
                updateCommand.Parameters.Add(paramItem);
                updateCommand.Parameters.Add(paramCategory);
                updateCommand.Parameters.Add(paramPrice);

                updateCommand.ExecuteNonQuery();
            }

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        return entry.Id;
    }

    public void DeleteEntry(Entry entry)
    {
        using (_connection)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            using (SqliteCommand deleteCommand = new(SqlQueries.DeleteEntry(), _connection))
            {
                SqliteParameter paramId = ParameterSelector.GetParameter(ConstStrings.ID, entry.Id);
                deleteCommand.Parameters.Add(paramId);
                deleteCommand.ExecuteNonQuery();
            }

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }
    }


    // Helper
    public bool CheckDbConnection()
    {
        bool success;
        try
        {
            using (_connection)
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                // Attempt to read the ID of the first position.
                // if the database has no tables, an exception occurs
                // if the table exists, but has no entries, 0 will be returned
                using (SqliteCommand selectCommand = new(SqlQueries.GetFirstPositionId(), _connection))
                {
                    SqliteDataReader reader = selectCommand.ExecuteReader();
                    reader.Read();
                    reader.Close();
                }
                
                success = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"In DataAccess: {ex.Message}");
            success = false;
        }
        finally
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        return success;
    }

    private int GetLatestPositionId()
    {
        int latestPostitionId = 0;
        using (_connection)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            using (SqliteCommand selectCommand = new(SqlQueries.GetLatestPositionId(), _connection))
            {
                SqliteDataReader reader = selectCommand.ExecuteReader();
                if (!reader.HasRows)
                    return latestPostitionId;

                while (reader.Read())
                {
                    latestPostitionId = Convert.ToInt32(reader[ConstStrings.ID]);
                }
                reader.Close();
            }

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        return latestPostitionId;
    }

    private int GetLatestEntryId()
    {
        int latestEntryId = 0;
        using (_connection)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            
            using (SqliteCommand selectCommand = new(SqlQueries.GetLatestEntryId(), _connection))
            {
                SqliteDataReader reader = selectCommand.ExecuteReader();
                if (!reader.HasRows)
                    return latestEntryId;

                while (reader.Read())
                {
                    latestEntryId = Convert.ToInt32(reader[ConstStrings.ID]);
                }
                reader.Close();
            }

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        return latestEntryId;
    }
}