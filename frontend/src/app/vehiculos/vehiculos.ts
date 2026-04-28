import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../core/api.service';

interface Vehiculo {
  id_vehiculo: number;
  placa_vehiculo: string;
  anio_vehiculo: number;
  color_vehiculo: string;
  estado_vehiculo: boolean;
  id_modelo_vehiculo: number | null;
  id_cliente: number | null;
  nombre_cliente: string;
  nombre_modelo_vehiculo: string;
  nombre_marca_vehiculo: string;
  nombre_tipo_vehiculo: string;
}

interface ModeloVehiculo {
  id_modelo_vehiculo: number;
  nombre_modelo_vehiculo: string;
}

interface Cliente {
  id_cliente: number;
  nombre_cliente: string;
  apellido_cliente?: string;
}

interface VehiculoForm {
  placa_vehiculo: string;
  anio_vehiculo: number;
  color_vehiculo: string;
  id_modelo_vehiculo: number | null;
  id_cliente: number | null;
}

@Component({
  selector: 'app-vehiculos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './vehiculos.html',
  styleUrl: './vehiculos.css'
})
export class VehiculosComponent implements OnInit {

  vehiculos: Vehiculo[] = [];
  modelos: ModeloVehiculo[] = [];
  clientes: Cliente[] = [];

  cargando = false;
  error: string | null = null;

  form: VehiculoForm = this.defaultForm();
  editingId: number | null = null;

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.cargar();
    this.cargarModelos();
    this.cargarClientes();
  }

  cargar(): void {
    this.cargando = true;
    this.error = null;

    this.api.getVehiculos().subscribe({
      next: (d) => {
        this.vehiculos = d ?? [];
        this.cargando = false;
      },
      error: () => {
        this.error = 'Error al cargar vehículos.';
        this.cargando = false;
      }
    });
  }

  cargarModelos(): void {
    this.api.get<ModeloVehiculo[]>('modelos-vehiculo').subscribe({
      next: (d) => this.modelos = d ?? [],
      error: () => {
        console.error('No se pudieron cargar los modelos');
      }
    });
  }

  cargarClientes(): void {
    this.api.get<Cliente[]>('clientes').subscribe({
      next: (d) => this.clientes = d ?? [],
      error: () => {
        console.error('No se pudieron cargar los clientes');
      }
    });
  }

  guardar(): void {
    if (!this.form.placa_vehiculo?.trim()) {
      alert('Ingresa la placa.');
      return;
    }

    if (!this.form.id_cliente) {
      alert('Selecciona un cliente.');
      return;
    }

    if (this.editingId === null) {
      this.api.createVehiculo(this.form).subscribe({
        next: () => {
          this.cargar();
          this.resetForm();
        },
        error: () => alert('Error al crear vehículo.')
      });
    } else {
      this.api.updateVehiculo(this.editingId, this.form).subscribe({
        next: () => {
          this.cargar();
          this.resetForm();
        },
        error: () => alert('Error al actualizar vehículo.')
      });
    }
  }

  editar(v: Vehiculo): void {
    this.form = {
      placa_vehiculo: v.placa_vehiculo,
      anio_vehiculo: v.anio_vehiculo,
      color_vehiculo: v.color_vehiculo,
      id_modelo_vehiculo: v.id_modelo_vehiculo,
      id_cliente: v.id_cliente
    };

    this.editingId = v.id_vehiculo;
  }

  eliminar(id: number): void {
    if (!confirm('¿Eliminar este vehículo?')) return;

    this.api.deleteVehiculo(id).subscribe({
      next: () => this.cargar(),
      error: () => alert('Error al eliminar vehículo.')
    });
  }

  cancelar(): void {
    this.resetForm();
  }

  private defaultForm(): VehiculoForm {
    return {
      placa_vehiculo: '',
      anio_vehiculo: new Date().getFullYear(),
      color_vehiculo: '',
      id_modelo_vehiculo: null,
      id_cliente: null
    };
  }

  private resetForm(): void {
    this.form = this.defaultForm();
    this.editingId = null;
  }
}