import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../core/api.service';

interface Producto {
  id_producto: number;
  nombre_producto: string;
  stock_producto: number;
}

interface Proveedor {
  id_proveedor: number;
  nombre_proveedor: string;
}

interface Entrada {
  id?: number;
  id_entrada?: number;
  fechaISO?: string;
  fecha_registro?: string;
  repuestoNombre?: string;
  nombre_producto?: string;
  cantidad: number;
  motivo: 'Compra' | 'Devolución' | 'Ajuste';
  proveedorNombre?: string;
  nombre_proveedor?: string;
  observacion?: string;
  costoUnitario?: number;
  total?: number;
}

interface EntradaForm {
  repuestoId: number | null;
  cantidad: number;
  motivo: 'Compra' | 'Devolución' | 'Ajuste';
  costoUnitario: number;
  proveedorId: number | null;
  observacion: string;
}

@Component({
  selector: 'app-entradas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './entradas.html',
  styleUrl: './entradas.css'
})
export class EntradasComponent implements OnInit {

  entradas: Entrada[] = [];
  repuestos: Producto[] = [];
  proveedores: Proveedor[] = [];
  cargando = false;
  error: string | null = null;
  showForm = false;

  form: EntradaForm = this.defaultForm();

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.cargar();
    this.api.getProductos().subscribe({
      next: (d) => this.repuestos = d ?? [],
      error: () => {}
    });
    this.api.getProveedores().subscribe({
      next: (d) => this.proveedores = d ?? [],
      error: () => {}
    });
  }

  cargar(): void {
    this.cargando = true;
    this.error = null;

    this.api.getEntradas().subscribe({
      next: (d) => {
        this.entradas = d ?? [];
        this.cargando = false;
      },
      error: () => {
        this.error = 'Error al cargar entradas.';
        this.cargando = false;
      }
    });
  }

  toggleForm(): void {
    this.showForm = !this.showForm;

    if (!this.showForm) {
      this.form = this.defaultForm();
    }
  }

  onMotivoChange(): void {
    if (this.form.motivo !== 'Compra') {
      this.form.costoUnitario = 0;
      this.form.proveedorId = null;
    }
  }

  guardarEntrada(): void {
    if (!this.form.repuestoId) {
      alert('Selecciona un repuesto.');
      return;
    }

    if (!this.form.cantidad || this.form.cantidad < 1) {
      alert('La cantidad debe ser mayor a 0.');
      return;
    }

    if (!this.form.motivo) {
      alert('Selecciona un motivo.');
      return;
    }

    if (this.form.motivo === 'Compra' && this.form.costoUnitario < 0) {
      alert('El costo unitario no puede ser negativo.');
      return;
    }

    const payload = {
      repuestoId: this.form.repuestoId,
      cantidad: Number(this.form.cantidad),
      motivo: this.form.motivo,
      costoUnitario: this.form.motivo === 'Compra' ? Number(this.form.costoUnitario) : 0,
      proveedorId: this.form.motivo === 'Compra' ? this.form.proveedorId : null,
      observacion: this.form.observacion?.trim() ?? ''
    };

    this.api.createEntrada(payload).subscribe({
      next: () => {
        this.cargar();
        this.form = this.defaultForm();
        this.showForm = false;
      },
      error: () => alert('Error al registrar entrada.')
    });
  }

  eliminarEntrada(id: number | string): void {
    if (!confirm('¿Eliminar esta entrada?')) return;

    this.api.delete(`inventario/entradas/${id}`).subscribe({
      next: () => this.cargar(),
      error: () => alert('Error al eliminar entrada.')
    });
  }

  formatDate(iso?: string): string {
    if (!iso) return '—';

    const d = new Date(iso);
    return isNaN(d.getTime()) ? '—' : d.toLocaleString('es-PE');
}

  private defaultForm(): EntradaForm {
    return {
      repuestoId: null,
      cantidad: 1,
      motivo: 'Compra',
      costoUnitario: 0,
      proveedorId: null,
      observacion: ''
    };
  }
}