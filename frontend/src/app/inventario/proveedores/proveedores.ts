import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../core/api.service';

interface Proveedor {
  id_proveedor: number;
  nombre_proveedor: string;
  numero_documento_proveedor: string;
  contacto_proveedor: string;
  telefono_proveedor: string;
  correo_proveedor: string;
  direccion_proveedor: string;
  estado_proveedor: boolean;
}

@Component({
  selector: 'app-proveedores',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './proveedores.html',
  styleUrl: './proveedores.css'
})
export class ProveedoresComponent implements OnInit {

  proveedores: Proveedor[] = [];
  cargando = false;
  error: string | null = null;
  filtro = '';
  showForm = false;
  tipoDocProveedor: 'RUC' | 'DNI' = 'RUC';

  form: Partial<Proveedor> = this.defaultForm();
  editingId: number | null = null;

  constructor(private api: ApiService) {}

  ngOnInit(): void { this.cargar(); }

  cargar(): void {
    this.cargando = true;
    this.api.getProveedores().subscribe({
      next: (d) => { this.proveedores = d; this.cargando = false; },
      error: () => { this.error = 'Error al cargar proveedores.'; this.cargando = false; }
    });
  }

  get proveedoresFiltrados(): Proveedor[] {
    const t = this.filtro.trim().toLowerCase();
    if (!t) return this.proveedores;
    return this.proveedores.filter(p =>
      p.nombre_proveedor.toLowerCase().includes(t) ||
      p.numero_documento_proveedor.includes(t) ||
      (p.telefono_proveedor ?? '').includes(t)
    );
  }

  toggleForm(): void { this.showForm = !this.showForm; if (!this.showForm) this.cancelarEdicion(); }

  guardarProveedor(): void {
    if (!this.form.nombre_proveedor?.trim()) { alert('Razón social es obligatoria.'); return; }

    if (this.editingId === null) {
      this.api.createProveedor(this.form).subscribe({
        next: () => { this.cargar(); this.cancelarEdicion(); this.showForm = false; },
        error: () => alert('Error al crear proveedor.')
      });
    } else {
      this.api.updateProveedor(this.editingId, this.form).subscribe({
        next: () => { this.cargar(); this.cancelarEdicion(); this.showForm = false; },
        error: () => alert('Error al actualizar proveedor.')
      });
    }
  }

  editar(p: Proveedor): void {
    this.showForm = true;
    this.form = { ...p };
    this.editingId = p.id_proveedor;
  }

  eliminar(id: number): void {
    if (!confirm('¿Eliminar este proveedor?')) return;
    this.api.deleteProveedor(id).subscribe({
      next: () => this.cargar(),
      error: () => alert('Error al eliminar proveedor.')
    });
  }

  cancelarEdicion(): void { this.form = this.defaultForm(); this.editingId = null; }

  private defaultForm(): Partial<Proveedor> {
    return { nombre_proveedor: '', numero_documento_proveedor: '', contacto_proveedor: '',
             telefono_proveedor: '', correo_proveedor: '', direccion_proveedor: '', estado_proveedor: true };
  }
}
