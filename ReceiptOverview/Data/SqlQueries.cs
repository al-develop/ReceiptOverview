using System.Text;

namespace ReceiptOverview.Data;

public static class SqlQueries
{
    private const string POSITION = "Position";
    private const string ENTRY = "Entry";
    
    // For connection tests
    public static string GetFirstPositionId()
    {
	    return $"SELECT {ColumnNames.ID} FROM {POSITION} LIMIT 1";
    }
    
    // CRUD Positions
    public static string GetPositions()
    {
        return $"SELECT * FROM {POSITION}";
    }

    
    
    public static string NewPosition()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine($"INSERT INTO {POSITION} ({ColumnNames.DATE}, {ColumnNames.TOTAL}) ");
        builder.AppendLine("VALUES @date, @total");
        return builder.ToString();
    }

    public static string GetLatestPositionId()
    {
	    return $"SELECT {ColumnNames.ID} FROM {POSITION} ORDER BY {ColumnNames.ID} DESC LIMIT 1";
    }
    
    public static string UpdatePosition()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"UPDATE {POSITION} ");
	    builder.AppendLine($"SET {ColumnNames.DATE} = @date");
	    builder.AppendLine($"SET {ColumnNames.TOTAL} = @total");
	    builder.AppendLine($"WHERE {ColumnNames.ID} = @id");
	    return builder.ToString();
    }

    public static string DeletePosition()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"DELETE FROM {POSITION}");
	    builder.AppendLine($"WHERE {ColumnNames.ID} = @id");
	    return builder.ToString();
    }
    
    
    // CRUD Entries
    public static string GetEntriesForPosition()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"SELECT * FROM {ENTRY}");
	    builder.AppendLine($"WHERE {ColumnNames.POS_ID} = @positionId");
	    return builder.ToString();
    }
    public static string NewEntry()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"INSERT INTO {ENTRY} ({ColumnNames.ID}, {ColumnNames.POS_ID}, {ColumnNames.ITEM}, {ColumnNames.CATEGORY}, {ColumnNames.PRICE}) ");
	    builder.AppendLine("VALUES @id, @positionId, @item, @category, @price");
	    return builder.ToString();
    }

    public static string UpdateEntry()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"UPDATE {ENTRY} ");
	    builder.AppendLine($"SET {ColumnNames.ITEM} = @title");
	    builder.AppendLine($"SET {ColumnNames.CATEGORY} = @category");
	    builder.AppendLine($"SET {ColumnNames.PRICE} = @price");
	    builder.AppendLine($"WHERE {ColumnNames.ID} = @id");
	    return builder.ToString();
    }

    public static string DeleteEntry()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"DELETE FROM {ENTRY}");
	    builder.AppendLine($"WHERE {ColumnNames.ID} = @id");
	    return builder.ToString();
    }
    
    public static string GetLatestEntryId()
    {
	    return $"SELECT {ColumnNames.ID} FROM {ENTRY} ORDER BY {ColumnNames.ID} DESC LIMIT 1";
    }
}
