using System.Data;
using Microsoft.Data.Sqlite;

namespace ReceiptOverview.Data;

public class ParameterSelector
{
    public static SqliteParameter GetParameter(string parameterName, object value)
    {
        switch (parameterName)
        {
            case ColumnNames.ID:
                return IdParameter(value);
            
            case ColumnNames.DATE:
                return DateParameter(value);
            case ColumnNames.TOTAL:
                return TotalParameter(value);
            
            case ColumnNames.POS_ID:
                return PositionIdParameter(value);
            case ColumnNames.ITEM:
                return ItemParameter(value);
            case ColumnNames.CATEGORY:
                return CategoryParameter(value);
            case ColumnNames.PRICE:
                return PriceParameter(value);
            
            default:
                return null!;
        }
    }
  
    // ID - both in Position and Entry
    private static SqliteParameter IdParameter(object value)
    {
        return new()
        {
            DbType = DbType.Decimal,
            SourceColumn = ColumnNames.ID,
            ParameterName = "@id",
            Value = value
        };
    }

    // Position Parameter
    public static SqliteParameter DateParameter(object value)
    {
        return new()
        {
            DbType = DbType.Date,
            SourceColumn = ColumnNames.DATE,
            ParameterName = "@date",
            Value = value
        };
    }

    public static SqliteParameter TotalParameter(object value)
    {
        return new()
        {
            DbType = DbType.Decimal,
            SourceColumn = ColumnNames.TOTAL,
            ParameterName = "@total",
            Value = value
        };
    }
    
    // Entry Parameter
    private static SqliteParameter PositionIdParameter(object value)
    {
        return new()
        {
            DbType = DbType.Date,
            SourceColumn = ColumnNames.POS_ID,
            ParameterName = "@positionId",
            Value = value
        };
    }

    private static SqliteParameter ItemParameter(object value)
    {
        return new()
        {
            DbType = DbType.Decimal,
            SourceColumn = ColumnNames.ITEM,
            ParameterName = "@item",
            Value = value
        };
    }

    private static SqliteParameter CategoryParameter(object value)
    {
        return new()
        {
            DbType = DbType.Decimal,
            SourceColumn = ColumnNames.CATEGORY,
            ParameterName = "@category",
            Value = value
        };
    }

    private static SqliteParameter PriceParameter(object value)
    {
        return new()
        {
            DbType = DbType.Decimal,
            SourceColumn = ColumnNames.PRICE,
            ParameterName = "@price",
            Value = value
        };
    }
}