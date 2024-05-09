using System.Text;

namespace ReceiptOverview.Data;

public static class SqlQueries
{
    private const string POSITION = "Position";
    private const string ENTRY = "Entry";
    
    // CRUD Positions
    public static string GetPositions()
    {
        return $"SELECT * FROM {POSITION}";
    }

    public static string NewPosition()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine($"INSERT INTO {POSITION} (Date, Total) ");
        builder.AppendLine("VALUES @date, @total");
        return builder.ToString();
    }

    public static string GetLatestPositionId()
    {
	    return $"SELECT TOP 1 ID FROM {POSITION} ORDER BY DESC";
    }
    
    public static string UpdatePosition()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"UPDATE {POSITION} ");
	    builder.AppendLine("SET Date = @new_date");
	    builder.AppendLine("SET Total = @new_total");
	    builder.AppendLine("WHERE ID = @id");
	    return builder.ToString();
    }

    public static string DeletePosition()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"DELETE FROM {POSITION}");
	    builder.AppendLine("WHERE ID = @id");
	    return builder.ToString();
    }
    
    
    // CRUD Entries
    public static string GetEntries()
    {
	    string query = $"SELECT * FROM {ENTRY}";
	    return query;
    }

    public static string NewEntry()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"INSERT INTO {ENTRY} (ID, Title, Category, Price) ");
	    builder.AppendLine("VALUES @id, @title, @category, @price");
	    return builder.ToString();
    }

    public static string UpdateEntry()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"UPDATE {ENTRY} ");
	    builder.AppendLine("SET Title = @n_title");
	    builder.AppendLine("SET Category = @n_category");
	    builder.AppendLine("SET Price = @n_price");
	    builder.AppendLine("WHERE ID = @id");
	    return builder.ToString();
    }

    public static string DeleteEntry()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"DELETE FROM {ENTRY}");
	    builder.AppendLine("WHERE ID = @id");
	    return builder.ToString();
    }
}
