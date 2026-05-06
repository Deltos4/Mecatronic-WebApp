import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../core/api.service';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-proformas-cliente',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './proformas.html',
  styleUrls: ['./proformas.css']
})
export class ProformasClienteComponent implements OnInit {
  proformas: any[] = [];
  cargando = false;
  error: string | null = null;

  constructor(private api: ApiService, private auth: AuthService) {}

  ngOnInit(): void {
    this.cargarProformas();
  }

  cargarProformas(): void {
    const idCliente = this.auth.getUsuarioActual()?.IdCliente;
    if (!idCliente) {
      this.error = 'No se encontró el cliente asociado.';
      return;
    }

    this.cargando = true;
    this.error = null;

    this.api.getProformasCliente(idCliente).subscribe({
      next: (data) => {
        this.proformas = data ?? [];
        this.cargando = false;
      },
      error: () => {
        this.error = 'No se pudieron cargar las proformas.';
        this.cargando = false;
      }
    });
  }

  formatearFecha(fecha: string): string {
    if (!fecha) return '—';
    const d = new Date(fecha);
    if (isNaN(d.getTime())) return '—';
    return d.toLocaleDateString('es-PE', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric'
    });
  }
}
