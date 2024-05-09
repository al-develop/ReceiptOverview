using System.Collections.Generic;
using ReceiptOverview.Data;
using ReceiptOverview.Models;

namespace ReceiptOverview.Logic;

public class CentralLogic
{
    private DataAccess access;

    public CentralLogic()
    {
        access = new DataAccess();
    }

    public List<Position> GetPositions()
    {
        return access.GetPositions();
    }


    public int NewPosition(Position newPosition)
    {
        return access.NewPosition(newPosition);
    }

    public void UpdatePosition(Position position)
    {
        access.UpdatePosition(position);
    }

    public void DeletePosition(Position position)
    {
        // on deletion of a position, the data layer handles the deletion of entries associated to a Position
        access.DeletePosition(position);
    }

    // CRUD Entries
    public List<Entry> GetEntriesForPosition(int positionId)
    {
        return positionId == 0
            ? new List<Entry>()
            : access.GetEntries(positionId);
    }

    public int NewEntry(Entry newEntry)
    {
        return access.NewEntry(newEntry);
    }

    public void UpdateEntry(Entry entry)
    {
        access.UpdateEntry(entry);
    }

    public void DeleteEntry(Entry entry)
    {
        access.DeleteEntry(entry);
    }
}