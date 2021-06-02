using System;
using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.Shared;
using System.IO;
using PracticaEvaluadaReport.Models;
using PracticaEvaluadaReport.Rpts;

namespace PracticaEvaluadaReportes.Controllers
{
    public class HomeController : Controller
    {
        private neptunoEntities conexto = new neptunoEntities();
        public ActionResult ReporteProductoPorCategoria()
        {
            var categorias = conexto.categorias.ToList();
            ViewBag.categorias = categorias;
            return View();
        }

        public ActionResult ReporteProductosLacteos()
        {
            return View();
        }

        public ActionResult ReportePedidosSegunRangoDeFecha()
        {
            return View();
        }
        public ActionResult ReporteProveedoresPorCategoria()
        {
            var categorias = conexto.categorias.ToList();
            ViewBag.categorias = categorias;
            return View();
        }
        public ActionResult ReporteEmpleadosEdad()
        {
            return View();
        }


        public ActionResult VerReporteProductoCategoria(int parametro)
        {
            var reporte = new ProductosPorCategoria();
            reporte.FileName = Server.MapPath("/Rpts/ProductosPorCategoria.rpt");
            reporte.SetParameterValue("paramIdCategoria", parametro);
              var coninfo = getConexion();
            TableLogOnInfo logoninfo = new TableLogOnInfo();
            Tables tables;
            tables = reporte.Database.Tables;
            reporte.DataSourceConnections.Clear();
            foreach (Table item in tables)
            {
                logoninfo = item.LogOnInfo;
                logoninfo.ConnectionInfo = coninfo;
                item.ApplyLogOnInfo(logoninfo);
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult VerReporteClientesLacteos()
        {
            var reporte = new ClientesPorProductosLacteos();

            reporte.FileName = Server.MapPath("/Rpts/ClientesPorProductosLacteos.rpt");
            var clientes = (from c in conexto.clientes
                            join p in conexto.Pedidos on c.idCliente equals p.IdCliente
                            join dp in conexto.detallesdepedidos on p.IdPedido equals dp.idpedido
                            join pro in conexto.productos on dp.idproducto equals pro.idproducto
                            where pro.idCategoria == 4
                            select c).ToList();
            reporte.SetDataSource(clientes);
             var coninfo = getConexion();
            TableLogOnInfo logoninfo = new TableLogOnInfo();
            Tables tables;
            tables = reporte.Database.Tables;
            reporte.DataSourceConnections.Clear();
            foreach (Table item in tables)
            {
                logoninfo = item.LogOnInfo;
                logoninfo.ConnectionInfo = coninfo;
                item.ApplyLogOnInfo(logoninfo);
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult VerReportePedidosPorFecha(string fecha1, string fecha2)
        {
            var reporte = new ClientesPorProductosLacteos();
            DateTime fechaInicio = Convert.ToDateTime(fecha1);
            DateTime fechFin = Convert.ToDateTime(fecha2);
            reporte.FileName = Server.MapPath("/Rpts/ReportePedidosPorFecha.rpt");
            reporte.SetParameterValue("fechaInicio", fecha1);
            reporte.SetParameterValue("fechaFin", fecha2);
              var coninfo = getConexion();
            TableLogOnInfo logoninfo = new TableLogOnInfo();
            Tables tables;
            tables = reporte.Database.Tables;
            reporte.DataSourceConnections.Clear();
            foreach (Table item in tables)
            {
                logoninfo = item.LogOnInfo;
                logoninfo.ConnectionInfo = coninfo;
                item.ApplyLogOnInfo(logoninfo);
            }

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }
        
        public ConnectionInfo getConexion()
        {
            ConnectionInfo crconnectioninfo = new ConnectionInfo();
            crconnectioninfo.ServerName = ".";
            crconnectioninfo.DatabaseName = "neptuno";
            crconnectioninfo.IntegratedSecurity = true;
            return crconnectioninfo;
        }

        public ActionResult VerReporteProveedorCategoria(int parametro)
        {
            var reporte = new ProductosPorCategoria();
            reporte.FileName = Server.MapPath("/Rpts/ReportePorveedoresPorCategoria.rpt");
            reporte.SetParameterValue("paramIdCategoria", parametro);
              var coninfo = getConexion();
            TableLogOnInfo logoninfo = new TableLogOnInfo();
            Tables tables;
            tables = reporte.Database.Tables;
            reporte.DataSourceConnections.Clear();
            foreach (Table item in tables)
            {
                logoninfo = item.LogOnInfo;
                logoninfo.ConnectionInfo = coninfo;
                item.ApplyLogOnInfo(logoninfo);
            }
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult VerReporteEmpleadosPorEdad(int edad1, int edad2)
        {
            var reporte = new ClientesPorProductosLacteos();

            reporte.FileName = Server.MapPath("/Rpts/ReporteEmpleadosEdad.rpt");
            reporte.SetParameterValue("edadInicio", edad1);
            reporte.SetParameterValue("edadFin", edad2);
              var coninfo = getConexion();
            TableLogOnInfo logoninfo = new TableLogOnInfo();
            Tables tables;
            tables = reporte.Database.Tables;
            reporte.DataSourceConnections.Clear();
            foreach (Table item in tables)
            {
                logoninfo = item.LogOnInfo;
                logoninfo.ConnectionInfo = coninfo;
                item.ApplyLogOnInfo(logoninfo);
            }

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "application/pdf");
        }
    }
}
