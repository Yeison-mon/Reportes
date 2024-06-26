using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MIS_REPORTES.Controllers
{
    public class FG
    {
        private readonly string _connectionString;

        public FG(string connectionString)
        {
            _connectionString = connectionString;
        }

        private async Task<DataTable> ExecuteQueryAsync(string query)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var dataTable = new DataTable();
                var reader = await connection.ExecuteReaderAsync(query);
                dataTable.Load(reader);
                return dataTable;
            }
        }

        public async Task<DataTable> GetRecepcionEncabezado(int documento)
        {
            string query = $@"SELECT 
                    r.id, r.id, r.observacion, c.nombrecompleto as cliente, c.documento, ru.nombrecompleto as usuario,
                    'NR-' || TO_CHAR(r.fecha, 'YY-') || TO_CHAR(r.recepcion, '0000') AS recepcion, to_char(r.fecha, 'YYYY-MM-DD') as fecha,
                    CASE
                        WHEN EXISTS (SELECT 1 FROM recepcion_detalle rd WHERE rd.idrecepcion = r.id AND rd.material = false) THEN 1
                        ELSE 0 
                    END as item,
                    CASE 
                        WHEN EXISTS (SELECT 1 FROM recepcion_detalle rd WHERE rd.idrecepcion = r.id AND rd.material = true) THEN 1
                        ELSE 0 
                    END as material
                FROM recepciones r
                INNER JOIN clientes c ON c.id = r.idcliente
                INNER JOIN seguridad.rbac_usuarios ru ON ru.id = r.idusuario
                WHERE r.recepcion = {documento}
                GROUP BY 
                    r.id, r.fecha, c.nombrecompleto, c.documento, ru.nombrecompleto, ru.cargo, ru.email";
            return await ExecuteQueryAsync(query);
        }

        public async Task<DataTable> GetRecepcionDetalle(int documento)
        {
            string query = $@"SELECT rd.id, renglon,
                   'NR-' || to_char(rd.fechaing, 'YY-') || to_char(r.recepcion, '0000') || '-' || rd.renglon AS ingreso,
                   '<b>EQUIPO:</b> ' || e.descripcion || '; <b>MAGNITUD:</b> ' || m.descripcion ||
                   '; <b>MARCA:</b> ' || ma.descripcion || '; <b>MODELO:</b> ' || mo.descripcion || CASE WHEN rd.codigo not in ('', 'Sin Información') THEN ' <b>SERIE:</b> ' || rd.serie || ' <b>CÓDIGO:</b> ' || rd.codigo ELSE ' <b>SERIE:</b> ' || rd.serie end ||
                   CASE WHEN rd.idintervalo1 > 0 then '; <b>RANGO:</b> (' || mi1.desde || ' a ' || mi1.hasta || ') ' || mi1.medida else '; <b>RANGO:</b> VER ESPECIFICACIONES' end ||
                   CASE WHEN rd.idintervalo2 > 0 then '; <b>RANGO 2:</b> (' || mi2.desde || ' a ' || mi2.hasta || ') ' || mi2.medida else '' end ||
                   CASE WHEN rd.accesorios != '' THEN '; <b>OBSERVACIÓN:</b> ' || rd.observacion || '; <b>ACCESORIOS:</b> ' || rd.accesorios ELSE '; <b>OBSERVACIÓN:</b> ' || rd.observacion end || '; <b>SERVICIO:</b> ' || rd.tipo_servicio  as descripcion
                FROM recepcion_detalle rd  
                INNER JOIN equipos e ON rd.idequipo = e.id  
                INNER JOIN magnitudes m ON rd.idmagnitud = m.id  
                INNER JOIN magnitud_intervalos mi1 ON rd.idintervalo1 = mi1.id  
                INNER JOIN marcas ma ON rd.idmarca = ma.id  
                INNER JOIN modelos mo ON rd.idmodelo = mo.id  
                INNER JOIN recepciones r ON rd.idrecepcion = r.id
                inner join magnitud_intervalos mi2 ON rd.idintervalo2 = mi2.id
                WHERE recepcion = {documento} order by ingreso";
            return await ExecuteQueryAsync(query);
        }

        public async Task<DataTable> GetInspeccionEncabezado(int documento)
        {
            string query = $@"select c.nombrecompleto as cliente, c.documento,'NR-' || to_char(r.fecha, 'YY-') || trim(to_char(r.recepcion, '0000')) as recepcion, 
	                'IR-' || to_char(i.fecha, 'YY-') || trim(to_char(i.inspeccion, '0000')) as inspeccion,
	                to_char(r.fecha, 'DD-MM-YYYY') as fecha_recepcion, to_char(i.fecha, 'DD-MM-YYYY') as fecha_inspeccion,
	                (select count(*) from recepcion_detalle rd2 where rd2.idrecepcion = r.id) as ingresos,
	                (select count(*) from material_detalle md where md.idrecepcion = r.id) as materiales,
	                (select count(*) from recepcion_detalle rd3 where rd3.idrecepcion = r.id) + (select count(*) from material_detalle md2 where md2.idrecepcion = r.id) as recibidos,
	                sum(case when id.idingreso > 0 then 1 else 0 end + case when id.idmaterial > 0 then 1 else 0 end) as inspeccionados,
	                sum(case when id.estado = true then 1 else 0 end) as aprobados,
	                sum(case when id.estado = false then 1 else 0 end) as rechazados,
	                i.observacion, ru.nombrecompleto as usuario
	                from inspecciones i
	                inner join inspeccion_detalle id on id.idinspeccion = i.id 
                inner join recepcion_detalle rd on rd.id = id.idingreso 
                inner join recepciones r on r.id = rd.idrecepcion
                inner join clientes c on c.id = r.idcliente
                inner join seguridad.rbac_usuarios ru on ru.id = i.idusuario
                WHERE i.inspeccion = {documento}
                group by c.nombrecompleto, c.documento, r.fecha, r.recepcion, i.fecha, i.inspeccion, r.id, id.idmaterial, i.observacion, ru.nombrecompleto";
            return await ExecuteQueryAsync(query);
        }

        public async Task<DataTable> GetInspeccionDetalle(int documento)
        {
            string query = $@"SELECT rd.id, renglon,
                   'NR-' || to_char(rd.fechaing, 'YY-') || to_char(r.recepcion, '0000') || '-' || rd.renglon AS ingreso,
                   '<b>EQUIPO:</b> ' || e.descripcion || '; <b>MAGNITUD:</b> ' || m.descripcion ||
                   '; <b>MARCA:</b> ' || ma.descripcion || '; <b>MODELO:</b> ' || mo.descripcion || CASE WHEN rd.codigo not in ('', 'Sin Información') THEN ' <b>SERIE:</b> ' || rd.serie || ' <b>CÓDIGO:</b> ' || rd.codigo ELSE ' <b>SERIE:</b> ' || rd.serie end ||
                   CASE WHEN rd.idintervalo1 > 0 then '; <b>RANGO:</b> (' || mi1.desde || ' a ' || mi1.hasta || ') ' || mi1.medida else '; <b>RANGO:</b> VER ESPECIFICACIONES' end ||
                   CASE WHEN rd.idintervalo2 > 0 then '; <b>RANGO 2:</b> (' || mi2.desde || ' a ' || mi2.hasta || ') ' || mi2.medida else '' end ||
                   CASE WHEN rd.accesorios != '' THEN '; <b>OBSERVACIÓN:</b> ' || rd.observacion || '; <b>ACCESORIOS:</b> ' || rd.accesorios ELSE '; <b>OBSERVACIÓN:</b> ' || rd.observacion end || '; <b>SERVICIO:</b> ' || rd.tipo_servicio  as descripcion,
                   id.piezas, id.funcionalidades, id.acabado
                FROM recepcion_detalle rd  
                INNER JOIN equipos e ON rd.idequipo = e.id  
                INNER JOIN magnitudes m ON rd.idmagnitud = m.id  
                INNER JOIN magnitud_intervalos mi1 ON rd.idintervalo1 = mi1.id  
                INNER JOIN marcas ma ON rd.idmarca = ma.id  
                INNER JOIN modelos mo ON rd.idmodelo = mo.id  
                INNER JOIN recepciones r ON rd.idrecepcion = r.id
                inner join magnitud_intervalos mi2 ON rd.idintervalo2 = mi2.id
                inner join inspeccion_detalle id on id.idingreso = rd.id
                inner join inspecciones i on i.id = id.idinspeccion 
                WHERE i.inspeccion = {documento} order by rd.renglon";
            return await ExecuteQueryAsync(query);
        }

        public async Task<DataTable> GetOrdenTrabajoEncabezado(int documento)
        {
            string query = $@"select r.id, c.nombrecompleto as cliente, o.observacion,
                'ODT-'|| trim(to_char(o.fecha, 'YY-')) || trim(to_char(o.orden,'0000')) as orden, to_char(o.fecha, 'DD-MM-YYYY') as fecha,
                'NR-' || trim(to_char(r.fecha, 'YY-')) || trim(to_char(r.recepcion, '0000')) as recepcion,
                'IR-' || trim(to_char(i.fecha, 'YY-')) || trim(to_char(i.inspeccion, '0000')) as inspeccion,
                to_char(r.fecha, 'DD-MM-YYYY') as fecharecepcion,
                to_char(i.fecha, 'DD-MM-YYYY') as fechainspeccion,
                string_agg(distinct m.abreviatura, ', ') as magnitud,
                ru.nombrecompleto as usuario
                from ordentrabajo o 
                inner join ordentrabajo_detalle od on o.id = od.idorden 
                inner join recepcion_detalle rd on rd.id = od.idingreso 
                inner join inspeccion_detalle id on rd.id = id.idingreso 
                inner join inspecciones i on i.id = id.idinspeccion 
                inner join recepciones r on r.id = rd.idrecepcion 
                inner join clientes c on c.id = r.idcliente 
                inner join magnitudes m on m.id = rd.idmagnitud 
                inner join seguridad.rbac_usuarios ru on ru.id = o.idusuario 
                WHERE o.orden = {documento}
                group by r.id, c.nombrecompleto, o.orden, o.fecha, r.recepcion, r.fecha, i.inspeccion, i.fecha, ru.nombrecompleto, o.observacion";
            return await ExecuteQueryAsync(query);
        }

        public async Task<DataTable> GetOrdenTrabajoDetalle(int documento)
        {
            string query = $@"SELECT rd.id, renglon,
                   'NR-' || to_char(rd.fechaing, 'YY-') || trim(to_char(r.recepcion, '0000')) || '-' || rd.renglon AS ingreso,
                   '<b>EQUIPO:</b> ' || e.descripcion || '; <b>MAGNITUD:</b> ' || m.descripcion ||
                   '; <b>MARCA:</b> ' || ma.descripcion || '; <b>MODELO:</b> ' || mo.descripcion || CASE WHEN rd.codigo not in ('', 'Sin Información') THEN ' <b>SERIE:</b> ' || rd.serie || ' <b>CÓDIGO:</b> ' || rd.codigo ELSE ' <b>SERIE:</b> ' || rd.serie end ||
                   CASE WHEN rd.idintervalo1 > 0 then '; <b>RANGO:</b> (' || mi1.desde || ' a ' || mi1.hasta || ') ' || mi1.medida else '; <b>RANGO:</b> VER ESPECIFICACIONES' end ||
                   CASE WHEN rd.idintervalo2 > 0 then '; <b>RANGO 2:</b> (' || mi2.desde || ' a ' || mi2.hasta || ') ' || mi2.medida else '' end ||
                   CASE WHEN rd.accesorios != '' THEN '; <b>OBSERVACIÓN:</b> ' || rd.observacion || '; <b>ACCESORIOS:</b> ' || rd.accesorios ELSE '; <b>OBSERVACIÓN:</b> ' || rd.observacion end || '; <b>SERVICIO:</b> ' || rd.tipo_servicio  as descripcion
                FROM recepcion_detalle rd  
                INNER JOIN equipos e ON rd.idequipo = e.id  
                INNER JOIN magnitudes m ON rd.idmagnitud = m.id  
                INNER JOIN magnitud_intervalos mi1 ON rd.idintervalo1 = mi1.id  
                INNER JOIN marcas ma ON rd.idmarca = ma.id  
                INNER JOIN modelos mo ON rd.idmodelo = mo.id  
                INNER JOIN recepciones r ON rd.idrecepcion = r.id
                inner join magnitud_intervalos mi2 ON rd.idintervalo2 = mi2.id
                inner join ordentrabajo_detalle od on od.idingreso = rd.id
                inner join ordentrabajo o on o.id = od.idorden 
                where o.orden = {documento} order by rd.id";
            return await ExecuteQueryAsync(query);
        }
    }


}
