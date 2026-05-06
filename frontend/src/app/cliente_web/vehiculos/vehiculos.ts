import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../core/api.service';
import { AuthService } from '../../auth/auth.service';

interface VehiculoForm {
  placa: string;
  anio: number | null;
  color: string;
  idModeloVehiculo: number | null;
}

@Component({
  selector: 'app-vehiculos-cliente',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './vehiculos.html',
  styleUrls: ['./vehiculos.css']
})
export class VehiculosClienteComponent implements OnInit {
  vehiculos: any[] = [];
  modelos: any[] = [];

  cargando = false;
  guardando = false;
  error: string | null = null;

  form: VehiculoForm = this.defaultForm();

  constructor(private api: ApiService, private auth: AuthService) {}

  ngOnInit(): void {
    this.cargarModelos();
    this.cargarVehiculos();
  }

  cargarModelos(): void {
    this.api.getModelosVehiculo().subscribe({
      next: (data) => { this.modelos = data ?? []; },
      error: () => { this.error = 'No se pudieron cargar los modelos.'; }
    });
  }

  cargarVehiculos(): void {
    const idCliente = this.obtenerClienteId();
    if (!idCliente) {
      this.error = 'No se encontró el cliente asociado.';
      return;
    }

    this.cargando = true;
    this.error = null;

    this.api.getVehiculosCliente(idCliente).subscribe({
      next: (data) => {
        this.vehiculos = data ?? [];
        this.cargando = false;
      },
      error: () => {
        this.error = 'No se pudieron cargar tus vehículos.';
        this.cargando = false;
      }
    });
  }

  guardar(): void {
    const idCliente = this.obtenerClienteId();
    if (!idCliente) {
      this.error = 'No se encontró el cliente asociado.';
      return;
    }

    const placa = this.form.placa.trim().toUpperCase();
    if (!placa) {
      this.error = 'Ingresa la placa del vehículo.';
      return;
    }

    if (!this.form.idModeloVehiculo) {
      this.error = 'Selecciona el modelo del vehículo.';
      return;
    }

    this.guardando = true;
    this.error = null;

    const payload = {
      idModeloVehiculo: Number(this.form.idModeloVehiculo),
      placaVehiculo: placa,
      anioVehiculo: this.form.anio ?? null,
      colorVehiculo: this.form.color.trim() || null,
      urlFotoVehiculo: null
    };

    this.api.createVehiculo(payload).subscribe({
      next: (vehiculo) => {
        const idVehiculo = vehiculo?.id_vehiculo ?? vehiculo?.idVehiculo;

        if (!idVehiculo) {
          this.error = 'No se pudo asignar el vehículo.';
          this.guardando = false;
          return;
        }

        this.api.asignarVehiculoCliente({ idVehiculo, idCliente }).subscribe({
          next: () => {
            this.guardando = false;
            this.form = this.defaultForm();
            this.cargarVehiculos();
          },
          error: () => {
            this.guardando = false;
            this.error = 'No se pudo asignar el vehículo.';
          }
        });
      },
      error: () => {
        this.guardando = false;
        this.error = 'No se pudo registrar el vehículo.';
      }
    });
  }

  eliminar(idVehiculo: number): void {
    if (!confirm('¿Eliminar este vehículo?')) return;

    this.api.deleteVehiculo(idVehiculo).subscribe({
      next: () => this.cargarVehiculos(),
      error: () => { this.error = 'No se pudo eliminar el vehículo.'; }
    });
  }

  private obtenerClienteId(): number | null {
    return this.auth.getUsuarioActual()?.IdCliente ?? null;
  }

  private defaultForm(): VehiculoForm {
    return {
      placa: '',
      anio: new Date().getFullYear(),
      color: '',
      idModeloVehiculo: null
    };
  }
}
