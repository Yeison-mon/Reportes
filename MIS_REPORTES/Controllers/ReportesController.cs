using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Reporting.NETCore;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace MIS_REPORTES.Controllers
{
    public class ReportesController : Controller
    {
        private readonly FG _dbHelper;
        private readonly string _reportPath;
        private readonly ILogger<ReportesController> _logger;

        public ReportesController(FG dbHelper, ILogger<ReportesController> logger)
        {
            _dbHelper = dbHelper;
            _logger = logger;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _reportPath = "wwwroot/reportes";
        }

        public async Task<IActionResult> GenerateReport(int documento, string tipo)
        {
            try
            {
                var reportFile = GetReportPath(tipo);
                var parameters = new Dictionary<string, string>();
                var dataSources = await GetReportDataSources(tipo, documento);

                _logger.LogInformation("Generating report: {ReportType} for document: {Documento}", tipo, documento);

                LocalReport report = new LocalReport();
                using (var fs = new FileStream(reportFile, FileMode.Open, FileAccess.Read))
                {
                    report.LoadReportDefinition(fs);
                }

                foreach (var dataSource in dataSources)
                {
                    _logger.LogInformation("Adding DataSource: {DataSourceName} with {RowCount} rows", dataSource.Key, dataSource.Value.Rows.Count);
                    report.DataSources.Add(new ReportDataSource(dataSource.Key, dataSource.Value));
                }

                var result = report.Render("PDF");

                return File(result, "application/pdf");
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.LogError(ex, "IndexOutOfRangeException while generating report: {ReportType} for document: {Documento}", tipo, documento);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error generating report due to index out of range.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while generating report: {ReportType} for document: {Documento}", tipo, documento);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error generating report.");
            }
        }

        private void ValidateDataTable(DataTable table, string tableName)
        {
            if (table == null)
            {
                throw new Exception($"DataTable {tableName} is null.");
            }
            if (table.Rows.Count == 0)
            {
                _logger.LogWarning("DataTable {TableName} has no rows.", tableName);
            }
            else
            {
                _logger.LogInformation("DataTable {TableName} loaded with {RowCount} rows.", tableName, table.Rows.Count);
            }

            foreach (DataColumn column in table.Columns)
            {
                _logger.LogInformation("DataTable {TableName} has column: {ColumnName} of type {DataType}", tableName, column.ColumnName, column.DataType);
            }

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    _logger.LogInformation("DataTable {TableName} row: {RowIndex} column: {ColumnName} value: {Value}", tableName, table.Rows.IndexOf(row), column.ColumnName, row[column]);
                }
            }
        }

        private string GetReportPath(string tipo)
        {
            return tipo switch
            {
                "Recepcion" => $"{_reportPath}/Recepcion/rpRecepcion.rdlc",
                "Inspeccion" => $"{_reportPath}/Recepcion/rpInspeccion.rdlc",
                "ODT" => $"{_reportPath}/Laboratorio/rpOrdenTrabajo.rdlc",
                _ => throw new Exception("Tipo de reporte desconocido")
            };
        }

        private async Task<Dictionary<string, DataTable>> GetReportDataSources(string tipo, int documento)
        {
            var dataSources = new Dictionary<string, DataTable>();
            DataTable dt;
            switch (tipo)
            {
                case "Recepcion":
                    dt = await _dbHelper.GetRecepcionEncabezado(documento);
                    ValidateDataTable(dt, "recepciones");
                    dataSources.Add("recepciones", dt);
                    dt = await _dbHelper.GetRecepcionDetalle(documento);
                    ValidateDataTable(dt, "recepcion_detalle");
                    dataSources.Add("recepcion_detalle", dt);
                    break;
                case "Inspeccion":
                    dt = await _dbHelper.GetInspeccionEncabezado(documento);
                    ValidateDataTable(dt, "inspecciones");
                    dataSources.Add("inspecciones", dt);
                    dt = await _dbHelper.GetInspeccionDetalle(documento);
                    ValidateDataTable(dt, "inspeccion_detalle");
                    dataSources.Add("inspeccion_detalle", dt);
                    break;
                case "ODT":
                    dt = await _dbHelper.GetOrdenTrabajoEncabezado(documento);
                    ValidateDataTable(dt, "ordentrabajo");
                    dataSources.Add("ordentrabajo", dt);
                    dt = await _dbHelper.GetOrdenTrabajoDetalle(documento);
                    ValidateDataTable(dt, "ordentrabajo_detalle");
                    dataSources.Add("ordentrabajo_detalle", dt);
                    break;
            }
            return dataSources;
        }
    }
}
