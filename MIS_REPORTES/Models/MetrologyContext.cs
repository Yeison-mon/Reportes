using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MIS_REPORTES.Models;

public partial class MetrologyContext : DbContext
{
    public MetrologyContext(DbContextOptions<MetrologyContext> options)
            : base(options)
    {
    }


    public virtual DbSet<Accesorios> Accesorios { get; set; }

    public virtual DbSet<ArchivosLaboratorio> ArchivosLaboratorios { get; set; }

    public virtual DbSet<Ciudad> Ciudads { get; set; }

    public virtual DbSet<Clientes> Clientes { get; set; }

    public virtual DbSet<ContactosCliente> ContactosClientes { get; set; }

    public virtual DbSet<Contador> Contadors { get; set; }

    public virtual DbSet<CotizacionAprobacion> CotizacionAprobacions { get; set; }

    public virtual DbSet<CotizacionDetalle> CotizacionDetalles { get; set; }

    public virtual DbSet<Cotizaciones> Cotizaciones { get; set; }

    public virtual DbSet<Departamentos> Departamentos { get; set; }

    public virtual DbSet<Equipos> Equipos { get; set; }

    public virtual DbSet<FormasLlegada> FormasLlegada { get; set; }

    public virtual DbSet<FotosLaboratorio> FotosLaboratorios { get; set; }

    public virtual DbSet<InspeccionDetalle> InspeccionDetalles { get; set; }

    public virtual DbSet<Inspecciones> Inspecciones { get; set; }

    public virtual DbSet<Iva> Ivas { get; set; }

    public virtual DbSet<MagnitudIntervalo> MagnitudIntervalos { get; set; }

    public virtual DbSet<Magnitudes> Magnitudes { get; set; }

    public virtual DbSet<Marcas> Marcas { get; set; }

    public virtual DbSet<MaterialDetalle> MaterialDetalles { get; set; }

    public virtual DbSet<Medidas> Medidas { get; set; }

    public virtual DbSet<Modelos> Modelos { get; set; }

    public virtual DbSet<Observaciones> Observaciones { get; set; }

    public virtual DbSet<Ordentrabajo> Ordentrabajos { get; set; }

    public virtual DbSet<OrdentrabajoDetalle> OrdentrabajoDetalles { get; set; }

    public virtual DbSet<Pais> Pais { get; set; }

    public virtual DbSet<ProcesofinalDetalles> ProcesofinalDetalles { get; set; }

    public virtual DbSet<Procesosfinales> Procesosfinales { get; set; }

    public virtual DbSet<RbacUsuarios> RbacUsuarios { get; set; }

    public virtual DbSet<RecepcionDetalle> RecepcionDetalles { get; set; }

    public virtual DbSet<Recepciones> Recepciones { get; set; }

    public virtual DbSet<SedesCliente> SedesClientes { get; set; }

    public virtual DbSet<TipoAcreditacion> TipoAcreditacions { get; set; }

    public virtual DbSet<TipoIndicacion> TipoIndicacions { get; set; }

    public virtual DbSet<TipoMoneda> TipoMoneda { get; set; }

    public virtual DbSet<TipoServicio> TipoServicios { get; set; }

    public virtual DbSet<Ubicaciones> Ubicaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accesorios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("accesorios_id_primarykey");

            entity.ToTable("accesorios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasDefaultValueSql("0")
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<ArchivosLaboratorio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("archivos_laboratorio_pkey");

            entity.ToTable("archivos_laboratorio", "archivos");

            entity.HasIndex(e => e.Nrocontrol, "archivos_laboratorio_nrocontrol_idx");

            entity.HasIndex(e => e.Tipo, "archivos_laboratorio_tipo_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Base64).HasColumnName("base64");
            entity.Property(e => e.Documento)
                .HasDefaultValue(0)
                .HasColumnName("documento");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");
            entity.Property(e => e.Nroarchivo)
                .HasDefaultValue(0)
                .HasColumnName("nroarchivo");
            entity.Property(e => e.Nrocontrol)
                .HasDefaultValue(0)
                .HasColumnName("nrocontrol");
            entity.Property(e => e.Tipo)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ciudad_pkey");

            entity.ToTable("ciudad");

            entity.HasIndex(e => e.Id, "ciudad_id_idx");

            entity.HasIndex(e => new { e.Iddepartamento, e.Descripcion }, "ciudad_iddepartamento_descripcion_key").IsUnique();

            entity.HasIndex(e => e.Iddepartamento, "ciudad_iddepartamento_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasDefaultValueSql("0")
                .HasColumnType("character varying")
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Iddepartamento).HasColumnName("iddepartamento");

            entity.HasOne(d => d.IddepartamentoNavigation).WithMany(p => p.Ciudads)
                .HasForeignKey(d => d.Iddepartamento)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("ciudad_iddepartamento_fkey");
        });

