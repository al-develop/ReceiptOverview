using System.Text;

namespace ReceiptOverview.Data;

public static class SqlQueries
{

    
    // For connection tests
    public static string GetFirstPositionId()
    {
	    return $"SELECT {ConstStrings.ID} FROM {ConstStrings.POSITION} LIMIT 1";
    }
    
    // CRUD Positions
    public static string GetPositions()
    {
        return $"SELECT * FROM {ConstStrings.POSITION}";
    }

    
    
    public static string NewPosition()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine($"INSERT INTO {ConstStrings.POSITION} ({ConstStrings.DATE}, {ConstStrings.TOTAL})");
        builder.AppendLine($"VALUES ({ConstStrings.DATE_PARAM}, {ConstStrings.TOTAL_PARAM})");
        return builder.ToString();
    }

    public static string GetLatestPositionId()
    {
	    return $"SELECT {ConstStrings.ID} FROM {ConstStrings.POSITION} ORDER BY {ConstStrings.ID} DESC LIMIT 1";
    }
    
    public static string UpdatePosition()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"UPDATE {ConstStrings.POSITION} ");
	    builder.AppendLine($"SET {ConstStrings.DATE} = {ConstStrings.DATE_PARAM},");
	    builder.AppendLine($"{ConstStrings.TOTAL} = {ConstStrings.TOTAL_PARAM}");
	    builder.AppendLine($"WHERE {ConstStrings.ID} = {ConstStrings.ID_PARAM}");
	    return builder.ToString();
    }

    public static string DeletePosition()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"DELETE FROM {ConstStrings.POSITION}");
	    builder.AppendLine($"WHERE {ConstStrings.ID} = {ConstStrings.ID_PARAM}");
	    return builder.ToString();
    }
    
    
    // CRUD Entries
    public static string GetEntriesForPosition()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"SELECT * FROM {ConstStrings.ENTRY}");
	    builder.AppendLine($"WHERE {ConstStrings.POS_ID} = {ConstStrings.POS_ID_PARAM}");
	    return builder.ToString();
    }
    public static string NewEntry()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"INSERT INTO {ConstStrings.ENTRY} ({ConstStrings.POS_ID}, {ConstStrings.ITEM}, {ConstStrings.CATEGORY}, {ConstStrings.PRICE}) ");
	    builder.AppendLine($"VALUES ({ConstStrings.POS_ID_PARAM}, {ConstStrings.ITEM_PARAM}, {ConstStrings.CATEGORY_PARAM}, {ConstStrings.PRICE_PARAM})");
	    return builder.ToString();
    }

    public static string UpdateEntry()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"UPDATE {ConstStrings.ENTRY}");
	    builder.AppendLine($"SET {ConstStrings.ITEM} = {ConstStrings.ITEM_PARAM},");
	    builder.AppendLine($"{ConstStrings.CATEGORY} = {ConstStrings.CATEGORY_PARAM},");
	    builder.AppendLine($"{ConstStrings.PRICE} = {ConstStrings.PRICE_PARAM}");
	    builder.AppendLine($"WHERE {ConstStrings.ID} = {ConstStrings.ID_PARAM}");
	    return builder.ToString();
    }

    public static string DeleteEntry()
    {
	    StringBuilder builder = new StringBuilder();
	    builder.AppendLine($"DELETE FROM {ConstStrings.ENTRY}");
	    builder.AppendLine($"WHERE {ConstStrings.ID} = {ConstStrings.ID_PARAM}");
	    return builder.ToString();
    }
    
    public static string GetLatestEntryId()
    {
	    return $"SELECT {ConstStrings.ID} FROM {ConstStrings.ENTRY} ORDER BY {ConstStrings.ID} DESC LIMIT 1";
    }
}
