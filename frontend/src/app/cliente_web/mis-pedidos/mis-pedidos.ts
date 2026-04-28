import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../core/api.service';

@Component({
  selector: 'app-mis-pedidos',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './mis-pedidos.html',
  styleUrl: './mis-pedidos.css',
})
export class MisPedidosComponent implements OnInit {

  pedidos: any[] = [];
  cargando = false;
  error: string | null = null;

  constructor(private api: ApiService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.cargar();
  }

  cargar(): void {
    this.cargando = true;
    this.error = null;

    this.api.getMisPedidos().subscribe({
      next: (data) => {
        this.pedidos = data ?? [];
        this.cargando = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.api.getPedidos().subscribe({
          next: (data) => { this.pedidos = data ?? []; this.cargando = false; this.cdr.detectChanges(); },
          error: () => { this.error = 'Error al cargar pedidos.'; this.cargando = false; this.cdr.detectChanges(); }
        });
      }
    });
  }

  getTotalPedido(p: any): number {
    return p.total ?? p.total_pedido ?? (p.items ?? []).reduce((acc: number, it: any) => acc + (it.precio * it.cantidad), 0);
  }

  getEstadoClase(estado: string): string {
    if (estado === 'Pendiente') return 'bg-warning text-dark';
    if (estado === 'Pagado' || estado === 'Completado') return 'bg-success';
    if (estado === 'En proceso') return 'bg-info';
    if (estado === 'Cancelado') return 'bg-danger';
    return 'bg-secondary';
  }

  formatearFecha(fecha: string): string {
    if (!fecha) return '—';
    const d = new Date(fecha);
    if (isNaN(d.getTime())) return '—';
    return d.toLocaleString('es-PE', {
      day: '2-digit', month: '2-digit', year: 'numeric',
      hour: '2-digit', minute: '2-digit'
    });
  }
}