        modelBuilder.Entity<Clientes>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("clientes");

            entity.HasIndex(e => e.Documento, "clientes_documento_unique").IsUnique();

            entity.HasIndex(e => e.Nombrecompleto, "clientes_nombrecompleto_unique").IsUnique();

            entity.HasIndex(e => new { e.Documento, e.Nombrecompleto }, "unique_documento_nombrecompleto").IsUnique();

            entity.Property(e => e.Celular)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("celular");
            entity.Property(e => e.Convenio)
                .HasDefaultValue(0)
                .HasColumnName("convenio");
            entity.Property(e => e.CorreoFactura)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("correo_factura");
            entity.Property(e => e.CorreoPrincipal)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("correo_principal");
            entity.Property(e => e.Direccion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("direccion");
            entity.Property(e => e.Documento)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("documento");
            entity.Property(e => e.Estado)
                .HasDefaultValue(1)
                .HasColumnName("estado");
            entity.Property(e => e.Habitual)
                .HasDefaultValue(0)
                .HasColumnName("habitual");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Idciudad)
                .HasDefaultValue(0)
                .HasColumnName("idciudad");
            entity.Property(e => e.Nombrecompleto)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("nombrecompleto");
            entity.Property(e => e.Telefono)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<ContactosCliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contactos_cliente_id_primarykey");

