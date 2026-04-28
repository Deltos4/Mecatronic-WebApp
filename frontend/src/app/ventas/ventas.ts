import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../core/api.service';

interface Venta {
  id_pedido: number;
  id_cliente: number;
  nombre_cliente?: string;
  apellido_cliente?: string;
  fecha_pedido: string;
  total_pedido: number;
  tipo_comprobante?: string;
  estado_pedido: string;
  origen_venta?: string;
}

@Component({
  selector: 'app-ventas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ventas.html',
  styleUrl: './ventas.css'
})
export class VentasComponent implements OnInit {

  ventas: Venta[] = [];
  cargando = false;
  error: string | null = null;

  constructor(private api: ApiService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.cargar();
  }

  cargar(): void {
    this.cargando = true;
    this.error = null;

    this.api.getVentas().subscribe({
      next: (d) => {
        this.ventas = d ?? [];
        this.cargando = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.error = 'Error al cargar ventas.';
        this.cargando = false;
        this.cdr.detectChanges();
      }
    });
  }
}