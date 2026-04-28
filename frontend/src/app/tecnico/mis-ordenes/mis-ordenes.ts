import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../core/api.service';

@Component({
  selector: 'app-mis-ordenes',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './mis-ordenes.html',
  styleUrls: ['./mis-ordenes.css']
})
export class MisOrdenesComponent implements OnInit {

  ordenes: any[] = [];
  cargando = false;
  error: string | null = null;
  filtroEstado = '';

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.cargar();
  }

  cargar(): void {
    this.cargando = true;
    this.api.get<any[]>('mis-ordenes').subscribe({
      next: (d) => { this.ordenes = d ?? []; this.cargando = false; },
      error: () => {
        this.api.getOrdenes().subscribe({
          next: (d) => { this.ordenes = d ?? []; this.cargando = false; },
          error: () => { this.error = 'Error al cargar órdenes.'; this.cargando = false; }
        });
      }
    });
  }

  get ordenesFiltradas(): any[] {
    if (!this.filtroEstado) return this.ordenes;
    return this.ordenes.filter(o => o.estado === this.filtroEstado);
  }

  cambiarEstado(orden: any, nuevoEstado: string): void {
    this.api.updateOrden(orden.id_orden_servicio ?? orden.id, { ...orden, estado: nuevoEstado }).subscribe({
      next: () => { orden.estado = nuevoEstado; },
      error: () => alert('Error al actualizar estado.')
    });
  }

  formatDate(fecha: string): string {
    if (!fecha) return '—';
    const d = new Date(fecha);
    return isNaN(d.getTime()) ? '—' : d.toLocaleDateString('es-PE');
  }
}
