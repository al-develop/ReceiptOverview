using System.Data;
using Microsoft.Data.Sqlite;

namespace ReceiptOverview.Data;

public class ParameterSelector
{
    public static SqliteParameter GetParameter(string parameterName, object value)
    {
        switch (parameterName)
        {
            case ConstStrings.ID:
                return IdParameter(value);
            
            case ConstStrings.DATE:
                return DateParameter(value);
            case ConstStrings.TOTAL:
                return TotalParameter(value);
            
            case ConstStrings.POS_ID:
                return PositionIdParameter(value);
            case ConstStrings.ITEM:
                return ItemParameter(value);
            case ConstStrings.CATEGORY:
                return CategoryParameter(value);
            case ConstStrings.PRICE:
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
            SourceColumn = ConstStrings.ID,
            ParameterName = ConstStrings.ID_PARAM,
            Value = value
        };
    }

    // Position Parameter
    public static SqliteParameter DateParameter(object value)
    {
        return new()
        {
            DbType = DbType.Date,
            SourceColumn = ConstStrings.DATE,
            ParameterName = ConstStrings.DATE_PARAM,
            Value = value
        };
    }

    public static SqliteParameter TotalParameter(object value)
    {
        return new()
        {
            DbType = DbType.Decimal,
            SourceColumn = ConstStrings.TOTAL,
            ParameterName = ConstStrings.TOTAL_PARAM,
            Value = value
        };
    }
    
    // Entry Parameter
    private static SqliteParameter PositionIdParameter(object value)
    {
        return new()
        {
            DbType = DbType.Date,
            SourceColumn = ConstStrings.POS_ID,
            ParameterName = ConstStrings.POS_ID_PARAM,
            Value = value
        };
    }

    private static SqliteParameter ItemParameter(object value)
    {
        return new()
        {
            DbType = DbType.Decimal,
            SourceColumn = ConstStrings.ITEM,
            ParameterName = ConstStrings.ITEM_PARAM,
            Value = value
        };
    }

    private static SqliteParameter CategoryParameter(object value)
    {
        return new()
        {
            DbType = DbType.Decimal,
            SourceColumn = ConstStrings.CATEGORY,
            ParameterName = ConstStrings.CATEGORY_PARAM,
            Value = value
        };
    }

    private static SqliteParameter PriceParameter(object value)
    {
        return new()
        {
            DbType = DbType.Decimal,
            SourceColumn = ConstStrings.PRICE,
            ParameterName = ConstStrings.PRICE_PARAM,
            Value = value
        };
    }
}