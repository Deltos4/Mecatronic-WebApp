import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../core/api.service';

type TipoDetalle = 'servicio' | 'producto_taller' | 'mano_obra' | 'producto_cliente';
type TipoComprobante = 'BOLETA' | 'FACTURA' | '';

interface DetalleProforma {
  tipo: TipoDetalle;
  descripcion: string;
  cantidad: number;
  precio: number;
  subtotal: number;
  aplicaGarantia: boolean;
  observacion: string;
}

@Component({
  selector: 'app-proforma',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './proforma.html',
  styleUrls: ['./proforma.css']
})
export class ProformaComponent implements OnInit {

  clientes: any[] = [];
  vehiculos: any[] = [];
  serviciosCatalogo: any[] = [];
  productosCatalogo: any[] = [];

  cargando = false;

  clienteId: number | null = null;
  vehiculoId: number | null = null;
  fecha: string = this.getToday();
  observacionesGenerales = '';
  motivoIngreso = '';

  detalles: DetalleProforma[] = [];

  proformaAceptada = false;
  tipoComprobante: TipoComprobante = '';

  dniBoleta = '';
  razonSocial = '';
  ruc = '';
  direccionFiscal = '';

  metodoPago = '';
  metodosPago: any[] = [];

  historial: any[] = [];

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.cargarDatos();
  }

  cargarDatos(): void {
    this.cargando = true;

    this.api.getClientes().subscribe({ next: d => this.clientes = d ?? [] });
    this.api.getVehiculos().subscribe({ next: d => this.vehiculos = d ?? [] });
    this.api.getPagosMethods().subscribe({ next: d => this.metodosPago = d ?? [] });

    this.api.getProformas().subscribe({
      next: d => { this.historial = d ?? []; this.cargando = false; },
      error: () => { this.cargando = false; }
    });
  }

  getToday(): string {
    const now = new Date();
    return now.toISOString().split('T')[0];
  }

  get vehiculosFiltrados(): any[] {
    if (!this.clienteId) return this.vehiculos;
    return this.vehiculos.filter(v => v.id_cliente === Number(this.clienteId));
  }

  onClienteChange(): void {
    this.vehiculoId = null;

    const cli = this.clientes.find(c => c.id_cliente === Number(this.clienteId));

    if (cli?.numero_documento_cliente) {
      if (cli.numero_documento_cliente.length === 8) this.dniBoleta = cli.numero_documento_cliente;
      if (cli.numero_documento_cliente.length === 11) this.ruc = cli.numero_documento_cliente;
    }
  }

  agregarDetalle(tipo: TipoDetalle): void {
    this.detalles.push({
      tipo,
      descripcion: '',
      cantidad: 1,
      precio: 0,
      subtotal: 0,
      aplicaGarantia: tipo !== 'producto_cliente',
      observacion: tipo === 'producto_cliente'
        ? 'Producto traído por el cliente. No aplica garantía.'
        : ''
    });
  }

  recalcular(d: DetalleProforma): void {
    if (d.cantidad <= 0) d.cantidad = 1;

    if (d.tipo === 'producto_cliente') {
      d.precio = 0;
      d.subtotal = 0;
      d.aplicaGarantia = false;
      return;
    }

    d.subtotal = d.cantidad * d.precio;
  }

  eliminarDetalle(i: number): void {
    this.detalles.splice(i, 1);
  }

  get totalServicios(): number {
    return this.detalles
      .filter(d => d.tipo === 'servicio')
      .reduce((s, d) => s + d.subtotal, 0);
  }

  get totalProductosTaller(): number {
    return this.detalles
      .filter(d => d.tipo === 'producto_taller')
      .reduce((s, d) => s + d.subtotal, 0);
  }

  get totalManoObra(): number {
    return this.detalles
      .filter(d => d.tipo === 'mano_obra')
      .reduce((s, d) => s + d.subtotal, 0);
  }

  get subtotalSinIGV(): number {
    return this.detalles.reduce((s, d) => s + d.subtotal, 0);
  }

  get igv(): number {
    return this.subtotalSinIGV * 0.18;
  }

  get totalGeneral(): number {
    return this.subtotalSinIGV + this.igv;
  }

  aceptarProforma(): void {
    if (!this.validarCabecera()) return;

    if (this.detalles.length === 0) {
      alert('Agrega al menos un detalle.');
      return;
    }

    const ok = this.detalles.every(d =>
      d.descripcion.trim().length > 0 &&
      d.cantidad > 0 &&
      (d.tipo === 'producto_cliente' || d.precio >= 0)
    );

    if (!ok) {
      alert('Revisa los detalles.');
      return;
    }

    this.proformaAceptada = true;
  }

  generarComprobante(): void {
    if (!this.proformaAceptada) {
      alert('Primero acepta la proforma.');
      return;
    }

    if (!this.tipoComprobante) {
      alert('Selecciona tipo de comprobante.');
      return;
    }

    const vehiculo = this.vehiculos.find(v => v.id_vehiculo === Number(this.vehiculoId));

    if (!vehiculo || vehiculo.id_cliente !== Number(this.clienteId)) {
      alert('El vehículo no pertenece al cliente.');
      return;
    }

    if (!this.metodoPago) {
      alert('Selecciona método de pago.');
      return;
    }

    // Validaciones comprobante
    if (this.tipoComprobante === 'BOLETA' && !/^\d{8}$/.test(this.dniBoleta)) {
      alert('DNI inválido.');
      return;
    }

    if (this.tipoComprobante === 'FACTURA' && !/^(10|20)\d{9}$/.test(this.ruc)) {
      alert('RUC inválido.');
      return;
    }

    alert('Comprobante generado correctamente (simulado)');
    this.limpiarFormulario();
  }

  validarCabecera(): boolean {
    if (!this.clienteId) return alert('Selecciona cliente.'), false;
    if (!this.vehiculoId) return alert('Selecciona vehículo.'), false;
    if (!this.fecha) return alert('Selecciona fecha.'), false;
    return true;
  }

  limpiarFormulario(): void {
    this.clienteId = null;
    this.vehiculoId = null;
    this.fecha = this.getToday();
    this.detalles = [];
    this.proformaAceptada = false;
    this.tipoComprobante = '';
    this.metodoPago = '';
  }

  getTipoLabel(tipo: TipoDetalle): string {
    return {
      servicio: 'Servicio',
      producto_taller: 'Producto taller',
      mano_obra: 'Mano de obra',
      producto_cliente: 'Producto cliente'
    }[tipo];
  }
}
