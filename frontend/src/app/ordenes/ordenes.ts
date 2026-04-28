import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../core/api.service';

interface Cliente {
  id_cliente: number;
  nombre_cliente: string;
  apellido_cliente: string;
}

interface Vehiculo {
  id_vehiculo: number;
  id_cliente: number;
  placa_vehiculo: string;
  nombre_marca_vehiculo?: string;
  nombre_modelo_vehiculo?: string;
}

interface Tecnico {
  id_tecnico: number;
  nombre_empleado: string;
  apellido_empleado: string;
  cargo_empleado?: string;
}

interface Orden {
  id_orden_servicio: number;
  id_cliente: number;
  id_vehiculo: number;
  id_tecnico: number;
  estado: 'Pendiente' | 'En proceso' | 'Completado';
  fecha_inicio: string;
  fecha_fin: string;
  total: number;
  observaciones: string;

  nombre_cliente?: string;
  apellido_cliente?: string;
  placa_vehiculo?: string;
  nombre_tecnico?: string;
  apellido_tecnico?: string;
}

interface OrdenForm {
  id_cliente: number | null;
  id_vehiculo: number | null;
  id_tecnico: number | null;
  estado: 'Pendiente' | 'En proceso' | 'Completado';
  fecha_inicio: string;
  fecha_fin: string;
  total: number;
  observaciones: string;
}

@Component({
  selector: 'app-ordenes',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './ordenes.html',
  styleUrl: './ordenes.css'
})
export class OrdenesComponent implements OnInit {

  ordenes: Orden[] = [];
  clientes: Cliente[] = [];
  vehiculos: Vehiculo[] = [];
  tecnicos: Tecnico[] = [];

  cargando = false;
  error: string | null = null;

  form: OrdenForm = this.defaultForm();
  editingId: number | null = null;

  filtroEstado = '';
  filtroTexto = '';

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.cargar();
    this.api.getClientes().subscribe({
      next: (d) => this.clientes = d ?? [],
      error: () => {}
    });
    this.api.getVehiculos().subscribe({
      next: (d) => this.vehiculos = d ?? [],
      error: () => {}
    });
    this.api.getTecnicos().subscribe({
      next: (d) => this.tecnicos = d ?? [],
      error: () => {}
    });
  }

  cargar(): void {
    this.cargando = true;
    this.error = null;

    this.api.getOrdenes().subscribe({
      next: (d) => {
        this.ordenes = d ?? [];
        this.cargando = false;
      },
      error: () => {
        this.error = 'Error al cargar órdenes.';
        this.cargando = false;
      }
    });
  }

  get vehiculosFiltrados(): Vehiculo[] {
    if (!this.form.id_cliente) return this.vehiculos;
    return this.vehiculos.filter(v => v.id_cliente === Number(this.form.id_cliente));
  }

  get ordenesFiltradas(): Orden[] {
    return this.ordenes.filter(o => {
      const coincideEstado = !this.filtroEstado || o.estado === this.filtroEstado;
      const t = this.filtroTexto.trim().toLowerCase();

      const coincideTexto = !t ||
        (o.nombre_cliente ?? '').toLowerCase().includes(t) ||
        (o.apellido_cliente ?? '').toLowerCase().includes(t) ||
        (o.placa_vehiculo ?? '').toLowerCase().includes(t) ||
        (o.nombre_tecnico ?? '').toLowerCase().includes(t) ||
        (o.apellido_tecnico ?? '').toLowerCase().includes(t) ||
        String(o.id_orden_servicio).includes(t);

      return coincideEstado && coincideTexto;
    });
  }

  onClienteChange(): void {
    if (!this.form.id_cliente) {
      this.form.id_vehiculo = null;
      return;
    }

    const vehiculoSeleccionado = this.vehiculos.find(v => v.id_vehiculo === this.form.id_vehiculo);
    if (vehiculoSeleccionado && vehiculoSeleccionado.id_cliente !== Number(this.form.id_cliente)) {
      this.form.id_vehiculo = null;
    }
  }

  getNombreCliente(id: number): string {
    const c = this.clientes.find(x => x.id_cliente === id);
    return c ? `${c.nombre_cliente} ${c.apellido_cliente}` : `#${id}`;
  }

  getPlacaVehiculo(id: number): string {
    const v = this.vehiculos.find(x => x.id_vehiculo === id);
    return v ? v.placa_vehiculo : `#${id}`;
  }

  getNombreTecnico(id: number): string {
    const t = this.tecnicos.find(x => x.id_tecnico === id);
    return t ? `${t.nombre_empleado} ${t.apellido_empleado}` : `#${id}`;
  }

  guardar(): void {
    if (!this.form.id_cliente) {
      alert('Selecciona un cliente.');
      return;
    }

    if (!this.form.id_vehiculo) {
      alert('Selecciona un vehículo.');
      return;
    }

    if (!this.form.id_tecnico) {
      alert('Selecciona un técnico.');
      return;
    }

    if (!this.form.fecha_inicio) {
      alert('Ingresa la fecha de inicio.');
      return;
    }

    if (this.form.fecha_fin && this.form.fecha_fin < this.form.fecha_inicio) {
      alert('La fecha fin no puede ser menor que la fecha inicio.');
      return;
    }

    if ((this.form.total ?? 0) < 0) {
      alert('El total no puede ser negativo.');
      return;
    }

    const vehiculo = this.vehiculos.find(v => v.id_vehiculo === Number(this.form.id_vehiculo));
    if (!vehiculo || vehiculo.id_cliente !== Number(this.form.id_cliente)) {
      alert('El vehículo seleccionado no pertenece al cliente.');
      return;
    }

    if (this.editingId === null) {
      this.api.createOrden(this.form).subscribe({
        next: () => {
          this.cargar();
          this.resetForm();
        },
        error: () => alert('Error al crear orden.')
      });
    } else {
      this.api.updateOrden(this.editingId, this.form).subscribe({
        next: () => {
          this.cargar();
          this.resetForm();
        },
        error: () => alert('Error al actualizar orden.')
      });
    }
  }

  editar(o: Orden): void {
    this.form = {
      id_cliente: o.id_cliente,
      id_vehiculo: o.id_vehiculo,
      id_tecnico: o.id_tecnico,
      estado: o.estado ?? 'Pendiente',
      fecha_inicio: o.fecha_inicio ?? '',
      fecha_fin: o.fecha_fin ?? '',
      total: o.total ?? 0,
      observaciones: o.observaciones ?? ''
    };

    this.editingId = o.id_orden_servicio;
  }

  eliminar(id: number): void {
    if (!confirm('¿Eliminar esta orden?')) return;

    this.api.deleteOrden(id).subscribe({
      next: () => this.cargar(),
      error: () => alert('Error al eliminar orden.')
    });
  }

  cancelar(): void {
    this.resetForm();
  }

  private defaultForm(): OrdenForm {
    return {
      id_cliente: null,
      id_vehiculo: null,
      id_tecnico: null,
      estado: 'Pendiente',
      fecha_inicio: '',
      fecha_fin: '',
      total: 0,
      observaciones: ''
    };
  }

  private resetForm(): void {
    this.form = this.defaultForm();
    this.editingId = null;
  }
}