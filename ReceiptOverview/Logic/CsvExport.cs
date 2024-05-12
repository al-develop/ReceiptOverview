using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ReceiptOverview.Models;

namespace ReceiptOverview.Logic;

public class CsvExport
{
    public void ExportToCsV(CentralLogic logic)
    {
        var positions = logic.GetPositions();
        
        string exportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Export");
        string exportPath = Path.Combine(exportDir, "Positions.csv");

        var builder = new StringBuilder();
        builder.AppendLine("ID,Date,Total,EntryID,Position_ID,Item,Category,Price");

        foreach (var position in positions)
        {
            var entries = logic.GetEntriesForPosition(position.Id);
            foreach (var entry in entries)
            {
                builder.AppendLine($"{position.Id}," +
                                   $"{position.Date:yyyy-MM-dd}," +
                                   $"{position.Total.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}," +
                                   $"{entry.Id}," +
                                   $"{entry.PositionId}," +
                                   $"{entry.Item}," +
                                   $"{entry.Category}," +
                                   $"{entry.Price.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}");
            }
        }

        if (!Directory.Exists(exportDir))
            Directory.CreateDirectory(exportDir);

        File.WriteAllText(exportPath, builder.ToString());
    }
}