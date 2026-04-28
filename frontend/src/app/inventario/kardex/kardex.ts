import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../core/api.service';

interface Repuesto {
  id?: number;
  id_producto?: number;
  nombre?: string;
  nombre_producto?: string;
}

interface MovimientoKardex {
  fecha: string;
  tipo: 'Entrada' | 'Salida';
  repuestoNombre: string;
  motivo: string;
  referencia?: string;
  entrada: number;
  salida: number;
  costoUnitario?: number | null;
  total?: number | null;
  obs?: string;
  saldo: number;
}

@Component({
  selector: 'app-kardex',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './kardex.html',
  styleUrl: './kardex.css'
})
export class KardexComponent implements OnInit {

  repuestos: Repuesto[] = [];
  repuestoIdSel: number | null = null;
  movimientos: MovimientoKardex[] = [];
  cargando = false;
  error: string | null = null;

  entradas: any[] = [];
  salidas: any[] = [];

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.api.getProductos().subscribe({
      next: (d) => {
        this.repuestos = d ?? [];
        this.cargarKardex();
      },
      error: () => {
        this.error = 'Error al cargar repuestos.';
      }
    });
  }

  onChangeRepuesto(): void {
    this.cargarKardex();
  }

  cargarKardex(): void {
    this.cargando = true;
    this.error = null;
    this.movimientos = [];

    const request$ = this.api.getKardex(this.repuestoIdSel ?? 0);

    request$.subscribe({
      next: (data: any) => {
        const lista = Array.isArray(data) ? data : [];
        this.movimientos = this.normalizarMovimientosBackend(lista);
        this.cargando = false;
      },
      error: () => {
        this.cargarKardexLocal();
      }
    });
  }

  private normalizarMovimientosBackend(data: any[]): MovimientoKardex[] {
    const ordenados = [...data].sort((a, b) => {
      const fa = new Date(a.fecha ?? a.fechaISO ?? a.fecha_registro ?? '').getTime();
      const fb = new Date(b.fecha ?? b.fechaISO ?? b.fecha_registro ?? '').getTime();
      return fa - fb;
    });

    let saldo = 0;

    const movimientos = ordenados.map((m: any) => {
      const tipo = (m.tipo === 'Salida' ? 'Salida' : 'Entrada') as 'Entrada' | 'Salida';
      const cantidad = Number(m.cantidad ?? 0);
      const entrada = tipo === 'Entrada' ? cantidad : 0;
      const salida = tipo === 'Salida' ? cantidad : 0;

      saldo += entrada - salida;

      const costoUnitario = m.costoUnitario ?? m.costo_unitario ?? null;
      const total =
        m.total ??
        (tipo === 'Entrada' && costoUnitario != null
          ? cantidad * Number(costoUnitario)
          : null);

      return {
        fecha: m.fecha ?? m.fechaISO ?? m.fecha_registro ?? '',
        tipo,
        repuestoNombre: m.repuestoNombre ?? m.nombre_producto ?? m.nombre ?? '',
        motivo: m.motivo ?? '',
        referencia: m.referencia ?? m.proveedorNombre ?? m.nombre_proveedor ?? '',
        entrada,
        salida,
        costoUnitario,
        total,
        obs: m.observacion ?? '',
        saldo
      };
    });

    return movimientos.reverse();
  }

  private cargarKardexLocal(): void {
    let pendientes = 2;

    const checkDone = () => {
      pendientes--;
      if (pendientes === 0) {
        this.construirMovimientos();
        this.cargando = false;
      }
    };

    this.api.getEntradas().subscribe({
      next: (d) => {
        this.entradas = d ?? [];
        checkDone();
      },
      error: () => {
        this.entradas = [];
        checkDone();
      }
    });

    this.api.getSalidas().subscribe({
      next: (d) => {
        this.salidas = d ?? [];
        checkDone();
      },
      error: () => {
        this.salidas = [];
        checkDone();
      }
    });
  }

  private construirMovimientos(): void {
    const repId = this.repuestoIdSel;
    const movs: Omit<MovimientoKardex, 'saldo'>[] = [];

    for (const e of this.entradas) {
      const id = e.repuestoId ?? e.id_producto ?? e.id_repuesto;
      if (repId != null && Number(id) !== Number(repId)) continue;

      const cantidad = Number(e.cantidad ?? 0);
      const costoUnitario = Number(e.costoUnitario ?? e.costo_unitario ?? 0);

      movs.push({
        fecha: e.fechaISO ?? e.fecha_registro ?? e.fecha ?? '',
        tipo: 'Entrada',
        repuestoNombre: e.repuestoNombre ?? e.nombre_producto ?? e.nombre ?? '',
        motivo: e.motivo ?? '',
        referencia: e.referencia ?? e.proveedorNombre ?? e.nombre_proveedor ?? '',
        entrada: cantidad,
        salida: 0,
        costoUnitario,
        total: Number(e.total ?? (cantidad * costoUnitario)),
        obs: e.observacion ?? ''
      });
    }

    for (const s of this.salidas) {
      const id = s.repuestoId ?? s.id_producto ?? s.id_repuesto;
      if (repId != null && Number(id) !== Number(repId)) continue;

      movs.push({
        fecha: s.fechaISO ?? s.fecha_registro ?? s.fecha ?? '',
        tipo: 'Salida',
        repuestoNombre: s.repuestoNombre ?? s.nombre_producto ?? s.nombre ?? '',
        motivo: s.motivo ?? '',
        referencia: s.referencia ?? '',
        entrada: 0,
        salida: Number(s.cantidad ?? 0),
        costoUnitario: null,
        total: null,
        obs: s.observacion ?? ''
      });
    }

    movs.sort((a, b) => {
      const fa = new Date(a.fecha).getTime();
      const fb = new Date(b.fecha).getTime();
      return fa - fb;
    });

    let saldo = 0;

    this.movimientos = movs
      .map((m) => {
        saldo += (m.entrada || 0) - (m.salida || 0);
        return { ...m, saldo };
      })
      .reverse();
  }

  formatDate(iso: string): string {
    const d = new Date(iso);
    if (isNaN(d.getTime())) return '—';

    return d.toLocaleString('es-PE', {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit'
    });
  }
}