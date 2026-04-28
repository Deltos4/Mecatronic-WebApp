import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../core/api.service';

interface Producto {
  id_producto: number;
  nombre_producto: string;
  stock_producto: number;
}

interface Salida {
  id?: number;
  id_salida?: number;
  fechaISO?: string;
  fecha_registro?: string;
  repuestoNombre?: string;
  nombre_producto?: string;
  cantidad: number;
  motivo: 'Venta' | 'Servicio' | 'Merma' | 'Ajuste';
  referencia?: string;
  observacion?: string;
}

interface SalidaForm {
  repuestoId: number | null;
  cantidad: number;
  motivo: 'Venta' | 'Servicio' | 'Merma' | 'Ajuste';
  referencia: string;
  observacion: string;
}

@Component({
  selector: 'app-salidas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './salidas.html',
  styleUrl: './salidas.css'
})
export class SalidasComponent implements OnInit {

  salidas: Salida[] = [];
  repuestos: Producto[] = [];
  cargando = false;
  error: string | null = null;
  showForm = false;

  form: SalidaForm = this.defaultForm();

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.cargar();
    this.api.getProductos().subscribe({
      next: (d) => this.repuestos = d ?? [],
      error: () => {}
    });
  }

  cargar(): void {
    this.cargando = true;
    this.error = null;

    this.api.getSalidas().subscribe({
      next: (d) => {
        this.salidas = d ?? [];
        this.cargando = false;
      },
      error: () => {
        this.error = 'Error al cargar salidas.';
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

  guardarSalida(): void {
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

    const repuesto = this.repuestos.find(r => r.id_producto === Number(this.form.repuestoId));
    if (!repuesto) {
      alert('No se encontró el repuesto seleccionado.');
      return;
    }

    if (this.form.cantidad > repuesto.stock_producto) {
      alert('La cantidad supera el stock disponible.');
      return;
    }

    const payload = {
      repuestoId: this.form.repuestoId,
      cantidad: Number(this.form.cantidad),
      motivo: this.form.motivo,
      referencia: this.form.referencia?.trim() ?? '',
      observacion: this.form.observacion?.trim() ?? ''
    };

    this.api.createSalida(payload).subscribe({
      next: () => {
        this.cargar();
        this.form = this.defaultForm();
        this.showForm = false;
      },
      error: () => alert('Error al registrar salida.')
    });
  }

  eliminarSalida(id: number | string): void {
    if (!confirm('¿Eliminar esta salida?')) return;

    this.api.delete(`inventario/salidas/${id}`).subscribe({
      next: () => this.cargar(),
      error: () => alert('Error al eliminar salida.')
    });
  }

  formatDate(iso: string): string {
    const d = new Date(iso);
    return isNaN(d.getTime()) ? '—' : d.toLocaleString('es-PE');
  }

  private defaultForm(): SalidaForm {
    return {
      repuestoId: null,
      cantidad: 1,
      motivo: 'Venta',
      referencia: '',
      observacion: ''
    };
  }
}