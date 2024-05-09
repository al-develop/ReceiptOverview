using System;
using System.Collections.Generic;
using System.IO;
using ReceiptOverview.Data;
using ReceiptOverview.Models;

namespace ReceiptOverview.Logic;

public class CentralLogic
{
    private DataAccess Access { get; }
    private readonly string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "data.db");

    public CentralLogic()
    {
        Access = new DataAccess(dbPath);
    }

    public List<Position> GetPositions()
    {
        List<Position> positions;
        try
        {
            positions = Access.GetPositions();
        }
        catch (Exception)
        {
            positions = new();
        }

        return positions;
    }

    public int SavePosition(Position position)
    {
        return position.Id == 0 ? Access.NewPosition(position) 
                                : Access.UpdatePosition(position);
    }

    public void DeletePosition(Position position)
    {
        // on deletion of a position, the data layer handles the deletion of entries associated to a Position
        Access.DeletePosition(position);
    }

    // CRUD Entries
    public List<Entry> GetEntriesForPosition(int positionId)
    {
        return positionId == 0
            ? new List<Entry>()
            : Access.GetEntries(positionId);
    }

    public int SaveEntry(Entry entry)
    {
        return entry.Id == 0 ? Access.NewEntry(entry)
                             : Access.UpdateEntry(entry);
    }

    public void UpdateEntry(Entry entry)
    {
        Access.UpdateEntry(entry);
    }

    public void DeleteEntry(Entry entry)
    {
        Access.DeleteEntry(entry);
    }

    public bool CheckDbConnection()
    {
        return File.Exists(dbPath) && Access.CheckDbConnection();
    }

}