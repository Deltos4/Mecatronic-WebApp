import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ApiService } from '../../core/api.service';

interface Producto {
  id_producto: number;
  nombre_producto: string;
  descripcion_producto: string;
  precio_producto: number;
  stock_producto: number;
  estado_producto: boolean;
  imagen_url?: string;
}

type ProductoForm = {
  nombre_producto: string;
  descripcion_producto: string;
  precio_producto: number;
  estado_producto: boolean;
  stock_producto?: number;
};

@Component({
  selector: 'app-repuestos',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './repuestos.html',
  styleUrl: './repuestos.css'
})
export class RepuestosComponent implements OnInit {

  repuestos: Producto[] = [];
  cargando = false;
  error: string | null = null;

  form: ProductoForm = this.defaultForm();
  editingId: number | null = null;
  mostrarForm = false;

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.cargar();
  }

  cargar(): void {
    this.cargando = true;
    this.error = null;

    this.api.getProductos().subscribe({
      next: (d) => {
        this.repuestos = d ?? [];
        this.cargando = false;
      },
      error: () => {
        this.error = 'Error al cargar repuestos.';
        this.cargando = false;
      }
    });
  }

  guardarRepuesto(): void {
    if (!this.form.nombre_producto?.trim()) {
      alert('Ingresa el nombre.');
      return;
    }

    if ((this.form.precio_producto ?? 0) <= 0) {
      alert('Ingresa un precio mayor a 0.');
      return;
    }

    const payload = {
      nombre_producto: this.form.nombre_producto.trim(),
      descripcion_producto: this.form.descripcion_producto?.trim() ?? '',
      precio_producto: Number(this.form.precio_producto),
      estado_producto: this.form.estado_producto
    };

    if (this.editingId === null) {
      this.api.createProducto(payload).subscribe({
        next: () => {
          this.cargar();
          this.limpiarFormulario();
        },
        error: () => alert('Error al crear repuesto.')
      });
    } else {
      this.api.updateProducto(this.editingId, payload).subscribe({
        next: () => {
          this.cargar();
          this.limpiarFormulario();
        },
        error: () => alert('Error al actualizar repuesto.')
      });
    }
  }

  editarRepuesto(id: number): void {
    const r = this.repuestos.find(x => x.id_producto === id);
    if (!r) return;

    this.form = {
      nombre_producto: r.nombre_producto,
      descripcion_producto: r.descripcion_producto,
      precio_producto: r.precio_producto,
      estado_producto: r.estado_producto,
      stock_producto: r.stock_producto
    };

    this.editingId = id;
    this.mostrarForm = true;
  }

  eliminarRepuesto(id: number): void {
    if (!confirm('¿Eliminar este repuesto?')) return;

    this.api.deleteProducto(id).subscribe({
      next: () => this.cargar(),
      error: () => alert('Error al eliminar repuesto.')
    });
  }

  toggleForm(): void {
    this.mostrarForm = !this.mostrarForm;

    if (!this.mostrarForm) {
      this.limpiarFormulario();
    }
  }

  limpiarFormulario(): void {
    this.form = this.defaultForm();
    this.editingId = null;
  }

  esStockBajo(r: Producto): boolean {
    return r.stock_producto < 5;
  }

  private defaultForm(): ProductoForm {
    return {
      nombre_producto: '',
      descripcion_producto: '',
      precio_producto: 0,
      estado_producto: true
    };
  }
}