import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../core/api.service';

interface Servicio {
  id_servicio: number;
  nombre_servicio: string;
  descripcion_servicio: string;
  precio_servicio: number;
  duracion_servicio: number;
  estado_servicio: boolean;
  imagen_url?: string;
}

@Component({
  selector: 'app-servicios',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './servicios.html',
  styleUrl: './servicios.css'
})
export class ServiciosComponent implements OnInit {

  servicios: Servicio[] = [];
  cargando = false;
  error: string | null = null;

  form: Partial<Servicio> = this.defaultForm();
  editingId: number | null = null;

  constructor(private api: ApiService, private cdr: ChangeDetectorRef) {}

  ngOnInit(): void { this.cargar(); }

  cargar(): void {
    this.cargando = true;
    this.api.getServicios().subscribe({
      next: (d) => { this.servicios = d; this.cargando = false; this.cdr.detectChanges(); },
      error: () => { this.error = 'Error al cargar servicios.'; this.cargando = false; this.cdr.detectChanges(); }
    });
  }

  guardar(): void {
    if (!this.form.nombre_servicio?.trim()) { alert('Ingresa el nombre del servicio.'); return; }
    if (!this.form.precio_servicio || this.form.precio_servicio <= 0) { alert('El precio debe ser mayor a 0.'); return; }

    if (this.editingId === null) {
      this.api.createServicio(this.form).subscribe({
        next: () => { this.cargar(); this.resetForm(); },
        error: () => alert('Error al crear servicio.')
      });
    } else {
      this.api.updateServicio(this.editingId, this.form).subscribe({
        next: () => { this.cargar(); this.resetForm(); },
        error: () => alert('Error al actualizar servicio.')
      });
    }
  }

  editar(s: Servicio): void {
    this.form = { ...s };
    this.editingId = s.id_servicio;
  }

  eliminar(id: number): void {
    if (!confirm('¿Eliminar este servicio?')) return;
    this.api.deleteServicio(id).subscribe({
      next: () => this.cargar(),
      error: () => alert('Error al eliminar servicio.')
    });
  }

  cancelar(): void { this.resetForm(); }

  private defaultForm(): Partial<Servicio> {
    return { nombre_servicio: '', descripcion_servicio: '', precio_servicio: 0, duracion_servicio: 0, estado_servicio: true };
  }

  private resetForm(): void {
    this.form = this.defaultForm();
    this.editingId = null;
  }
}
