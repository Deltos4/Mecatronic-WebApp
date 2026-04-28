import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ApiService } from '../../core/api.service';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-tecnico-panel',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './panel.html',
  styleUrls: ['./panel.css']
})
export class PanelComponent implements OnInit {
  tecnicoNombre = '';
  especialidad = '';
  ordenes: any[] = [];
  cargando = false;

  constructor(private api: ApiService, private auth: AuthService) {}

  ngOnInit(): void {
    const usuario = this.auth.getUsuarioActual();
    this.tecnicoNombre = usuario?.nombre ?? 'Técnico';

    this.cargando = true;
    // Intentar cargar órdenes asignadas al técnico
    this.api.get<any[]>('mis-ordenes').subscribe({
      next: (d) => {
        this.ordenes = (d ?? []).map(o => ({
          id: o.id_orden_servicio ?? o.id,
          cliente: o.nombre_cliente ? `${o.nombre_cliente} ${o.apellido_cliente ?? ''}` : `Cliente #${o.id_cliente}`,
          vehiculo: o.placa_vehiculo ? `${o.nombre_marca_vehiculo ?? ''} ${o.nombre_modelo_vehiculo ?? ''} - ${o.placa_vehiculo}` : `Vehículo #${o.id_vehiculo ?? ''}`,
          servicio: o.nombre_servicio ?? o.descripcion ?? 'Servicio',
          fecha: o.fecha_inicio ? new Date(o.fecha_inicio).toLocaleDateString('es-PE') : '—',
          estado: o.estado ?? 'Pendiente'
        }));
        this.cargando = false;
      },
      error: () => {
        // Fallback: cargar todas las órdenes
        this.api.getOrdenes().subscribe({
          next: (d) => {
            this.ordenes = (d ?? []).map(o => ({
              id: o.id_orden_servicio ?? o.id,
              cliente: o.nombre_cliente ? `${o.nombre_cliente} ${o.apellido_cliente ?? ''}` : `Cliente #${o.id_cliente}`,
              vehiculo: o.placa_vehiculo ?? `Vehículo #${o.id_vehiculo ?? ''}`,
              servicio: o.nombre_servicio ?? 'Servicio',
              fecha: o.fecha_inicio ? new Date(o.fecha_inicio).toLocaleDateString('es-PE') : '—',
              estado: o.estado ?? 'Pendiente'
            }));
            this.cargando = false;
          },
          error: () => { this.cargando = false; }
        });
      }
    });
  }

  get pendientes(): number {
    return this.ordenes.filter(o => o.estado === 'Pendiente').length;
  }

  get enProceso(): number {
    return this.ordenes.filter(o => o.estado === 'En proceso').length;
  }

  get completadas(): number {
    return this.ordenes.filter(o => o.estado === 'Completado').length;
  }
}