            entity.ToTable("contactos_cliente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cargo)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("cargo");
            entity.Property(e => e.Correo)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("correo");
            entity.Property(e => e.Idcliente)
                .HasDefaultValue(0)
                .HasColumnName("idcliente");
            entity.Property(e => e.Idsede)
                .HasDefaultValue(0)
                .HasColumnName("idsede");
            entity.Property(e => e.Nombre)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("nombre");
            entity.Property(e => e.Telefonos)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("telefonos");
        });

        modelBuilder.Entity<Contador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contador_id_primarykey");

            entity.ToTable("contador");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cotizacion)
                .HasDefaultValue(0)
                .HasColumnName("cotizacion");
            entity.Property(e => e.Ingreso)
                .HasDefaultValue(0)
                .HasColumnName("ingreso");
            entity.Property(e => e.Inspeccion)
                .HasDefaultValue(0)
                .HasColumnName("inspeccion");
            entity.Property(e => e.Ordentrabajo)
                .HasDefaultValue(0)
                .HasColumnName("ordentrabajo");
            entity.Property(e => e.Recepcion)
                .HasDefaultValue(0)
                .HasColumnName("recepcion");
        });

        modelBuilder.Entity<CotizacionAprobacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cotizacion_aprobacion_pkey");

            entity.ToTable("cotizacion_aprobacion");

            entity.HasIndex(e => e.Id, "cotizacion_aprobacion_id_idx");

            entity.HasIndex(e => e.Idcotizacion, "cotizacion_aprobacion_idcotizacion_idx");

            entity.HasIndex(e => e.Idusuario, "cotizacion_aprobacion_idusuario_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cargo)
                .HasMaxLength(200)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("cargo");
            entity.Property(e => e.Cedula)
                .HasMaxLength(200)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("cedula");
            entity.Property(e => e.Contacto)
                .HasMaxLength(200)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("contacto");
            entity.Property(e => e.Conversacion)
                .HasDefaultValueSql("''::text")
                .HasColumnName("conversacion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(0)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");
            entity.Property(e => e.Idcotizacion)
                .HasDefaultValue(0)
                .HasColumnName("idcotizacion");
            entity.Property(e => e.Idusuario)
                .HasDefaultValue(0)
                .HasColumnName("idusuario");
            entity.Property(e => e.Telefono)
                .HasMaxLength(200)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("telefono");
            entity.Property(e => e.Tiempo)
                .HasMaxLength(200)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("tiempo");
            entity.Property(e => e.Tipo)
                .HasDefaultValue(0)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<CotizacionDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cotizacion_detalle_pkey");

            entity.ToTable("cotizacion_detalle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Codigo)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("codigo");
            entity.Property(e => e.Descuento).HasColumnName("descuento");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'Temporal'::character varying")
                .HasColumnType("character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha");
            entity.Property(e => e.Idcliente)
                .HasDefaultValue(0)
                .HasColumnName("idcliente");
            entity.Property(e => e.Idcotizacion)
                .HasDefaultValue(0)
                .HasColumnName("idcotizacion");
            entity.Property(e => e.Idequipo)
                .HasDefaultValue(0)
                .HasColumnName("idequipo");
            entity.Property(e => e.Idingreso)
                .HasDefaultValue(0)
                .HasColumnName("idingreso");
            entity.Property(e => e.Idintervalo1)
                .HasDefaultValue(0)
                .HasColumnName("idintervalo1");
            entity.Property(e => e.Idintervalo2)
                .HasDefaultValue(0)
                .HasColumnName("idintervalo2");
            entity.Property(e => e.Idmodelo)
                .HasDefaultValue(0)
                .HasColumnName("idmodelo");
            entity.Property(e => e.Idmoneda)
                .HasDefaultValue(1)
                .HasColumnName("idmoneda");
            entity.Property(e => e.Idservicio)
                .HasDefaultValue(0)
                .HasColumnName("idservicio");
            entity.Property(e => e.Idsubservicio)
                .HasDefaultValue(0)
                .HasColumnName("idsubservicio");
            entity.Property(e => e.Iva).HasColumnName("iva");
            entity.Property(e => e.Observacion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("observacion");
            entity.Property(e => e.Precio)
                .HasPrecision(20, 2)
                .HasColumnName("precio");
            entity.Property(e => e.Serie)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("serie");
        });

        modelBuilder.Entity<Cotizaciones>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cotizaciones_pkey");

            entity.ToTable("cotizaciones");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Anio)
                .HasColumnType("character varying")
                .HasColumnName("anio");
            entity.Property(e => e.Aprobada)
                .HasDefaultValue(0)
                .HasColumnName("aprobada");
            entity.Property(e => e.Correoenviado)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("correoenviado");
            entity.Property(e => e.Cotizacion)
                .HasDefaultValue(0)
                .HasColumnName("cotizacion");
            entity.Property(e => e.Descuento)
                .HasPrecision(20, 2)
                .HasColumnName("descuento");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'Temporal'::character varying")
                .HasColumnType("character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Exento)
                .HasPrecision(20, 2)
                .HasColumnName("exento");
            entity.Property(e => e.Fecenviada).HasColumnName("fecenviada");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha");
            entity.Property(e => e.Fechaanula).HasColumnName("fechaanula");
            entity.Property(e => e.Fechaaprobacion).HasColumnName("fechaaprobacion");
            entity.Property(e => e.Fecharechazada).HasColumnName("fecharechazada");
            entity.Property(e => e.Gravable)
                .HasPrecision(20, 2)
                .HasColumnName("gravable");
            entity.Property(e => e.Idcliente)
                .HasDefaultValue(0)
                .HasColumnName("idcliente");
            entity.Property(e => e.Idcontacto)
                .HasDefaultValue(0)
                .HasColumnName("idcontacto");
            entity.Property(e => e.Idsede)
                .HasDefaultValue(0)
                .HasColumnName("idsede");
            entity.Property(e => e.Idusuario)
                .HasDefaultValue(0)
                .HasColumnName("idusuario");
            entity.Property(e => e.Idusuarioanula)
                .HasDefaultValue(0)
                .HasColumnName("idusuarioanula");
            entity.Property(e => e.Idusuenviada)
                .HasDefaultValue(0)
                .HasColumnName("idusuenviada");
            entity.Property(e => e.Iva)
                .HasPrecision(20, 2)
                .HasColumnName("iva");
            entity.Property(e => e.Obserenvio)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("obserenvio");
            entity.Property(e => e.Observacion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("observacion");
            entity.Property(e => e.Observacionanula)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("observacionanula");
            entity.Property(e => e.Rechazada)
                .HasDefaultValue(0)
                .HasColumnName("rechazada");
            entity.Property(e => e.Retefuente)
                .HasPrecision(20, 2)
                .HasColumnName("retefuente");
            entity.Property(e => e.Reteica)
                .HasPrecision(20, 2)
                .HasColumnName("reteica");
            entity.Property(e => e.Reteiva)
                .HasPrecision(20, 2)
                .HasColumnName("reteiva");
            entity.Property(e => e.Subtotal)
                .HasPrecision(20, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasPrecision(20, 2)
                .HasColumnName("total");
            entity.Property(e => e.Trm).HasColumnName("trm");
            entity.Property(e => e.Validez)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("validez");
        });

        modelBuilder.Entity<Departamentos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("departamento_pkey");

            entity.ToTable("departamento");

            entity.HasIndex(e => e.Id, "departamento_id_idx");

            entity.HasIndex(e => new { e.Idpais, e.Descripcion }, "departamento_idpais_descripcion_key").IsUnique();

            entity.HasIndex(e => e.Idpais, "departamento_idpais_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.Idpais)
                .HasDefaultValue(0)
                .HasColumnName("idpais");

            entity.HasOne(d => d.IdpaisNavigation).WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.Idpais)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("departamento_idpais_fkey");
        });

        modelBuilder.Entity<Equipos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("equipo_pkey");

            entity.ToTable("equipos");

            entity.HasIndex(e => new { e.Descripcion, e.Idmagnitud }, "descripcion_idmagnitud_unique").IsUnique();

            entity.HasIndex(e => new { e.Descripcion, e.Idmagnitud }, "equipo_descripcion_idmagnitud_key").IsUnique();

            entity.HasIndex(e => e.Id, "equipo_id_idx");

            entity.HasIndex(e => e.Idmagnitud, "equipo_idmagnitud_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(1)
                .HasColumnName("estado");
            entity.Property(e => e.Idmagnitud)
                .HasDefaultValue(0)
                .HasColumnName("idmagnitud");
        });

        modelBuilder.Entity<FormasLlegada>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("formas_llegada_pkey");

            entity.ToTable("formas_llegada");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<FotosLaboratorio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("fotos_laboratorio_pkey");

            entity.ToTable("fotos_laboratorio", "archivos");

            entity.HasIndex(e => e.Nrocontrol, "fotos_laboratorio_nrocontrol_idx");

            entity.HasIndex(e => e.Tipo, "fotos_laboratorio_tipo_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Base64).HasColumnName("base64");
            entity.Property(e => e.Comprimido).HasColumnName("comprimido");
            entity.Property(e => e.Documento)
                .HasDefaultValue(0)
                .HasColumnName("documento");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");
            entity.Property(e => e.Nrocontrol)
                .HasDefaultValue(0)
                .HasColumnName("nrocontrol");
            entity.Property(e => e.Nrofoto)
                .HasDefaultValue(0)
                .HasColumnName("nrofoto");
            entity.Property(e => e.Tipo)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<InspeccionDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("inspeccion_detalle_pkey");

            entity.ToTable("inspeccion_detalle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Acabado)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("acabado");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Funcionalidades)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("funcionalidades");
            entity.Property(e => e.Idingreso)
                .HasDefaultValue(0)
                .HasColumnName("idingreso");
            entity.Property(e => e.Idinspeccion)
                .HasDefaultValue(0)
                .HasColumnName("idinspeccion");
            entity.Property(e => e.Idmaterial)
                .HasDefaultValue(0)
                .HasColumnName("idmaterial");
            entity.Property(e => e.Observacion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("observacion");
            entity.Property(e => e.Piezas)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("piezas");
        });

        modelBuilder.Entity<Inspecciones>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("inspecciones_pkey");

            entity.ToTable("inspecciones");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'Registrado'::character varying")
                .HasColumnType("character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha");
            entity.Property(e => e.Idusuario)
                .HasDefaultValue(0)
                .HasColumnName("idusuario");
            entity.Property(e => e.Inspeccion)
                .HasDefaultValue(0)
                .HasColumnName("inspeccion");
            entity.Property(e => e.Observacion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("observacion");
        });

        modelBuilder.Entity<Iva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("iva_pkey");

            entity.ToTable("iva");

            entity.HasIndex(e => e.Descripcion, "iva_descripcion_key").IsUnique();

            entity.HasIndex(e => e.Valor, "iva_valor_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(0)
                .HasColumnName("estado");
            entity.Property(e => e.Valor).HasColumnName("valor");
        });

        modelBuilder.Entity<MagnitudIntervalo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("magnitud_intervalos_pkey");

            entity.ToTable("magnitud_intervalos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Desde)
                .HasMaxLength(25)
                .HasColumnName("desde");
            entity.Property(e => e.Estado)
                .HasDefaultValue(1)
                .HasColumnName("estado");
            entity.Property(e => e.Hasta)
                .HasMaxLength(25)
                .HasColumnName("hasta");
            entity.Property(e => e.Idmagnitud).HasColumnName("idmagnitud");
            entity.Property(e => e.Medida)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("medida");
        });

        modelBuilder.Entity<Magnitudes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("magnitudes_id_primarykey");

            entity.ToTable("magnitudes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Abreviatura)
                .HasMaxLength(10)
                .HasColumnName("abreviatura");
            entity.Property(e => e.Color)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("color");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.DesdeAcreditado).HasColumnName("desde_acreditado");
            entity.Property(e => e.Estado)
                .HasDefaultValue(1)
                .HasColumnName("estado");
            entity.Property(e => e.Graficar)
                .HasDefaultValue(1)
                .HasColumnName("graficar");
            entity.Property(e => e.HastaAcreditado).HasColumnName("hasta_acreditado");
            entity.Property(e => e.Meta).HasColumnName("meta");
            entity.Property(e => e.MetaEquipos).HasColumnName("meta_equipos");
        });

        modelBuilder.Entity<Marcas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("marcas_pkey");

            entity.ToTable("marcas");

            entity.HasIndex(e => e.Descripcion, "descripcion_unique").IsUnique();

            entity.HasIndex(e => e.Descripcion, "marcas_descripcion_key").IsUnique();

            entity.HasIndex(e => e.Id, "marcas_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(1)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<MaterialDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("material_detalle_pkey");

            entity.ToTable("material_detalle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
            entity.Property(e => e.Fechaing)
                .HasDefaultValueSql("now()")
                .HasColumnName("fechaing");
            entity.Property(e => e.Idcliente)
                .HasDefaultValue(0)
                .HasColumnName("idcliente");
            entity.Property(e => e.Idrecepcion)
                .HasDefaultValue(0)
                .HasColumnName("idrecepcion");
            entity.Property(e => e.Ingreso)
                .HasDefaultValue(0)
                .HasColumnName("ingreso");
            entity.Property(e => e.Serie)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("serie");
        });

        modelBuilder.Entity<Medidas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("medidas_id_primarykey");

            entity.ToTable("medidas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(1)
                .HasColumnName("estado");
            entity.Property(e => e.Idmagnitud)
                .HasDefaultValue(0)
                .HasColumnName("idmagnitud");
        });

        modelBuilder.Entity<Modelos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("modelos_pkey");

            entity.ToTable("modelos");

            entity.HasIndex(e => new { e.Descripcion, e.Idmarca }, "descripcion_idmarca_unique").IsUnique();

            entity.HasIndex(e => e.Id, "modelos_id_idx");

            entity.HasIndex(e => new { e.Idmarca, e.Descripcion }, "modelos_idmarca_descripcion_key").IsUnique();

            entity.HasIndex(e => e.Idmarca, "modelos_idmarca_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(1)
                .HasColumnName("estado");
            entity.Property(e => e.Idmarca).HasColumnName("idmarca");
        });

        modelBuilder.Entity<Observaciones>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("observaciones_id_primarykey");

            entity.ToTable("observaciones");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasDefaultValueSql("0")
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Ordentrabajo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ordentrabajo_pkey");

            entity.ToTable("ordentrabajo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'Registrado'::character varying")
                .HasColumnType("character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha");
            entity.Property(e => e.Idusuario)
                .HasDefaultValue(0)
                .HasColumnName("idusuario");
            entity.Property(e => e.Idusuorden)
                .HasDefaultValue(0)
                .HasColumnName("idusuorden");
            entity.Property(e => e.Observacion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("observacion");
            entity.Property(e => e.Orden)
                .HasDefaultValue(0)
                .HasColumnName("orden");
        });

        modelBuilder.Entity<OrdentrabajoDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ordentrabajo_detalle_pkey");

            entity.ToTable("ordentrabajo_detalle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idingreso)
                .HasDefaultValue(0)
                .HasColumnName("idingreso");
            entity.Property(e => e.Idorden)
                .HasDefaultValue(0)
                .HasColumnName("idorden");
            entity.Property(e => e.Observacion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("observacion");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pais_pkey");

            entity.ToTable("pais");

            entity.HasIndex(e => e.Descripcion, "pais_descripcion_key").IsUnique();

            entity.HasIndex(e => e.Id, "pais_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.Inicial)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("inicial");
        });

        modelBuilder.Entity<ProcesofinalDetalles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("procesosfinales_detalle_pkey");

            entity.ToTable("procesofinal_detalle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Certificado)
                .HasDefaultValue(0)
                .HasColumnName("certificado");
            entity.Property(e => e.Componentes)
                .HasDefaultValueSql("''::text")
                .HasColumnName("componentes");
            entity.Property(e => e.Desarme)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("desarme");
            entity.Property(e => e.Ensamblaje)
                .HasDefaultValueSql("''::text")
                .HasColumnName("ensamblaje");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha");
            entity.Property(e => e.FechaCertificado)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("fecha_certificado");
            entity.Property(e => e.Idacreditado)
                .HasDefaultValue(0)
                .HasColumnName("idacreditado");
            entity.Property(e => e.Idingreso)
                .HasDefaultValue(0)
                .HasColumnName("idingreso");
            entity.Property(e => e.Idprocesofinal)
                .HasDefaultValue(0)
                .HasColumnName("idprocesofinal");
            entity.Property(e => e.Lavado)
                .HasDefaultValueSql("''::text")
                .HasColumnName("lavado");
            entity.Property(e => e.Observacion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("observacion");
        });

        modelBuilder.Entity<Procesosfinales>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("procesosfinales_pkey");

            entity.ToTable("procesosfinales");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'Registrado'::character varying")
                .HasColumnType("character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha");
            entity.Property(e => e.Idusuario)
                .HasDefaultValue(0)
                .HasColumnName("idusuario");
            entity.Property(e => e.Observacion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("observacion");
            entity.Property(e => e.Procesofinal)
                .HasDefaultValue(0)
                .HasColumnName("procesofinal");
        });

        modelBuilder.Entity<RbacUsuarios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rbac_usuarios_pkey");

            entity.ToTable("rbac_usuarios", "seguridad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cargo)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("cargo");
            entity.Property(e => e.Clave)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("clave");
            entity.Property(e => e.Email)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Ip)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("ip");
            entity.Property(e => e.Mac)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("mac");
            entity.Property(e => e.Nombrecompleto)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("nombrecompleto");
            entity.Property(e => e.Nomusu)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("nomusu");

        });

        modelBuilder.Entity<RecepcionDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recepcion_detalle_pkey");

            entity.ToTable("recepcion_detalle");

            entity.HasIndex(e => e.Id, "recepcion_detalle_id_idx");

            entity.HasIndex(e => e.Idequipo, "recepcion_detalle_idequipo_idx");

            entity.HasIndex(e => e.Idmarca, "recepcion_detalle_idmarca_idx");

            entity.HasIndex(e => e.Idmodelo, "recepcion_detalle_idmodelo_idx");

            entity.HasIndex(e => e.Idrecepcion, "recepcion_detalle_idrecepcion_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accesorios)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("accesorios");
            entity.Property(e => e.Cantidad)
                .HasDefaultValue(1)
                .HasColumnName("cantidad");
            entity.Property(e => e.Codigo)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("codigo");
            entity.Property(e => e.ConSerie)
                .HasDefaultValue(true)
                .HasColumnName("con_serie");
            entity.Property(e => e.Cotizado)
                .HasDefaultValue(0)
                .HasColumnName("cotizado");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'Temporal'::character varying")
                .HasColumnType("character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Fechaing)
                .HasDefaultValueSql("now()")
                .HasColumnName("fechaing");
            entity.Property(e => e.Fotos)
                .HasDefaultValue(0)
                .HasColumnName("fotos");
            entity.Property(e => e.Idcliente)
                .HasDefaultValue(0)
                .HasColumnName("idcliente");
            entity.Property(e => e.Idequipo)
                .HasDefaultValue(0)
                .HasColumnName("idequipo");
            entity.Property(e => e.Idintervalo1)
                .HasDefaultValue(0)
                .HasColumnName("idintervalo1");
            entity.Property(e => e.Idintervalo2)
                .HasDefaultValue(0)
                .HasColumnName("idintervalo2");
            entity.Property(e => e.Idmagnitud)
                .HasDefaultValue(0)
                .HasColumnName("idmagnitud");
            entity.Property(e => e.Idmarca)
                .HasDefaultValue(0)
                .HasColumnName("idmarca");
            entity.Property(e => e.Idmodelo)
                .HasDefaultValue(0)
                .HasColumnName("idmodelo");
            entity.Property(e => e.Idrecepcion)
                .HasDefaultValue(0)
                .HasColumnName("idrecepcion");
            entity.Property(e => e.Idtipoindicacion)
                .HasDefaultValue(0)
                .HasColumnName("idtipoindicacion");
            entity.Property(e => e.Idusuario)
                .HasDefaultValue(0)
                .HasColumnName("idusuario");
            entity.Property(e => e.Inactivo)
                .HasDefaultValue(0)
                .HasColumnName("inactivo");
            entity.Property(e => e.Ingreso).HasColumnName("ingreso");
            entity.Property(e => e.Material)
                .HasDefaultValue(false)
                .HasColumnName("material");
            entity.Property(e => e.Observacion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("observacion");
            entity.Property(e => e.Renglon)
                .HasDefaultValue(0)
                .HasColumnName("renglon");
            entity.Property(e => e.Serie)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("serie");
            entity.Property(e => e.TipoServicio)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("tipo_servicio");
            entity.Property(e => e.Ubicacion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("ubicacion");
        });

        modelBuilder.Entity<Recepciones>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recepcion_pkey");

            entity.ToTable("recepciones");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Anio)
                .HasColumnType("character varying")
                .HasColumnName("anio");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("'Temporal'::character varying")
                .HasColumnType("character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha");
            entity.Property(e => e.Idcliente)
                .HasDefaultValue(0)
                .HasColumnName("idcliente");
            entity.Property(e => e.Idcontacto)
                .HasDefaultValue(0)
                .HasColumnName("idcontacto");
            entity.Property(e => e.IdformaLlegada)
                .HasDefaultValue(0)
                .HasColumnName("idforma_llegada");
            entity.Property(e => e.Idsede)
                .HasDefaultValue(0)
                .HasColumnName("idsede");
            entity.Property(e => e.Idusuario)
                .HasDefaultValue(0)
                .HasColumnName("idusuario");
            entity.Property(e => e.Observacion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("observacion");
            entity.Property(e => e.Recepcion)
                .HasDefaultValue(0)
                .HasColumnName("recepcion");
            entity.Property(e => e.Talonario)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("talonario");
        });

        modelBuilder.Entity<SedesCliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sedes_cliente_id_primarykey");

            entity.ToTable("sedes_cliente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactoFactura)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("contacto_factura");
            entity.Property(e => e.CopiaCorreoFactura)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("copia_correo_factura");
            entity.Property(e => e.CorreoFactura)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("correo_factura");
            entity.Property(e => e.Direccion)
                .HasColumnType("character varying")
                .HasColumnName("direccion");
            entity.Property(e => e.Idciudad)
                .HasDefaultValue(0)
                .HasColumnName("idciudad");
            entity.Property(e => e.Idcliente)
                .HasDefaultValue(0)
                .HasColumnName("idcliente");
            entity.Property(e => e.Nombre)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("nombre");
            entity.Property(e => e.TelefonoFactura)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("telefono_factura");
        });

        modelBuilder.Entity<TipoAcreditacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipo_acreditacion_pkey");

            entity.ToTable("tipo_acreditacion");

            entity.HasIndex(e => e.Descripcion, "tipo_acreditacion_descripcion_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Acreditado)
                .HasMaxLength(2)
                .HasDefaultValueSql("'NO'::bpchar")
                .IsFixedLength()
                .HasColumnName("acreditado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Mantenimiento)
                .HasDefaultValue(0)
                .HasColumnName("mantenimiento");
        });

        modelBuilder.Entity<TipoIndicacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipo_indicacion_pkey");

            entity.ToTable("tipo_indicacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(1)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<TipoMoneda>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipo_moneda_pkey");

            entity.ToTable("tipo_moneda");

            entity.HasIndex(e => e.Descripcion, "tipo_moneda_descripcion_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(1)
                .HasColumnName("estado");
            entity.Property(e => e.Simbolo)
                .HasMaxLength(50)
                .HasColumnName("simbolo");
        });

        modelBuilder.Entity<TipoServicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tipo_servicio_pkey");

            entity.ToTable("tipo_servicio");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(1)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Ubicaciones>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ubicaciones_id_primarykey");

            entity.ToTable("ubicaciones");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasDefaultValueSql("''::character varying")
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
