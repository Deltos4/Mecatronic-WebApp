import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../core/api.service';

@Component({
  selector: 'app-mis-servicios',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './mis-servicios.html',
  styleUrls: ['./mis-servicios.css'],
})
export class MisServiciosComponent implements OnInit {

  citas: any[] = [];
  cargando = false;
  error: string | null = null;

  constructor(private api: ApiService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.cargar();
  }

  cargar(): void {
    this.cargando = true;
    this.error = null;

    this.api.getMisReservas().subscribe({
      next: (data) => {
        this.citas = (data ?? []).map((c: any) => ({
          ...c,
          id: c.id_reserva ?? c.id,
          servicioNombre: c.nombre_servicio ?? c.servicioNombre ?? 'Servicio',
          fechaISO: c.fecha_programada ?? c.fechaISO ?? '',
          estado: c.estado ?? 'Pendiente',
          placa: c.placa_vehiculo ?? c.placa ?? '',
          telefono: c.telefono ?? '',
          comentario: c.comentario ?? c.observaciones ?? '',
          precio: c.precio_servicio ?? c.precio ?? null
        }));
        this.cargando = false;
        this.cdr.detectChanges();
      },
      error: () => {
        // Fallback: intentar con getReservas general
        this.api.getReservas().subscribe({
          next: (data) => {
            this.citas = (data ?? []).map((c: any) => ({
              ...c,
              id: c.id_reserva ?? c.id,
              servicioNombre: c.nombre_servicio ?? c.servicioNombre ?? 'Servicio',
              fechaISO: c.fecha_programada ?? c.fechaISO ?? '',
              estado: c.estado ?? 'Pendiente',
              placa: c.placa_vehiculo ?? c.placa ?? '',
              telefono: c.telefono ?? '',
              comentario: c.comentario ?? c.observaciones ?? '',
              precio: c.precio_servicio ?? c.precio ?? null
            }));
            this.cargando = false;
            this.cdr.detectChanges();
          },
          error: () => { this.error = 'Error al cargar servicios.'; this.cargando = false; this.cdr.detectChanges(); }
        });
      }
    });
  }

  formatearFecha(fechaISO: string): string {
    if (!fechaISO) return '—';
    const d = new Date(fechaISO);
    if (isNaN(d.getTime())) return '—';
    return d.toLocaleString('es-PE', {
      day: '2-digit', month: '2-digit', year: 'numeric',
      hour: '2-digit', minute: '2-digit'
    });
  }

  cancelarCita(id: number): void {
    const cita = this.citas.find(c => c.id === id);
    if (!cita) return;
    if (cita.estado === 'Cancelada' || cita.estado === 'Atendida' || cita.estado === 'Completada') return;
    if (!confirm('¿Cancelar este servicio?')) return;

    this.api.cancelarReserva(id).subscribe({
      next: () => {
        cita.estado = 'Cancelada';
      },
      error: () => {
        // Fallback: actualizar local
        cita.estado = 'Cancelada';
      }
    });
  }
}
